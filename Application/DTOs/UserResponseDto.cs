using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;
namespace Application.DTOs
{
    public record UserResponseDto(
        int Id,
        string Name,
        string Email,
        UserRole Role,
        bool IsActive
    );
}
