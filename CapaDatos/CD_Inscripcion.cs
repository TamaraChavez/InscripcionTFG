using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Inscripcion
    {
        public string Registrar(Inscripcion obj)
        {
            string mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    // Consulta SQL para insertar un nuevo registro en la tabla Inscripcion
                    string query = @"
                        INSERT INTO Inscripcion (idUsuarioEstudiante, idCarrera, idModalidad, 
                        idEmpresa, idPeriodo, Pendiente)
                        VALUES (@IdUsuario, @IdCarrera, @IdModalidad, @IdEmpresa, @IdPeriodo, @Pendiente)";

                    using (SqlCommand cmd = new SqlCommand(query, oconexion))
                    {
                        // Añadir parámetros para prevenir inyecciones SQL
                        cmd.Parameters.AddWithValue("@IdUsuario", obj.IdUsuario);
                        cmd.Parameters.AddWithValue("@IdCarrera", obj.IdCarrera);
                        cmd.Parameters.AddWithValue("@IdModalidad", obj.IdModalidad);                        

                        // Validar si IdEmpresa es null y agregar el parámetro adecuado
                        if (obj.IdEmpresa.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@IdEmpresa", obj.IdEmpresa.Value);  // Pasar el valor de IdEmpresa
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@IdEmpresa", DBNull.Value);  // Si es null, enviar DBNull.Value
                        }

                        cmd.Parameters.AddWithValue("@IdPeriodo", obj.IdPeriodo);
                        cmd.Parameters.AddWithValue("@Pendiente", obj.Pendiente);

                        oconexion.Open();

                        int filasAfectadas = cmd.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            mensaje = "Inscripción realizada correctamente.";
                        }
                        else
                        {
                            mensaje = "No se pudo realizar la inscripción.";
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // Verificar si el error es debido a una violación de clave primaria duplicada
                if (ex.Number == 2627 || ex.Number == 2601) // Ambos números están relacionados con duplicados de clave primaria o índice único
                {
                    mensaje = "Ya existe una inscripción para esa carrera.";
                }
                else
                {
                    // Mensaje genérico para otros errores
                    mensaje = "Error: " + ex.Message;
                }
            }

            return mensaje;
        }


    }
}
