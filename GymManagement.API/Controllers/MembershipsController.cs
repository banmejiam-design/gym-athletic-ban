using AutoMapper;
using GymManagement.API.DTOs;
using GymManagement.Domain.Entities;
using GymManagement.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MembershipsController : ControllerBase
{
    private readonly IMembershipService _service;
    private readonly IMapper _mapper;

    public MembershipsController(IMembershipService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MembershipDto>>> GetAll()
    {
        var memberships = await _service.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<MembershipDto>>(memberships));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MembershipDto>> GetById(int id)
    {
        var membership = await _service.GetByIdAsync(id);
        if (membership is null) return NotFound();
        return Ok(_mapper.Map<MembershipDto>(membership));
    }

    [HttpGet("member/{memberId}")]
    public async Task<ActionResult<IEnumerable<MembershipDto>>> GetByMember(int memberId)
    {
        var memberships = await _service.GetByMemberIdAsync(memberId);
        return Ok(_mapper.Map<IEnumerable<MembershipDto>>(memberships));
    }

    [HttpPost]
    public async Task<ActionResult<MembershipDto>> Create([FromBody] CreateMembershipDto dto)
    {
        try
        {
            var membership = _mapper.Map<Membership>(dto);
            var created = await _service.CreateAsync(membership);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, _mapper.Map<MembershipDto>(created));
        }
        catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
        catch (ArgumentException ex) { return BadRequest(ex.Message); }
        catch (InvalidOperationException ex) { return Conflict(ex.Message); }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<MembershipDto>> Update(int id, [FromBody] UpdateMembershipDto dto)
    {
        try
        {
            var membership = _mapper.Map<Membership>(dto);
            membership.Id = id;
            var updated = await _service.UpdateAsync(membership);
            return Ok(_mapper.Map<MembershipDto>(updated));
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
