using Microsoft.Data.SqlClient;
using RegistroDeTarefas.Models;

namespace RegistroDeTarefas.Data
{
    public class ListaDeTarefasDAL
    {
        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Usuarios;Integrated Security=True;";

        public IEnumerable<PListaDeTarefa> Listar(int usuarioId, string tipoUsuario)
        {
            var lista = new List<PListaDeTarefa>();
            using var connection = new SqlConnection(connectionString);
            string sql;

            if (tipoUsuario == "admin")
            {
                // Administrador vê todas as tarefas
                sql = "SELECT * FROM ListaDeTarefasDeCadaUsuario";
            }
            else
            {
                // Usuário comum vê apenas suas tarefas
                sql = "SELECT * FROM ListaDeTarefasDeCadaUsuario WHERE UsuarioId = @usuarioId";
            }

            using var command = new SqlCommand(sql, connection);

            if (tipoUsuario != "admin")
            {
                command.Parameters.AddWithValue("@usuarioId", usuarioId);
            }

            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new PListaDeTarefa
                {
                    Id = reader.IsDBNull(reader.GetOrdinal("IdTarefa")) ? 0 : reader.GetInt32(reader.GetOrdinal("IdTarefa")),
                    NomeTarefa = reader.IsDBNull(reader.GetOrdinal("Nome")) ? string.Empty : reader.GetString(reader.GetOrdinal("Nome")),
                    ImportanciaDaTarefa = reader.IsDBNull(reader.GetOrdinal("ImportanciaTarefa")) ? string.Empty : reader.GetString(reader.GetOrdinal("ImportanciaTarefa")),
                    UsuarioId = reader.IsDBNull(reader.GetOrdinal("UsuarioId")) ? 0 : reader.GetInt32(reader.GetOrdinal("UsuarioId"))
                });
            }
            return lista;
        }

        public void Adicionar(PListaDeTarefa tarefa)
        {
            using var connection = new SqlConnection(connectionString);
            string sql = "INSERT INTO ListaDeTarefasDeCadaUsuario (Nome, ImportanciaTarefa, UsuarioId) VALUES (@nome, @importanciaTarefa, @usuarioId)";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@nome", tarefa.NomeTarefa);
            command.Parameters.AddWithValue("@importanciaTarefa", tarefa.ImportanciaDaTarefa);
            command.Parameters.AddWithValue("@usuarioId", tarefa.UsuarioId);
            connection.Open();
            command.ExecuteNonQuery();
        }

        public void Remover(int tarefaId, int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            string sql = "DELETE FROM ListaDeTarefasDeCadaUsuario WHERE IdTarefa = @Id AND UsuarioId = @UsuarioId";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Id", tarefaId);
            command.Parameters.AddWithValue("@UsuarioId", usuarioId);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
