using DataAccess.Dto.Request;
using DataAccess.Dto;
using DataAccess.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Contracts;

namespace Business.Helpers
{
    public class HelperWrapper:IHelperWrapper
    {

        private CommonValidationHelper _commonHelper;
        private readonly ErrorResponse _error;
        private readonly DtoWrapper _dto;
        private readonly IValidator<PostReqDto> _PunchPostReqDtoValidator;

        public HelperWrapper(CommonValidationHelper commonHelper,ErrorResponse error,DtoWrapper dto, IValidator<PostReqDto> PunchPostReqDtoValidator)


        {
            _commonHelper = commonHelper;
            _error = error;
            _dto = dto;
            _PunchPostReqDtoValidator = PunchPostReqDtoValidator;
        
        }

        public CommonValidationHelper CHelper
        {
            get
            {
                if (_commonHelper == null)
                {
                    _commonHelper = new CommonValidationHelper(_error, _dto, _PunchPostReqDtoValidator);
                }
                return _commonHelper;
            }
        }


     




    }
}
