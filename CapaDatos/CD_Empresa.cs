using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Empresa
    {
        public int Registrar(Empresa obj)
        {
            int idEmpresa = 0;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    // Consulta SQL para insertar un nuevo registro en la tabla Empresa_Inscripcion
                    string query = @"
                        INSERT INTO Empresa_Inscripcion (nomEmpresa,telefono1,telefono2,
                        nomContacto,emailContacto)
                        VALUES (@nomEmpresa,@telefono1, @telefono2, @nomContacto, @EmailContacto)";

                    using (SqlCommand cmd = new SqlCommand(query, oconexion))
                    {
                        // Añadir parámetros para prevenir inyecciones SQL
                        cmd.Parameters.AddWithValue("@nomEmpresa", obj.NomEmpresa);
                        cmd.Parameters.AddWithValue("@telefono1", obj.Telefono1);
                        cmd.Parameters.AddWithValue("@telefono2", obj.Telefono2);
                        cmd.Parameters.AddWithValue("@nomContacto", obj.NomContacto);
                        cmd.Parameters.AddWithValue("@EmailContacto", obj.CorreoContacto);
                        

                        oconexion.Open();

                        int filasAfectadas = cmd.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            idEmpresa = ObtenerIdEmpresaPorNombre(obj.NomEmpresa);
                            return idEmpresa;   
                        }
                        return idEmpresa;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int ObtenerIdEmpresaPorNombre(string nomEmpresa)
        {
            int idEmpresa = 0;

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cn))
                {
                    string query = @"
                        SELECT idEmpresa from Empresa_Inscripcion 
                        where nomEmpresa = @NombreEmpresa";

                    SqlCommand command = new SqlCommand(query, conexion);
                    command.Parameters.AddWithValue("@NombreEmpresa", nomEmpresa);

                    conexion.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        idEmpresa = Convert.ToInt32(reader["idEmpresa"]);
                    }
                }

                return idEmpresa;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
