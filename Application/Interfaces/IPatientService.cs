using Application.DTOs;

namespace Application.Interfaces
{
    public interface IPatientService
    {
        Task<PatientResponseDto> CreatePatientAsync(CreatePatientDto dto, int assistantId);
        Task<PatientResponseDto> UpdatePatientAsync(int patientId, UpdatePatientDto dto);
        Task<IEnumerable<PatientResponseDto>> SearchPatientsAsync(string? searchTerm = null);
        Task<PatientResponseDto?> GetPatientByIdAsync(int patientId);
    }
}