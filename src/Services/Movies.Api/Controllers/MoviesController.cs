using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.Api.Repositories;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace Movies.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController(IMovieRepository repository) : ControllerBase
    {
        private readonly IMovieRepository _repository = repository;

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetMovieById([FromRoute] Guid id)
        {
            return Ok(await _repository.GetMovieByIdAsync(id));
        }

        [HttpPost]
        [Route("/")]
        public async Task<IActionResult> CreateMovieById(
            [FromBody] CreateMovieRequest request)
        {
            return Ok(await _repository.CreateMovieAsync(new Entities.Models.Movie(request.Title, request.Description)));
        }

        public record CreateMovieRequest(
            [Required]
            [StringLength(100)]
            string Title,
            [StringLength(500)]
            string Description);
    }
}
