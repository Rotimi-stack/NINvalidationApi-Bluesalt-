using Nin.Application.Common.Model.Bluesalt.Resources;
using Nin.Application.Common.Responses.BluesaltResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nin.Application.Common.Interface
{
    public interface IBluesaltServiceInterface
    {
        Task<NinResponse> NinValidation(NinValidationResource ra);
        Task<NinPhResponse> NinPhoneValidation(NinPhoneResource ra);
        Task<NinPhoneBasicResponse> NinPhoneValidationBasic(NinPhonebasicValidationResource ra);


    }
}
