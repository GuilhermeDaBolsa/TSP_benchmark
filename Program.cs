using System.Globalization;
using TSP_benchmark.Generators;

Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");


var BASE_DIR = "C:/Users/Guilh/Documents/CodeProjects/TSP_benchmark/GeneratedTests";
var TSP_FOLDER_PREFIX = "Random_";
var TSP_FILE_PREFIX = "Random";

var MIN_TSP_SIZE = 5;
var MAX_TSP_SIZE = 100;

var NUMBER_OF_SAMPLES = 20;

var MAX_CITY_X_COORD_PROPORTION = 3;
var MAX_CITY_Y_COORD_PROPORTION = 3;

TSPLibFileWriter.WriteSamples(
    BASE_DIR,
    TSP_FOLDER_PREFIX,
    TSP_FILE_PREFIX,
    NUMBER_OF_SAMPLES,
    MIN_TSP_SIZE,
    MAX_TSP_SIZE,
    MAX_CITY_X_COORD_PROPORTION,
    MAX_CITY_Y_COORD_PROPORTION);

