using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Backend.Models;
using Backend.Domain.DTOs.User;
using Backend.Domain.Patterns.Builders.Interfaces;
using Backend.Domain.Patterns.Strategies.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Domain.Patterns.Builders;

public class JwtTokenBuilder : IJwtTokenBuilder
{
    private readonly IConfiguration _config;
    private readonly IRoleStrategy _roleStrategy;

    public JwtTokenBuilder(IConfiguration config, IRoleStrategy roleStrategy)
    {
        _config = config;
        _roleStrategy = roleStrategy;
    }

    public UserTokenDto BuildToken(UserEntity user)
    {
        var role = _roleStrategy.GetRole(user);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.IdUtilizador.ToString()),
            new Claim(ClaimTypes.Name,          user.Username),
            new Claim(ClaimTypes.Role,          role)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]!);

        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Subject            = new ClaimsIdentity(claims),
            Expires            = DateTime.UtcNow.AddMinutes(int.Parse(_config["Jwt:ExpireMinutes"]!)),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature),
            Issuer             = _config["Jwt:Issuer"],
            Audience           = _config["Jwt:Audience"]
        });

        return new UserTokenDto()
        {
            Token      = tokenHandler.WriteToken(token),
            Expiration = token.ValidTo,
            UserId    = user.IdUtilizador
        };
    }
}