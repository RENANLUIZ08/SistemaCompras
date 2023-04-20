using AutoMapper;
using MediatR;
using SistemaCompra.Application.Produto.Query.ObterProduto;
using SistemaCompra.Domain.SolicitacaoCompraAggregate;
using System.Threading;
using System.Threading.Tasks;

namespace SistemaCompra.Application.SolicitacaoCompra.Query.ObterSolicitacaoCompra
{
    public class ObterSolicitacaoCompraQueryHandler : IRequestHandler<ObterSolicitacaoCompraQuery, ObterSolicitacaoCompraViewModel>
    {
        private readonly ISolicitacaoCompraRepository _solicitacaoCompraRepository;
        private readonly IMapper mapper;

        public ObterSolicitacaoCompraQueryHandler(ISolicitacaoCompraRepository solicitacaoCompraRepository, IMapper mapper)
        {
            _solicitacaoCompraRepository = solicitacaoCompraRepository;
            this.mapper = mapper;
        }

        public Task<ObterSolicitacaoCompraViewModel> Handle(ObterSolicitacaoCompraQuery request, CancellationToken cancellationToken)
        {
            var solicitacaoCompra = _solicitacaoCompraRepository.Obter(request.Id);
            var solicitacaoCompraViewModel = mapper.Map<ObterSolicitacaoCompraViewModel>(solicitacaoCompra);

            return Task.FromResult(solicitacaoCompraViewModel);
        }
    }
}
