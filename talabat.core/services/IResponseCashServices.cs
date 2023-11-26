using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace talabat.core.services
{
    public interface IResponseCashServices
    {
        Task CashResponseAsync(string CashKey,object Response,TimeSpan TimeAlive);
        Task<string> GetCashedResponseAsync(string CashKey);
    }
}
