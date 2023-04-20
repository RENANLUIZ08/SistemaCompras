using MediatR;
using Microsoft.AspNetCore.Mvc;
using SistemaCompra.Application.Produto.Command.AtualizarPreco;
using SistemaCompra.Application.Produto.Command.RegistrarProduto;
using SistemaCompra.Application.Produto.Query.ObterProduto;
using SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra;
using System;
using System.Threading.Tasks;

namespace SistemaCompra.API.Produto
{
    public class SolicitacaoCompraController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SolicitacaoCompraController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost, Route("solicitacao-compra/registrar-compra")]
        [ProducesResponseType(201)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> RegistrarCompra([FromBody] RegistrarCompraCommand registrarCompraCommand)
        {
            try
            {
                await _mediator.Send(registrarCompraCommand);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


    }
}
