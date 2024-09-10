using PMS.Application.Interfaces;
using PMS.Application.Repository_Interfaces;
using PMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Services
{
    public class MedicalHistoryService : IMedicalhistoryService
    {
        public readonly IMedicalHistoryRepository _medicalHistoryRepository;
       
        public MedicalHistoryService(IMedicalHistoryRepository _medicalRepository)
        {
            _medicalHistoryRepository = _medicalRepository;
          
        }

        public async Task<MedicalHistory> AddMedicalHistory(MedicalHistory medicalHistory)
        {
            var medicalhistory = new MedicalHistory() { 
                HistoryId = medicalHistory.HistoryId,
                PatientId = medicalHistory.PatientId,
                DoctorId = medicalHistory.DoctorId,
                RecordedDate = medicalHistory.RecordedDate,
                Reason = medicalHistory.Reason,
                Patient=medicalHistory.Patient,
            };
            var addingMedicalHistory=await _medicalHistoryRepository.AddMedicalHistory(medicalhistory);
            return medicalhistory;

        }

        public async Task<MedicalHistory> GetMedicalHistory(int historyId)
        {
            return await _medicalHistoryRepository.GetMedicalHistory(historyId);
        }

        public async Task<MedicalHistory> UpdateMedicalHistory(int historyid, MedicalHistory medicalHistory)
        {
            return await _medicalHistoryRepository.UpdateMedicalHistory(historyid, medicalHistory); 
        }
    }
}
