using Microsoft.AspNetCore.Mvc;
using ProductManager.Models;
using ProductManager.Repositorio;

namespace ProductManager.Controllers
{
    public class UsuarioController : Controller
    {

        private readonly UsuarioRepositorio _usuarioRepositorio;


        public UsuarioController(UsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string senha)
        {
            var usuario = _usuarioRepositorio.ObterUsuario(email);

            if (usuario != null && usuario.Senha == senha)
            {
                return RedirectToAction("Index", "Produto");
            }

            ModelState.AddModelError("", "Email ou senha inválidos");
            return View();
        }

        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(Usuario usario)
        {
            if(ModelState.IsValid)
            {
                _usuarioRepositorio.AdicionarUsuario(usario);

                return RedirectToAction("Login");
            }

            return View();
        }

        public IActionResult Contato()
        {
            return View();
        }
    }
}
