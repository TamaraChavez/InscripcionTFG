using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Usuario_Carreras
    {
        /*Obtener la lista de carreras*/
        public List<Carrera> ObtenerCarrerasPorUsuario(int idUsuario)
        {
            List<Carrera> carreras = new List<Carrera>();

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cn))
                {
                    string query = @"
                SELECT c.idCarrera, c.nomCarrera
                FROM Usuarios_Carrera uc
                INNER JOIN Carrera c ON uc.idCarrera = c.idCarrera
                WHERE uc.idUsuario = @IdUsuario";

                    SqlCommand command = new SqlCommand(query, conexion);
                    command.Parameters.AddWithValue("@IdUsuario", idUsuario);

                    conexion.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        carreras.Add(new Carrera
                        {
                            IdCarrera = reader["idCarrera"].ToString(),
                            NomCarrera = reader["nomCarrera"].ToString()
                        });
                    }
                }

                return carreras;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}