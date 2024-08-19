using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using System.Diagnostics;

namespace CapaDatos
{
    public class CD_AsigTutor
    {

   
            public List<InscripcionesResueltas> Listar()
            {
                List<InscripcionesResueltas> lista = new List<InscripcionesResueltas>();
                try
                {
                    using (SqlConnection conexion = new SqlConnection(Conexion.cn))
                    {
                        string query = "SELECT idInscripcionResuelta, idInscripcion, idUsuarioDirector, estado, idUsuarioTutor FROM Inscripciones_Resueltas";
                        SqlCommand cmd = new SqlCommand(query, conexion);
                        cmd.CommandType = CommandType.Text;
                        conexion.Open();

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                lista.Add(new InscripcionesResueltas()
                                {
                                    idInscripcionResuelta = Convert.ToInt32(dr["idInscripcionResuelta"]),
                                    idInscripcion = Convert.ToInt32(dr["idInscripcion"]),
                                    idUsuarioDirector = Convert.ToInt32(dr["idUsuarioDirector"]),
                                    estado = dr["estado"].ToString(),
                                    idUsuarioTutor = Convert.ToInt32(dr["idUsuarioTutor"]),
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Ha ocurrido un error listando la info: " + ex.Message);
                    lista = new List<InscripcionesResueltas>();
                    Debug.WriteLine("Acaba de crearse una nueva lista");
                }
                Debug.WriteLine("Lista retornada.");
                return lista;
            }



            public List<Usuario> ListarTutoresCarrera()
        {
            List<Usuario> lista = new List<Usuario>();

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cn))
                {
                    string query = @"
                SELECT u.IdUsuario, u.Nombre, u.Apellido1, u.Apellido2
  FROM Tutores_Carrera tc
  INNER JOIN Usuario u ON tc.idUsuarioTutor = u.IdUsuario
  WHERE u.TipoUsuario = '2'";

                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.CommandType = CommandType.Text;
                    conexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Usuario()
                            {
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                Nombre = $"{dr["Nombre"]} {dr["Apellido1"]} {dr["Apellido2"]}" // Concatenando nombre y apellidos
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Ha ocurrido un error listando los tutores: " + ex.Message);
                lista = new List<Usuario>();
            }

            return lista;
        }

        public int Editar(InscripcionesResueltas obj, out string Mensaje)
        {
            bool resultado = false;
            int idautogenerado = 0;
            Mensaje = string.Empty;
            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "UPDATE Inscripciones_Resueltas SET idUsuarioTutor=@idUsuarioTutor WHERE idInscripcionResuelta=@idInscripcionResuelta";
                                 

                    using (SqlCommand cmd = new SqlCommand(query, oconexion))
                    {
                        cmd.Parameters.AddWithValue("@idInscripcionResuelta", obj.idInscripcionResuelta);
                        cmd.Parameters.AddWithValue("@idUsuarioTutor", obj.idUsuarioTutor);
                        oconexion.Open();
                        idautogenerado = (int)cmd.ExecuteScalar();
                        Mensaje = "Asignacion de tutor realizada exitosamente.";
                        Debug.WriteLine("Asignacion de tutor realizada exitosamente");

                    }
                }
            }
            catch (Exception ex)
            {
                idautogenerado = 0;
                Debug.WriteLine("Ha ocurrido un error actualizando la asignacion de tutor: " + ex.Message);
                Mensaje = ex.Message;
            }
            return idautogenerado;



        }




    }
}
