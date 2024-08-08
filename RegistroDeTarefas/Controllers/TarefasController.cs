using Microsoft.AspNetCore.Mvc;
using RegistroDeTarefas.Data;
using RegistroDeTarefas.Models;

namespace RegistroDeTarefas.Controllers
{
    public class TarefasController : Controller
    {
        private ListaDeTarefasDAL tarefaDAL = new ListaDeTarefasDAL();

        public IActionResult Index()
        {
            int? userId = HttpContext.Session.GetInt32("UsuarioId");
            if (userId == null) return RedirectToAction("Login", "Usuario");

            string tipoUsuario = HttpContext.Session.GetString("TipoUsuario");

            var tarefas = tarefaDAL.Listar(userId.Value, tipoUsuario);
            return View(tarefas);
        }

        [HttpPost]
        public IActionResult Adicionar(string nome, string importancia)
        {
            int? userId = HttpContext.Session.GetInt32("UsuarioId");
            if (userId == null) return RedirectToAction("Login", "Usuario");

            tarefaDAL.Adicionar(new PListaDeTarefa { NomeTarefa = nome, ImportanciaDaTarefa = importancia, UsuarioId = userId.Value });
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Remover(int id)
        {
            int? userId = HttpContext.Session.GetInt32("UsuarioId");
            if (userId == null) return RedirectToAction("Login", "Usuario");

            tarefaDAL.Remover(id, userId.Value);
            return RedirectToAction("Index");
        }
    }
}
