using MediatR;
using Nin.Application.Common.Responses.SmileResponses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nin.Application.Common.SmileIdentity.EnhancedKycVerification.Command
{

    public class EnhancedVerificationCommand : IRequest<EnhancedKYCResponse>
    {
        public string source_sdk { get; set; } = string.Empty;
        public string source_sdk_version { get; set; } = string.Empty;
        public string partner_id { get; set; } = string.Empty;
        public string timestamp { get; set; }
        public string signature { get; set; } = string.Empty;
        public string country { get; set; } = string.Empty;
        public string id_type { get; set; } = string.Empty;
        public string id_number { get; set; } = string.Empty;
        public string callback_url { get; set; } = string.Empty;
        public string first_name { get; set; } = string.Empty;
        //public string middle_name { get; set; } = string.Empty;
        public string last_name { get; set; } = string.Empty;
        public string bank_code { get; set; } = string.Empty;
        public string dob { get; set; } = string.Empty;
        //public string gender { get; set; } = string.Empty;
        public PartnerParams partner_params { get; set; }
    }
}
