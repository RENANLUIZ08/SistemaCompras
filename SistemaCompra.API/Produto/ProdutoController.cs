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
    public class ProdutoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProdutoController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet, Route("produto/{id}")]
        public async Task<IActionResult> Obter(Guid id)
        {
            try
            {
                var query = new ObterProdutoQuery() { Id = id };
                var produtoViewModel = await _mediator.Send(query);
                return Ok(produtoViewModel);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }

        [HttpPost, Route("produto/cadastro")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CadastrarProduto([FromBody] RegistrarProdutoCommand registrarProdutoCommand)
        {
            try
            {
                await _mediator.Send(registrarProdutoCommand);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut, Route("produto/atualiza-preco")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AtualizarPreco([FromBody] AtualizarPrecoCommand atualizarPrecoCommand)
        {
            try
            {
                await _mediator.Send(atualizarPrecoCommand);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
