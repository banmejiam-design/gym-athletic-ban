using AutoMapper;
using GymManagement.API.DTOs;
using GymManagement.Domain.Entities;
using GymManagement.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrainersController : ControllerBase
{
    private readonly ITrainerService _service;
    private readonly IMapper _mapper;

    public TrainersController(ITrainerService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TrainerDto>>> GetAll()
    {
        var trainers = await _service.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<TrainerDto>>(trainers));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TrainerDto>> GetById(int id)
    {
        var trainer = await _service.GetByIdAsync(id);
        if (trainer is null) return NotFound();
        return Ok(_mapper.Map<TrainerDto>(trainer));
    }

    [HttpPost]
    public async Task<ActionResult<TrainerDto>> Create([FromBody] CreateTrainerDto dto)
    {
        try
        {
            var trainer = _mapper.Map<Trainer>(dto);
            var created = await _service.CreateAsync(trainer);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, _mapper.Map<TrainerDto>(created));
        }
        catch (ArgumentException ex) { return BadRequest(ex.Message); }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TrainerDto>> Update(int id, [FromBody] UpdateTrainerDto dto)
    {
        try
        {
            var trainer = _mapper.Map<Trainer>(dto);
            trainer.Id = id;
            var updated = await _service.UpdateAsync(trainer);
            return Ok(_mapper.Map<TrainerDto>(updated));
        }
        catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
    }
}
