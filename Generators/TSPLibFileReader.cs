using System.Globalization;

public class TSPLibFileReader {

    public static TSP Import(string fileCompletePath) {
        var tsp = new TSP();
        ReadTSPFile(ref tsp, fileCompletePath);
        //ReadOptTourFile(ref tsp, fileFolderPath, fileName);

        return tsp;
    }

    private static void ReadTSPFile(ref TSP tsp, string fileCompletePath) {
        if (!File.Exists(fileCompletePath))
            throw new Exception("TSP file does not exist: " + fileCompletePath);

        IEnumerable<string> lines = File.ReadLines(fileCompletePath);
        var linesIterator = lines.GetEnumerator();

        ReadTSPFileSpecificationPart(ref tsp, ref linesIterator);
        ReadTSPFileDataPart(ref tsp, ref linesIterator);
    }

    private static void ReadOptTourFile(ref TSP tsp, string fileFolderPath, string fileName) {
        string fileCompletePath = fileFolderPath + fileName + ".opt.tour";

        if (File.Exists(fileCompletePath)) {
            IEnumerable<string> lines = File.ReadLines(fileCompletePath);
            var linesIterator = lines.GetEnumerator();

            ReadOptTourFileSpecificationPart(ref linesIterator);
            //ReadOptTourFileDataPart(ref tsp, ref linesIterator);
        }
    }


    private static void ReadTSPFileSpecificationPart(ref TSP tsp, ref IEnumerator<string> iterator) {
        while (iterator.MoveNext()) {
            string line = iterator.Current;

            string[] keyValueArray = line.Split(':');

            if (keyValueArray.Length == 0)
                return;

            string key = keyValueArray[0].Trim();
            string value = keyValueArray.Length > 1 ? keyValueArray[1].Trim() : "";

            switch (key) {
                case "NAME": tsp.name = value; break;
                case "TYPE": tsp.type = value; break;
                case "COMMENT": break;
                case "DIMENSION":
                    tsp.size = int.Parse(value);
                    tsp.cities = new List<TSP_City>(tsp.size);
                    break;

                case "CAPACITY": break;
                case "EDGE_WEIGHT_TYPE": break;
                case "EDGE_WEIGHT_FORMAT": break;
                case "EDGE_DATA_FORMAT": break;
                case "NODE_COORD_TYPE": break;
                case "DISPLAY_DATA_TYPE": break;

                default: return;
            }
        }
    }

    private static void ReadTSPFileDataPart(ref TSP tsp, ref IEnumerator<string> iterator) {
        string line = iterator.Current;

        if (!line.Contains("NODE_COORD_SECTION"))
            throw new Exception("Cannot read TSP file because it does not contain the NODE_COORD_SECTION");

        while (iterator.MoveNext()) {
            line = iterator.Current.Trim();
            string[] IXYarray = line.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            if (IXYarray.Length != 3)
                return;

            int I = int.Parse(IXYarray[0]) - 1;
            float X = float.Parse(IXYarray[1], CultureInfo.InvariantCulture.NumberFormat);
            float Y = float.Parse(IXYarray[2], CultureInfo.InvariantCulture.NumberFormat);

            tsp.cities.Add(new TSP_City { X = X, Y = Y, index = I });
        }
    }

    private static void ReadOptTourFileSpecificationPart(ref IEnumerator<string> iterator) {
        while (iterator.MoveNext()) {
            string line = iterator.Current;

            string[] keyValueArray = line.Split(':');

            if (keyValueArray.Length == 0)
                return;

            string key = keyValueArray[0].Trim();

            switch (key) {
                case "NAME": break;
                case "TYPE": break;
                case "COMMENT": break;
                case "DIMENSION": break;
                case "CAPACITY": break;
                case "EDGE_WEIGHT_TYPE": break;
                case "EDGE_WEIGHT_FORMAT": break;
                case "EDGE_DATA_FORMAT": break;
                case "NODE_COORD_TYPE": break;
                case "DISPLAY_DATA_TYPE": break;

                default: return;
            }
        }
    }

    /*private static void ReadOptTourFileDataPart(ref TSP tsp, ref IEnumerator<string> iterator) {
        string line = iterator.Current;

        if (!line.Contains("TOUR_SECTION"))
            return;

        while (iterator.MoveNext() && iterator.Current != "EOF") {
            line = iterator.Current.Trim();
            string[] tour = line.Split(' ');

            foreach (string index in tour) {

                if (string.IsNullOrEmpty(index))
                    continue;

                int indexValue = int.Parse(index);

                if (indexValue == -1)
                    break;

                tsp.m_BestTourIndexes.Add(indexValue - 1);
            }
        }

        tsp.m_BestTourIndexes.Add(tsp.m_BestTourIndexes[0]);
    }*/
}