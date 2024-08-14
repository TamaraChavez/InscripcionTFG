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
                    // Consulta SQL para insertar un nuevo registro en la tabla PeriodosInscripcion
                    string query = @"
                        INSERT INTO Inscripcion (idUsuarioEstudiante,idCarrera,idModalidad,
                        idEmpresa,idPeriodo)
                        VALUES (@IdUsuario,@IdCarrera,@IdModalidad,@IdEmpresa, @IdPeriodo) ";

                    using (SqlCommand cmd = new SqlCommand(query, oconexion))
                    {
                        // Añadir parámetros para prevenir inyecciones SQL
                        cmd.Parameters.AddWithValue("@IdUsuario", obj.IdUsuario);
                        cmd.Parameters.AddWithValue("@IdCarrera", obj.IdCarrera);
                        cmd.Parameters.AddWithValue("@IdModalidad", obj.IdModalidad);
                        cmd.Parameters.AddWithValue("@IdEmpresa", obj.IdEmpresa);
                        cmd.Parameters.AddWithValue("@IdPeriodo", obj.IdPeriodo);

                        oconexion.Open();

                        int filasAfectadas = cmd.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            mensaje = "Inscripcion realizada correctamente correctamente.";
                        }
                        else
                        {
                            mensaje = "No se pudo realizar la inscripción.";
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
    }
}
