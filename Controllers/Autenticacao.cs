using System.Collections.Generic;
using System.Linq;
using Biblioteca.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Biblioteca.Controllers
{
    public class Autenticacao
    {
        private static UsuarioService us;
        public static UsuarioService UsuarioService
        {
            get
            {
                if (us == null) us = new UsuarioService();
                return us;
            }
        }
        public static Usuario UsuarioLogado = null;
        public static void CheckLogin(Controller controller, bool checkAdmin = false)
        {
            if(checkAdmin & controller.HttpContext.Session.GetInt32("tipo") != 1){
                controller.Request.HttpContext.Response.Redirect("/Usuario/AcessoAdm");
            }
            else if (string.IsNullOrEmpty(controller.HttpContext.Session.GetString("user")))
            {
                controller.Request.HttpContext.Response.Redirect("/Home/Login");
            }
        }

        public static bool ValidateLogin(string login, string senha, Controller controller)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                VerificaAdmin(bc);

                senha = Criptografo.TextoCriptografado(senha);

                IQueryable<Usuario> Usuario = bc.Usuarios.Where(u => u.Login == login && u.Senha == senha);
                List<Usuario> ListaUsuario = Usuario.ToList();

                if (ListaUsuario.Count == 0) return false;

                controller.HttpContext.Session.SetString("user", ListaUsuario[0].Login);
                controller.HttpContext.Session.SetString("Nome", ListaUsuario[0].Nome);
                controller.HttpContext.Session.SetInt32("tipo", ((int)ListaUsuario[0].Tipo));

                UsuarioLogado = ListaUsuario[0];
                return true;
            }
        }

        public static void VerificaAdmin(BibliotecaContext bc){
            IQueryable<Usuario> user = bc.Usuarios.Where(u => u.Login=="admin");

            if(user.ToList().Count==0){
                Usuario admin = new Usuario();
                admin.Login = "admin";
                admin.Senha = Criptografo.TextoCriptografado("admin");
                admin.Tipo = AUTH_TYPE.ADMIN;
                admin.Nome = "Administrador";

                bc.Usuarios.Add(admin);
                bc.SaveChanges();
            }
        }
    }
}