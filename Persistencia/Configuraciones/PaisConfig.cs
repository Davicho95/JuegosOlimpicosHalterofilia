using Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Configuraciones
{
    public class PaisConfig : IEntityTypeConfiguration<Pais>
    {
        public void Configure(EntityTypeBuilder<Pais> builder)
        {
            builder.HasMany(p => p.Deportistas)
                   .WithOne(d => d.PaisNavigation)
                   .HasForeignKey(d => d.CodigoPais);
        }
    }
}
