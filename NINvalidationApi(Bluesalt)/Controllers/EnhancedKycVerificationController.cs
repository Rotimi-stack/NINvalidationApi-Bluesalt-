using Microsoft.AspNetCore.Mvc;
using Nin.Application.Common.Responses.SmileResponses;
using Nin.Application.Common.SmileIdentity.EnhancedKycVerification.Command;

namespace NINvalidationApi_Bluesalt_.Controllers
{
    public class EnhancedKycVerificationController : BaseController
    {
        [HttpPost("/enhanced/kyc/verification")]
        public async Task<ActionResult<EnhancedKYCResponse>> GetEnhancedKYCVerification(EnhancedVerificationCommand lyc)
        {
            return await Mediator.Send(lyc);
        }

        [HttpPost("/callbackUrl")]
        public async Task<ActionResult<EnhancedKYCResponse>> CallBackUrl(CallBackUrlCommand vyc)
        {
            return await Mediator.Send(vyc);
        }
    }
}
