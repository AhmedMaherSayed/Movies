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
    public class GenresController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GenresController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var genresList = await _unitOfWork.Repository<Genre>().GetAllAsync();
            
            if(genresList is not null)
            {
                var genresDto = _mapper.Map<IEnumerable<Genre>, IEnumerable<GenreDTO>>(genresList);
                return Ok(genresDto);
            }

            return BadRequest();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var genreModel = await _unitOfWork.Repository<Genre>().GetByIdAsync(id);

            if(genreModel is not null)
            {
                var genreDto = _mapper.Map<Genre, GenreDTO>(genreModel);
                return Ok(genreDto);
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Add(GenreDTO genreDTO)
        {
            if(genreDTO is not null)
            {
                Genre genreModel = _mapper.Map<GenreDTO, Genre>(genreDTO);
                await _unitOfWork.Repository<Genre>().AddAsync(genreModel);
                await _unitOfWork.CompleteAsync();
                return Ok(genreModel);
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Update(GenreDetailsDTO genreDTO)
        {
            var genreModel = await _unitOfWork.Repository<Genre>().GetByIdAsync(genreDTO.Id);

            if(genreModel is not null)
            {
                genreModel = _mapper.Map<GenreDetailsDTO, Genre>(genreDTO);

                _unitOfWork.Repository<Genre>().UpdateAsync(genreModel); // There is a problem here 
                await _unitOfWork.CompleteAsync();
                return Ok(genreModel);
            }
            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var genreModel = await _unitOfWork.Repository<Genre>().GetByIdAsync(id);

            if(genreModel is not null)
            {
                _unitOfWork.Repository<Genre>().DeleteAsync(genreModel);
                await _unitOfWork.CompleteAsync();
                return Ok($"Genre with id = {id} is deleted Succesfully!");
            }
            return BadRequest();
        }
    }
}
