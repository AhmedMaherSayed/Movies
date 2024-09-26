using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.API.DTOs;
using Movies.Core;
using Movies.Core.Models;

namespace Movies.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MovieController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var MoviesModel = await _unitOfWork.Repository<Movie>().GetAllAsync();

            if(MoviesModel is not null)
            {
                var MoviesDTO = _mapper.Map<IEnumerable<Movie>, IEnumerable<MovieDTO>>(MoviesModel);
                return Ok(MoviesDTO);
            }
            return BadRequest();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var movieModel = await _unitOfWork.Repository<Movie>().GetByIdAsync(id);

            if(movieModel is not null)
            {
                var movieDTO = _mapper.Map<Movie, MovieDTO>(movieModel);
                return Ok(movieDTO);
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Add(MovieWithGenreIdDTO movieDTO)
        {
            if(movieDTO is not null)
            {
                var movieModel = _mapper.Map<MovieWithGenreIdDTO, Movie>(movieDTO);
                await _unitOfWork.Repository<Movie>().AddAsync(movieModel);
                await _unitOfWork.CompleteAsync();
                return Ok(movieModel);
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Update(MovieDetailsDTO movieDTO)
        {
            var movieModel = await _unitOfWork.Repository<Movie>().GetByIdAsync(movieDTO.Id);

            if(movieModel is not null )
            {
                movieModel = _mapper.Map<MovieDetailsDTO, Movie>(movieDTO);
                _unitOfWork.Repository<Movie>().UpdateAsync(movieModel);
                await _unitOfWork.CompleteAsync();
                return Ok(movieModel);
            }
            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var movieModel = await _unitOfWork.Repository<Movie>().GetByIdAsync(id);

            if(movieModel is not null )
            {
                _unitOfWork.Repository<Movie>().DeleteAsync(movieModel);
                await _unitOfWork.CompleteAsync();
                return Ok($"Movie with Id = {id} is deleted Sucessfully!");
            }
            return BadRequest();
        }
    }
}
