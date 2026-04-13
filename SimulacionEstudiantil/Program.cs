
using SimulacionEstudiantil;

public class Program
{
    public static void Main()
    {
        var simulador = new SimuladorAcademico();
        var pensum = new List<Asignatura> { 
            new Asignatura("Lengua Española", 4, 10), 
            new Asignatura("Matemáticas", 4, 5), 
            new Asignatura("Programación", 3, 14), 
            new Asignatura("Inglés", 3, 7) 
        };

        while (pensum.Any(m => !m.Aprobada))
        {
            var reprobadas = pensum.Where(m => !m.Aprobada).ToList();
            Console.WriteLine($"\n--- NUEVO CICLO DE INSCRIPCIÓN ({reprobadas.Count} materias) ---");
            foreach (var materia in reprobadas)
            {
                materia.ResetearProgreso();
                simulador.SimularMateriaCompleta(materia);
            }
        }

        double totalPuntos = pensum.Sum(m => m.ValorPuntos * m.Creditos);
        int totalCreditos = pensum.Sum(m => m.Creditos);
        Console.WriteLine("\n" + new string('=', 40));
        Console.WriteLine($"Total Notas: {totalPuntos:F3}");
         Console.WriteLine($"Total Uni Matriculadas: {totalCreditos:F3}");
       Console.WriteLine($"ÍNDICE ACADÉMICO: {(totalPuntos / totalCreditos):F2}");
        Console.WriteLine(new string('=', 40));
    }
}
