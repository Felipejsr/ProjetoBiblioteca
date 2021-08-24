using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Biblioteca.Models;
using Microsoft.AspNetCore.Http;

namespace Biblioteca.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(ILogger<UsuarioController> logger)
        {
            _logger = logger;
        }
        public IActionResult AcessoAdm()
        {
            return View();
        }
        public IActionResult CadastrarUsuario()
        {
            Autenticacao.CheckLogin(this, true);
            return View();
        }
        [HttpPost]
        public IActionResult CadastrarUsuario(Usuario user)
        {
            Autenticacao.CheckLogin(this, true);

            Autenticacao.UsuarioService.Inserir(user);
            return RedirectToAction("ListaUsuarios");
        }
        public IActionResult EditarUsuario(int id)
        {
            Autenticacao.CheckLogin(this, true);
            Usuario u = Autenticacao.UsuarioService.Listar(id);

            return View(u);
        }
        [HttpPost]
        public IActionResult EditarUsuario(Usuario user)
        {
            Autenticacao.CheckLogin(this, true);
            Usuario us = Autenticacao.UsuarioService.Listar(user.ID);
            Autenticacao.UsuarioService.AtualizaUsuario(user);

            return RedirectToAction("ListaUsuarios");
        }
        public IActionResult ExcluirUsuario(int id)
        {
            Autenticacao.CheckLogin(this, true);
            Usuario u = Autenticacao.UsuarioService.Listar(id);

            return View(u);
        }
        [HttpPost]
        public IActionResult ExcluirUsuario(Usuario user)
        {
            Autenticacao.CheckLogin(this, true);
            Autenticacao.UsuarioService.ExcluiUsuario(user.ID);

            return RedirectToAction("ListaUsuarios");
        }
        public IActionResult ListaUsuarios()
        {
            Autenticacao.CheckLogin(this, true);
            return View(Autenticacao.UsuarioService.Listar());
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
