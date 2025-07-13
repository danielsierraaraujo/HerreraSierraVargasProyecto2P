using SQLite;

namespace HerreraSierraVargasProyecto2P.Models
{
    public class DetallePedidoDto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int PedidoId { get; set; }
        [Ignore]
        public PedidoDto Pedido { get; set; }
        public int ProductoId { get; set; }
        [Ignore]    
        public ProductoDto Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
