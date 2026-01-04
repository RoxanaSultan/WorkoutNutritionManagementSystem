using FitnessTracker.Application.DTOs.MeasurementLog;
using FitnessTracker.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace FitnessTracker.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MeasurementLogsController : ControllerBase
{
    private readonly IMeasurementLogService _service;

    public MeasurementLogsController(IMeasurementLogService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MeasurementLogDto>>> GetMeasurementLogs()
    {
        var result = await _service.GetAllAsync();
        if (!result.Success) return BadRequest(result.Error);
        return Ok(result.Data);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MeasurementLogDto>> GetMeasurementLog(int id)
    {
        var result = await _service.GetByIdAsync(id);

        if (!result.Success)
        {
            if (result.Error == "Measurement log not found.")
                return NotFound(result.Error);

            return BadRequest(result.Error);
        }

        return Ok(result.Data);
    }

    [HttpPost]
    public async Task<ActionResult<MeasurementLogDto>> PostMeasurementLog(CreateMeasurementLogDto dto)
    {
        var result = await _service.CreateAsync(dto);
        if (!result.Success) return BadRequest(result.Error);

        var created = result.Data!;
        return CreatedAtAction(nameof(GetMeasurementLog), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutMeasurementLog(int id, UpdateMeasurementLogDto dto)
    {
        var result = await _service.UpdateAsync(id, dto);

        if (!result.Success)
        {
            if (result.Error == "Measurement log not found.")
                return NotFound(result.Error);

            return BadRequest(result.Error);
        }

        return Ok(result.Data);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMeasurementLog(int id)
    {
        var result = await _service.DeleteAsync(id);

        if (!result.Success)
        {
            if (result.Error == "Measurement log not found.")
                return NotFound(result.Error);

            return BadRequest(result.Error);
        }

        return NoContent();
    }
}
