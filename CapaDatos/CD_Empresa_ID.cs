using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Empresa_ID
    {
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
