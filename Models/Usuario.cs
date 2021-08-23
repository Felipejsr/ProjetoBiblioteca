public enum AUTH_TYPE { ADMIN = 1, DEFAULT = 2 }

namespace Biblioteca.Models
{
    public class Usuario
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public AUTH_TYPE Tipo { get; set; }

    }
}