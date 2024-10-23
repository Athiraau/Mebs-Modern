using Business.Contracts;
using Business.Helpers;
using DataAccess.Contracts;
using DataAccess.Dto;
using DataAccess.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class ServiceWrapper:IServiceWrapper
    {
        private readonly LoansModuleRepo _LoansRepo;
        private readonly HRMModuleRepo _HRMRepo;
        private readonly OthersModuleRepo _OthersRepo;

        private ILoansModuleService _loansservice;
        private IHRMModuleService _HRMservice;
        private IOthersModuleService _Otherservice;
        private readonly DtoWrapper _dto;
        private IJwtUtils _jwtUtils;
        private readonly ILoggerService _logger;
        private IConfiguration _config;
        private readonly HelperWrapper _helper;

        public ServiceWrapper(LoansModuleRepo LoansRepo, HRMModuleRepo HRMRepo, OthersModuleRepo OthersRepo, ILoansModuleService loansservice, IHRMModuleService HRMService, DtoWrapper dto,
            IJwtUtils jwtUtils , ILoggerService logger, IConfiguration config, HelperWrapper helper)

        {
            _LoansRepo = LoansRepo;
            _HRMRepo = HRMRepo;
            _OthersRepo = OthersRepo;
            _HRMservice = HRMService;
            _loansservice=loansservice;

            _dto = dto;
            _jwtUtils = jwtUtils;
            _logger = logger;
            _config = config;
            _helper = helper;
        }

        public IJwtUtils JwtUtils
        {
            get
            {
                if (_jwtUtils == null)
                {
                    _jwtUtils = new JwtUtils(_config,_logger);
                }
                return _jwtUtils;
            }
        }

        public ILoansModuleService loanService
        {
            get
            {
                if (_loansservice == null)
                {

                    _loansservice = new LoansModuleService(_LoansRepo, _dto,_config,_helper);
                }
                return _loansservice;
            }
        }

        public IHRMModuleService HRMService
        {
            get
            {
                if (_HRMservice == null)
                {

                    _HRMservice = new HRMModuleService(_HRMRepo, _dto, _config, _helper);
                }
                return _HRMservice;
            }
        }

        public IOthersModuleService OthersService
        {
            get
            {
                if (_Otherservice == null)
                {

                    _Otherservice = new OthersModuleService(_OthersRepo, _dto, _config, _helper);
                }
                return _Otherservice;
            }
        }




    }
}
