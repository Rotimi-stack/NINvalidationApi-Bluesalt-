using Microsoft.AspNetCore.Mvc;
using Nin.Application.Common.Bluesalt.BluesaltCommand;
using Nin.Application.Common.Responses.BluesaltResponse;

namespace NINvalidationApi_Bluesalt_.Controllers
{
    public class NinValidationController : BaseController
    {
        [HttpPost("nin-validation")]
        public async Task<ActionResult<NinResponse>> NinValidation([FromForm]NinValidationCommand lyc)
        {
            return await Mediator.Send(lyc);
        }

        [HttpPost("nin-validation-phone")]
        public async Task<ActionResult<NinPhResponse>> NinPhoneValidation([FromForm] NinPhoneCommand lyc)
        {
            return await Mediator.Send(lyc);
        }

        [HttpPost("nin-validation-phone-basic")]
        public async Task<ActionResult<NinPhoneBasicResponse>> NinPhoneValidationBasic([FromForm] NinPhoneBasicCommand lyc)
        {
            return await Mediator.Send(lyc);
        }
    }
}
