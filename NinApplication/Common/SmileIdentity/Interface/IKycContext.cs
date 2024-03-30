using Microsoft.EntityFrameworkCore;
using Nin.Application.Common.Responses.SmileResponses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nin.Application.Common.SmileIdentity.Interface
{
    public interface IKycContext
    {
        DbSet<SmiletblRequestandResponseLogs> SmiletblRequestAndResponse { get; set; }
    }
}
