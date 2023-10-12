using System.Diagnostics;
using System.Numerics;

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

        var pathDistance = CalculatePathDistance(answer);

        OutputResultToFile(problemInstance, answer, clock.ElapsedMilliseconds, pathDistance, outputFile);

        clock.Reset();
    }

    private void OutputResultToFile(TSP problemInstance, List<TSP_City> result, long elapsedTimeMs, float pathDistance, string outputFile) {
        using (StreamWriter writer = File.CreateText(outputFile)) {
            writer.WriteLine($"NAME: {problemInstance.name}");
            writer.WriteLine($"ELLAPSED_TIME_(ms): {elapsedTimeMs}");
            writer.WriteLine($"TOUR_SIZE: {pathDistance}");
            writer.WriteLine("TOUR_SECTION");

            for (int i = 0; i < result.Count; i++) {
                writer.WriteLine($"{result[i].index}");
            }

            writer.WriteLine("EOF");
        }
    }

    private float CalculatePathDistance(List<TSP_City> path) {
        float distance = 0;

        for (int i = 0; i < path.Count - 1; i++)
            distance += path[i].DistanceTo(path[i + 1]);

        return distance;
    }

}

