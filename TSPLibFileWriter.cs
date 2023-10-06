namespace TSP_TESTS {

    public class TSPLibFileWriter {


        public void WriteFile(string directory, string fileNamePrefix, int fileNumber, List<TSP_City> cities) {

            var fileName = $"{fileNamePrefix}_{cities.Count}_{fileNumber}";

            using (StreamWriter writer = File.CreateText($"{directory}/{fileName}.txt")) {
                writer.WriteLine($"NAME: {fileName}");
                writer.WriteLine("TYPE: TSP");
                writer.WriteLine($"DIMENSION: {cities.Count}");
                writer.WriteLine("EDGE_WEIGHT_TYPE: EUC_2D");
                writer.WriteLine("NODE_COORD_SECTION");

                for (int i = 0; i < cities.Count; i++) {
                    writer.WriteLine($"{i + 1} {cities[i].X} {cities[i].Y}");
                }

                writer.WriteLine("EOF");
            }
        }

    }
}
