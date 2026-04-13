namespace SimulacionEstudiantil;

public enum TipoActividad { Practica, Examen, Participacion, Foro, Presentacion }

public class RegistroActividad
{
    public TipoActividad Tipo { get; set; }
    public bool Completada { get; set; }
    public double Nota { get; set; }
    public string RazonFallo { get; set; } 
}