namespace Inventory.API.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }

        // Relación a Categoría (FK)
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
    }
}