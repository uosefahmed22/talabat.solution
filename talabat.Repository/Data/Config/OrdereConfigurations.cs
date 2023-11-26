using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.core.Entites.Order_Aggregate;

namespace talabat.Repository.Data.Config
{
    public class OrdereConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(O => O.status)
                   .HasConversion(OStatus => OStatus.ToString(),
                   OStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), OStatus));

            builder.OwnsOne(O => O.ShipingAddress, SA => SA.WithOwner()); //[ 1 : 1 ]

            builder.Property(O => O.SubTotal)
                   .HasColumnType("decimal(18,2)");

        }
    }
}
