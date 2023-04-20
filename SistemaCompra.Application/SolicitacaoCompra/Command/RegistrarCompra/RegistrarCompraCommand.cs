using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;

namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra
{
    public class RegistrarCompraCommand : IRequest<bool>
    {
        public string NomeSolicitante { get; set; }
        public string NomeFornecedor { get; set; }
        public int Condicao { get; set; }
        public IList<(Guid id, int qtd)> items { get; set; }
    }
}
