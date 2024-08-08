using Microsoft.Data.SqlClient;
using RegistroDeTarefas.Models;

namespace RegistroDeTarefas.Data
{
    public class UsuarioDAL
    {
        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Usuarios;Integrated Security=True;";

        public void RegistrarUsuario(Users user)
        {
            using var connection = new SqlConnection(connectionString);
            string sql = "INSERT INTO Usuario (Nome, ULogin, Senha) VALUES (@nome, @uLogin, @senha)";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@nome", user.Nome);
            command.Parameters.AddWithValue("@uLogin", user.LoginUsers);
            command.Parameters.AddWithValue("@senha", user.Password);
            connection.Open();
            command.ExecuteNonQuery();
        }

        public bool VerificarLogin(string login, string senha)
        {
            using var connection = new SqlConnection(connectionString);
            string sql = "SELECT COUNT(*) FROM Usuario WHERE ULogin = @uLogin AND Senha = @senha";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@uLogin", login);
            command.Parameters.AddWithValue("@senha", senha);
            connection.Open();
            int count = (int)command.ExecuteScalar();
            return count > 0;
        }

        public int ObterUsuarioId(string login, string senha)
        {
            using var connection = new SqlConnection(connectionString);
            string sql = "SELECT IdUsuario FROM Usuario WHERE ULogin = @uLogin AND Senha = @senha";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@uLogin", login);
            command.Parameters.AddWithValue("@senha", senha);
            connection.Open();
            var result = command.ExecuteScalar();
            return result != null ? Convert.ToInt32(result) : -1;
        }

        public string ObterTipoUsuario(string login, string senha)
        {
            using var connection = new SqlConnection(connectionString);
            string sql = "SELECT TipoUsuario FROM Usuario WHERE ULogin = @uLogin AND Senha = @senha";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@uLogin", login);
            command.Parameters.AddWithValue("@senha", senha);
            connection.Open();
            var result = command.ExecuteScalar();
            return result?.ToString();
        }
    }
}
