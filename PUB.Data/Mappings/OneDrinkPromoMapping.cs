using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PUB.Domain.Entities;

namespace PUB.Data.Mappings
{
    internal class OneDrinkPromoMapping : IEntityTypeConfiguration<OneDrinkPromo>
    {
        public void Configure(EntityTypeBuilder<OneDrinkPromo> builder)
        {

            builder.Property(c => c.Name)
                .IsRequired();

            builder.Property(c => c.Cpf)
                .IsRequired()
                .HasColumnType("varchar(11)");
        }
    }
}
