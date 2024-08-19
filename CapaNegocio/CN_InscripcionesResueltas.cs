using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;

public class CN_InscripcionesResueltas
{
    private CD_Usuarios objCapaDato = new CD_Usuarios();

    public List<Usuario> Listar(int idDirector)
    {
        return objCapaDato.ListarUsuariosCarrera(idDirector);
    }

    public bool ActualizarEstadoInscripcion(int idUsuario, string nuevoEstado)
    {
        return objCapaDato.ActualizarEstado(idUsuario, nuevoEstado);
    }
}
