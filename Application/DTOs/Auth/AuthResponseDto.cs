using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Auth
{
    // Login response: البيانات اللي بيرجعها السيرفر بعد تسجيل الدخول
    public record AuthResponseDto(
        string Token,    // JWT Token صالح للوصول للـ Endpoints المحمية
        string UserName, // اسم المستخدم
        UserRole Role        // الدور الخاص بالمستخدم (Admin, Clinician, etc)
    );
}
