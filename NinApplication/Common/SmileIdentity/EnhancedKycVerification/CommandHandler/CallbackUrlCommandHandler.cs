using MediatR;
using Nin.Application.Common.Model.SmileIdentity;
using Nin.Application.Common.Responses.SmileResponses;
using Nin.Application.Common.SmileIdentity.EnhancedKycVerification.Command;
using Nin.Application.Common.SmileIdentity.Interface;

namespace Nin.Application.Common.SmileIdentity.EnhancedKycVerification.CommandHandler
{
    public class CallbackUrlCommandHandler : IRequestHandler<CallBackUrlCommand, EnhancedKYCResponse>
    {
        private readonly EnhancedKycVerificationInterface _kyc;
        public CallbackUrlCommandHandler(EnhancedKycVerificationInterface kyc)
        {
            _kyc = kyc;
        }
        public async Task<EnhancedKYCResponse> Handle(CallBackUrlCommand request, CancellationToken cancellationToken)
        {
            var data = new CallbackUrlResource
            {
                success = request.success
            };
            return await _kyc.CallBackUrl(data);
        }
    }
}
