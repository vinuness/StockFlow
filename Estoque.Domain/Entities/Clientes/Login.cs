namespace Estoque.Domain.Entities.Clientes
{
    public class Login
    {
        public string Email {  get; set; }
        public string Senha { get; set; }
    }

    public class LoginResponse  
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }
        public string Token { get; set; }
    }
}
