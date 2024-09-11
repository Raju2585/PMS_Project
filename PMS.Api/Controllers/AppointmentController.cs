using Microsoft.AspNetCore.Mvc;
using PMS.Application.Interfaces;
using PMS.Domain.Entities;

namespace PMS.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpPost]
        [Route("schedule")]
        public async Task<IActionResult> CreateAppointment([FromBody] AppointmentDto appointmentDto)
        {
            if (appointmentDto == null)
            {
                return BadRequest("Appointment cannot be null");
            }
            var result = await _appointmentService.ScheduleAppointment(appointmentDto);
            return Ok(result);
        }

        [HttpGet]
        [Route("Retrieve/{id:int}")]
        public async Task<IActionResult> RetrieveAppointmentById(int appointmentId)
        {
            var appointment = await _appointmentService.GetAppointment(appointmentId);
            if (appointment == null)
            {
                return NotFound();
            }
            return Ok(appointment);
        }

        [HttpPut]
        [Route("Modify/{id:int}")]
        public async Task<IActionResult> ModifyAppointment(int id, [FromBody] Appointment appointment)
        {
            if (id != appointment.AppointmentId)
            {
                return BadRequest("Appointment ID mismatch.");
            }

            var updatedAppointment = await _appointmentService.UpdateAppointment(id, appointment);
            if (updatedAppointment == null)
            {
                return NotFound();
            }

            return Ok(updatedAppointment);
        }

        [HttpGet]
        [Route("patient/{patientId:int}")]
        public async Task<IActionResult> RetrieveAppointmentsByPatientId(int patientId)
        {
            var appointment = await _appointmentService.GetAppointmentsByPatientId(patientId);
            return Ok(appointment);
        }

        [HttpGet]
        [Route("doctor/{doctorId:int}")]
        public async Task<IActionResult> RetrieveAppointmentsByDoctorId(int doctorId)
        {
            var appointment = await _appointmentService.GetAppointmentsByDoctorId(doctorId);
            return Ok(appointment);
        }
    }
}

