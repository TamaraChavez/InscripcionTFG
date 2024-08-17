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
    public class CD_Modalidad
    {

        public List<Modalidad> Listar()
        {
            List<Modalidad> lista = new List<Modalidad>();
            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cn))
                {
                    string query = "select idModalidad, nomModalidad from Modalidad";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.CommandType = CommandType.Text;
                    conexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add
                                (
                                new Modalidad()
                                {
                                    idModalidad = Convert.ToInt32(dr["idModalidad"]),
                                    nomModalidad = dr["nomModalidad"].ToString()
                                }
                                );
                        }
                    }
                }




            }
            catch (Exception ex)
            {
                Debug.WriteLine("Ha ocurrido un error listando la info: " + ex.Message);
                lista = new List<Modalidad>();
                Debug.WriteLine("Acaba de crearse una nueva lista");
            }
            Debug.WriteLine("Lista retornada.");
            return lista;

        }

        public int Registrar(Modalidad obj, out string Mensaje)
        {
            int idautogenerado = 0;
            Mensaje = string.Empty;
            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "INSERT INTO Modalidad (idModalidad, nomModalidad) " +
                                   "OUTPUT INSERTED.idModalidad " +
                                   "VALUES (@idModaliad, @nomModalidad)";

                    using (SqlCommand cmd = new SqlCommand(query, oconexion))
                    {
                        cmd.Parameters.AddWithValue("@idModalidad", obj.idModalidad);
                        cmd.Parameters.AddWithValue("@nomModalidad", obj.nomModalidad);
                        oconexion.Open();
                        idautogenerado = (int)cmd.ExecuteScalar();
                        Mensaje = "Modalidad registrada exitosamente.";
                        Debug.WriteLine("Modalidad registrada exitosamente");

                    }
                }
            }
            catch (Exception ex)
            {
                idautogenerado = 0;
                Debug.WriteLine("Ha ocurrido un error registrando la modalidad: " + ex.Message);
                Mensaje = ex.Message;
            }
            return idautogenerado;



        }

        public int Editar(Modalidad obj, out string Mensaje)
        {
            bool resultado = false;
            int idautogenerado = 0;
            Mensaje = string.Empty;
            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "UPDATE Modalidad SET nomModalidad =@nomModalidad WHERE idModalidad=@idModaliad";
                                 

                    using (SqlCommand cmd = new SqlCommand(query, oconexion))
                    {
                        cmd.Parameters.AddWithValue("@idModalidad", obj.idModalidad);
                        cmd.Parameters.AddWithValue("@nomModalidad", obj.nomModalidad);
                        oconexion.Open();
                        idautogenerado = (int)cmd.ExecuteScalar();
                        Mensaje = "Modalidad registrada exitosamente.";
                        Debug.WriteLine("Modalidad registrada exitosamente");

                    }
                }
            }
            catch (Exception ex)
            {
                idautogenerado = 0;
                Debug.WriteLine("Ha ocurrido un error registrando la modalidad: " + ex.Message);
                Mensaje = ex.Message;
            }
            return idautogenerado;



        }




    }
}
