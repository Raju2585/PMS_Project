﻿using PMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Interfaces
{
    public interface IHospitalService
    {
        Task<List<Hospital>> GetAllHospitals();

        Task<List<Hospital>> GetHospitalByLocation(string location);

        Task<List<Hospital>> GetHospitalByPinCode(int pinCode);

    }
}
