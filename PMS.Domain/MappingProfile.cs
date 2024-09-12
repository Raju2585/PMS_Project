﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PMS.Domain.Entities;
using PMS.Domain.Entities.DTOs;
using PMS.Domain.Entities.Request;
using PMS.Domain.Entities.Response;
using PMS.Domain.NewFolder;
namespace PMS.Domain
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Patient, PatientReq>();
            CreateMap<PatientReq, Patient>();
            CreateMap<Patient, PatientDtl>();
            CreateMap<PatientDtl, Patient>();
<<<<<<< HEAD
<<<<<<< HEAD
            CreateMap<MedicalHistory,MedicalHistoryDTOs>();
            CreateMap<MedicalHistoryDTOs,MedicalHistory>();
=======
            CreateMap<Doctor,DoctorDTO>().ReverseMap();  
            
>>>>>>> HospitalService
=======

            CreateMap<Appointment,AppointmentDto>();
            CreateMap<AppointmentDto,Appointment>();
            CreateMap<Appointment,RequestAppointmentDto>();
            CreateMap<RequestAppointmentDto, Appointment>();
>>>>>>> 5bcabd489a64c63b1afae32c055a7747e067c46f
        }
    }
}
