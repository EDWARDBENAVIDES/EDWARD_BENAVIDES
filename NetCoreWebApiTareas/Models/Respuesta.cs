namespace NetCoreWebApiTareas.Models
{
    public class Respuesta
    {
        public Int64 Codigo { get; set; }
        public String? Estado { get; set; }
        public String? Descripcion { get; set; }
        public Object? Objeto { get; set; }
    }
}
