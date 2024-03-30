using MediatR;
using Nin.Application.Common.Model.SmileIdentity;
using Nin.Application.Common.Responses.SmileResponses;
using Nin.Application.Common.SmileIdentity.EnhancedKycVerification.Command;
using Nin.Application.Common.SmileIdentity.Interface;

namespace Nin.Application.Common.SmileIdentity.EnhancedKycVerification.CommandHandler
{
    public class EnhancedVerificationCommandHandler : IRequestHandler<EnhancedVerificationCommand, EnhancedKYCResponse>
    {
        private readonly EnhancedKycVerificationInterface _kyc;
        public EnhancedVerificationCommandHandler(EnhancedKycVerificationInterface kyc)
        {
            _kyc = kyc;
        }
        public async Task<EnhancedKYCResponse> Handle(EnhancedVerificationCommand request, CancellationToken cancellationToken)
        {
            var data = new EnhancedKycVerificationResources
            {
                callback_url = request.callback_url,
                country = request.country,
                dob = request.dob,
                first_name = request.first_name,
                //gender = request.gender,
                id_number = request.id_number,
                id_type = request.id_type,
                last_name = request.last_name,
                //middle_name = request.middle_name,
                partner_id = request.partner_id,
                partner_params = request.partner_params,
                bank_code = request.bank_code,
                signature = request.signature,
                source_sdk = request.source_sdk,
                source_sdk_version = request.source_sdk_version,
                timestamp = request.timestamp,
            };
            return await _kyc.GetEnhancedKYCVerification(data);
        }
    }
}
