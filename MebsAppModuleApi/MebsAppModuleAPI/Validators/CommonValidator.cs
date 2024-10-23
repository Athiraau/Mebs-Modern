using DataAccess.Dto.Request;
using FluentValidation;

namespace MebsAppModuleAPI.Validators
{
    public class CommonValidator:AbstractValidator<PostReqDto>
    {
        public CommonValidator() 
        {
            RuleFor(d => d.p_flag).NotNull().NotEmpty().WithMessage("Flag is required");


        }
    }
}
