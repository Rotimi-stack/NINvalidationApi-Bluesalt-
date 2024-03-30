using System;
using System.Collections.Generic;
using System.Text;

namespace Nin.Application.Common.Responses.SmileResponses
{
    public class Response
    {
        public string JSONVersion { get; set; } = string.Empty;
        public string SmileJobID { get; set; } = string.Empty;
        public PartnerParams PartnerParams { get; set; }
        public string ResultType { get; set; } = string.Empty;
        public string ResultText { get; set; } = string.Empty;
        public string ResultCode { get; set; } = string.Empty;
        public string IsFinalResult { get; set; } = string.Empty;
        public Actions Actions { get; set; }
        public string Country { get; set; } = string.Empty;
        public string IDType { get; set; } = string.Empty;
        public string IDNumber { get; set; } = string.Empty;
        public string ExpirationDate { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string DOB { get; set; } = string.Empty;
        public string Photo { get; set; } = string.Empty;
        public string signature { get; set; } = string.Empty;
        public DateTime timestamp { get; set; }
    }

    public class Actions
    {
        public string Verify_ID_Number { get; set; } = string.Empty;
        public string Return_Personal_Info { get; set; } = string.Empty;
    }

    public class PartnerParams
    {
        public string user_id { get; set; } = string.Empty;
        public string job_id { get; set; } = string.Empty;
        public int job_type { get; set; }
    }



    public class EnhancedKYCResponse
    {
        public string error { get; set; }
        public string code { get; set; }
        public bool success { get; set; }
        public string message { get; set; }
    }
}
