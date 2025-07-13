using System;
using System.Collections.Generic;
using SQLite;

namespace HerreraSierraVargasProyecto2P.Models
{
    public class PedidoDto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int ClienteId { get; set; }
        [Ignore]
        public ClienteDto Cliente { get; set; }
        public decimal Total { get; set; }
        [Ignore]
        public List<DetallePedidoDto> DetallePedidos { get; set; }
    }
}