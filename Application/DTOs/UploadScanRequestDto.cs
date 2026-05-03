using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
namespace Application.DTOs
{
    // رفع Scan جديد
    public record UploadScanRequestDto(
        int PatientId,   // ID المريض اللي هنرفع له الـ Scan
        IFormFile File   // ملف الـ Scan (CT Scan)
    );
}
