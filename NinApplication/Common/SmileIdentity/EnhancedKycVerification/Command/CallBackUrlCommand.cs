using MediatR;
using Nin.Application.Common.Responses.SmileResponses;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Nin.Application.Common.SmileIdentity.EnhancedKycVerification.Command
{
    public class CallBackUrlCommand : IRequest<EnhancedKYCResponse>
    {
        public bool success { get; set; }
    }
}

