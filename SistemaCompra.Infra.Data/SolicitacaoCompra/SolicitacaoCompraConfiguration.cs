using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using SolicitacaoCompraAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;

namespace SistemaCompra.Infra.Data.Produto
{
    public class SolicitacaoCompraConfiguration : IEntityTypeConfiguration<SolicitacaoCompraAgg.SolicitacaoCompra>
    {
        public void Configure(EntityTypeBuilder<SolicitacaoCompraAgg.SolicitacaoCompra> builder)
        {
            builder.ToTable("SolicitacaoCompra");
            builder.OwnsOne(c => c.TotalGeral, b => b.Property("Value").HasColumnName("TotalGeral"));
            builder.OwnsOne(c => c.NomeFornecedor, b => b.Property(x => x.Nome).HasColumnName("NomeFornecedor"));
            builder.OwnsOne(c => c.CondicaoPagamento, b => b.Property(x => x.Valor).HasColumnName("CondicaoPagamento"));
            builder.OwnsOne(c => c.UsuarioSolicitante, b => b.Property(x => x.Nome).HasColumnName("UsuarioSolicitante"));
            builder.HasMany(s => s.Itens)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
