using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contracts
{
    public interface IServiceWrapper
    {
        ILoansModuleService loanService { get; }
        IHRMModuleService HRMService { get; }
        IOthersModuleService OthersService { get; }
        IJwtUtils JwtUtils { get; }
    }
}
