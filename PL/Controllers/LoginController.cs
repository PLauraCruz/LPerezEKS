using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            ML.Usuario usuario = new ML.Usuario();
            return View(usuario);
        }
        [HttpGet]
        public ActionResult LoginOut()
        {
            Session.Clear();
            return RedirectToAction("Login", "Login");
        }
        [HttpPost]
        public ActionResult Login(ML.Usuario usuario)
        {
            using (var client = new HttpClient())
            {
                string uriApi = ConfigurationManager.AppSettings["WebApi"].ToString();
                client.BaseAddress = new Uri(uriApi);

                var responseTask = client.PostAsJsonAsync("login/authenticate", usuario);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<string>();
                    readTask.Wait();
                    Session["Token"] = readTask.Result;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Mensaje = "Usuario y/o Contraseña Incorrectos";
                    return PartialView("Modal");
                }
            }
            // GET: Login
            //[HttpGet]
            //public ActionResult Login ()
            //{
            //    ML.Usuario usuario = new ML.Usuario ();
            //    return View(usuario);
            //}
            //[HttpPost]
            //public ActionResult Login (ML.Usuario usuarioLogin)
            //{
            //    ML.Result result = BL.Usuario.GetByEmail(usuarioLogin.Email);
            //    if (result.Correct)
            //    {
            //        ML.Usuario usuario = (ML.Usuario)result.Object;
            //        if(usuarioLogin.Password== usuario.Password)
            //        {
            //            return RedirectToAction("Index", "Home");
            //        }
            //        else
            //        {
            //            ViewBag.Mensaje="Credenciales invalidas";
            //            return ViewBag("Modal");
            //        }
            //    }
            //    else
            //    {
            //        ViewBag.Mensaje = "El email no esta registrado verifique";
            //        return ViewBag("Modal");

            //    }
            //}
        }
    }
}