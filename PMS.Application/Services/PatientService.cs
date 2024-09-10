﻿using Microsoft.Extensions.Configuration;
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
        public PatientService(IPatientRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _config = configuration;
        }


        public async Task<List<Patient>> GetAllPatients()
        {
            return await _repository.GetAllPatients();
        }

        public async Task<PatientRes> RegisterPatient(PatientReq patientReq)
        {
            if(patientReq == null)
            {
                return new PatientRes { IsSuccess=false,ErrorMessage="Required a patient"};
            }
            var newPatient = new Patient
            {
                PatientName = patientReq.FirstName + " " + patientReq.LastName,
                PatientEmail = patientReq.PatientEmail,
                Password = patientReq.Password,
                Age = patientReq.Age,
                Gender = patientReq.Gender,
                ContactNumber = patientReq.ContactNumber,
                DeviceName = patientReq.DeviceName,
                Date = DateTime.Now,
            };
            var isPatientAdded=await _repository.RegisterPatient(newPatient);
            if(isPatientAdded)
            {
                return new PatientRes { IsSuccess = true, PatientEmail = newPatient.PatientEmail };
            }
            return new PatientRes { IsSuccess = false, ErrorMessage = "Patient not added" };

        }
        private async Task<PatientLogin> AuthenticatePatient(PatientLogin patient)
        {
            PatientLogin _patient = null;
            var patientOb = await _repository.GetPatientByEmail(patient.Email);

            if (patientOb != null && (patient.Email == patientOb.PatientEmail && patient.Password == patientOb.Password))
            {
                _patient = patient;
            }

            return _patient;
        }
        private string GenerateToken(PatientLogin patient)
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
            var _user = AuthenticatePatient(patient);
            if (_user != null)
            {
                token = GenerateToken(patient);
            }
            return token;
        }
    }
}
