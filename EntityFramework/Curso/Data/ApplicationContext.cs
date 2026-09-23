using CursoEFCore.Data.Configurations;
using CursoEFCore.Domain;
using CursoEFCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    internal class ApplicationContext : DbContext
    {
        //Criando modelo de dados DbSet
        public DbSet<Pedido> Pedidos { get; set; }

        //Configurando a conexão com o banco
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Setando o provider que utilizaremos
            optionsBuilder.UseSqlServer("Data source=(localdb)\\mssqllocaldb;Initial Catalog=CursoEFCore;Integrated Security=true");
        }

        //Criando modelo de dados OnModelCreating()
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //FORMA AUTOMATIZADA - O ENTITY VAI PROCURAR TODAS A CLASSES EM QUAL FOI IMPLEMENTADA A INTERFACE  IEntityTypeConfiguration

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);

            /* FORMA MANUAL DE INSTANCIAR OS MODELOS DE DADOS
            modelBuilder.ApplyConfiguration(new ClienteConfiguration());
            modelBuilder.ApplyConfiguration(new PedidoConfiguration());
            modelBuilder.ApplyConfiguration(new PedidoItemConfiguration());
            modelBuilder.ApplyConfiguration(new ProdutoConfiguration());
            */
        }

    }
}
