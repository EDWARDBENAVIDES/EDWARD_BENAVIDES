using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCoreWebApiTareas.DAL;
using NetCoreWebApiTareas.Models;

namespace NetCoreWebApiTareas.Controllers
{
    [ApiController]
    //[Route("tarea")]
    [Route("[controller]/[action]")]

    public class TareaController : ControllerBase
    {
        private readonly IConfiguration conf;
        public TareaController(IConfiguration config)
        {
            conf = config;
        }

        [HttpGet]
        [AllowAnonymous]
        [ActionName("ConsultarTarea")]
        public ActionResult<object> ConsultarTarea( Int64 _id)
        {
            TareaDAL _TareaDAL = new TareaDAL(conf);
            List<Tarea> _Tareas = new List<Tarea>();
            Respuesta _Respuesta = new Respuesta();

            try
            {
  
                _Tareas = _TareaDAL.ConsultarTarea(_id);
                if (_Tareas != null)
                {
                    _Respuesta.Codigo = 0;
                    _Respuesta.Estado = "OK";
                    _Respuesta.Descripcion = "Consulta realizada con éxito";
                    _Respuesta.Objeto = _Tareas;

                    return Ok(_Respuesta);
                }
                else
                {
                    _Respuesta.Codigo = 1;
                    _Respuesta.Estado = "OK";
                    _Respuesta.Descripcion = "La presente consulta no se pudo realizar";

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

        [HttpPost]
        [AllowAnonymous]
        [ActionName("CrearTarea")]
        public ActionResult<object> CrearTarea([FromBody] Tarea _Tarea)
        {
            TareaDAL _TareaDAL = new TareaDAL(conf);
            Respuesta _Respuesta = new Respuesta();

            try
            {
                if (_Tarea == null)
                {
                    return BadRequest(_Tarea);
                }

                if (_TareaDAL.CrearTarea(_Tarea))
                {
                    _Respuesta.Codigo = 0;
                    _Respuesta.Estado = "OK";
                    _Respuesta.Descripcion = "Se creó con éxito la tarea";
                    //_Respuesta.Objeto = _Id;

                    return Ok(_Respuesta);
                }
                else
                {
                    _Respuesta.Codigo = 1;
                    _Respuesta.Estado = "OK";
                    _Respuesta.Descripcion = "La creación de la tarea no finalizó correctamente";

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

        [HttpPost]
        [AllowAnonymous]
        [ActionName("ActualizarTarea")]
        public ActionResult<object> ActualizarTarea([FromBody] Tarea _Tarea)
        {
            TareaDAL _TareaDAL = new TareaDAL(conf);
            Respuesta _Respuesta = new Respuesta();

            try
            {
                if (_Tarea == null)
                {
                    return BadRequest(_Tarea);
                }

                if (_TareaDAL.ActualizarTarea(_Tarea))
                {
                    _Respuesta.Codigo = 0;
                    _Respuesta.Estado = "OK";
                    _Respuesta.Descripcion = "Actualización realizada con éxito";
                    _Respuesta.Objeto = 0;

                    return Ok(_Respuesta);
                }
                else
                {
                    _Respuesta.Codigo = 1;
                    _Respuesta.Estado = "OK";
                    _Respuesta.Descripcion = "La presente actualización no se pudo realizar";

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

        [HttpPost]
        [AllowAnonymous]
        [ActionName("EliminarTarea")]
        public ActionResult<object> EliminarTarea(Int64 _id)
        {
            TareaDAL _TareaDAL = new TareaDAL(conf);
            Respuesta _Respuesta = new Respuesta();

            try
            {
                //if (_Tarea == null)
                //{
                //    return BadRequest(_Tarea);
                //}

                if (_TareaDAL.EliminarTarea(_id))
                {
                    _Respuesta.Codigo = 0;
                    _Respuesta.Estado = "OK";
                    _Respuesta.Descripcion = "Se Eliminó con éxito la tarea";
                    _Respuesta.Objeto = _id;

                    return Ok(_Respuesta);
                }
                else
                {
                    _Respuesta.Codigo = 1;
                    _Respuesta.Estado = "OK";
                    _Respuesta.Descripcion = "la eliminación de la tarea no se pudo realizar";

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
