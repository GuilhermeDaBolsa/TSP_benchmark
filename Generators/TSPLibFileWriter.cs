public class TSPLibFileWriter
{

    public static void WriteFile(string directory, string namePrefix, int fileNumber, List<TSP_City> cities)
    {

        var fileName = $"{namePrefix}_{cities.Count}_{fileNumber}";

        using (StreamWriter writer = File.CreateText($"{directory}/{fileName}.tsp"))
        {
            writer.WriteLine($"NAME: {fileName}");
            writer.WriteLine("TYPE: TSP");
            writer.WriteLine($"DIMENSION: {cities.Count}");
            writer.WriteLine("EDGE_WEIGHT_TYPE: EUC_2D");
            writer.WriteLine("NODE_COORD_SECTION");

            for (int i = 0; i < cities.Count; i++)
            {
                writer.WriteLine($"{i + 1} {cities[i].X} {cities[i].Y}");
            }

            writer.WriteLine("EOF");
        }
    }


    public static void WriteSamples(
        string SAMPLES_BASE_DIR,
        string SAMPLES_FOLDER_PREFIX,
        string SAMPLES_FILE_PREFIX,
        int NUM_OF_SAMPLES,
        int MIN_SAMPLE_SIZE,
        int MAX_SAMPLE_SIZE,
        float MAX_SAMPLE_X_COORD_PROPORTION,
        float MAX_SAMPLE_Y_COORD_PROPORTION
    )
    {
        for (int tspSize = MIN_SAMPLE_SIZE; tspSize <= MAX_SAMPLE_SIZE; tspSize++)
        {
            var newDirectory = $"{SAMPLES_BASE_DIR}/{SAMPLES_FOLDER_PREFIX}{tspSize}";
            Directory.CreateDirectory(newDirectory);

            var TSPGenerator = new TSPGenerator(
                tspSize,
                tspSize * MAX_SAMPLE_X_COORD_PROPORTION,
                tspSize * MAX_SAMPLE_Y_COORD_PROPORTION);

            for (int sample = 0; sample < NUM_OF_SAMPLES; sample++)
            {
                var cities = TSPGenerator.GenerateRandomCities();
                WriteFile(newDirectory, SAMPLES_FILE_PREFIX, sample + 1, cities);
            }
        }
    }

}