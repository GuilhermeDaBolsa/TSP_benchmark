/* THIS CODE IS MOSTLY FROM geeksforgeeks.org (Rohit Pradhan) */

class BranchAndBoundDFS : Solver {

    int N;
    int[] final_path;
    bool[] visited;
    float final_res;

    public List<TSP_City> Solve(List<TSP_City> cities) {

        N = cities.Count;
        final_path = new int[N + 1];
        visited = new bool[N];
        final_res = float.MaxValue;

        float[,] adj = new float[N, N];

        for (int i = 0; i < N; i++) {
            for (int j = i; j < N; j++) {

                float distance = i == j ?
                    float.MaxValue : cities[i].DistanceTo(cities[j]);

                adj[i, j] = distance;
                adj[j, i] = distance;
            }
        }

        TSP(adj);

        List<TSP_City> final_ = new List<TSP_City>(final_path.Length);

        foreach (int i in final_path) {
            final_.Add(cities[i]);
        }

        return final_;
    }


    void TSP(float[,] adj) {
        int[] curr_path = new int[N + 1];

        Array.Fill(curr_path, -1);
        Array.Fill(visited, false);

        visited[0] = true;
        curr_path[0] = 0;

        TSPRec(adj, 0, 1, curr_path);
    }


    void TSPRec(float[,] adj, float curr_weight, int level, int[] curr_path) {

        if (level == N) {
            if (adj[curr_path[level - 1], curr_path[0]] != float.MaxValue) {
                float curr_res = curr_weight + adj[curr_path[level - 1], curr_path[0]];

                if (curr_res < final_res) {
                    copyToFinal(curr_path);
                    final_res = curr_res;
                }
            }
            return;
        }

        for (int i = 0; i < N; i++) {
            if (adj[curr_path[level - 1], i] != float.MaxValue && !visited[i]) {
                float next_weight = curr_weight + adj[curr_path[level - 1], i];

                if (next_weight < final_res) {
                    curr_path[level] = i;
                    visited[i] = true;

                    TSPRec(adj, next_weight, level + 1, curr_path);

                    visited[i] = false;
                }
            }
        }
    }

    void copyToFinal(int[] curr_path) {
        for (int i = 0; i < N; i++)
            final_path[i] = curr_path[i];

        final_path[N] = curr_path[0];
    }
}