namespace Inventory.API.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public ICollection<Producto> Productos { get; set; }
    }
}