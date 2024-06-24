using NetCoreWebApiTareas.Models;
using System.Data;
using System.Data.SqlClient;

namespace NetCoreWebApiTareas.DAL
{
    public class UsuarioDAL
    {
        private readonly IConfiguration _Conf;
        private String? DB_CONEXION = "";

        public UsuarioDAL(IConfiguration config)
        {
            _Conf = config;
            DB_CONEXION = _Conf.GetValue<string>("ConnectionStrings:CadenaConexion_DB");
        }

        public Usuario Login(Usuario _Usuario)
        {
            SqlConnection _Conn = new SqlConnection();
            SqlCommand _Comd = new SqlCommand();
            SqlDataReader _Dr;
            Usuario? _Result = null;

            try
            {
                _Conn.ConnectionString = DB_CONEXION;
                _Conn.Open();

                _Comd.CommandType = CommandType.StoredProcedure;
                _Comd.CommandText = "SP_USUARIOS";
                _Comd.Parameters.AddWithValue("@opcion", "LOGIN");
                _Comd.Parameters.AddWithValue("@usuario", _Usuario.NombreUsuario);
                _Comd.Parameters.AddWithValue("@clave", _Usuario.Clave);

                _Comd.Connection = _Conn;

                _Dr = _Comd.ExecuteReader();
                if (_Dr.Read())
                {
                    _Result = new Usuario();

                    _Result.Id = Convert.ToInt16(_Dr.IsDBNull(_Dr.GetOrdinal("ID")) ? 0 : _Dr["ID"]);
                    _Result.NombreUsuario = Convert.ToString(_Dr.IsDBNull(_Dr.GetOrdinal("USUARIO")) ? "" : _Dr["USUARIO"]);
                    _Result.Nombres = Convert.ToString(_Dr.IsDBNull(_Dr.GetOrdinal("NOMBRES")) ? "" : _Dr["NOMBRES"]);


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

    }

}
