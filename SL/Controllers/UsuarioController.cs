using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL.Controllers
{
    public class UsuarioController : ApiController
    {
        [Route ("api/Usuario/")]
        [HttpPost]
        public IHttpActionResult Add([FromBody]ML.Usuario usuario)
        {
            var result = BL.Usuario.Add(usuario);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }
        [Route("api/Usuario/")]
        [HttpPut]
        public IHttpActionResult Update([FromBody]ML.Usuario usuario)
        {
            var result = BL.Usuario.Update(usuario);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }
        [Route("api/Usuario/")]
        [HttpDelete]
        public IHttpActionResult Delete(int IdUsuario)
        {
            var result= BL.Usuario.Delete(IdUsuario);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }
        [Route("api/Usuario/")]
        [HttpGet]
        public IHttpActionResult Getall()
        {
            var result = BL.Usuario.Getall();
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }
        [Route("api/Usuario/")]
        [HttpGet]
        public IHttpActionResult GetById(int IdUsuario)
        {
            var result= BL.Usuario.GetById(IdUsuario);
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
