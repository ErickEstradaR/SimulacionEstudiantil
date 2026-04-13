using System.Linq;
using System.Collections.Generic;
using System;

namespace SimulacionEstudiantil;

public class Asignatura
{
    public string Nombre { get; set; }
    public int Creditos { get; set; }
    public int DiasDuracion { get; set; }
    public DateTime FechaInscripcion { get; set; }
    public DateTime FechaInicio { get; set; }
    public bool Aprobada { get; set; }
    public string ReporteFinal { get; set; }
    public Dictionary<int, List<RegistroActividad>> Ciclos { get; set; }

    public Asignatura(string nombre, int creditos, int dias)
    {
        Nombre = nombre;
        Creditos = creditos;
        DiasDuracion = dias;
        FechaInscripcion = DateTime.Now;
        FechaInicio = DateTime.Now.AddDays(2);
        ResetearProgreso();
    }

    public void ResetearProgreso()
    {
        Ciclos = new Dictionary<int, List<RegistroActividad>> { { 1, new List<RegistroActividad>() }, { 2, new List<RegistroActividad>() }, { 3, new List<RegistroActividad>() } };
        Aprobada = false;
    }

    public double NotaSimulada
    {
        get
        {
            var todas = Ciclos.Values.SelectMany(x => x).ToList();
            if (!todas.Any()) return 0;

            double sumaPonderada = 0;
            var pesos = new Dictionary<TipoActividad, double> { { TipoActividad.Examen, 0.60 }, { TipoActividad.Practica, 0.10 }, { TipoActividad.Participacion, 0.10 }, { TipoActividad.Presentacion, 0.10 }, { TipoActividad.Foro, 0.10 } };

            foreach (var p in pesos)
            {
                var actos = todas.Where(a => a.Tipo == p.Key).ToList();
                if (actos.Any()) sumaPonderada += actos.Average(a => a.Nota) * p.Value;
            }
            return sumaPonderada;
        }
    }

    public int TotalInasistencias => Ciclos.Values.SelectMany(x => x).Count(a => !a.Completada);

    public double ValorPuntos => NotaSimulada >= 90 ? 4.0 : NotaSimulada >= 80 ? 3.0 : NotaSimulada >= 75 ? 2.5 : NotaSimulada >= 70 ? 2.0 : 0.0;

    public string ObtenerLetraCalificacion() => NotaSimulada >= 90 ? "A" : NotaSimulada >= 80 ? "B" : NotaSimulada >= 75 ? "C+" : NotaSimulada >= 70 ? "C" : "F";
}