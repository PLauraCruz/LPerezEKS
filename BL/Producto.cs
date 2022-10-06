using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Producto
    {
        public static ML.Result Getall()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.LPerezEKSEntities context = new DL.LPerezEKSEntities())
                {
                    var query = context.ProductoGetall().ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Producto producto = new ML.Producto();
                            producto.IdProducto = obj.IdProducto;
                            producto.Nombre = obj.Nombre;
                            producto.Precio = (decimal)obj.Precio;
                            producto.Descripcion = obj.Descripcion;
                            producto.Imagen = obj.Imagen;
                            result.Objects.Add(producto);

                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result Add(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.LPerezEKSEntities context = new DL.LPerezEKSEntities())
                {
                    var query = context.ProductoAdd(producto.Nombre, producto.Precio, producto.Descripcion, producto.Imagen);
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage=ex.Message;
            }
            return result;
        }
        public static ML.Result Update(ML.Producto producto)
        {
            ML.Result resul = new ML.Result();
            try
            {
                using (DL.LPerezEKSEntities context = new DL.LPerezEKSEntities())
                {
                    var query = context.ProductoUpdate(producto.IdProducto, producto.Nombre, producto.Precio, producto.Descripcion, producto.Imagen);
                    if (query >= 1)
                    {
                        resul.Correct = true;
                    }
                    else
                    {
                        resul.Correct = false;
                        resul.ErrorMessage = "Hubo un error al modificar los datos";
                    }
                }
            }
            catch (Exception ex)
            {
                resul.Correct = false;
                resul.ErrorMessage = ex.Message;
            }
            return resul;
        }
        public static ML.Result GetById(int IdProducto)
        {
            ML.Result resul = new ML.Result();
            try
            {
                using (DL.LPerezEKSEntities context = new DL.LPerezEKSEntities())
                {
                    var query = context.ProductoGetById(IdProducto).FirstOrDefault();

                    if (query != null)
                    {
                        ML.Producto producto = new ML.Producto();
                        producto.IdProducto = query.IdProducto;
                        producto.Nombre = query.Nombre;
                        producto.Precio = (decimal)query.Precio;
                        producto.Descripcion = query.Descripcion;
                        producto.Imagen = query.Imagen;
                        resul.Object = producto;
                        resul.Correct = true;

                    }
                    else
                    {
                        resul.Correct = false;


                    }
                }
            }
            catch (Exception ex)
            {
                resul.Correct = false;
                resul.ErrorMessage = ex.Message;
            }
            return resul;
        }
        public static ML.Result Delete(int IdProducto)
        {
            ML.Result resul = new ML.Result();
            try
            {
                using (DL.LPerezEKSEntities context = new DL.LPerezEKSEntities())
                {
                    var query = context.ProductoDelete(IdProducto);
                    if (query >= 1)
                    {
                        resul.Correct = true;
                    }
                    else
                    {
                        resul.Correct = false;

                    }
                }
            }
            catch (Exception ex)
            {
                resul.Correct = false;
                resul.ErrorMessage = ex.Message;
            }
            return resul;
        }


    }
}
