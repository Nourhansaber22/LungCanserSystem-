using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;
namespace Application.DTOs
{
    // تحديث بيانات المستخدم (اسم أو دور)
    public record UpdateUserDto(
        string Name,      // الاسم الجديد
        UserRole Role         // الدور الجديد
    );

}
