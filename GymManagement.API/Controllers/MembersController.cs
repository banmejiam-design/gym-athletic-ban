using AutoMapper;
using GymManagement.API.DTOs;
using GymManagement.Domain.Entities;
using GymManagement.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MembersController : ControllerBase
{
    private readonly IMemberService _service;
    private readonly IMapper _mapper;

    public MembersController(IMemberService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetAll()
    {
        var members = await _service.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<MemberDto>>(members));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MemberDto>> GetById(int id)
    {
        var member = await _service.GetByIdAsync(id);
        if (member is null) return NotFound();
        return Ok(_mapper.Map<MemberDto>(member));
    }

    [HttpPost]
    public async Task<ActionResult<MemberDto>> Create([FromBody] CreateMemberDto dto)
    {
        try
        {
            var member = _mapper.Map<Member>(dto);
            var created = await _service.CreateAsync(member);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, _mapper.Map<MemberDto>(created));
        }
        catch (ArgumentException ex) { return BadRequest(ex.Message); }
        catch (InvalidOperationException ex) { return Conflict(ex.Message); }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<MemberDto>> Update(int id, [FromBody] UpdateMemberDto dto)
    {
        try
        {
            var member = _mapper.Map<Member>(dto);
            member.Id = id;
            var updated = await _service.UpdateAsync(member);
            return Ok(_mapper.Map<MemberDto>(updated));
        }
        catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
        catch (InvalidOperationException ex) { return Conflict(ex.Message); }
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
