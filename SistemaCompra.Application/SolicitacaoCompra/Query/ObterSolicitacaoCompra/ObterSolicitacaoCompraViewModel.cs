using SistemaCompra.Domain.SolicitacaoCompraAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaCompra.Application.Produto.Query.ObterProduto
{
    public class ObterSolicitacaoCompraViewModel
    {
        public string Solicitante { get; set; }
        public string Fornecedor { get; set; }
        public string Data { get; set; }
        public string Situacao { get; set; }

        public ObterSolicitacaoCompraViewModel(Domain.SolicitacaoCompraAggregate.SolicitacaoCompra solicitacao)
        {
            Solicitante = solicitacao.UsuarioSolicitante.Nome;
            Fornecedor = solicitacao.NomeFornecedor.Nome;
            Data = solicitacao.Data.ToString("dd/MM/yyyy HH:mm:ss");
            Situacao = solicitacao.Situacao.ToString();
        }
    }
}
