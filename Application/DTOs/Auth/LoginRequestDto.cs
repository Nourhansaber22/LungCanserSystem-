using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Auth
{
    // Login request: البيانات اللي المستخدم بيبعتها عشان يسجل الدخول
    public record LoginRequestDto(
        string Email,    // ايميل المستخدم
        string Password  // كلمة المرور
    );
}
