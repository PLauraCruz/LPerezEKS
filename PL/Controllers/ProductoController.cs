using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        [HttpGet]
        public ActionResult Getall()
        {
            ML.Producto producto = new ML.Producto();
            ML.Result resultApi = new ML.Result();
            //ML.Result result = BL.Producto.Getall();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54266/api/");
                var responseTask = client.GetAsync("Producto");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var redTask = result.Content.ReadAsAsync<ML.Result>();
                    redTask.Wait();
                    resultApi.Objects = new List<object>();
                    foreach (var resultItem in redTask.Result.Objects)
                    {
                        ML.Producto resultProducto = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Producto>(resultItem.ToString());
                        resultApi.Objects.Add(resultProducto);
                    }
                }
            }
            producto.Productos = resultApi.Objects;
            return View(producto);
        }
        [HttpGet]
        public ActionResult Form(int? IdProducto)
        {
            ML.Producto producto = new ML.Producto();
            if(IdProducto== null)
            {
                return View(producto);
            }
            else
            {
                //ML.Result result = BL.Producto.GetById(IdProducto.Value);
                //if (result.Correct) 
                //{ 
                //    producto=(ML.Producto)result.Object; 
                //    return View(producto);
                //}
                //else
                //{


                //}
                ML.Result resultApi = new ML.Result();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:54266/api/");
                    var responseTask = client.GetAsync("Producto/?IdProducto=" + IdProducto);
                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();
                        resultApi.Object = new List<object>();
                        var resultItem = readTask.Result.Object;
                        ML.Producto resultProducto = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Producto>(resultItem.ToString());
                        resultApi.Object = resultProducto;
                    }
                }
                producto= (ML.Producto)resultApi.Object;
            }
            return View(producto);

        }
        [HttpPost]
        public ActionResult Form (ML.Producto producto)
        {
            HttpPostedFileBase file = Request.Files["ImagenData"];
            if (file.ContentLength > 0)
            {
                producto.Imagen = ConvertToBytes(file);
            }
            if (producto.IdProducto== 0)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:54266/api/");
                    var resposeTask = client.PostAsJsonAsync<ML.Producto>("Producto", producto);
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
                //ML.Result result= BL.Producto.Add(producto);
                //if (result.Correct)
                //{
                //    ViewBag.Mensaje = "Se registro correctamente";
                //}
                //else 
                //{
                //    ViewBag.Mensaje = "Ocurrio un error al registrarlo";
                //}
            }
            else
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:54266/api/");
                    var resposeTask = client.PutAsJsonAsync<ML.Producto>("Producto", producto);
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
                    //ML.Result result = BL.Producto.Update(producto);
                    //if (result.Correct)
                    //{
                    //    ViewBag.Mensaje = "Se actualizo correctamente";
                    //}
                    //else
                    //{
                    //    ViewBag.Mensaje = "Ocurrio un error al momento de actualizar";
                    //}

                }
            }
        }

        [HttpGet]
        public ActionResult Delete(int IdProducto)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54266/api/");
                var resposeTask = client.DeleteAsync("Producto/?IdProducto=" + IdProducto);
                resposeTask.Wait();
                var resultService = resposeTask.Result;
                if (resultService.IsSuccessStatusCode)
                {
                    ViewBag.Mensaje = "Producto eliminado exitosamente";
                    return View("Modal");
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un error al eliminar el producto";
                    return View("Modal");
                }
                //ML.Result result = BL.Producto.Delete(IdProducto);
                //if (result.Correct)
                //{
                //    ViewBag.Mensaje = "Se elimino correctamente";

                //}
                //else
                //{
                //    ViewBag.Mensaje = "Ocurrio un error al momento de eliminar";
                //}
                //return View("Modal");
            }
        }

        public  byte[] ConvertToBytes(HttpPostedFileBase imagen)
        {
            byte[] data = null;
            System.IO.BinaryReader reader= new System.IO.BinaryReader(imagen.InputStream);
            data = reader.ReadBytes((int)imagen.ContentLength);
            return data;
        }
    }
}