using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.core.Entites.identity;

namespace talabat.core.services
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(AppUser user);

    }
}
