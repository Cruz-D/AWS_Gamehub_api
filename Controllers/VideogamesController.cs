using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using gamehub_API.Application.UseCases.Videogame.GetAllVideogamesUseCase;
using gamehub_API.Application.UseCases.Videogame.GetVideogameUseCase;
using gamehub_API.Infrastructure.Models;

namespace gamehub_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideogamesController : ControllerBase
    {


        private readonly IGetAllVideogamesUseCase _getAllVideogames;
        private readonly IGetVideogameUseCase _getVideogameUseCase;

        public VideogamesController
            (
            IGetAllVideogamesUseCase getAllVideogames,
            IGetVideogameUseCase getVideogameUseCase
            )
        {
            _getAllVideogames = getAllVideogames;
            _getVideogameUseCase = getVideogameUseCase;
        }

        // GET: api/Videogames
        [HttpGet]
        public async Task<IActionResult> GetVideogames()
        { 
            Console.WriteLine("1 GetVideogames called");
            var videogames = await _getAllVideogames.ExecuteAsync();

            return Ok(videogames);
        }

        // GET: api/Videogames/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Videogame>> GetVideogame(string id)
        {
            Console.WriteLine("GetVideogame called with id: " + id);
            var videogame = await _getVideogameUseCase.ExecuteAsync(id);

            if (videogame == null)
            {
                return NotFound();
            }

            return videogame;
            
        }
    }
}
