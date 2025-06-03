namespace Inventory.API.Models
{
    public class DetallePedido
    {
        public int Id { get; set; }

        // FK al Pedido
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }

        // FK al Producto
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }

        public int Cantidad { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.Column(TypeName = "decimal(18,2)")]
        public decimal PrecioUnitario { get; set; }
    }
}