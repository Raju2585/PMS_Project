using PMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Interfaces
{
    public interface IMedicalhistoryService
    {
        Task<MedicalHistory> AddMedicalHistory(MedicalHistory medicalHistory);
        Task<MedicalHistory> GetMedicalHistory(int historyId);
        Task<MedicalHistory> UpdateMedicalHistory(int  historyid, MedicalHistory medicalHistory);
    }
}
