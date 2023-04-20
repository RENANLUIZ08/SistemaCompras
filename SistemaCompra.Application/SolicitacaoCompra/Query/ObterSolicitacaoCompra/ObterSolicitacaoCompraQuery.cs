using MediatR;
using SistemaCompra.Application.Produto.Query.ObterProduto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaCompra.Application.SolicitacaoCompra.Query.ObterSolicitacaoCompra
{
    public class ObterSolicitacaoCompraQuery : IRequest<ObterSolicitacaoCompraViewModel>
    {
        public Guid Id { get; set; }
    }
}
