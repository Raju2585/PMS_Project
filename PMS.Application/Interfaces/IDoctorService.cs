using PMS.Domain.NewFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Interfaces
{
    public interface IDoctorService
    {
        Task<List<DoctorDTO>> GetAllDoctorsDTO();
    }
}
