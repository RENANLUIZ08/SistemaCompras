using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaCompra.Domain.ProdutoAggregate
{
    public interface IProdutoRepository
    {
        Produto Obter(Guid id);
        List<Produto> ObterItens(List<Guid> ids);
        void Registrar(Produto entity);
        void Atualizar(Produto entity);
        void Excluir(Produto entity);
    }
}
