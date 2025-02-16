namespace Aplicacion.Interfaces
{
    public interface IDatosIniciales
    {
        void CreacionBaseDeDatos(IServiceProvider serviceProvider);

        Task InsetarDatosIniciales();
    }
}
