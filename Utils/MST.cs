using Graph;

public class MST {

    /* FROM https://github.com/mikymaione/Held-Karp-algorithm */

    public static void PrimsGraphed(float[,] distance, Graph.Graph G, int r_id) {

        HashSet<int> S = new HashSet<int>(); // elements available

        // sorted min queue
        SortedSet<Node> Q = new SortedSet<Node>(Comparer<Node>.Create((lhs, rhs) => {
            if (lhs.key < rhs.key)
                return -1;
            if (lhs.key > rhs.key)
                return 1;

            return 0;
        }));

        foreach (var u in G.V) {
            u.key = float.MaxValue;
            u.π = null;

            S.Add(u.id);
        }

        var r = G.NodeById(r_id);
        r.key = 0;
        Q.Add(r);

        while (Q.Count > 0) {
            var u = Q.Min; // min
            Q.Remove(u);
            S.Remove(u.id);

            foreach (var v in G.Adj[u])
                if (u.id != v.id)
                    if (S.Contains(v.id) && distance[u.id, v.id] < v.key) {
                        Q.Remove(v);

                        v.π = u;
                        v.key = distance[u.id, v.id];

                        Q.Add(v);
                    }
        }
    }
}
