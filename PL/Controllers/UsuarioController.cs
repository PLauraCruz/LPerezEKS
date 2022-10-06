using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        [HttpGet]
        public ActionResult Getall()
        {
            ML.Usuario usuario = new ML.Usuario();
            //ML.Result result = BL.Usuario.Getall();
            //if (result.Correct)
            //{
            //    usuario.Usuarios = result.Objects;
            //}
            //else
            //{

            //}
            //return View(usuario);
            ML.Result resultApi = new ML.Result();
            
            using (var client = new HttpClient())
            {
                if (Session["Token"] != null)
                {
                    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["Token"].ToString());

                    client.BaseAddress = new Uri("http://localhost:54266/api/");
                    var responseTask = client.GetAsync("Usuario");
                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var redTask = result.Content.ReadAsAsync<ML.Result>();
                        redTask.Wait();
                        resultApi.Objects = new List<object>();
                        foreach (var resultItem in redTask.Result.Objects)
                        {
                            ML.Usuario resultUsuario = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(resultItem.ToString());
                            resultApi.Objects.Add(resultUsuario);
                        }
                    }
                }
                else
                {
                    
                ViewBag.Mensaje = "No tiene permisos para ver esta información";
                return PartialView("Modal");
          
                }
            }
            usuario.Usuarios = resultApi.Objects;
            return View(usuario);
        }
        [HttpGet]
        public ActionResult Form(int? IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            if (IdUsuario == null)
            {
                return View(usuario);
            }
            else
            {
                ML.Result resultApi = new ML.Result();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:54266/api/");
                    var responseTask = client.GetAsync("Usuario/?IdUsuario=" + IdUsuario);
                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();
                        resultApi.Object = new List<object>();
                        var resultItem = readTask.Result.Object;
                        ML.Usuario resultUsuario = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(resultItem.ToString());
                        resultApi.Object = resultUsuario;
                    }
                }
                usuario = (ML.Usuario)resultApi.Object;
            
            //ML.Result result = BL.Usuario.GetById(IdUsuario.Value);
            //if (result.Correct)
            //{
            //    usuario=(ML.Usuario)result.Object;
            //    return View(usuario);
            //}
        }
            return View(usuario);

        }
        [HttpPost]
        public ActionResult Form (ML.Usuario usuario)
        {
            if (usuario.IdUsuario == 0)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:54266/api/");
                    var resposeTask = client.PostAsJsonAsync<ML.Usuario>("Usuario", usuario);
                    resposeTask.Wait();
                    var resultService = resposeTask.Result;
                    if (resultService.IsSuccessStatusCode)
                    {
                        ViewBag.Mensaje = "Registro exitoso";
                        return View("Modal");
                    }
                    else
                    {
                        ViewBag.Mensaje = "Ocurrio un error";
                        return View("Modal");
                    }
                }
            }
            //    ML.Result result = BL.Usuario.Add(usuario);
            //    if (result.Correct)
            //    {
            //        ViewBag.Mensaje = "El registro fue exitoso";
            //    }
            //    else
            //    {
            //        ViewBag.Mensaje = "Ocurrio un error al registrar";
            //    }
            //}
            else
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:54266/api/");
                    var resposeTask = client.PutAsJsonAsync<ML.Usuario>("Usuario", usuario);
                    resposeTask.Wait();
                    var resultService = resposeTask.Result;
                    if (resultService.IsSuccessStatusCode)
                    {
                        ViewBag.Mensaje = "Modificacion exitosa";
                        return View("Modal");
                    }
                    else
                    {
                        ViewBag.Mensaje = "Ocurrio un error al modificar los datos";
                        return View("Modal");
                    }
                    //    ML.Result result= BL.Usuario.Update(usuario);
                    //    if (result.Correct)
                    //    {
                    //        ViewBag.Mensaje = "La modificación fue exitosa";
                    //    }
                    //    else
                    //    {
                    //        ViewBag.Mensaje = "Ocurrio un error al modificar los datos";
                    //    }
                    //}
                    //return View("Modal");
                }
            }
        }
        [HttpGet]
        public ActionResult Delete(int IdUsuario)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54266/api/");
                var resposeTask = client.DeleteAsync("Usuario/?IdUsuario=" + IdUsuario);
                resposeTask.Wait();
                var resultService = resposeTask.Result;
                if (resultService.IsSuccessStatusCode)
                {
                    ViewBag.Mensaje = "Usuario eliminado exitosamente";
                    return View("Modal");
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un error al eliminar el usuario";
                    return View("Modal");
                }

                //ML.Result result = BL.Usuario.Delete(IdUsuario);
                //if (result.Correct)
                //{
                //    ViewBag.Mensaje = "El usuario se elimino correctamente";
                //}
                //else
                //{
                //    ViewBag.Mensaje = "Ocurrio un error al eliminar";
                //}
                //return View("Modal");
            }
        }
    }
}