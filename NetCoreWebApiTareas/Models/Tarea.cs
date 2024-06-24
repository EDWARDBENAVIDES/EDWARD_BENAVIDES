namespace NetCoreWebApiTareas.Models
{
    public class Tarea
    {
        public Int64 Id { get; set; }
        public String? Titulo { get; set; }
        public String? Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime FechaVencimiento { get; set; }
        public Boolean Completada { get; set; }
        public Int32 UsuarioCreacion { get; set; }

        public String? Usuario { get; set; }
    }
}
