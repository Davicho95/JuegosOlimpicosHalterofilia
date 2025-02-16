using Aplicacion.Exceptions;
using Aplicacion.Interfaces;
using Aplicacion.Wrappers;
using Dominio;
using Newtonsoft.Json;
using System.Net;

namespace JuegosOlimpicosApi.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogService _logs;

        private string? body;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogService logs)
        {
            _next = next;
            _logs = logs;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                context.Request.EnableBuffering();
                var reader = new StreamReader(context.Request.Body);
                body = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0;

                if (string.IsNullOrEmpty(body) && context.Request.QueryString.HasValue)
                {
                    body = context.Request.QueryString.Value;
                }

                await _logs.Log(new Log()
                {
                    Fecha = DateTime.Now,
                    Json = JsonConvert.SerializeObject(body),
                    Mensaje = $"Consumo EndPoint {context.Request.RouteValues["controller"]}/{context.Request.RouteValues["action"]}",
                    TipoLog = "Consumo"
                });

                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new Response<string>() { Succeeded = false, Message = error.Message };

                switch (error)
                {
                    case ApiException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case ValidationException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        responseModel.Errors = e.Errors;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                int id = await _logs.Log(new Log()
                {
                    Fecha = DateTime.Now,
                    Json = body,
                    Mensaje = $"{responseModel.Message} -> {string.Join(". ", responseModel.Errors)} {error.InnerException?.Message}",
                    TipoLog = "Error"
                });

                if (id != 0)
                {
                    responseModel.Message += $" -> IdLog: {id}";
                }

                var result = JsonConvert.SerializeObject(responseModel);
                await response.WriteAsync(result);
            }
        }
    }
}
