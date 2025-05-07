using gamehub_API.Application.DTO.Auth;
using gamehub_API.Application.DTO.User;
using gamehub_API.Application.UseCases.Auth.LoginUserUseCase;
using gamehub_API.Application.UseCases.Auth.LogOutUserUseCase;
using gamehub_API.Application.UseCases.Auth.RefreshTokenUserUseCase;
using gamehub_API.Application.UseCases.User.CreateUserUseCase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace gamehub_API.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly ILoginUserUseCase _loginUserUseCase;
        private readonly ICreateUserUseCase _createUserUseCase;
        private readonly ILogOutUserUseCase _logOutUserUseCase;
        private readonly IRefreshTokenUserUseCase _refreshTokenUseCase;

        public AuthController(ILoginUserUseCase loginUserUseCase, ICreateUserUseCase createUserUseCase, ILogOutUserUseCase logOutUserUseCase, IRefreshTokenUserUseCase refreshTokenUserUseCase)
        {
            _loginUserUseCase = loginUserUseCase;
            _logOutUserUseCase = logOutUserUseCase;
            _refreshTokenUseCase = refreshTokenUserUseCase;
            _createUserUseCase = createUserUseCase;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDTO createUserDTO)
        {
            try
            {
                var registerResponse = await _createUserUseCase.ExecuteAsync(createUserDTO);
                return Ok(registerResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al registrar el usuario.", details = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO loginUserDTO)
        {
            try
            {
                var loginResponse = await _loginUserUseCase.ExecuteAsync(loginUserDTO);

                return Ok(loginResponse);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al iniciar sesión.", details = ex.Message });
            }
        }

        [Authorize]
        [HttpPost("logout/{userId}")]
        public async Task<IActionResult> Logout([FromRoute] string userId, [FromBody] LogOutUserDTO logOutUserDTO)
        {
            if (logOutUserDTO == null || string.IsNullOrEmpty(logOutUserDTO.userId) || string.IsNullOrEmpty(logOutUserDTO.token))
            {
                return BadRequest(new { message = "Datos de cierre de sesión inválidos." });
            }

            try
            {
                await _logOutUserUseCase.ExecuteAsync(logOutUserDTO);

                return Ok(new { message = "Sesión cerrada correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al cerrar la sesión.", details = ex.Message });
            }
        }

        [HttpPost("refresh-token/{userId}")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDTO refreshTokenDTO)
        {
            if (refreshTokenDTO == null || string.IsNullOrEmpty(refreshTokenDTO.UserId) || string.IsNullOrEmpty(refreshTokenDTO.RefreshToken))
            {
                return BadRequest(new { message = "Datos de refresh token inválidos." });
            }

            try
            {
                var refreshTokenResponse = await _refreshTokenUseCase.ExecuteAsync(refreshTokenDTO);
                return Ok(refreshTokenResponse);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al renovar el token.", details = ex.Message });
            }
        }



    }
}
