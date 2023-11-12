# TSP_benchmark

This project (Final Paper) contains 3 TSP algorithms:
- Branch and Bound depth-first
- Branch and Bound best-bound
- Christofides

The implementation of them can be found inside /Solvers folder.

It also contains every test (TSP instance) that was generated, as well as the generator.
Every possible generator is inside /Generators folder and the generated tests are inside /GeneratedTests folder.

## Tests and Results

Inside /GeneratedTests folder you will find 20 folders named from Random_5 to Random_100. A folder Random_X contains 5 TSP instances with X vertices each.
Also, inside of these folders there is a folder with the name of each algorith plus Concorde, another algorithm for solving TSPs. Inside of these, there are the results (performance and size) of each instance analised by that algoritm in a file named Random_X_Y.ans - Y beeing the number of the TSP instance.



Final paper -> https://docs.google.com/document/d/17ijemtNwPVvy-hh51rnbyE6NJtPMpJ9YW_O42uqBRro/edit?usp=sharing
Presentation -> Soon...
