using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inventory.API.Data;
using Inventory.API.Models;
// Generated with chat gpt
namespace Inventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PedidosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Pedidos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidos()
        {
            // Incluimos Cliente y DetallePedidos (y dentro de DetallePedidos, Producto).
            return await _context.Pedidos
                                 .Include(p => p.Cliente)
                                 .Include(p => p.DetallePedidos)
                                     .ThenInclude(d => d.Producto)
                                 .ToListAsync();
        }

        // GET: api/Pedidos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedido(int id)
        {
            var pedido = await _context.Pedidos
                                       .Include(p => p.Cliente)
                                       .Include(p => p.DetallePedidos)
                                           .ThenInclude(d => d.Producto)
                                       .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null)
                return NotFound();

            return pedido;
        }

        // POST: api/Pedidos
        // En este ejemplo, esperamos recibir un objeto Pedido con su lista DetallePedidos.
        // Calculamos el total automáticamente.
        [HttpPost]
        public async Task<ActionResult<Pedido>> PostPedido(Pedido pedido)
        {
            // 1. Verificar que exista el Cliente
            var clienteExistente = await _context.Clientes.FindAsync(pedido.ClienteId);
            if (clienteExistente == null)
                return BadRequest($"No existe un cliente con Id={pedido.ClienteId}");

            // 2. Calcular el total = suma de (cantidad * precioUnitario) de cada detalle
            decimal total = 0m;
            foreach (var detalle in pedido.DetallePedidos)
            {
                // Asegurarse de que exista cada producto
                var productoExistente = await _context.Productos.FindAsync(detalle.ProductoId);
                if (productoExistente == null)
                    return BadRequest($"No existe un producto con Id={detalle.ProductoId}");

                total += detalle.Cantidad * detalle.PrecioUnitario;
            }
            pedido.Total = total;
            pedido.Fecha = DateTime.UtcNow;

            // 3. Agregar el pedido (con sus detalles en cascada)
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPedido), new { id = pedido.Id }, pedido);
        }

        // PUT: api/Pedidos/5
        // En este ejemplo sólo permitiremos actualizar el Total o la fecha, no el cliente ni los detalles.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPedido(int id, Pedido pedido)
        {
            if (id != pedido.Id)
                return BadRequest();

            _context.Entry(pedido).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Pedidos.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/Pedidos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            var pedido = await _context.Pedidos
                                       .Include(p => p.DetallePedidos)
                                       .FirstOrDefaultAsync(p => p.Id == id);
            if (pedido == null)
                return NotFound();

            // Primero eliminamos los detalles (si la relación no está configurada en cascada)
            _context.DetallePedidos.RemoveRange(pedido.DetallePedidos);
            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
