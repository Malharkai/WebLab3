using BlazorProject.Server.Models;
using BlazorProject.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorProject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticionersController : ControllerBase
    {
        private readonly IParticipantRepository participantRepository;

        public ParticionersController(IParticipantRepository employeeRepository)
        {
            this.participantRepository = employeeRepository;
        }

        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<Participant>>> Search(string name, Gender? gender)
        {
            try
            {
                var result = await participantRepository.Search(name, gender);

                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetParticipants()
        {
            try
            {
                return Ok(await participantRepository.GetParticipants());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Participant>> GetParticipant(int id)
        {
            try
            {
                var result = await participantRepository.GetParticipant(id);

                if (result == null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Participant>> CreateEmployee(Participant participant)
        {
            try
            {
                if (participant == null)
                    return BadRequest();

                var emp = await participantRepository.GetParticipantByEmail(participant.Email);

                if(emp != null)
                {
                    ModelState.AddModelError("Email", "Employee email already in use");
                    return BadRequest(ModelState);
                }

                var createdParticipant = await participantRepository.AddParticipant(participant);

                return CreatedAtAction(nameof(GetParticipant),
                    new { id = createdParticipant.ParticipantId }, createdParticipant);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new employee record");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Participant>> UpdateEmployee(int id, Participant participant)
        {
            try
            {
                if (id != participant.ParticipantId)
                    return BadRequest("Employee ID mismatch");

                var employeeToUpdate = await participantRepository.GetParticipant(id);

                if (employeeToUpdate == null)
                {
                    return NotFound($"Employee with Id = {id} not found");
                }

                return await participantRepository.UpdateParticipant(participant);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating employee record");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            try
            {
                var employeeToDelete = await participantRepository.GetParticipant(id);

                if (employeeToDelete == null)
                {
                    return NotFound($"Employee with Id = {id} not found");
                }

                await participantRepository.DeleteParticipant(id);

                return Ok($"Employee with Id = {id} deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting employee record");
            }
        }
    }
}
