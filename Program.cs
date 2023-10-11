using System.Globalization;

Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");

string PROJECT_PATH = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
string TESTS_BASE_FOLDER = Path.Combine(PROJECT_PATH, "GeneratedTests");

//-- WRITE TEST FILES --//

var TSP_FOLDER_PREFIX = "Random_";
var TSP_FILE_PREFIX = "Random";

var MIN_TSP_SIZE = 5;
var MAX_TSP_SIZE = 100;

var NUMBER_OF_SAMPLES = 20;

var MAX_CITY_X_COORD_PROPORTION = 3;
var MAX_CITY_Y_COORD_PROPORTION = 3;

//COMMENTED BCAUSE TESTS WERE ALREADY GENERATED
/* 
TSPLibFileWriter.WriteSamples(
    TESTS_BASE_FOLDER,
    TSP_FOLDER_PREFIX,
    TSP_FILE_PREFIX,
    NUMBER_OF_SAMPLES,
    MIN_TSP_SIZE,
    MAX_TSP_SIZE,
    MAX_CITY_X_COORD_PROPORTION,
    MAX_CITY_Y_COORD_PROPORTION);
*/



var sampleDirectories = Directory.GetDirectories(TESTS_BASE_FOLDER).Where(name => name.Contains("14")); //TODO REMOVE 'WHERE' FILTER (its just for testing)

var benchmarkers = new List<Benchmarker> {
    new Benchmarker("BranchAndBoundDFS", new BranchAndBoundDFS()),
    new Benchmarker("BranchAndBoundBFS", new BranchAndBoundBFS()),
    new Benchmarker("Christofides", new Christofides())
};

foreach (var directory in sampleDirectories) {

    var sampleFiles = Directory.GetFiles(directory, "*.tsp");

    benchmarkers.ForEach(benchmarker
        => Directory.CreateDirectory($"{directory}/{benchmarker.name}"));

    foreach (var file in sampleFiles) {
        var TSP = TSPLibFileReader.Import(file);

        benchmarkers.ForEach(benchmarker
            => benchmarker.RunBenchmark(TSP, $"{directory}/{benchmarker.name}/{TSP.name}.ans"));
    }
}
