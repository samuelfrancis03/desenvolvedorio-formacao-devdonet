using CursoEFCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CursoEFCore.Data.Configurations
{
    internal class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        //Configurando de forma separada o modelo de dados
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Cliente");
            builder.HasKey(p => p.Id); //Definindo chave primaria
            builder.Property(p => p.Nome).HasColumnType("VARCHAR(80)").IsRequired(); //Definino a propiedade - Tipo da coluna - Se é obrigatório o não
            builder.Property(p => p.Telefone).HasColumnType("CHAR(11)");
            builder.Property(p => p.CEP).HasColumnType("CHAR(8)").IsRequired();
            builder.Property(p => p.Estado).HasColumnType("CHAR(12)").IsRequired();
            builder.Property(p => p.Cidade).HasMaxLength(60).IsRequired(); // Propiedade - Tamanho maximo, é tipado de acordo com o tipo da prop - Obrigatório

            builder.HasIndex(i => i.Telefone).HasName("idx_cliente_telefone"); //criando um index para otimizar busca
        }
    }
}
