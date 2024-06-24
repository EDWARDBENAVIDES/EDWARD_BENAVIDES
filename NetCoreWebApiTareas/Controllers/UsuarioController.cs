using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCoreWebApiTareas.DAL;
using NetCoreWebApiTareas.Models;

namespace NetCoreWebApiTareas.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    //[Route("usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly IConfiguration conf;
        public UsuarioController(IConfiguration config)
        {
            conf = config;
        }


        [HttpPost]
        [AllowAnonymous]
        [ActionName("Login")]
        public ActionResult<object> Login([FromBody] Usuario _usuario)
        {
            UsuarioDAL _UsuarioDAL = new UsuarioDAL(conf);
            Usuario _Usuario;
            Respuesta _Respuesta = new Respuesta();

            try
            {
                if (_usuario == null)
                {
                    return BadRequest(_usuario);
                }

                _Usuario = _UsuarioDAL.Login(_usuario);
                if (_Usuario != null)
                {
                    _Respuesta.Codigo = 0;
                    _Respuesta.Estado = "OK";
                    _Respuesta.Descripcion = "Login Exitoso";
                    _Respuesta.Objeto = _Usuario;

                    return Ok(_Respuesta);
                }
                else
                {
                    _Respuesta.Codigo = 1;
                    _Respuesta.Estado = "OK";
                    _Respuesta.Descripcion = "Login no válido verifique sus credenciales";

                    return NotFound(_Respuesta);
                }
            }
            catch (Exception ex)
            {
                _Respuesta.Codigo = -1;
                _Respuesta.Estado = "ERROR";
                _Respuesta.Descripcion = ex.Message;

                return BadRequest(_Respuesta);
            }
        }

    }
}
