using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.core.Entites.identity;
using talabat.core.Entites.Order_Aggregate;

namespace talabat.Repository.Data.Config
{
    public class DeliveryMethodeConfiguration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(P => P.Cost)
                   .HasColumnType("decimal(18,2)");
        }
    }
}
