using System.Globalization;

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

var sampleDirectories = Directory.GetDirectories(TESTS_BASE_FOLDER).ToList();

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
}






//-- FORMAT CONCORDE DATA --//

/*var sampleDirectories = Directory.GetDirectories(TESTS_BASE_FOLDER).Where(d => Int32.Parse(d.Substring(d.LastIndexOf("_") + 1)) > 30).ToList();
sampleDirectories.Sort((x, y) => Int32.Parse(x.Substring(x.LastIndexOf("_") + 1)) - Int32.Parse(y.Substring(y.LastIndexOf("_") + 1)));

var concordeDirectories = 
    sampleDirectories
        .Select(d => Directory.Exists($"{d}/Concorde") ? $"{d}/Concorde" : "REMOVE")
        .Where(cd => cd != "REMOVE")
        .ToList();


concordeDirectories.ForEach(cd => {

    var sampleFiles = Directory.GetFiles(cd, "*.ans");

    foreach (var sampleFile in sampleFiles) {

        if (!File.Exists(sampleFile))
            return;

        var fileNameAndExtension = sampleFile.Substring(sampleFile.LastIndexOf("Random"));

        var fileName = fileNameAndExtension.Substring(0, fileNameAndExtension.Length - 4);

        IEnumerable<string> lines = File.ReadLines(sampleFile);
        var linesIterator = lines.GetEnumerator();
        linesIterator.MoveNext();

        float timeInSec = float.Parse(linesIterator.Current);
        long timeInMs = (long)(timeInSec * 1000);

        linesIterator.MoveNext();

        int size = int.Parse(linesIterator.Current);
        var indexes = new List<int>(size);

        while (linesIterator.MoveNext()) {
            if(!string.IsNullOrWhiteSpace(linesIterator.Current))
                indexes.Add(int.Parse(linesIterator.Current));
        }

        indexes.Add(0);

        //WRITE FILE
        using (StreamWriter writer = File.CreateText(sampleFile)) {
            writer.WriteLine($"NAME: {fileName}");
            writer.WriteLine($"ELLAPSED_TIME_(ms): {timeInMs}");
            writer.WriteLine($"TOUR_SIZE: {size}");
            writer.WriteLine("TOUR_SECTION");

            for (int i = 0; i < indexes.Count; i++) {
                writer.WriteLine($"{indexes[i]}");
            }

            writer.WriteLine("EOF");
        }
    }
});
*/