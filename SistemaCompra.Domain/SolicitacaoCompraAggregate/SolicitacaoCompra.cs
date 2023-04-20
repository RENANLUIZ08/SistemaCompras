using MediatR;
using SistemaCompra.Domain.Core;
using SistemaCompra.Domain.Core.Model;
using SistemaCompra.Domain.ProdutoAggregate;
using SistemaCompra.Domain.SolicitacaoCompraAggregate.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCompra.Domain.SolicitacaoCompraAggregate
{
    public class SolicitacaoCompra : Entity
    {
        public UsuarioSolicitante UsuarioSolicitante { get; private set; }
        public NomeFornecedor NomeFornecedor { get; private set; }
        public IList<Item> Itens { get; private set; }
        public DateTime Data { get; private set; }
        public Money TotalGeral { get; private set; }
        public Situacao Situacao { get; private set; }
        public CondicaoPagamento CondicaoPagamento { get; private set; }

        private SolicitacaoCompra() { }

        public SolicitacaoCompra(string usuarioSolicitante, string nomeFornecedor)
        {
            Id = Guid.NewGuid();
            UsuarioSolicitante = new UsuarioSolicitante(usuarioSolicitante);
            NomeFornecedor = new NomeFornecedor(nomeFornecedor);
            Data = DateTime.Now;
            Situacao = Situacao.Solicitado;
            Itens = new List<Item>();
        }

        public void ManutencaoItens(IList<Produto> produtos, IList<(Guid id, int qtd)> values)
        {
            if (produtos.Count != values.Count) throw new BusinessRuleException("Um ou mais produtos não foram localizados para prosseguir, tente novamente!");

            var itens = values.Join(
                produtos,
                item => item.id,
                produto => produto.Id,
                (item, produto) => new Item(produto, item.qtd));

            Itens.Clear();
            Parallel.ForEach(itens, item =>
            { Itens.Add(item); });
        }

        public void AdicionarItem(Produto produto, int qtde)
        {
            Itens.Add(new Item(produto, qtde));
        }

        public void RegistrarCompra(int condicao = 0)
        {
            if ((Itens?.Count).GetValueOrDefault(0) == 0) throw new BusinessRuleException("A solicitação de compra deve possuir itens!");

            TotalGeral = new Money(Itens.Sum(x => x.Subtotal.Value));
            if (TotalGeral.Value > 50000)
            {
                if (condicao != 30) throw new BusinessRuleException("A condição de pagamento para compras acima de R$50.000 deve ser de 30 dias.");
                
                CondicaoPagamento = new CondicaoPagamento(30);
            }
            else
            { CondicaoPagamento = new CondicaoPagamento(0); }

            Situacao = Situacao.Atendido;

            var eventoCompraRegistrada = new CompraRegistradaEvent(Id, Itens, TotalGeral.Value);
            AddEvent(eventoCompraRegistrada);
        }
    }
}
