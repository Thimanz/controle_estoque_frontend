using GDE.Core.Controllers;
using GDE.Pedidos.API.Data;
using Microsoft.AspNetCore.Mvc;

namespace GDE.Pedidos.API.Controllers
{
    [Route("api/pedido")]
    public class PedidoController : MainController
    {
        private readonly PedidosContext _context;

        public PedidoController(PedidosContext context)
        {
            _context = context;
        }


    }
}
