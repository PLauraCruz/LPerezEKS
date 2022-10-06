using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL.Controllers
{
    public class ProductoController : ApiController
    {
        [Route("api/Producto/")]
        [HttpPost]
        public IHttpActionResult Add([FromBody]ML.Producto producto)
        {
            var result= BL.Producto.Add(producto);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }
        [Route("api/Producto/")]
        [HttpPut]
        public IHttpActionResult Update([FromBody]ML.Producto producto)
        {
            var result = BL.Producto.Update(producto);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }
        [Route("api/Producto/")]
        [HttpDelete]
        public IHttpActionResult Delete (int IdProducto)
        {
            var result = BL.Producto.Delete(IdProducto);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }
        [Route("api/Producto/")]
        [HttpGet]
        public IHttpActionResult Getall()
        {
            var result = BL.Producto.Getall();
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }
        [Route("api/Producto/")]
        [HttpGet]
        public IHttpActionResult GetById(int IdProducto)
        {
            var result = BL.Producto.GetById(IdProducto);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }
    }
}
