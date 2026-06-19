using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dsw2026Ej15.Api.Controllers
{
    
    

    
    public class DoctorsController : AppController

    {
        private readonly IPersistence _persistence;
        public DoctorsController (IPersistence persistence)
        {
            _persistence = persistence;
        }
        [HttpPost("doctors")]
        public async Task<IActionResult> CreateDoctor(DoctorModel.Request request)
        {
            if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.LicenseNumber))
            {
                return BadRequest("Nombre y matrícula requeridos");
            }

            var speciality = _persistence.GetSpecialityById(request.SpecialityId);
            if(speciality is null)
            {
                return BadRequest("Especialidad no existente");
            }
            var doctor = new Doctor(request.Name, request.LicenseNumber, speciality);
            _persistence.SaveDoctor(doctor);
                return Created();
        }

        [HttpGet("doctors")]
        public async Task<IActionResult> GetDoctors()
        {
            var doctors = _persistence.GetActiveDoctors();
            return Ok(doctors);
        }

        [HttpGet("doctors/{id}")]
        public async Task<IActionResult> GetDoctorById(Guid id)
        {
            var doctor = _persistence.GetActiveDoctorById(id);
            if (doctor is null)
            {
                return NotFound("Médico no encontrado o inactivo");
            }

            var response = new
            {
                doctor.Name,
                doctor.LicenseNumber,
                SpecialityName = doctor.Speciality?.Name
            };
            return Ok(response);
        }

    }
}
