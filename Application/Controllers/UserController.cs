using gamehub_API.Application.DTO.User;
using gamehub_API.Application.Interfaces;
using gamehub_API.Application.UseCases.User.CreateUserUseCase;
using gamehub_API.Application.UseCases.User.DeleteUserUseCase;
using gamehub_API.Application.UseCases.User.EditUserUseCase;
using gamehub_API.Application.UseCases.User.ViewUserUseCase;
using Microsoft.AspNetCore.Mvc;

namespace gamehub_API.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ICreateUserUseCase _createUserUseCase;
        private readonly IGetUserUseCase _viewUserUseCase;
        private readonly IUpdateUserUseCase _updateUserUseCase;
        private readonly IDeleteUserUseCase _deleteUserUseCase;

        public UserController(
            ICreateUserUseCase createUserUseCase,
            IGetUserUseCase viewUserUseCase,
            IUpdateUserUseCase updateUserUseCase,
            IDeleteUserUseCase deleteUserUseCase)
        {
            _createUserUseCase = createUserUseCase;
            _viewUserUseCase = viewUserUseCase;
            _updateUserUseCase = updateUserUseCase;
            _deleteUserUseCase = deleteUserUseCase;
        }

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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserDTO createUserDTO)
        {
            try
            {
                await _createUserUseCase.ExecuteAsync(createUserDTO);
                return CreatedAtAction(nameof(Get), new { userId = createUserDTO.userId }, createUserDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al crear el usuario.", details = ex.Message });
            }
        }

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
