namespace HerreraSierraVargasProyecto2P.Models
{
    public class CategoriaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        // (Opcional) Si quieres traer la lista de productos anidados:
        public List<ProductoDto> Productos { get; set; }
    }
}
