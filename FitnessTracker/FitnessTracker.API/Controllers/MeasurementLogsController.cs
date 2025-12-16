using FitnessTracker.Application.Interfaces;
using FitnessTracker.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FitnessTracker.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MeasurementLogsController : ControllerBase
{
    private readonly IMeasurementLogService _measurementLogService;

    public MeasurementLogsController(IMeasurementLogService measurementLogService)
    {
        _measurementLogService = measurementLogService;
    }

    // GET: api/MeasurementLogs
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MeasurementLog>>> GetMeasurementLogs()
    {
        var result = await _measurementLogService.GetAllAsync();

        if (!result.IsSuccess)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    // GET: api/MeasurementLogs/5
    [HttpGet("{id}")]
    public async Task<ActionResult<MeasurementLog>> GetMeasurementLog(int id)
    {
        var result = await _measurementLogService.GetByIdAsync(id);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    // PUT: api/MeasurementLogs/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutMeasurementLog(int id, MeasurementLog measurementLog)
    {
        var result = await _measurementLogService.UpdateAsync(id, measurementLog);

        if (!result.IsSuccess)
        {
            if (result.Error == "Measurement log not found.")
            {
                return NotFound(result.Error);
            }

            return BadRequest(result.Error);
        }

        return NoContent();
    }

    // POST: api/MeasurementLogs
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<MeasurementLog>> PostMeasurementLog(MeasurementLog measurementLog)
    {
        var result = await _measurementLogService.CreateAsync(measurementLog);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Error);
        }

        return CreatedAtAction(
            nameof(GetMeasurementLog),
            new { id = measurementLog.Id },
            result.Value
        );
    }

    // DELETE: api/MeasurementLogs/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMeasurementLog(int id)
    {
        var result = await _measurementLogService.DeleteAsync(id);

        if (!result.IsSuccess)
        {
            if (result.Error == "Measurement log not found.")
            {
                return NotFound(result.Error);
            }

            return BadRequest(result.Error);
        }

        return NoContent();
    }
}
