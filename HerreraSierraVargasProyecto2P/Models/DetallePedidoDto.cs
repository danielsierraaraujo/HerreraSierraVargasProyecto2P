namespace Inventory.Mobile.Models
{
    public class DetallePedidoDto
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public PedidoDto Pedido { get; set; }
        public int ProductoId { get; set; }
        public ProductoDto Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
