using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record RegisterUserDto(
    string Name,
    string Email,
    string Password,
    UserRole Role   
);
}
