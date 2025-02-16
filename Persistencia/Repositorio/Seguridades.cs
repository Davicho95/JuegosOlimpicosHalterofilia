using Aplicacion.Dto.Seguridades;
using Aplicacion.Dto.Seguridades.Request;
using Aplicacion.Exceptions;
using Aplicacion.Interfaces;
using Aplicacion.Wrappers;
using Dominio;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Persistencia.Contexto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Persistencia.Repositorio
{
    public class Seguridades : ISeguridades
    {
        private readonly AppDbContext _context;
        private readonly IOptions<JWTSettings> _options;

        public Seguridades(AppDbContext context, IOptions<JWTSettings> options)
        {
            _context = context;
            _options = options;
        }

        public async Task<Response<string>> GenerarToken(UsuarioRequest request)
        {
            try
            {
                #region Validaciones
                List<string> errores = new();

                if (string.IsNullOrEmpty(request.Usuario))
                {
                    errores.Add("El campo Usuario es obligatorio.");
                }
                if (string.IsNullOrEmpty(request.Contrasenia))
                {
                    errores.Add("El campo Contrasenia es obligatorio.");
                }

                if (errores.Count > 0)
                {
                    throw new ValidationException(errores);
                } 
                #endregion

                Usuario usuario = _context.Usuarios.FirstOrDefault(x => x.NombreUsuario.Equals(request.Usuario)) ?? throw new ApiException("Usuario o contraseña incorrectas");

                if (!usuario.Contrasenia.Equals(request.Contrasenia))
                    throw new ApiException("Usuario o contraseña incorrectas");

                JwtSecurityToken jwtSecurity = await GenerarJWTToken(usuario);

                return new Response<string>(new JwtSecurityTokenHandler().WriteToken(jwtSecurity), "Usuario autenticado crrectamente");

            }
            catch (ValidationException ex)
            {
                throw new ValidationException(ex.Errors);
            }
            catch (Exception ex)
            {
                throw new ApiException($"Ocurrió un error al momento de verificar usuario y contraseña {ex.Message}", ex.InnerException);
            }
        }

        private async Task<JwtSecurityToken> GenerarJWTToken(Usuario usuario)
        {
            try
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Name, usuario.NombreUsuario),
                    new Claim(JwtRegisteredClaimNames.NameId, usuario.IdUsuario.ToString())
                };

                var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Key));
                var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

                var jwtSecurityToken = new JwtSecurityToken(
                    issuer: _options.Value.Issuer,
                    audience: _options.Value.Audience,
                    claims: claims,
                    expires: DateTime.Now.AddHours(_options.Value.Expiracion),
                    signingCredentials: signingCredentials
                    );

                return await Task.FromResult(jwtSecurityToken);
            }
            catch (Exception ex)
            {
                throw new ApiException($"Ocurrió un error al momento de generar el Token ${ex.Message}", ex.InnerException);
            }
        }
    }
}
