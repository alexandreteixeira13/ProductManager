using MySql.Data.MySqlClient;
using ProductManager.Models;
using System.Data;

namespace ProductManager.Repositorio
{
    public class ProdutoRepositorio(IConfiguration configuration)
    {
        private readonly string _conexaoMySql = configuration.GetConnectionString("ConexaoMySQL");

        public void AdicionarProduto(Produto produto)
        {

            using (var conexao = new MySqlConnection(_conexaoMySql))
            {
                conexao.Open();

                MySqlCommand cmd = new("INSERT INTO PRODUTOS (Nome, Descricao, Preco, Quantidade) values (@Nome, @Descricao, @Preco, @Quantidade)", conexao);

                cmd.Parameters.AddWithValue("@Nome", produto.Nome);
                cmd.Parameters.AddWithValue("@Descricao", produto.descricao);
                cmd.Parameters.AddWithValue("@Preco", produto.preco);
                cmd.Parameters.AddWithValue("@Quantidade", produto.quantidade);
            }
        }


    }
}
