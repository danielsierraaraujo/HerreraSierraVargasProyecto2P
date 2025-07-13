using SQLite;

namespace HerreraSierraVargasProyecto2P.Models
{
    public class ProductoDto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public int CategoriaId { get; set; }
        [Ignore]
        public CategoriaDto Categoria { get; set; }
    }
}
