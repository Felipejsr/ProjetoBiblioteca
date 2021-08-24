using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace Biblioteca.Models
{
    public class UsuarioService
    {
        public void Inserir(Usuario newUser)
        {
            newUser.Senha = Criptografo.TextoCriptografado(newUser.Senha);
            using (BibliotecaContext bc = new BibliotecaContext()){
                bc.Add(newUser);
                bc.SaveChanges();
            }
        }
        public List<Usuario> Listar()
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Usuarios.ToList();
            }
        }
        public Usuario Listar(int id)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Usuarios.Find(id);
            }
        }
        public Usuario BuscaUsuario(string login){
            using (BibliotecaContext bc = new BibliotecaContext()){
                return bc.Usuarios.Find(login);
            }
        }
        public void AtualizaUsuario(Usuario user){
            using (BibliotecaContext bc = new BibliotecaContext()){
                Usuario u = bc.Usuarios.Find(user.ID);
                u.Login = user.Login;
                u.Nome = user.Login;
                u.Senha = Criptografo.TextoCriptografado(user.Senha);
                u.Tipo = user.Tipo;

                bc.SaveChanges();
            }
        }
        public void ExcluiUsuario(int id){
            using (BibliotecaContext bc = new BibliotecaContext()){
                bc.Usuarios.Remove(bc.Usuarios.Find(id));
                bc.SaveChanges();
            }
        }

    }
}