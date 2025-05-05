using gamehub_API.Application.DTO.User;
using gamehub_API.Application.Interfaces;
using gamehub_API.Application.UseCases.User.CreateUserUseCase;
using gamehub_API.Application.UseCases.User.DeleteUserUseCase;
using gamehub_API.Application.UseCases.User.UpdatePasswordUseCase;
using gamehub_API.Application.UseCases.User.EditUserUseCase;
using gamehub_API.Application.UseCases.User.ViewUserUseCase;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using gamehub_API.Application.UseCases.User.LoginUserUseCase;
using Microsoft.AspNetCore.Authorization;

namespace gamehub_API.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ICreateUserUseCase _createUserUseCase;
        private readonly IGetUserUseCase _viewUserUseCase;
        private readonly IUpdateUserUseCase _updateUserUseCase;
        private readonly IUpdatePasswordUserUseCase _updatePasswordUserUseCase;
        private readonly IDeleteUserUseCase _deleteUserUseCase;
        private readonly ILoginUserUseCase _loginUserUseCase;

        public UserController(
            ICreateUserUseCase createUserUseCase,
            IGetUserUseCase viewUserUseCase,
            IUpdateUserUseCase updateUserUseCase,
            IDeleteUserUseCase deleteUserUseCase,
            IUpdatePasswordUserUseCase updatePasswordUserUseCase,
            ILoginUserUseCase loginUserUseCase)
        {
            _createUserUseCase = createUserUseCase;
            _viewUserUseCase = viewUserUseCase;
            _updateUserUseCase = updateUserUseCase;
            _deleteUserUseCase = deleteUserUseCase;
            _updatePasswordUserUseCase = updatePasswordUserUseCase;
            _loginUserUseCase = loginUserUseCase;
        }

        [Authorize]
        [HttpGet("{userId}")]
        public async Task<IActionResult> Get([FromRoute] string userId)
        {
            try
            {
                var user = await _viewUserUseCase.ExecuteAsync(userId);
               
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor.", details = ex.Message });
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
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserDTO createUserDTO)
        {
            try
            {
                var createUser = await _createUserUseCase.ExecuteAsync(createUserDTO);
                return CreatedAtAction(nameof(Get), new { createUser.userId }, createUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al crear el usuario.", details = ex.Message, StackTrace = ex.StackTrace });
            }
        }

        [Authorize]
        [HttpPut("{userId}")]
        public async Task<IActionResult> Put([FromRoute] string userId, [FromBody] UpdateUserDTO updateUserDTO)
        {
            if (userId == null)
            {
                return BadRequest(new { message = "El ID de la ruta no existe." });
            }

            try
            {
                var updatedUser = await _updateUserUseCase.ExecuteAsync(updateUserDTO);

                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al actualizar el usuario.", details = ex.Message });
            }
        }

        [HttpPut("{userId}/password")]
        public async Task<IActionResult> PutPassword([FromRoute] string userId, [FromBody] ChangePasswordDTO changePasswordDTO)
        {
            if (userId == null)
            {
                return BadRequest(new { message = "El ID de la ruta no existe." });
            }

            try
            {
                //añadir caso de uso
                var changePassword = await _updatePasswordUserUseCase.ExecuteAsync(changePasswordDTO);

                return Ok(changePassword);
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "Error al actualizar la contraseña del usuario.", details = ex.Message });
            }

        }

        [Authorize]
        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete([FromRoute] string userId)
        {
            try
            {
                var deletedUser = await _deleteUserUseCase.ExecuteAsync(userId);
                if (deletedUser == null)
                {
                    return NotFound(new { message = $"Usuario con ID {userId} no encontrado." });
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al eliminar el usuario.", details = ex.Message });
            }
        }
    }
}
