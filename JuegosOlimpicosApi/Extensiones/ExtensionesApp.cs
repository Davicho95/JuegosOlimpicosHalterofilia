using JuegosOlimpicosApi.Middlewares;

namespace JuegosOlimpicosApi.Extensiones
{
    public static class ExtensionesApp
    {
        public static void UseErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}
