using CursoEFCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CursoEFCore.Data.Configurations
{
    internal class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("Pedidos");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.IniciadoEm).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd(); //Propiedade - Executa a operação no sql - Quando inserir o dado no banco gere o comando informado no HasDefaultValueSql() 
            builder.Property(p => p.Status).HasConversion<string>();
            builder.Property(p => p.TipoFrete).HasConversion<string>();
            builder.Property(p => p.Observacao).HasColumnType("VARCHAR(512)");

            builder.HasMany(p => p.Itens) //Configura o relacionamento n..1
                .WithOne(p => p.Pedido) //Ou seja, existe varios itens atrelados a um só pedido.
                .OnDelete(DeleteBehavior.Cascade); //Quando deletar um pedido sera deletado os itens desse pedidos.
        }
    }
}
