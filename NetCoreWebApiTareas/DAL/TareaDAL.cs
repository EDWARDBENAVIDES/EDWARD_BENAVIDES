using NetCoreWebApiTareas.Models;
using System.Data.SqlClient;
using System.Data;

namespace NetCoreWebApiTareas.DAL
{
    public class TareaDAL
    {
        private readonly IConfiguration _Conf;
        private String? DB_CONEXION = "";

        public TareaDAL(IConfiguration config)
        {
            _Conf = config;
            DB_CONEXION = _Conf.GetValue<string>("ConnectionStrings:CadenaConexion_DB");
        }

        public bool CrearTarea(Tarea _Tarea)
        {
            SqlConnection _Conn = new SqlConnection();
            SqlCommand _Comd = new SqlCommand();

            try
            {
                _Conn.ConnectionString = DB_CONEXION;
                _Conn.Open();

                _Comd.CommandType = CommandType.StoredProcedure;
                _Comd.CommandText = "SP_TAREAS";
                _Comd.Parameters.AddWithValue("@opcion", "NUEVA TAREA");
                _Comd.Parameters.AddWithValue("@titulo", _Tarea.Titulo);
                _Comd.Parameters.AddWithValue("@descripcion", _Tarea.Descripcion);
                _Comd.Parameters.AddWithValue("@fecha_vencimiento", _Tarea.FechaVencimiento);
                _Comd.Parameters.AddWithValue("@usuario_creacion", _Tarea.UsuarioCreacion);

                _Comd.Connection = _Conn;

                Int64 rowsAffected = Convert.ToInt64(_Comd.ExecuteScalar());

                if (rowsAffected > 0)
                {

                    return true;

                }
                else
                {
                    return false;
                }
                
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _Conn.Close();
                _Conn.Dispose();
                _Comd.Dispose();
            }
        }
        
        public List<Tarea> ConsultarTarea(Int64 _id)
        {
            List<Tarea> _Result = new List<Tarea>();
            SqlConnection _Conn = new SqlConnection();
            SqlCommand _Comd = new SqlCommand();
            SqlDataReader _Dr;

            try
            {
                _Conn.ConnectionString = DB_CONEXION;
                _Conn.Open();

                _Comd.CommandType = CommandType.StoredProcedure;
                _Comd.CommandText = "SP_TAREAS";
                _Comd.Parameters.AddWithValue("@opcion", "CONSULTAR TAREAS");
                _Comd.Parameters.AddWithValue("@id",_id);

                _Comd.Connection = _Conn;

                _Dr = _Comd.ExecuteReader();
                while (_Dr.Read())
                {
                    Tarea tarea = new Tarea
                    {
                        Id = Convert.ToInt16(_Dr.IsDBNull(_Dr.GetOrdinal("ID")) ? 0 : _Dr["ID"]),
                        Titulo = Convert.ToString(_Dr.IsDBNull(_Dr.GetOrdinal("TITULO")) ? "" : _Dr["TITULO"]),
                        Descripcion = Convert.ToString(_Dr.IsDBNull(_Dr.GetOrdinal("DESCRIPCION")) ? "" : _Dr["DESCRIPCION"]),
                        FechaCreacion = Convert.ToDateTime(_Dr.IsDBNull(_Dr.GetOrdinal("FECHA CREACION")) ? DateTime.MinValue : _Dr["FECHA CREACION"]),
                        FechaVencimiento = Convert.ToDateTime(_Dr.IsDBNull(_Dr.GetOrdinal("FECHA VENCIMIENTO")) ? DateTime.MinValue : _Dr["FECHA VENCIMIENTO"]),
                        Completada = Convert.ToBoolean(_Dr.IsDBNull(_Dr.GetOrdinal("COMPLETADA")) ? false : _Dr["COMPLETADA"]),
                        Usuario = Convert.ToString(_Dr.IsDBNull(_Dr.GetOrdinal("USUARIO")) ? "" : _Dr["USUARIO"])
                    };

                    _Result.Add(tarea);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _Conn.Close();
                _Conn.Dispose();
                _Comd.Dispose();
                _Dr = null;
            }

            return _Result;
        }

        public bool ActualizarTarea(Tarea _Tarea)
        {
            SqlConnection _Conn = new SqlConnection();
            SqlCommand _Comd = new SqlCommand();


            try
            {
                _Conn.ConnectionString = DB_CONEXION;
                _Conn.Open();

                _Comd.CommandType = CommandType.StoredProcedure;
                _Comd.CommandText = "SP_TAREAS";
                _Comd.Parameters.AddWithValue("@opcion", "ACTUALIZAR TAREA");
                _Comd.Parameters.AddWithValue("@id", _Tarea.Id);
                _Comd.Parameters.AddWithValue("@titulo", _Tarea.Titulo);
                _Comd.Parameters.AddWithValue("@descripcion", _Tarea.Descripcion);
                _Comd.Parameters.AddWithValue("@fecha_vencimiento", _Tarea.FechaVencimiento);

                _Comd.Connection = _Conn;

                Int64 rowsAffected = Convert.ToInt64(_Comd.ExecuteScalar());

                if (rowsAffected > 0)
                {

                    return true;

                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _Conn.Close();
                _Conn.Dispose();
                _Comd.Dispose();
            }
        }

        public bool EliminarTarea(Int64 _id)
        {
            SqlConnection _Conn = new SqlConnection();
            SqlCommand _Comd = new SqlCommand();


            try
            {
                _Conn.ConnectionString = DB_CONEXION;
                _Conn.Open();

                _Comd.CommandType = CommandType.StoredProcedure;
                _Comd.CommandText = "SP_TAREAS";
                _Comd.Parameters.AddWithValue("@opcion", "ELIMINAR TAREA");
                _Comd.Parameters.AddWithValue("@id", _id);
                _Comd.Connection = _Conn;

                Int64 rowsAffected = Convert.ToInt64(_Comd.ExecuteScalar());

                if (rowsAffected > 0)
                {

                    return true;

                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _Conn.Close();
                _Conn.Dispose();
                _Comd.Dispose();
            }
        }
    }
}
