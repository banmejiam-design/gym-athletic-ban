using AutoMapper;
using GymManagement.API.DTOs;
using GymManagement.Domain.Entities;
using GymManagement.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GymClassesController : ControllerBase
{
    private readonly IGymClassService _service;
    private readonly IMapper _mapper;

    public GymClassesController(IGymClassService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GymClassDto>>> GetAll()
    {
        var classes = await _service.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<GymClassDto>>(classes));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GymClassDto>> GetById(int id)
    {
        var gymClass = await _service.GetByIdAsync(id);
        if (gymClass is null) return NotFound();
        return Ok(_mapper.Map<GymClassDto>(gymClass));
    }

    [HttpPost]
    public async Task<ActionResult<GymClassDto>> Create([FromBody] CreateGymClassDto dto)
    {
        try
        {
            var gymClass = _mapper.Map<GymClass>(dto);
            var created = await _service.CreateAsync(gymClass);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, _mapper.Map<GymClassDto>(created));
        }
        catch (ArgumentException ex) { return BadRequest(ex.Message); }
        catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<GymClassDto>> Update(int id, [FromBody] UpdateGymClassDto dto)
    {
        try
        {
            var gymClass = _mapper.Map<GymClass>(dto);
            gymClass.Id = id;
            var updated = await _service.UpdateAsync(gymClass);
            return Ok(_mapper.Map<GymClassDto>(updated));
        }
        catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
        catch (ArgumentException ex) { return BadRequest(ex.Message); }
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
