namespace ProductManager.Models
{
    public class Produto
    {

        public int Id { get; set; }

        public string Nome { get; set; }
         
        public string descricao { get; set; }

        public int preco { get; set; }

        public int quantidade { get; set; }

        public List<Produto>? produtos { get; set;}

    }   
}
