using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOP.Core.Controllers;
using MOP.Core.Mediator;
using MOP.Core.Utils;
using MOP.Order.API.Application.Messages.Commands.OrderCommand;
using MOP.Order.API.Application.Messages.Queries.OrderQuery;
using System;
using System.Threading.Tasks;

namespace MOP.Order.API.Controllers
{
    [Produces("application/json")]
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : BaseController
    {

        private readonly IMediatorHandler _mediator;

        public OrdersController(IMediatorHandler mediator)
        {
            _mediator = mediator;
        }

        // GET: api/orders
        /// <summary>
        /// Obtêm os pedidos
        /// </summary>
        /// <returns>Coleção de objetos da classe Pedidos</returns>                
        /// <response code="200">Lista dos pedidos</response>        
        /// <response code="400">Falha na requisição</response>         
        /// <response code="404">Nenhum aluno foi localizado</response>         
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            var getAllOrdertQuery = new GetAllOrderQuery();

            var orders = await _mediator.SendQuery(getAllOrdertQuery);

            return ListUtils.isEmpty(orders) ? NotFound() : CustomResponse(orders);
        }

        // GET: api/orders/5
        /// <summary>
        /// Obtêm as informações do pedido pelo seu Id
        /// </summary>
        /// <param name="id">Código do pedido</param>
        /// <returns>Objetos da classe Pedido</returns>                
        /// <response code="200">Informações do Pedido</response>
        /// <response code="400">Falha na requisição</response>         
        /// <response code="404">O aluno não foi localizado</response>         
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var getByIdOrderQuery = new GetByIdOrderQuery(id);

            var order = await _mediator.SendQuery(getByIdOrderQuery);

            return order == null ? NotFound() : CustomResponse(order);
        }

        // POST api/orders
        /// <summary>
        /// Grava o pedido
        /// </summary>   
        /// <remarks>
        /// Exemplo request:
        ///
        ///     POST /Order
        ///     {
        ///         "customerId": "A9E5B222-313C-4AE2-8E04-809C3CFF4A80",
        ///         "shipping": "FOB",
        ///         "observation": "Cliente irá retirar na loja",
        ///         "items": [
        ///             {
        ///                 "productId": "BCE4F473-3DFA-4FB9-8E1E-5997951F5485",
        ///                 "quantity": 1,
        ///                 "unitPrice": 4500.00,
        ///                 "discount": 0,
        ///                 "discountValue": 200.00
        ///             }
        ///         ]                 
        ///     }
        /// </remarks>        
        /// <returns>Retorna objeto criado da classe Pedido</returns>                
        /// <response code="201">O pedido foi incluído corretamente</response>                
        /// <response code="400">Falha na requisição</response>         
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ActionName("NewOrder")]
        public async Task<IActionResult> PostAsync([FromBody] CreateOrderCommand command)
        {
            var result = await _mediator.SendCommand(command);

            return result.ValidationResult.IsValid ? CreatedAtAction("NewOrder", new { id = result.response }, command) : CustomResponse(result.ValidationResult);
        }

        // PUT: api/orders/5
        /// <summary>
        /// Altera o pedido
        /// </summary>        
        /// <param name="id">Código do pedido</param>        
        /// <response code="204">O pedido foi alterado corretamente</response>                
        /// <response code="400">Falha na requisição</response>         
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] UpdateOrderCommand command)
        {
            var result = await _mediator.SendCommand(command);

            return CustomResponse(result);
        }

        // DELETE: api/orders/5
        /// <summary>
        /// Deleta o pedido pelo seu Id
        /// </summary>        
        /// <param name="id">Código do pedido</param>        
        /// <response code="204">O pedido foi excluído corretamente</response>                
        /// <response code="400">Falha na requisição</response>         
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteOrderCommand(id);

            var result = await _mediator.SendCommand(command);

            return CustomResponse(result);
        }
    }
}
