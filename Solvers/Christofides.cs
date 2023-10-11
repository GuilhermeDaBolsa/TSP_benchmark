/* THIS CODE IS MOSTLY FROM https://github.com/mikymaione/Held-Karp-algorithm */

class Christofides : Solver {

    public List<TSP_City> Solve(List<TSP_City> cities) {

        //-- CALCULATE COST MATRIX AND MAKE A GRAPH OF IT --//

        float[,] costMatrix = new float[cities.Count, cities.Count];

        for (int i = 0; i < cities.Count; i++) {
            for (int j = 0; j < cities.Count; j++) {
                costMatrix[i, j] = cities[i].DistanceTo(cities[j]);
            }
        }

        Graph.Graph G = new Graph.Graph(costMatrix);


        //-- CALCULATE MINIMUM SPANNING TREE (MST) --//

        List<HashSet<int>> mst = new List<HashSet<int>>(cities.Count);
        for (int i = 0; i < cities.Count; i++) {
            mst.Add(new HashSet<int>());
        }

        MST.PrimsGraphed(costMatrix, G, 0);

        foreach (var u in G.V) {
            if (u.π != null) {
                mst[u.id].Add(u.π.id);
                mst[u.π.id].Add(u.id);
            }
        }


        //-- ODD VERICES FROM MST --//

        HashSet<int> oddVertices = new HashSet<int>(cities.Count);

        for (int i = 0; i < mst.Count; i++) {
            if (mst[i].Count % 2 != 0)
                oddVertices.Add(i);
        }


        //-- INDUCED SUB GRAPH FROM ODD VERTICES --//

        Graph.Graph IG = new Graph.Graph(oddVertices);
        IG.MakeConnected(costMatrix);


        //-- PERFECT MATCHING (BLOSSOM) --//

        BlossomMatching blossom = new BlossomMatching();
        var M = blossom.Solve(G);



        //-- COMBINE (multigraph) --//

        var H = mst; //copy (TODO MAYBE ITS NOT WORKING)

        foreach (var m in M) {
            if (!H[m.from.id].Contains(m.to.id))
                H[m.from.id].Add(m.to.id);

            if (!H[m.to.id].Contains(m.from.id))
                H[m.to.id].Add(m.from.id);
        }


        //-- SHORTCUT (make a eulerian circuit) --//

        List<int> E = new List<int>();
        HashSet<int> visited = new HashSet<int>();

        RecursiveHamiltonian(H, E, visited, 0);

        E.Add(0);

        //E IS THE ANSWER HERE
        return null;
    }


    private void RecursiveHamiltonian(List<HashSet<int>> H, List<int> E, HashSet<int> visited, int c) {
        visited.Add(c);
        E.Add(c);

        foreach (var e in H[c])
            if (!visited.Contains(e))
                RecursiveHamiltonian(H, E, visited, e);
    }
}