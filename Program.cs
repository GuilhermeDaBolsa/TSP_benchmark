
using System.Globalization;
using TSP_TESTS;

Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");



var BASE_DIR = "C:/Users/Guilh/Desktop/teste";
var TSP_FOLDER_PREFIX = "Random_";

var MIN_TSP_SIZE = 5;
var MAX_TSP_SIZE = 8;

var NUMBER_OF_SAMPLES = 3;

var MAX_CITY_X_COORD_PROPORTION = 3;
var MAX_CITY_Y_COORD_PROPORTION = 3;

var fileWriter = new TSPLibFileWriter();

for (int tspSize = MIN_TSP_SIZE; tspSize <= MAX_TSP_SIZE; tspSize++) {

    var newDirectory = $"{BASE_DIR}/{TSP_FOLDER_PREFIX}{tspSize}";

    Directory.CreateDirectory(newDirectory);

    var TSPGenerator = new TSPGenerator(tspSize, tspSize * MAX_CITY_X_COORD_PROPORTION, tspSize * MAX_CITY_Y_COORD_PROPORTION);

    for (int sample = 0; sample < NUMBER_OF_SAMPLES; sample++) {

        var cities = TSPGenerator.GenerateRandomCities();

        fileWriter.WriteFile(newDirectory, "Random", sample+1, cities);
    }
}
