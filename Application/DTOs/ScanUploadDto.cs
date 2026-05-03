using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;

namespace Application.DTOs
{
    public record ScanUploadDto(
         int Id,
         int PatientId,
         string FilePath,
         string FileName,
         DateTime UploadDate,
         string Status
     );
}
