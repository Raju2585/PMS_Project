using PMS.Application.Interfaces;
using PMS.Application.Repository_Interfaces;
using PMS.Domain.Entities;

namespace PMS.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<AppointmentDto> ScheduleAppointment(AppointmentDto appointmentDto)
        {
            if (appointmentDto == null)
                throw new ArgumentNullException(nameof(appointmentDto));

            if (appointmentDto.AppointmentDate < DateTime.UtcNow)
                throw new ArgumentException("Appointment date cannot be in the past.");

            var appointment = new Appointment
            {
                PatientId = appointmentDto.PatientId,
                DoctorId = appointmentDto.DoctorId,
                AppointmentDate = appointmentDto.AppointmentDate,
                Status = appointmentDto.Status,
                Reason = appointmentDto.Reason,
                CreatedAt = appointmentDto.CreatedAt
            };

            var scheduledAppointment = await _appointmentRepository.ScheduleAppointment(appointment);

            var resultDto = new AppointmentDto
            {

                PatientId = scheduledAppointment.PatientId,
                DoctorId = scheduledAppointment.DoctorId,
                AppointmentDate = scheduledAppointment.AppointmentDate,
                Status = scheduledAppointment.Status,
                Reason = scheduledAppointment.Reason,
                CreatedAt = scheduledAppointment.CreatedAt
            };

            return resultDto;
        }

        public async Task<Appointment> GetAppointment(int appointmentId)
        {
            if (appointmentId <= 0)
            {
                throw new ArgumentException("Invalid appointment ID.");
            }
            var appointment = await _appointmentRepository.GetAppointment(appointmentId);

            if (appointment == null)
            {
                throw new KeyNotFoundException("Appointment not found.");
            }
            return appointment;
        }

        public async Task<Appointment> UpdateAppointment(int appointmentId, Appointment updatedAppointment)
        {
            if (updatedAppointment == null)
                throw new ArgumentNullException(nameof(updatedAppointment));

            var appointment = await _appointmentRepository.UpdateAppointment(appointmentId, updatedAppointment);

            if (appointment == null)
                throw new KeyNotFoundException("Appointment not found.");

            return appointment;
        }

        public async Task<List<Appointment>> GetAppointmentsByPatientId(int patientId)
        {
            if (patientId <= 0)
                throw new ArgumentException("Invalid patient ID.");

            return await _appointmentRepository.GetAppointmentsByPatientId(patientId);
        }

        public async Task<List<Appointment>> GetAppointmentsByDoctorId(int doctorId)
        {
            if (doctorId <= 0)
                throw new ArgumentException("Invalid doctor ID.");

            return await _appointmentRepository.GetAppointmentsByDoctorId(doctorId);
        }
    }
}
