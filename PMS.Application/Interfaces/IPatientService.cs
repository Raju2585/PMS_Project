﻿using PMS.Domain.Entities;
using PMS.Domain.Entities.Request;
using PMS.Domain.Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Interfaces
{
    public interface IPatientService
    {
        Task<List<Patient>> GetAllPatients();
        Task<PatientRes> RegisterPatient(PatientReq patientReq);
        Task<string> Login(PatientLogin patient);
    }
}
