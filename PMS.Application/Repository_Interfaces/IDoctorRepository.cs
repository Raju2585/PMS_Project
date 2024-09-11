using PMS.Domain.NewFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Repository_Interfaces
{
    public interface IDoctorRepository
    {
        List<DoctorDTO> GetAllDoctorDTOs();
    }
}
