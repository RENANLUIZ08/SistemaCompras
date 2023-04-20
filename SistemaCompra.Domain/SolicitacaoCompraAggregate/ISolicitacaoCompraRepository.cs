using System;
using SolicitacaoCompraAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;
namespace SistemaCompra.Domain.SolicitacaoCompraAggregate
{
    public interface ISolicitacaoCompraRepository
    {
        void RegistrarCompra(SolicitacaoCompra solicitacaoCompra);

        SolicitacaoCompra Obter(Guid id);
        
    }
}
