using AutoMapper;
using GymManagement.API.DTOs;
using GymManagement.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnrollmentsController : ControllerBase
{
    private readonly IEnrollmentService _service;
    private readonly IMapper _mapper;

    public EnrollmentsController(IEnrollmentService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EnrollmentDto>>> GetAll()
    {
        var enrollments = await _service.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<EnrollmentDto>>(enrollments));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EnrollmentDto>> GetById(int id)
    {
        var enrollment = await _service.GetByIdAsync(id);
        if (enrollment is null) return NotFound();
        return Ok(_mapper.Map<EnrollmentDto>(enrollment));
    }

    [HttpGet("member/{memberId}")]
    public async Task<ActionResult<IEnumerable<EnrollmentDto>>> GetByMember(int memberId)
    {
        var enrollments = await _service.GetByMemberIdAsync(memberId);
        return Ok(_mapper.Map<IEnumerable<EnrollmentDto>>(enrollments));
    }

    [HttpPost]
    public async Task<ActionResult<EnrollmentDto>> Enroll([FromBody] CreateEnrollmentDto dto)
    {
        try
        {
            var enrollment = await _service.EnrollAsync(dto.MemberId, dto.GymClassId);
            return CreatedAtAction(nameof(GetById), new { id = enrollment.Id }, _mapper.Map<EnrollmentDto>(enrollment));
        }
        catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
        catch (InvalidOperationException ex) { return Conflict(ex.Message); }
    }

    [HttpPatch("{id}/unenroll")]
    public async Task<IActionResult> Unenroll(int id)
    {
        try
        {
            await _service.UnenrollAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
    }
}
