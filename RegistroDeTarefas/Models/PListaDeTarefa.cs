namespace RegistroDeTarefas.Models
{
    public class PListaDeTarefa
    {
        public int Id { get; set; }
        public string NomeTarefa { get; set; }
        public string ImportanciaDaTarefa { get; set; }
        public int UsuarioId { get; set; }
    }
}
