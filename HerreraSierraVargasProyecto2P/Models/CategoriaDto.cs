using SQLite;

namespace HerreraSierraVargasProyecto2P.Models
{
    public class CategoriaDto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nombre { get; set; }
        [Ignore]
        public List<ProductoDto> Productos { get; set; }
    }
}
