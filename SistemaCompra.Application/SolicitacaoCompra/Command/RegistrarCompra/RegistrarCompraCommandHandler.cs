using MediatR;
using SistemaCompra.Domain.Core;
using SistemaCompra.Infra.Data.UoW;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ProdutoAgg = SistemaCompra.Domain.ProdutoAggregate;
using SolicitacaoCompraAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;

namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra
{
    public class RegistrarCompraCommandHandler : CommandHandler, IRequestHandler<RegistrarCompraCommand, bool>
    {
        private readonly SolicitacaoCompraAgg.ISolicitacaoCompraRepository _solicitacaoCompraRepository;
        private readonly ProdutoAgg.IProdutoRepository _produtoRepository;


        public RegistrarCompraCommandHandler(
            SolicitacaoCompraAgg.ISolicitacaoCompraRepository solicitacaoCompraRepository,
            ProdutoAgg.IProdutoRepository produtoRepository,
            IUnitOfWork uow,
            IMediator mediator) : base(uow, mediator)
        {
            _solicitacaoCompraRepository = solicitacaoCompraRepository;
            _produtoRepository = produtoRepository;
        }

        public Task<bool> Handle(RegistrarCompraCommand request, CancellationToken cancellationToken)
        {
            var solicitacaoCompra = new SolicitacaoCompraAgg.SolicitacaoCompra(request.NomeSolicitante, request.NomeFornecedor);
            var produtos = _produtoRepository.ObterItens(request.items.Select(s => s.id).ToList());

            solicitacaoCompra.ManutencaoItens(produtos, request.items);
            solicitacaoCompra.RegistrarCompra(request.Condicao);
            _solicitacaoCompraRepository.RegistrarCompra(solicitacaoCompra);

            Commit();
            PublishEvents(solicitacaoCompra.Events);

            return Task.FromResult(true);
        }
    }
}
