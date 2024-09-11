using PMS.Domain.Entities;

namespace PMS.Application.Interfaces
{
    public interface IAppointmentService
    {
        Task<AppointmentDto> ScheduleAppointment(AppointmentDto appointmentDto);
        Task<Appointment> GetAppointment(int appointmentId);
        Task<Appointment> UpdateAppointment(int appointmentId, Appointment appointment);
        Task<List<Appointment>> GetAppointmentsByPatientId(int patientId);
        Task<List<Appointment>> GetAppointmentsByDoctorId(int doctorId);

    }
}
