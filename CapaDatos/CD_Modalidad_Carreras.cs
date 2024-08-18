using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Modalidad_Carreras
    {
        public List<Modalidad> ObtenerModalidadesPorCarrera(string idCarrera)
        {
            List<Modalidad> modalidades = new List<Modalidad>();

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cn))
                {
                    string query = @"
                        SELECT m.idModalidad, m.nomModalidad
                        FROM Modalidad_Carrera mc
                        INNER JOIN Modalidad m ON mc.idModalidad = m.idModalidad
                        WHERE mc.idCarrera = @IdCarrera";

                    SqlCommand command = new SqlCommand(query, conexion);
                    command.Parameters.AddWithValue("@IdCarrera", idCarrera);

                    conexion.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        modalidades.Add(new Modalidad
                        {
                            IdModalidad = Convert.ToInt32(reader["idModalidad"]),
                            NomModalidad = reader["nomModalidad"].ToString()
                        });
                    }
                }

                return modalidades;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
