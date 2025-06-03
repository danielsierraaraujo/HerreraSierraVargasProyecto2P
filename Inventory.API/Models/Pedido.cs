using System;
using System.Collections.Generic;

namespace Inventory.API.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }

        // Clave foránea al Cliente que hizo el pedido
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        // Total del pedido (podría calcularse a partir de DetallePedidos, pero lo guardamos directo)
        public decimal Total { get; set; }

        // Relación: un Pedido tiene múltiples DetallePedido
        public ICollection<DetallePedido> DetallePedidos { get; set; }
    }
}