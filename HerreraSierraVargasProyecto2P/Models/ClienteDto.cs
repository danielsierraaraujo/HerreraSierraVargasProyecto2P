using SQLite;

namespace HerreraSierraVargasProyecto2P.Models
{
    public class ClienteDto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        [Ignore]
        public List<PedidoDto> Pedidos { get; set; }
    }
}
