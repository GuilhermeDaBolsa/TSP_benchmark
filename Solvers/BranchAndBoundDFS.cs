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

        float curr_bound = 0;
        Array.Fill(curr_path, -1);
        Array.Fill(visited, false);

        for (int i = 0; i < N; i++)
            curr_bound += (firstMin(adj, i) + secondMin(adj, i));

        curr_bound = (curr_bound == 1) ? curr_bound / 2 + 1 : curr_bound / 2;

        visited[0] = true;
        curr_path[0] = 0;

        TSPRec(adj, curr_bound, 0, 1, curr_path);
    }


    void TSPRec(float[,] adj, float curr_bound, float curr_weight, int level, int[] curr_path) {

        if (level == N) {
            if (adj[curr_path[level - 1], curr_path[0]] != 0) {
                float curr_res = curr_weight + adj[curr_path[level - 1], curr_path[0]];

                if (curr_res < final_res) {
                    copyToFinal(curr_path);
                    final_res = curr_res;
                }
            }
            return;
        }

        for (int i = 0; i < N; i++) {
            if (adj[curr_path[level - 1], i] != 0 && visited[i] == false) {
                float temp = curr_bound;
                curr_weight += adj[curr_path[level - 1], i];

                if (level == 1)
                    curr_bound -= ((firstMin(adj, curr_path[level - 1]) + firstMin(adj, i)) / 2);
                else
                    curr_bound -= ((secondMin(adj, curr_path[level - 1]) + firstMin(adj, i)) / 2);

                if (curr_bound + curr_weight < final_res) {
                    curr_path[level] = i;
                    visited[i] = true;

                    TSPRec(adj, curr_bound, curr_weight, level + 1, curr_path);
                }

                curr_weight -= adj[curr_path[level - 1], i];
                curr_bound = temp;

                Array.Fill(visited, false);
                for (int j = 0; j <= level - 1; j++)
                    visited[curr_path[j]] = true;
            }
        }
    }


    void copyToFinal(int[] curr_path) {
        for (int i = 0; i < N; i++)
            final_path[i] = curr_path[i];

        final_path[N] = curr_path[0];
    }


    float firstMin(float[,] adj, int i) {
        float min = float.MaxValue;

        for (int k = 0; k < N; k++)
            if (adj[i, k] < min && i != k)
                min = adj[i, k];

        return min;
    }


    float secondMin(float[,] adj, int i) {
        float first = float.MaxValue;
        float second = float.MaxValue;

        for (int j = 0; j < N; j++) {
            if (i == j)
                continue;

            if (adj[i, j] <= first) {
                second = first;
                first = adj[i, j];
            } else if (adj[i, j] <= second && adj[i, j] != first)
                second = adj[i, j];
        }
        return second;
    }
}