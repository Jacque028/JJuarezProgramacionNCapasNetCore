using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {

        // ADD 

        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JjuarezProgramacionNcapasContext context = new DL.JjuarezProgramacionNcapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}', '{usuario.FechaNacimiento}', '{usuario.UserName}', '{usuario.Password}', '{usuario.Sexo}', '{usuario.Telefono}', '{usuario.Celular}', '{usuario.Curp}', '{usuario.Email}', {usuario.Rol.IdRol}, '{usuario.Imagen}' , '{usuario.Direccion.Calle}', '{usuario.Direccion.NumeroInterior}', '{usuario.Direccion.NumeroExterior}',{usuario.Direccion.Colonia.IdColonia}");
                   
                    if (query > 0) 
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

        // UPDATE  
        public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JjuarezProgramacionNcapasContext context = new DL.JjuarezProgramacionNcapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UsuarioUpdate {usuario.IdUsuario}, '{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}', '{usuario.FechaNacimiento}', '{usuario.UserName}', '{usuario.Password}', '{usuario.Sexo}', '{usuario.Telefono}', '{usuario.Celular}', '{usuario.Curp}', '{usuario.Email}', {usuario.Rol.IdRol}, '{usuario.Imagen}', '{usuario.Direccion.Calle}', '{usuario.Direccion.NumeroInterior}', '{usuario.Direccion.NumeroExterior}', {usuario.Direccion.Colonia.IdColonia}");
                    //var query = context.Database.ExecuteSqlRaw($"UsuarioUpdate @IdUsuario={usuario.IdUsuario}, @Nombre={usuario.Nombre}, @ApellidoPaterno={usuario.ApellidoPaterno}, @ApellidoMaterno={usuario.ApellidoMaterno}, @FechaNacimiento={usuario.FechaNacimiento}, @UserName={usuario.UserName}, @Password={usuario.Password}, @Sexo={usuario.Sexo}, @Telefono={usuario.Telefono}, @Celular={usuario.Celular}, @Curp={usuario.Curp}, @Email={usuario.Email}, @{usuario.Rol.IdRol}, @Imagen={usuario.Imagen} , '{usuario.Direccion.Calle}', '{usuario.Direccion.NumeroInterior}', '{usuario.Direccion.NumeroExterior}', {usuario.Direccion.Colonia.IdColonia}");
                    if (query > 0)
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


        // GETALL
        //public static ML.Result GetAll()
        //{
        //    ML.Result result = new ML.Result();
        //    try
        //    {
        //        using (DL.JjuarezProgramacionNcapasContext context = new DL.JjuarezProgramacionNcapasContext())
        //        {
        //            var query = context.Usuarios.FromSqlRaw("UsuarioGetAll").ToList();
                    
        //            result.Objects = new List<object>();

        //            if (query != null)
        //            {
        //                foreach (var item in query)
        //                {

        //                    ML.Usuario usuario = new ML.Usuario();
        //                    usuario.IdUsuario = item.IdUsuario;
        //                    usuario.Nombre = item.Nombre;
        //                    usuario.ApellidoPaterno = item.ApellidoPaterno;
        //                    usuario.ApellidoMaterno = item.ApellidoMaterno;
        //                    usuario.FechaNacimiento = item.FechaNacimiento.Value.ToString("dd-MM-yyyy");
        //                    usuario.UserName = item.UserName;
        //                    usuario.Password = item.Password;
        //                    usuario.Sexo = item.Sexo;
        //                    usuario.Telefono = item.Telefono;
        //                    usuario.Celular = item.Celular;
        //                    usuario.Curp = item.Curp;
        //                    usuario.Email = item.Email;

        //                    usuario.Rol = new ML.Rol();
        //                    usuario.Rol.IdRol = item.IdRol.Value;
        //                    usuario.Rol.Nombre = item.NombreRol;

        //                    usuario.Imagen = item.Imagen;

        //                    //Direccion
        //                    usuario.Direccion = new ML.Direccion();
        //                    usuario.Direccion.IdDireccion = item.IdDireccion;
        //                    usuario.Direccion.Calle = item.Calle;
        //                    usuario.Direccion.NumeroInterior = item.NumeroInterior;
        //                    usuario.Direccion.NumeroExterior = item.NumeroExterior;

        //                    //Colonia
        //                    usuario.Direccion.Colonia = new ML.Colonia();
        //                    usuario.Direccion.Colonia.IdColonia = item.IdColonia;
        //                    usuario.Direccion.Colonia.Nombre = item.NombreColonia;
        //                    usuario.Direccion.Colonia.CodigoPostal = item.CodigoPostal;

        //                    //Municipio
        //                    usuario.Direccion.Colonia.Municipio = new ML.Municipio();
        //                    usuario.Direccion.Colonia.Municipio.IdMunicipio = item.IdMunicipio;
        //                    usuario.Direccion.Colonia.Municipio.Nombre = item.NombreMunicipio;

        //                    //Estado
        //                    usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
        //                    usuario.Direccion.Colonia.Municipio.Estado.IdEstado = item.IdEstado;
        //                    usuario.Direccion.Colonia.Municipio.Estado.Nombre = item.NombreEstado;

        //                    //Pais
        //                    usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
        //                    usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = item.IdPais;
        //                    usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = item.NombrePais;


        //                    result.Objects.Add(usuario);

        //                }
        //                result.Correct = true;
        //            }
        //            else
        //            {
        //                result.Correct = false;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Correct = false;
        //        result.ErrorMessage = ex.Message;
        //        result.Ex = ex;

        //    }
        //    return result;
        //}

       
        public static ML.Result GetAll(ML.Usuario usuario)  // GETALL
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JjuarezProgramacionNcapasContext context = new DL.JjuarezProgramacionNcapasContext())
                {
                    //OPERADOR TERNARIO
                    usuario.Rol.IdRol = (byte)((usuario.Rol.IdRol == null) ? 0 : usuario.Rol.IdRol);
                    var query = context.Usuarios.FromSqlRaw($"UsuarioGetAll '{usuario.Nombre}', '{usuario.ApellidoPaterno}', {usuario.Rol.IdRol}").ToList();

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {

                            ML.Usuario usuarioobj = new ML.Usuario();
                            usuarioobj.IdUsuario = item.IdUsuario;
                            usuarioobj.Nombre = item.Nombre;
                            usuarioobj.ApellidoPaterno = item.ApellidoPaterno;
                            usuarioobj.ApellidoMaterno = item.ApellidoMaterno;
                            usuarioobj.FechaNacimiento = item.FechaNacimiento.Value.ToString("dd-MM-yyyy");
                            usuarioobj.UserName = item.UserName;
                            usuarioobj.Password = item.Password;
                            usuarioobj.Sexo = item.Sexo;
                            usuarioobj.Telefono = item.Telefono;
                            usuarioobj.Celular = item.Celular;
                            usuarioobj.Curp = item.Curp;
                            usuarioobj.Email = item.Email;

                            usuarioobj.Rol = new ML.Rol();
                            usuarioobj.Rol.IdRol = item.IdRol.Value;
                            usuarioobj.Rol.Nombre = item.NombreRol;

                            usuarioobj.Imagen = item.Imagen;
                            usuarioobj.Status = item.Status.Value;

                            //Direccion
                            usuarioobj.Direccion = new ML.Direccion();
                            usuarioobj.Direccion.IdDireccion = item.IdDireccion;
                            usuarioobj.Direccion.Calle = item.Calle;
                            usuarioobj.Direccion.NumeroInterior = item.NumeroInterior;
                            usuarioobj.Direccion.NumeroExterior = item.NumeroExterior;

                            //Colonia
                            usuarioobj.Direccion.Colonia = new ML.Colonia();
                            usuarioobj.Direccion.Colonia.IdColonia = item.IdColonia;
                            usuarioobj.Direccion.Colonia.Nombre = item.NombreColonia;
                            usuarioobj.Direccion.Colonia.CodigoPostal = item.CodigoPostal;

                            //Municipio
                            usuarioobj.Direccion.Colonia.Municipio = new ML.Municipio();
                            usuarioobj.Direccion.Colonia.Municipio.IdMunicipio = item.IdMunicipio;
                            usuarioobj.Direccion.Colonia.Municipio.Nombre = item.NombreMunicipio;

                            //Estado
                            usuarioobj.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                            usuarioobj.Direccion.Colonia.Municipio.Estado.IdEstado = item.IdEstado;
                            usuarioobj.Direccion.Colonia.Municipio.Estado.Nombre = item.NombreEstado;

                            //Pais
                            usuarioobj.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                            usuarioobj.Direccion.Colonia.Municipio.Estado.Pais.IdPais = item.IdPais;
                            usuarioobj.Direccion.Colonia.Municipio.Estado.Pais.Nombre = item.NombrePais;


                            result.Objects.Add(usuarioobj);

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
                result.Ex = ex;

            }
            return result;
        }

        // GETBYID 

        public static ML.Result GetById(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JjuarezProgramacionNcapasContext context = new DL.JjuarezProgramacionNcapasContext())
                {

                    var objUsuario = context.Usuarios.FromSqlRaw($"UsuarioGetById {IdUsuario}").AsEnumerable().FirstOrDefault();
                        

                    result.Objects = new List<object>();

                    if (objUsuario != null)
                    {

                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = objUsuario.IdUsuario;
                        usuario.Nombre = objUsuario.Nombre;
                        usuario.ApellidoPaterno = objUsuario.ApellidoPaterno;
                        usuario.ApellidoMaterno = objUsuario.ApellidoMaterno;
                        usuario.FechaNacimiento = objUsuario.FechaNacimiento.Value.ToString("dd-MM-yyyy");
                        usuario.UserName = objUsuario.UserName;
                        usuario.Password = objUsuario.Password;
                        usuario.Sexo = objUsuario.Sexo;
                        usuario.Telefono = objUsuario.Telefono;
                        usuario.Celular = objUsuario.Celular;
                        usuario.Curp = objUsuario.Curp;
                        usuario.Email = objUsuario.Email;

                        usuario.Rol = new ML.Rol();
                        usuario.Rol.IdRol = objUsuario.IdRol.Value;

                        usuario.Imagen = objUsuario.Imagen;
                        usuario.Status = objUsuario.Status.Value;

                        //Direccion
                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.IdDireccion = objUsuario.IdDireccion;
                        usuario.Direccion.Calle = objUsuario.Calle;
                        usuario.Direccion.NumeroInterior = objUsuario.NumeroInterior;
                        usuario.Direccion.NumeroExterior = objUsuario.NumeroExterior;

                        //Colonia
                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.IdColonia = objUsuario.IdColonia;
                        usuario.Direccion.Colonia.Nombre = objUsuario.NombreColonia;
                        usuario.Direccion.Colonia.CodigoPostal = objUsuario.CodigoPostal;

                        //Municipio
                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.IdMunicipio = objUsuario.IdMunicipio;
                        usuario.Direccion.Colonia.Municipio.Nombre = objUsuario.NombreMunicipio;

                        //Estado
                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                        usuario.Direccion.Colonia.Municipio.Estado.IdEstado = objUsuario.IdEstado;
                        usuario.Direccion.Colonia.Municipio.Estado.Nombre = objUsuario.NombreEstado;

                        //Pais
                        usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = objUsuario.IdPais;
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = objUsuario.NombrePais;

                        result.Object = usuario;

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
                result.Ex = ex;
            }
            return result;
        }


        // DELETE 
        public static ML.Result Delete(int idUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JjuarezProgramacionNcapasContext context = new DL.JjuarezProgramacionNcapasContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"UsuarioDelete '{idUsuario}'");

                    if (query > 0)
                    {
                        result.ErrorMessage = "Se elimino el usuario correctamente";
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "Ocurrio un error al eliminar el usuario" + result.Ex;
            }
            return result;
        }

        // STATUS 
        public static ML.Result ChangeStatus(byte idUsuario, bool status)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JjuarezProgramacionNcapasContext context = new DL.JjuarezProgramacionNcapasContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"UsuarioChangeStatus '{idUsuario}','{status}'");

                    if (query > 0)
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
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }



        // CONEXIÓN A EXCEL CON OLEDB --> EXTRAE INFORMACION DE DIFERENTES TIPOS DE ARCHIVOS
        // INSTALAR OLEDB 

        // Metodo convertir excel a tabla y extraer la informacion de la copia del archivo de excel 
        public static ML.Result ConvertirExceltoDataTable(string connString)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (OleDbConnection context = new OleDbConnection(connString))
                {
                    string query = "SELECT * FROM [Sheet1$]";
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;

                        // DataAdarpter  = Adapta o convierte
                        OleDbDataAdapter da = new OleDbDataAdapter();
                        da.SelectCommand = cmd;

                        DataTable tableusuario = new DataTable();
                        da.Fill(tableusuario);

                        if (tableusuario.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();
                            foreach (DataRow row in tableusuario.Rows)
                            {
                                ML.Usuario usuario = new ML.Usuario(); //instancia de usuario
                                usuario.Nombre = row[0].ToString();
                                usuario.ApellidoPaterno = row[1].ToString();
                                usuario.ApellidoMaterno = row[2].ToString();
                                usuario.FechaNacimiento = row[3].ToString();
                                usuario.UserName = row[4].ToString();
                                usuario.Password = row[5].ToString();
                                usuario.Sexo = row[6].ToString();
                                usuario.Telefono = row[7].ToString();
                                usuario.Celular = row[8].ToString();
                                usuario.Curp = row[9].ToString();
                                usuario.Email = row[10].ToString();

                                usuario.Rol = new ML.Rol(); //instancia de ROL
                                usuario.Rol.IdRol = byte.Parse(row[11].ToString());

                                usuario.Direccion = new ML.Direccion(); //instancia de direccion 
                                usuario.Direccion.Calle = row[12].ToString();
                                usuario.Direccion.NumeroInterior = row[13].ToString();
                                usuario.Direccion.NumeroExterior = row[14].ToString();

                                usuario.Direccion.Colonia = new ML.Colonia(); //instancia de Colonia 
                                usuario.Direccion.Colonia.IdColonia = byte.Parse(row[15].ToString());

                                result.Objects.Add(usuario);
                            }
                            result.Correct = true;
                        }
                        result.Object = tableusuario; 
                        
                        if(tableusuario.Rows.Count > 1)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct=false;
                            result.ErrorMessage = "No existen registros en el archivo de excel";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage =ex.Message;
            }
            return result;
        }

        // Metodo para validar excel 
        public static ML.Result ValidarExcel(List<object> Object) 
        {
            ML.Result result = new ML.Result();

            try
            {
                result.Objects = new List<object>();
                int i = 1;
                foreach(ML.Usuario usuario in Object)
                {
                    ML.ErrorExcel error = new ML.ErrorExcel(); // Instanciar lista de los registros que esten mal
                    error.IdRegistro = i++; //Incremento por cada registro de error  y al terminar de utilizarce de destruye

                    //Operador terario 
                    usuario.Nombre = (usuario.Nombre == " ") ? error.Mensaje += "Error al ingresar el nombre: " : usuario.Nombre; 

                    if (usuario.ApellidoPaterno == " ")
                    {
                        error.Mensaje += "Error al ingresar el ApellidoPaterno :";
                    }
                    if(usuario.ApellidoMaterno == " ")
                    {
                        error.Mensaje += "Error al ingresar el ApellidoMaterno :";
                    }
                    if (usuario.FechaNacimiento == " ")
                    {
                        error.Mensaje += "Error al ingresar la FechaNacimiento :";
                    }
                    if (usuario.UserName == " ")
                    {
                        error.Mensaje += "Error al ingresar el UserName : ";
                    }
                    if (usuario.Password == " ")
                    {
                        error.Mensaje += "Error al ingresar el Password : ";
                    }
                    if (usuario.Sexo == " ")
                    {
                        error.Mensaje += "Error al ingresar el Sexo : ";
                    }
                    if (usuario.Telefono == " ")
                    {
                        error.Mensaje += "Error al ingresar el Telefono : ";
                    }
                    if (usuario.Celular == " ")
                    {
                        error.Mensaje += "Error al ingresar el Celular :";
                    }
                    if (usuario.Curp == " ")
                    {
                        error.Mensaje += "Error al ingresar el Curp :";
                    }
                    if (usuario.Email == " ")
                    {
                        error.Mensaje += "Error al ingresar el Email :";
                    }
                    if (usuario.Rol.IdRol.ToString() == " ")
                    {
                        error.Mensaje += "Error al ingresar el IdRol :";
                    }
                    if (usuario.Direccion.Calle == " ")
                    {
                        error.Mensaje += "Error al ingresar la Calle :";
                    }
                    if (usuario.Direccion.NumeroInterior == " ")
                    {
                        error.Mensaje += "Error al ingresar el NumeroInterior :";
                    }
                    if (usuario.Direccion.NumeroInterior == " ")
                    {
                        error.Mensaje += "Error al ingresar el NumeroExterior :";
                    }
                    if (usuario.Direccion.Colonia.IdColonia.ToString() == " ")
                    {
                        error.Mensaje += "Error al ingresar el IdColonia :";
                    }

                    if (error.Mensaje != null)
                    {
                        result.Objects.Add(error);
                    }
                }
                result.Correct = true; 
            }
            catch (Exception ex )
            {
                result.Correct = false;
                result.Ex= ex;
                result.ErrorMessage = "Error, no se inserto el registro Correctamente" + result.Ex;
            }
            return result;
        }

    }
}
