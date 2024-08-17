using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Periodos
    {
        public string Registrar(Periodo obj)
        {
            string mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    // Consulta SQL para insertar un nuevo registro en la tabla PeriodosInscripcion
                    string query = @"INSERT INTO Periodo (Activo, Cuatrimestre, FechaInicio, HoraInicio, FechaFin, HoraFin) 
                             VALUES (@Activo, @Cuatrimestre, @FechaInicio, @HoraInicio, @FechaFin, @HoraFin)";

                    using (SqlCommand cmd = new SqlCommand(query, oconexion))
                    {
                        // Añadir parámetros para prevenir inyecciones SQL
                        cmd.Parameters.AddWithValue("@Activo", obj.Activo);
                        cmd.Parameters.AddWithValue("@Cuatrimestre", obj.Cuatrimestre);
                        cmd.Parameters.AddWithValue("@FechaInicio", obj.FechaInicio);
                        cmd.Parameters.AddWithValue("@HoraInicio", obj.HoraInicio);
                        cmd.Parameters.AddWithValue("@FechaFin", obj.FechaFin);
                        cmd.Parameters.AddWithValue("@HoraFin", obj.HoraFin);

                        oconexion.Open();

                        int filasAfectadas = cmd.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            mensaje = "El Periodo de inscripción se habilito correctamente.";
                        }
                        else
                        {
                            mensaje = "No se pudo habilitar el periodo de inscripción.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error: " + ex.Message;
            }

            return mensaje;
        }


        public Periodo ObtenerPeriodoActivo()
        {
            Periodo periodoActivo = null;  // Inicializa como null para comprobar si hay resultados

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cn))
                {
                    string query = @"SELECT * FROM Periodo WHERE Activo = @Activo";

                    SqlCommand command = new SqlCommand(query, conexion);
                    command.Parameters.AddWithValue("@Activo", 1);  // Parámetro para buscar el periodo activo

                    conexion.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())  // Si hay resultados
                    {
                        periodoActivo = new Periodo
                        {
                            IdPeriodo = Convert.ToInt32(reader["idPeriodo"]),
                            Cuatrimestre = reader["cuatrimestre"].ToString(),
                            FechaInicio = Convert.ToDateTime(reader["fechaInicio"]),
                            HoraInicio = (TimeSpan)reader["HoraInicio"],
                            FechaFin = Convert.ToDateTime(reader["fechaFin"]),
                            HoraFin = (TimeSpan)reader["HoraFin"],
                            Activo = Convert.ToInt32(reader["Activo"])
                        };
                    }
                }

                return periodoActivo;  // Retorna el periodo activo o null si no hay periodo activo
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el periodo activo", ex);  
            }
        }
    }
}
