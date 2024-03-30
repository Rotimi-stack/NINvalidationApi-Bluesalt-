using Microsoft.EntityFrameworkCore;
using Nin.Application.Common.Interface;
using Nin.Application.Common.Model.Bluesalt;
using Nin.Application.Common.Responses.BluesaltResponse;
using Nin.Application.Common.Responses.SmileResponses;

namespace Bluesalt.Infrastructure.Context
{
    public class NinValidationContext : DbContext, IBluesaltInterface
    {
        public NinValidationContext(DbContextOptions<NinValidationContext> options) : base(options)
        {
        }

        protected NinValidationContext()
        {
        }

        public DbSet<tblRequestandResponseLogs> tblRequestAndResponse { get; set; }
        public DbSet<SmiletblRequestandResponseLogs> SmiletblRequestAndResponse { get; set; }
    }
}
