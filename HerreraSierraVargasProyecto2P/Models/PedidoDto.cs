using System;
using System.Collections.Generic;

namespace Inventory.Mobile.Models
{
    public class PedidoDto
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int ClienteId { get; set; }
        public ClienteDto Cliente { get; set; }
        public decimal Total { get; set; }
        public List<DetallePedidoDto> DetallePedidos { get; set; }
    }
}