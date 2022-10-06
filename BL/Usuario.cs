using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace BL
{
    public class Usuario
    {
        public static ML.Result Getall()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.LPerezEKSEntities conexion = new DL.LPerezEKSEntities())
                {
                    var query = conexion.UsuarioGetall().ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.IdUsuario = obj.IdUsuario;
                            usuario.Nombre= obj.Nombre;
                            usuario.ApellidoPaterno = obj.ApellidoPaterno;
                            usuario.ApellidoMaterno = obj.ApellidoMaterno;
                            usuario.Email= obj.Email;
                            usuario.Password = obj.Password;
                            usuario.UserName = obj.UserName;
                            usuario.Sexo = obj.Sexo;
                            usuario.Telefono = obj.Telefono;
                            result.Objects.Add(usuario);
                           
                           
                        }
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
                result.ErrorMessage= ex.Message;
            }
            return result;
        }
        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.LPerezEKSEntities context = new DL.LPerezEKSEntities())
                {
                    var query = context.UsuarioAdd(usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.Email, usuario.Password, usuario.UserName, usuario.Sexo, usuario.Telefono);
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
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.LPerezEKSEntities context = new DL.LPerezEKSEntities())
                {
                    var query = context.UsuarioUpdate(usuario.IdUsuario, usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.Email, usuario.Password, usuario.UserName, usuario.Sexo, usuario.Telefono);
                    if(query >= 1)
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
                result.ErrorMessage= ex.Message;
            }
            return result;
        }
        public static ML.Result GetById(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.LPerezEKSEntities context = new DL.LPerezEKSEntities())
                {
                    var query = context.UsuarioGetById(IdUsuario).FirstOrDefault();
                    if (query != null)
                    {
                        ML.Usuario usuario= new ML.Usuario();
                        usuario.IdUsuario = query.IdUsuario;
                        usuario.Nombre= query.Nombre;
                        usuario.ApellidoPaterno= query.ApellidoPaterno;
                        usuario.ApellidoMaterno = query.ApellidoMaterno; 
                        usuario.Email= query.Email;
                        usuario.Password= query.Password;
                        usuario.UserName= query.UserName;
                        usuario.Sexo= query.Sexo;
                        usuario.Telefono= query.Telefono;
                        result.Object = usuario;
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
                result.ErrorMessage = ex.Message;

                
            }
            return result;
        }
        public static ML.Result Delete (int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.LPerezEKSEntities context = new DL.LPerezEKSEntities())
                {
                    var query= context.UsuarioDelete(IdUsuario);
                    if(query>= 1)
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
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result GetByEmail(string Email)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.LPerezEKSEntities context = new DL.LPerezEKSEntities())
                {
                    var query= context.GetByEmail(Email).AsEnumerable().FirstOrDefault();
                    if(query!= null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = query.IdUsuario;
                        usuario.Nombre= query.Nombre;
                        usuario.ApellidoPaterno = query.ApellidoPaterno;
                        usuario.ApellidoMaterno = query.ApellidoMaterno;
                        usuario.Email = query.Email;
                        usuario.Password = query.Password;
                        usuario.Sexo = query.Sexo;
                        usuario.Telefono = query.Telefono;
                        result.Object = usuario;
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
                result.ErrorMessage= ex.Message;
            }
            return result;
        }
    }
}
