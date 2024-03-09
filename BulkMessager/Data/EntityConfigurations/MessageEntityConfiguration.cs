using BulkMessager.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BulkMessager.Data.EntityConfigurations {
    public class MessageEntityConfiguration {
        public static void Configure(EntityTypeBuilder<Message> entityBuilder) {
            entityBuilder.HasKey(t => t.Id);
            //entityBuilder.Property(t => t.Text).HasMaxLength(225);
            //entityBuilder.Property(t => t.ShortName).HasMaxLength(80);
            //entityBuilder.Property(t => t.Branch).HasMaxLength(120);
            //entityBuilder.Property(t => t.City).HasMaxLength(120);
            //entityBuilder.Property(t => t.Swift).HasMaxLength(80);
            //entityBuilder.Property(t => t.MmCode).HasMaxLength(20).IsFixedLength();
            //entityBuilder.Property(t => t.AbCode).HasMaxLength(20).IsFixedLength();
            //entityBuilder.Property(t => t.AccountNumber).HasMaxLength(30).IsFixedLength();
            //entityBuilder.Property(t => t.PostedOn).HasColumnName("added_on").IsRequired();
            //entityBuilder.Property(t => t.Active);
            //entityBuilder.Property(t => t.Deleted);
            //entityBuilder.Property(t => t.Field1).HasMaxLength(225);
            //entityBuilder.Property(t => t.AddedBy).HasMaxLength(30).IsFixedLength();
        }
    }
}
