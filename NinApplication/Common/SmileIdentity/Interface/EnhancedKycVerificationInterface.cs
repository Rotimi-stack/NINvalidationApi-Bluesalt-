using Nin.Application.Common.Model.SmileIdentity;
using Nin.Application.Common.Responses.SmileResponses;

namespace Nin.Application.Common.SmileIdentity.Interface
{
    public interface EnhancedKycVerificationInterface
    {
        Task<EnhancedKYCResponse> GetEnhancedKYCVerification(EnhancedKycVerificationResources kyc);
        Task<EnhancedKYCResponse> CallBackUrl(CallbackUrlResource kyc);
    }
}
