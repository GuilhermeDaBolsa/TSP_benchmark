using System.Diagnostics;

class Benchmarker {

    public string name;
    Stopwatch clock;
    Solver tsp_solver;

    public Benchmarker(string name, Solver solver) {
        this.name = name;
        clock = new Stopwatch();
        tsp_solver = solver;
    }

    public void RunBenchmark(TSP problemInstance, string outputFile) {
        clock.Start();

        var answer = tsp_solver.Solve(problemInstance.cities);

        clock.Stop();

        OutputResultToFile(problemInstance, answer, clock.ElapsedMilliseconds, outputFile);

        clock.Reset();
    }

    private void OutputResultToFile(TSP problemInstance, List<TSP_City> result, long elapsedTimeMs, string outputFile) {
        using (StreamWriter writer = File.CreateText(outputFile)) {
            writer.WriteLine($"NAME: {problemInstance.name}");
            writer.WriteLine($"ELLAPSED_TIME_(ms): {elapsedTimeMs}");
            writer.WriteLine("TOUR_SECTION");

            for (int i = 0; i < result.Count; i++) {
                writer.WriteLine($"{result[i].index}");
            }

            writer.WriteLine("EOF");
        }
    }

}

