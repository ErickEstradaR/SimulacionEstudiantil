namespace SimulacionEstudiantil;

public class SimuladorAcademico
    {
        private Random _rng = new Random();
        private string[] _excusas = { "Fallo de internet", "Se quedó dormido", "Priorizó otra materia", "Emergencia laboral", "No entendió el tema", "Plataforma bloqueada" };

        public void SimularMateriaCompleta(Asignatura materia)
        {
            Console.WriteLine($"\n>>> INSCRIBIENDO: {materia.Nombre} | Créditos: {materia.Creditos}");
            for (int i = 1; i <= 3; i++) SimularActividadesDeCiclo(materia, i);
            EvaluarMateria(materia);
        }

        private void SimularActividadesDeCiclo(Asignatura materia, int ciclo)
        {
            foreach (TipoActividad tipo in Enum.GetValues(typeof(TipoActividad)))
            {
                bool completada = _rng.NextDouble() > 0.12;
                int tMax = materia.DiasDuracion * 2;
                int tInv = _rng.Next(1, tMax + 5);
                double nota = completada ? Math.Min(100, ((double)tInv / tMax * 35) + _rng.Next(60, 70)) : 0;
                string razon = completada ? "Entregado" : _excusas[_rng.Next(_excusas.Length)];

                materia.Ciclos[ciclo].Add(new RegistroActividad { Tipo = tipo, Completada = completada, Nota = nota, RazonFallo = razon });
                Console.WriteLine($"  P{ciclo} - {tipo,-15} | Nota: {nota,6:F2} | {razon}");
            }
        }

        public void EvaluarMateria(Asignatura materia)
        {
            if (materia.TotalInasistencias >= 4)
            {
                materia.Aprobada = false;
                materia.ReporteFinal = $"Fallo por inasistencias ({materia.TotalInasistencias})";
            }
            else if (materia.NotaSimulada < 70)
            {
                materia.Aprobada = false;
                materia.ReporteFinal = $"Fallo por nota ({materia.NotaSimulada:F2})";
            }
            else
            {
                materia.Aprobada = true;
                materia.ReporteFinal = $"APROBADA ({materia.ObtenerLetraCalificacion()})";
            }
            Console.WriteLine($"  [RESULTADO: {materia.ReporteFinal}]");
        }
    }