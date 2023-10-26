using System.Globalization;
using System.Text;

Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");

string PROJECT_PATH = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
string TESTS_BASE_FOLDER = Path.Combine(PROJECT_PATH, "GeneratedTests");



//-- WRITE TEST FILES --//

var TSP_FOLDER_PREFIX = "Random_";
var TSP_FILE_PREFIX = "Random";

var MIN_TSP_SIZE = 5;
var MAX_TSP_SIZE = 100;
var TSP_SIZE_STEP = 5;

var NUMBER_OF_SAMPLES = 5;

var MAX_CITY_X_COORD_PROPORTION = 3;
var MAX_CITY_Y_COORD_PROPORTION = 3;

//COMMENT IF TESTS WERE ALREADY GENERATED

//TSPLibFileWriter.WriteSamples(
//    TESTS_BASE_FOLDER,
//    TSP_FOLDER_PREFIX,
//    TSP_FILE_PREFIX,
//    NUMBER_OF_SAMPLES,
//    MIN_TSP_SIZE,
//    MAX_TSP_SIZE,
//    TSP_SIZE_STEP,
//    MAX_CITY_X_COORD_PROPORTION,
//    MAX_CITY_Y_COORD_PROPORTION);







//-- RUN BENCHMARKS ON TEST FILES (AND SAVE RESULTS) --//
//COMMENT IF RESULTS WERE ALREADY GENERATED

/*var sampleDirectories = Directory.GetDirectories(TESTS_BASE_FOLDER).ToList();

sampleDirectories = sampleDirectories.Where(d => d.EndsWith("_20")).ToList();

//SORTING SAMPLES DIRECTORIES IN ORDER OF INSTANCE SIZES
//sampleDirectories.Sort((x, y) => Int32.Parse(x.Substring(x.LastIndexOf("_")+1)) - Int32.Parse(y.Substring(y.LastIndexOf("_")+1)));

var benchmarkers = new List<Benchmarker> {
    new Benchmarker("BranchAndBoundDFS", new BranchAndBoundDFS()),
    //new Benchmarker("BranchAndBoundBFS", new BranchAndBoundBFS()),
    //new Benchmarker("Christofides", new Christofides())
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
}*/




/*
 * EXPORT DATA TO CSV
 */

using (StreamWriter writer = File.CreateText($"{TESTS_BASE_FOLDER}/results.csv")) {
    var methods = new List<string> { "Concorde", "Christofides", "Best-Bound", "Depth-First" };
    var methodsDirs = new List<string> { "Concorde", "Christofides", "BranchAndBoundBFS", "BranchAndBoundDFS" };

    writer.WriteLine($",{string.Join(",,", methods)},,");
    writer.WriteLine($",{string.Concat(Enumerable.Repeat("Tempo (ms),Tamanho,", methods.Count))}");

    for (int tspSize = MIN_TSP_SIZE; tspSize <= MAX_TSP_SIZE; tspSize += TSP_SIZE_STEP) {

        for(int i = 0; i < NUMBER_OF_SAMPLES; i++) {
            var sb = new StringBuilder();

            sb.Append($"{TSP_FOLDER_PREFIX}{tspSize}_{i+1},");

            var instanceOfNSizeDir = $"{TESTS_BASE_FOLDER}/{TSP_FOLDER_PREFIX}{tspSize}";

            methodsDirs.ForEach(methodDir => {

                string timeInMs = "";
                string size = "";

                if (File.Exists($"{instanceOfNSizeDir}/{methodDir}/{TSP_FOLDER_PREFIX}{tspSize}_{i + 1}.ans")) {
                    IEnumerable<string> lines = File.ReadLines($"{instanceOfNSizeDir}/{methodDir}/{TSP_FOLDER_PREFIX}{tspSize}_{i + 1}.ans");
                    var linesIterator = lines.GetEnumerator();
                    linesIterator.MoveNext();
                    linesIterator.MoveNext();
                    timeInMs = linesIterator.Current.Split(':')[1].Trim();
                    linesIterator.MoveNext();
                    size = linesIterator.Current.Split(':')[1].Trim();
                }
                
                sb.Append($"{timeInMs},");
                sb.Append($"{size},");
            });

            writer.WriteLine(sb.ToString());
        }
    }
}