using Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Configuraciones
{
    public class Deportistaconfig : IEntityTypeConfiguration<Deportista>
    {
        public void Configure(EntityTypeBuilder<Deportista> builder)
        {
            builder.HasMany(d => d.Intentos)
                   .WithOne(i => i.DeportistaNavigation)
                   .HasForeignKey(i => i.IdDeportista);
        }
    }
}
