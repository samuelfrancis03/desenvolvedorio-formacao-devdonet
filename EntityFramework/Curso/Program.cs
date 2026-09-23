
using CursoEFCore.Data;
using CursoEFCore.Domain;
using CursoEFCore.Entities;
using CursoEFCore.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

/*
 * Verificando se existe migração pendente
 * 
using var db = new ApplicationContext();
var existe = db.Database.GetPendingMigrations().Any();

if (existe) 
{
    // Operações    
}
*/

//InserirDados();
//InserirDadosEmMassa();
//ConsultaDados();
//CadastrarPedido();
//ConsultarPedidoCarregamentoAdiantado();
//AtualizarDados();
RemoverRegistro();

static void RemoverRegistro() 
{
    using var db = new ApplicationContext();

    var cliente = db.Client.Find(6);
    db.Client.Remove(cliente);
    //db.Remove(cliente);
    //db.Entry(cliente).State = EntityState.Deleted;
    db.SaveChanges();
}

static void AtualizarDados() 
{
    using var db = new ApplicationContext();
    var cliente = db.Client.FirstOrDefault(p => p.Id == 2);

    cliente.Email = "email.alterado@teste.com";


    //Uptade -> atualiza todos os campos independente do qual foi alterado
    db.Client.Update(cliente);
    //db.Entry(cliente).State = EntityState.Modified; //Atualizando com mapeamento
   
    db.SaveChanges();

}

static void ConsultarPedidoCarregamentoAdiantado() 
{
    using var db = new ApplicationContext();
    //Include -> Realiza o carregamento adiantado
    var pedidos = db.Pedidos
        .Include(p=>p.Itens)
        .ThenInclude(p=>p.Produto)
        .ToList();

    Console.WriteLine(pedidos.Count);
}

//Cadastrando pedido
static void CadastrarPedido()
{
    using var db = new ApplicationContext();

    //Buscando um cliente e um produto já existentes na base de dados
    var cliente = db.Client.FirstOrDefault();
    var produto = db.Produtos.FirstOrDefault();

    if (cliente == null || produto == null)
    {
        Console.WriteLine("É necessário ter ao menos um cliente e um produto cadastrados.");
        return;
    }

    var pedido = new Pedido
    {
        ClienteId = cliente.Id,
        IniciadoEm = DateTime.Now,
        FinalizadoEm = DateTime.Now,
        Observacao = "Pedido Teste",
        Status = StatusPedido.Analise,
        TipoFrete = TipoFrete.SemFrete,
        Itens = new List<PedidoItem>
        {
            new PedidoItem
            {
                ProdutoId = produto.Id,
                Desconto = 0,
                Quantidade = 1,
                Valor = 10
            }
        }
    };

    //Ao adicionar o pedido, o EF também rastreia e insere os itens do pedido
    db.Pedidos.Add(pedido);

    var registros = db.SaveChanges();
    Console.WriteLine($"Total registro: {registros}");
}

//Consultando dados
static void ConsultaDados() 
{
    using var db = new ApplicationContext();

    //var consultaPorSintaxe = (from c in db.Client where c.Id > 0 select c).ToList();
    //AsNoTracking -> Ignora os objetos em memoria, e faz a busca direto na base de dados.
    //var consultaPorMetodo = db.Client.AsNoTracking().Where(p=>p.Id>0).ToList();
    //var consultaPorMetodo = db.Client.Where(p => p.Id > 0).ToList();
    var consultaPorMetodo = db.Client.Where(p => p.Id > 0).OrderBy(p=>p.Id).ToList(); //consulta ordenada
    foreach (var cliente in consultaPorMetodo)
    {
        
        Console.WriteLine($"Consultando cliente: {cliente.Id}");
        //Find -> Executa a consulta primeiro em memoria, caso não encontre, busca na base de dados.
        //db.Client.Find(cliente.Id);
        //FirstOrDefault -> Executa a consulta direto na base de dados.
        db.Client.FirstOrDefault(p => p.Id == cliente.Id);

    }
}


static void InserirDadosEmMassa()
{
    var produto = new Produto
    {
        Descricao = "Produto Em Massa",
        CodigoBarras = "7891234567890",
        Valor = 25.90m,
        TipoProduto = TipoProduto.Embalagem,
        Ativo = true

    };

    var cliente = new Cliente
    {
        Nome = "Maria Oliveira",
        CEP = "30130010",
        Cidade = "Belo Horizonte",
        Estado = "MG",
        Telefone = "31988887777",
        Email = "maria.oliveira@teste.com"

    };


    var listaClientes = new[]
    {
        new Cliente
        {
            Nome = "Oliveira",
            CEP = "30130010",
            Cidade = "Belo Horizonte",
            Estado = "MG",
            Telefone = "31988889999",
            Email = "oliveira@teste.com"
        },

        new Cliente
        {
            Nome = "Teste",
            CEP = "30130010",
            Cidade = "Belo Horizonte",
            Estado = "MG",
            Telefone = "11111111111",
            Email = "Teste@teste.com"
        },
    };

    using var db = new ApplicationContext();
    //db.AddRange(produto, cliente);

    db.Set<Cliente>().AddRange(listaClientes);

    var registros = db.SaveChanges();
    Console.WriteLine($"Total registro: {registros}");
}


//Operações do Entity
//Inserindo registro
static void InserirDados()
{
    var produto = new Produto
    {
        Descricao = "Produto Teste",
        CodigoBarras = "1234567891231",
        Valor = 10m,
        TipoProduto = TipoProduto.MercadoriaParaRevenda,
        Ativo = true

    };


    using var db = new ApplicationContext();
    //Rastreando as informações do objeto a ser a adicionado
    //db.Produtos.Add(produto);
    //db.Set<Produto>().Add(produto);
    //db.Entry(produto).State = EntityState.Added;
    db.Add(produto);

    //Pega o objeto rastreado, e salva no banco
    var registros = db.SaveChanges();
    Console.WriteLine($"Total registro: {registros}");
}

