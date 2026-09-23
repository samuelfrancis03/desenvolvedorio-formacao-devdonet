using CursoEFCore.Data.Configurations;
using CursoEFCore.Domain;
using CursoEFCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CursoEFCore.Data
{
    internal class ApplicationContext : DbContext
    {
        //Propiedade para ler log das operações feitas pelo o Entity FrameWork, pacote Microsoft.Extensions.Logging.Console
        private static readonly ILoggerFactory _logger = LoggerFactory.Create(p=>p.AddConsole());

        //Criando modelo de dados DbSet
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Cliente> Client { get; set; }

        //Configurando a conexão com o banco
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Setando o provider que utilizaremos
            optionsBuilder
                .UseLoggerFactory(_logger)
                .EnableSensitiveDataLogging()
                .UseSqlServer("Data source=(localdb)\\mssqllocaldb;Initial Catalog=CursoEFCore;Integrated Security=true", 
                p=>p.EnableRetryOnFailure(maxRetryCount: 3, maxRetryDelay: TimeSpan.FromSeconds(15), errorNumbersToAdd: null)//Configuração de tentativas de conexão
                .MigrationsHistoryTable("curso_ef_core_migrations")); //Configurando nome da tabela de Migrations
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
