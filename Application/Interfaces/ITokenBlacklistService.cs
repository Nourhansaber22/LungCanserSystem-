using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITokenBlacklistService
    {
        Task RevokeTokenAsync(string token);
        Task<bool> IsTokenRevokedAsync(string token);
    }
}
