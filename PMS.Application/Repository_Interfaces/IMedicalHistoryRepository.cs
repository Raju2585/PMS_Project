using PMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Repository_Interfaces
{
    public interface IMedicalHistoryRepository
    {
         Task<bool> AddMedicalHistory(MedicalHistory medicalHistory);
        Task<MedicalHistory> GetMedicalHistory(int historyId);

        Task<MedicalHistory> UpdateMedicalHistory(int historyid, MedicalHistory medicalHistory);
    }
}
