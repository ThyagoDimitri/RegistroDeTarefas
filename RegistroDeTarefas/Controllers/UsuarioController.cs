using Microsoft.AspNetCore.Mvc;
using RegistroDeTarefas.Data;
using RegistroDeTarefas.Models;

namespace RegistroDeTarefas.Controllers
{
    public class UsuarioController : Controller
    {
        private UsuarioDAL usuarioDAL = new UsuarioDAL();

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string login, string senha)
        {
            if (usuarioDAL.VerificarLogin(login, senha))
            {
                int userId = usuarioDAL.ObterUsuarioId(login, senha);
                HttpContext.Session.SetInt32("UsuarioId", userId);

                // Obter o tipo de usuário e armazenar na sessão
                string tipoUsuario = usuarioDAL.ObterTipoUsuario(login, senha);
                HttpContext.Session.SetString("TipoUsuario", tipoUsuario);

                return RedirectToAction("Index", "Tarefas");
            }
            ViewBag.Message = "Login ou senha incorretos";
            return View();
        }

        [HttpGet]
        public IActionResult Registrar() => View();

        [HttpPost]
        public IActionResult Registrar(string nome, string login, string senha)
        {
            usuarioDAL.RegistrarUsuario(new Users { Nome = nome, LoginUsers = login, Password = senha });
            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
