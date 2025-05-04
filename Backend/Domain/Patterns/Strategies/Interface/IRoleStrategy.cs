namespace Backend.Domain.Patterns.Strategies.Interfaces;

using Backend.Models;

public interface IRoleStrategy
{
    string GetRole(UserEntity user);
}