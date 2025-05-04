using System.Security.Claims;
using Backend.Domain.Patterns.Strategies.Interfaces;
using Backend.Domain.Security;
using Backend.Models;

namespace Backend.Domain.Patterns.Strategies;

public class DefaultRoleStrategy : IRoleStrategy
{
    public string GetRole(UserEntity user)
    {
        if (user.Admin) return Roles.Admin;
        if (user.SuperUser) return Roles.UserManager;
        return Roles.User;
    }
}