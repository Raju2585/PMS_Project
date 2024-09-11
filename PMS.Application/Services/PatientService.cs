﻿
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PMS.Application.Interfaces;
using PMS.Application.Repository_Interfaces;
using PMS.Domain.Entities;
using PMS.Domain.Entities.Request;
using PMS.Domain.Entities.Response;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Services
{
    public class PatientService:IPatientService
    {
        private readonly IPatientRepository _repository;
        private IConfiguration _config;
        private readonly IMapper _mapper;
        public PatientService(
            IPatientRepository repository,
            IConfiguration configuration,
            IMapper mapper)
        {
            _repository = repository;
            _config = configuration;
            _mapper = mapper;
        }


        public async Task<List<PatientDtl>> GetAllPatientDtls()
        {
            List<PatientDtl> PatientResList = null;
            try
            {
                var PatientList = await _repository.GetAllPatients();
                PatientResList = _mapper.Map<List<PatientDtl>>(PatientList);
            }
            catch (Exception ex)
            {
                return PatientResList;
            }
            return PatientResList;
        }

        public async Task<PatientRes> RegisterPatient(PatientReq patientReq)
        {
            if(patientReq == null)
            {
                return new PatientRes { IsSuccess=false,ErrorMessage="Required a patient"};
            }
            try
            {
                var newPatient = _mapper.Map<Patient>(patientReq);
                newPatient.PatientName = patientReq.FirstName + " " + patientReq.LastName;
                newPatient.Date = DateTime.Now;
                var isPatientAdded = await _repository.RegisterPatient(newPatient);
                if (isPatientAdded)
                {
                    return new PatientRes { IsSuccess = true, PatientEmail = newPatient.PatientEmail };
                }
            }
            catch (Exception ex)
            {
                return new PatientRes { IsSuccess = false, ErrorMessage = "Patient not added" };
            }
            return new PatientRes { IsSuccess = false, ErrorMessage = "Patient not added" };
        }
        private async Task<PatientLogin> AuthenticatePatient(PatientLogin patient)
        {
            PatientLogin _patient = null;

            try
            {
                var patientOb = await _repository.GetPatientByEmail(patient.Email);

                if (patientOb != null && (patient.Email == patientOb.PatientEmail && patient.Password == patientOb.Password))
                {
                    _patient = patient;
                }
            }
            catch (Exception ex)
            {
                return _patient;
            }

            return _patient;

        }
        private async Task<string> GenerateToken(PatientLogin patient)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], null,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<string> Login(PatientLogin patient)
        {
            var token = "";
            try
            {
                var _user = await AuthenticatePatient(patient);
                if (_user != null)
                {
                    token = await GenerateToken(patient);
                }
            }
            catch (Exception e)
            {
                return token;
            }
            return token;
        }
    }
}
