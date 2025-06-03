using System.Collections.Generic;

namespace Inventory.API.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }

        // Relación: un Cliente puede tener muchos Pedidos
        public ICollection<Pedido> Pedidos { get; set; }
    }
}