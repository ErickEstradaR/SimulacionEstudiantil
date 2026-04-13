using System;
using System.Threading.Tasks;
using SimulacionEstudiantil;

class Program
{
    static async Task Main(string[] args)
    {
        Console.Title = "Simulador Académico";
        var simulador = new SimuladorAcademico();
        
        var materias = new List<Asignatura>
        {
            new Asignatura("Lengua Española", 4, 10), 
            new Asignatura("Matemáticas", 4, 5), 
            new Asignatura("Programación", 3, 14), 
            new Asignatura("Inglés", 3, 7) 
        };

        foreach (var materia in materias)
        {
            await simulador.SimularMateriaCompleta(materia);
            Console.WriteLine("Presiona una tecla para continuar con la siguiente materia...");
            Console.ReadKey();
        }

        Console.WriteLine("\n--- Simulación Finalizada ---");
    }
}