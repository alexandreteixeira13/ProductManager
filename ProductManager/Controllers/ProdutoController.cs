using Microsoft.AspNetCore.Mvc;
using ProductManager.Models;
using ProductManager.Repositorio;

namespace ProductManager.Controllers
{
    public class ProdutoController : Controller
    {


        private readonly ProdutoRepositorio _produtoRepositorio;


        public ProdutoController(ProdutoRepositorio produtoRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
        }

        public IActionResult Index()
        {
            return View(_produtoRepositorio.TodosProdutos());
        }

        public IActionResult CadastrarProduto()
        {
            return View();
        }

        [HttpPost]

        public IActionResult CadastrarProduto(Produto produto)
        {
            if (ModelState.IsValid)
            {
                _produtoRepositorio.AdicionarProduto(produto);
                return RedirectToAction("CadastrarProduto");
            }
            return View(produto);
        }
    }
}
