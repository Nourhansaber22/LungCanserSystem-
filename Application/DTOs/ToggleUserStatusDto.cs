using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    // تغيير حالة المستخدم (تفعيل/تعطيل)
    public record ToggleUserStatusDto(
        bool IsActive     // true = مفعل, false = معطل
    );
}
