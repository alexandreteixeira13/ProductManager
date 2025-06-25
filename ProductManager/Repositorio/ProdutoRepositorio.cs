using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using ProductManager.Models;
using System.Data;

namespace ProductManager.Repositorio
{
    public class ProdutoRepositorio(IConfiguration configuration)
    {
        private readonly string _conexaoMySQL = configuration.GetConnectionString("ConexaoMySQL");

        public void AdicionarProduto(Produto produto)
        {

            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new("INSERT INTO PRODUTOS (Nome, Descricao, Preco, Quantidade) values (@Nome, @Descricao, @Preco, @Quantidade)", conexao);

                cmd.Parameters.AddWithValue("@Nome", produto.Nome);
                cmd.Parameters.AddWithValue("@Descricao", produto.descricao);
                cmd.Parameters.AddWithValue("@Preco", produto.preco);
                cmd.Parameters.AddWithValue("@Quantidade", produto.quantidade);
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        // Método para listar todos os produtos do banco de dados
        public IEnumerable<Produto> TodosProdutos()
        {
            // Cria uma nova lista para armazenar os objetos produto
            List<Produto> Produtolist = new List<Produto>();

            // Bloco using para garantir que a conexão seja fechada e os recursos liberados após o uso
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                // Abre a conexão com o banco de dados MySQL
                conexao.Open();
                // Cria um novo comando SQL para selecionar todos os registros da tabela 'produto'
                MySqlCommand cmd = new MySqlCommand("SELECT * from Produtos", conexao);

                // Cria um adaptador de dados para preencher um DataTable com os resultados da consulta
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                // Cria um novo DataTable
                DataTable dt = new DataTable();
                // metodo fill- Preenche o DataTable com os dados retornados pela consulta
                da.Fill(dt);
                // Fecha explicitamente a conexão com o banco de dados 
                conexao.Close();

                // interage sobre cada linha (DataRow) do DataTable
                foreach (DataRow dr in dt.Rows)
                {
                    // Cria um novo objeto produto e preenche suas propriedades com os valores da linha atual
                    Produtolist.Add(
                                new Produto
                                {
                                    Id = Convert.ToInt32(dr["IdProd"]), // Converte o valor da coluna "codigo" para inteiro
                                    Nome = ((string)dr["Nome"]), // Converte o valor da coluna "nome" para string
                                    preco = Convert.ToInt32(dr["Preco"]),
                                    quantidade = Convert.ToInt32(dr["Quantidade"]),// Converte o valor da coluna "telefone" para string
                                    descricao = ((string)dr["Descricao"]), // Converte o valor da coluna "email" para string
                                });
                }
                // Retorna a lista de todos os produtos
                return Produtolist;
            }
        }

        // Método para buscar um produto específico pelo seu código (Codigo)
        public Produto ObterProduto(int Id)
        {
            // Bloco using para garantir que a conexão seja fechada e os recursos liberados após o uso
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                // Abre a conexão com o banco de dados MySQL
                conexao.Open();
                // Cria um novo comando SQL para selecionar um registro da tabela 'produto' com base no código
                MySqlCommand cmd = new MySqlCommand("SELECT * from Produtos where IdProd=@Id ", conexao);

                // Adiciona um parâmetro para o código a ser buscado, definindo seu tipo e valor
                cmd.Parameters.AddWithValue("@Id", Id);

                // Cria um adaptador de dados (não utilizado diretamente para ExecuteReader)
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                // Declara um leitor de dados do MySQL
                MySqlDataReader dr;
                // Cria um novo objeto produto para armazenar os resultados
                Produto produto = new Produto();

                /* Executa o comando SQL e retorna um objeto MySqlDataReader para ler os resultados
                CommandBehavior.CloseConnection garante que a conexão seja fechada quando o DataReader for fechado*/

                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                // Lê os resultados linha por linha
                while (dr.Read())
                {
                    // Preenche as propriedades do objeto produto com os valores da linha atual
                    produto.Id = Convert.ToInt32(dr["IdProd"]);//propriedade Codigo e convertendo para int
                    produto.Nome = (string)(dr["Nome"]); // propriedade Nome e passando string
                    produto.preco = Convert.ToInt32(dr["Preco"]);
                    produto.quantidade = Convert.ToInt32(dr["Quantidade"]);//propriedade telefone e passando string
                    produto.descricao = (string)(dr["Descricao"]); //propriedade email e passando string
                }
                // Retorna o objeto produto encontrado (ou um objeto com valores padrão se não encontrado)
                return produto;
            }
        }


    }
}
