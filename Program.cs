using System.Globalization;
using System.IO;
using TSP_benchmark.Generators;

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



//-- READ TEST FILES --//

var sampleDirectories = Directory.GetDirectories(TESTS_BASE_FOLDER);

foreach(var directory in sampleDirectories) {

    var sampleFiles = Directory.GetFiles(directory, "*.tsp");

    foreach(var file in sampleFiles) {
        var TSP = TSPLibFileReader.Import(file);

        Console.WriteLine($"{TSP.name} - {TSP.size} cities (checksum {TSP.cities.Count})");
    }
}


