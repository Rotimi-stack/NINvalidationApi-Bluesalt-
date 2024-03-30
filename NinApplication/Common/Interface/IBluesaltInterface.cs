using Microsoft.EntityFrameworkCore;
using Nin.Application.Common.Responses.BluesaltResponse;

namespace Nin.Application.Common.Interface
{
    public interface IBluesaltInterface
    {
        DbSet<tblRequestandResponseLogs> tblRequestAndResponse { get; set; }
    }
}
