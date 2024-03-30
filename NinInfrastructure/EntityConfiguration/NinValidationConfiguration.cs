using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nin.Application.Common.Responses.BluesaltResponse;

namespace Nin.Infrastructure.EntityConfiguration
{
    public class NinValidationConfiguration : IEntityTypeConfiguration<tblRequestandResponseLogs>
    {
        public void Configure(EntityTypeBuilder<tblRequestandResponseLogs> builder)
        {
            builder.ToTable("tblRequestAndResponse");
            builder.Property(e => e.Id)
                 .IsRequired()
                 .HasMaxLength(200)
                 .IsUnicode(false);
            builder.Property(e => e.RequestPayload)
                 .IsRequired()
                 .HasMaxLength(10)
                 .IsUnicode(false);
            builder.Property(e => e.RequestType)
                 .IsRequired()
                 .HasMaxLength(300)
                 .IsUnicode(false);
            builder.Property(e => e.Response)
                 .IsRequired()
                 .HasMaxLength(20)
                 .IsUnicode(false);

            builder.Property(e => e.ResponseTimestamp)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.RequestUrl)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.RequestTimestamp)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);
        }
    }
}
