using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Luvia.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class PatientService : IPatientService
    {
        private readonly LuviaDbContext _context;

        public PatientService(LuviaDbContext context)
        {
            _context = context;
        }

        // ✅ CREATE PATIENT
        public async Task<PatientResponseDto> CreatePatientAsync(CreatePatientDto dto, int assistantId)
        {
            var lastPatient = await _context.Patients
                .OrderByDescending(p => p.Id)
                .FirstOrDefaultAsync();

            string patientCode = $"PAT-{(lastPatient?.Id + 1 ?? 1):D4}";

            var patient = new Patient
            {
                PatientCode = patientCode,
                FullName = dto.FullName,
                DateOfBirth = dto.DateOfBirth,
                Gender = Enum.Parse<Gender>(dto.Gender, true),
                ContactNumber = dto.ContactNumber,
                CreatedBy = assistantId,
                CreatedAt = DateTime.UtcNow
            };

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            return MapToDto(patient);
        }

        // ✅ UPDATE PATIENT
        public async Task<PatientResponseDto> UpdatePatientAsync(int patientId, UpdatePatientDto dto)
        {
            var patient = await _context.Patients.FindAsync(patientId);

            if (patient == null)
                throw new KeyNotFoundException("Patient not found");

            patient.FullName = dto.FullName;
            patient.ContactNumber = dto.ContactNumber;
            patient.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return MapToDto(patient);
        }

        // ✅ SEARCH
        public async Task<IEnumerable<PatientResponseDto>> SearchPatientsAsync(string? searchTerm = null)
        {
            var query = _context.Patients.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(p =>
                    p.FullName.Contains(searchTerm) ||
                    p.PatientCode.Contains(searchTerm));
            }

            return await query
                .Select(p => MapToDto(p))
                .ToListAsync();
        }

        // ✅ GET BY ID
        public async Task<PatientResponseDto?> GetPatientByIdAsync(int patientId)
        {
            var patient = await _context.Patients.FindAsync(patientId);

            return patient == null ? null : MapToDto(patient);
        }

        // 🔥 helper (clean code)
        private static PatientResponseDto MapToDto(Patient p)
        {
            return new PatientResponseDto
            {
                Id = p.Id,
                PatientCode = p.PatientCode,
                FullName = p.FullName,
                DateOfBirth = p.DateOfBirth,
                Gender = p.Gender.ToString(),
                ContactNumber = p.ContactNumber,
                CreatedBy = p.CreatedBy,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            };
        }
    }
}