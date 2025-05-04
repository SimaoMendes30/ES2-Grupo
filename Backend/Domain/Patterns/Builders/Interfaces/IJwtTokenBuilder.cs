using Backend.Models;
using Backend.Domain.DTOs.User;

namespace Backend.Domain.Patterns.Builders.Interfaces;

public interface IJwtTokenBuilder
{
    UserTokenDto BuildToken(UserEntity user);
}