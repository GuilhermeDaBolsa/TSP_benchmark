namespace TSP_TESTS {

    public class TSPGenerator {

        private Random m_RandomNumberGenerator;
        private float m_CoordXMaxBound;
        private float m_CoordYMaxBound;
        private int m_NumOfCities;

        public TSPGenerator(int numOfCities, float maxX, float maxY) {
            m_RandomNumberGenerator = new Random();
            m_NumOfCities = numOfCities;
            m_CoordXMaxBound = maxX;
            m_CoordYMaxBound = maxY;
        }

        public List<TSP_City> GenerateRandomCities() {
            var cities = new List<TSP_City>();

            for(int i = 0; i < m_NumOfCities; i++) {
                var city = GenerateRandomCity();
                city.index = i;

                cities.Add(city);
            }

            return cities;
        }

        private TSP_City GenerateRandomCity() {
            var xRandomCoordBounded = m_RandomNumberGenerator.NextSingle() * m_CoordXMaxBound;
            var yRandomCoordBounded = m_RandomNumberGenerator.NextSingle() * m_CoordYMaxBound;

            return new TSP_City() { X = xRandomCoordBounded, Y = yRandomCoordBounded };
        }

    }

}
