using Backend.Domain.DTOs.User;
using Backend.Domain.DTOs.Common;
using Backend.Features.User.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Features.User;

[ApiController]
[Route("api/utilizadores")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;
    private readonly ILogger<UserController> _logger;

    public UserController(IUserService service, ILogger<UserController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet("paged")]
    public async Task<ActionResult<PagedResult<UserDetailsDto>>> GetPaged([FromQuery] UserFilterDto filter, int page = 1, int pageSize = 10)
    {
        var result = await _service.FilteredPagedAsync(filter, page, pageSize);
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDetailsDto>>> GetAll([FromQuery] UserFilterDto filter)
    {
        var result = await _service.FilteredListAsync(filter);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDetailsExtendedDto>> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<UserDetailsDto>> Create(UserCreateDto dto)
    {
        var result = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.IdUtilizador }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UserUpdateDto dto)
    {
        await _service.UpdateAsync(id, dto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return Ok();
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserTokenDto>> Login([FromBody] UserLoginDto loginDto)
    {
        try
        {
            var token = await _service.GenerateTokenAsync(loginDto);
            return Ok(token);
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning("Tentativa de login falhada: {Mensagem}", ex.Message);
            return Unauthorized(new { mensagem = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro inesperado durante o login");
            return StatusCode(500, new { mensagem = "Erro interno no servidor" });
        }
    }
}