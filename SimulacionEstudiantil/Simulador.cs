using System;
using System.Threading.Tasks;

namespace SimulacionEstudiantil;

public class SimuladorAcademico
{
    private Random _rng = new Random();
    private string[] _excusas = { "Fallo de internet", "Se quedó dormido", "Priorizó otra materia", "Emergencia laboral", "No entendió el tema", "Plataforma bloqueada" };

    public async Task SimularMateriaCompleta(Asignatura materia)
    {
        Console.WriteLine($"\n>>> INSCRIBIENDO: {materia.Nombre} | Créditos: {materia.Creditos}");
        await Task.Delay(1000);

        for (int i = 1; i <= 3; i++) 
        {
            Console.WriteLine($"\n--- Iniciando Ciclo {i} ---");
            await SimularActividadesDeCiclo(materia, i);
            await Task.Delay(800);
        }
        
        EvaluarMateria(materia);
    }

    private async Task SimularActividadesDeCiclo(Asignatura materia, int ciclo)
    {
        foreach (TipoActividad tipo in Enum.GetValues(typeof(TipoActividad)))
        {
            bool completada = _rng.NextDouble() > 0.15;
            int tMax = materia.DiasDuracion * 2;
            int tInv = _rng.Next(1, tMax + 5);
            double nota = completada ? Math.Min(100, ((double)tInv / tMax * 40) + _rng.Next(55, 65)) : 0;
            string razon = completada ? "Entregado" : _excusas[_rng.Next(_excusas.Length)];

            materia.Ciclos[ciclo].Add(new RegistroActividad { Tipo = tipo, Completada = completada, Nota = nota, RazonFallo = razon });
            
            Console.WriteLine($"  P{ciclo} - {tipo,-15} | Nota: {nota,6:F2} | {razon}");
            await Task.Delay(300);
        }
    }

    public void EvaluarMateria(Asignatura materia)
    {
        if (materia.TotalInasistencias >= 4)
        {
            materia.Aprobada = false;
            materia.ReporteFinal = $"REPROBADA por inasistencias ({materia.TotalInasistencias})";
        }
        else if (materia.NotaSimulada < 70)
        {
            materia.Aprobada = false;
            materia.ReporteFinal = $"REPROBADA por nota ({materia.NotaSimulada:F2})";
        }
        else
        {
            materia.Aprobada = true;
            materia.ReporteFinal = $"APROBADA ({materia.ObtenerLetraCalificacion()})";
        }
        Console.WriteLine($"\n[RESULTADO FINAL: {materia.ReporteFinal}]\n");
    }
}