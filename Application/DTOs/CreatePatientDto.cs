using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    // إنشاء مريض جديد
    public record CreatePatientDto(
     string FullName,
     DateTime DateOfBirth,
     string Gender,
     string? ContactNumber
 );
}
