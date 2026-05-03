using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;
namespace Application.DTOs
{
    // إنشاء مستخدم جديد
    public record CreateUserDto(
        string Name,      // اسم المستخدم
        string Email,     // ايميل المستخدم
        string Password,  // كلمة المرور (هتتحول لـ Hash قبل التخزين)
        UserRole Role         // الدور اللي المستخدم هيشتغل بيه
    );
}
