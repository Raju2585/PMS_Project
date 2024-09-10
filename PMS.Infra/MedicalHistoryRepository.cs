using PMS.Application.Repository_Interfaces;
using PMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Infra
{
    public class MedicalHistoryRepository : IMedicalHistoryRepository
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public MedicalHistoryRepository(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<bool> AddMedicalHistory(MedicalHistory medicalHistory)
        {
            var isData=_applicationDbContext.MedicalHistories.Where(d=>d.HistoryId==medicalHistory.HistoryId).Any();
            if (isData)
            {
                return false;
            }
            _applicationDbContext.MedicalHistories.Add(medicalHistory);
            _applicationDbContext.SaveChanges();
            return true;

        }

        public  async Task<MedicalHistory> GetMedicalHistory(int historyId)
        {
            if (historyId <= 0)
            {
                throw new ArgumentException("Invalid history ID.", nameof(historyId));
            }

            return await _applicationDbContext.MedicalHistories.FindAsync(historyId);
            
        }

        public async Task<MedicalHistory> UpdateMedicalHistory(int historyid, MedicalHistory medicalHistory)
        {
            if (historyid <= 0)
            {
                throw new ArgumentException("Invalid history ID.", nameof(historyid));
            }
            var existingMedicalHistory= await GetMedicalHistory(historyid);
            if (existingMedicalHistory == null)
            {
                return null;
            }
            existingMedicalHistory.HistoryId = historyid;
            existingMedicalHistory.PatientId = medicalHistory.PatientId;
            existingMedicalHistory.DoctorId = medicalHistory.DoctorId;
            existingMedicalHistory.RecordedDate = medicalHistory.RecordedDate;
            existingMedicalHistory.Patient=medicalHistory.Patient;
            existingMedicalHistory.Reason=medicalHistory.Reason;
            _applicationDbContext.MedicalHistories.Update(existingMedicalHistory);
            await _applicationDbContext.SaveChangesAsync();
            return existingMedicalHistory;
        }
    }
}
