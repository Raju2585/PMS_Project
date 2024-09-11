using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PMS.Domain.Entities;
using PMS.Domain.Entities.Request;
using PMS.Domain.Entities.Response;
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
        }
    }
}
