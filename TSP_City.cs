public class TSP_City {

    //X COORDINATE
    public float X;

    //Y COORDINATE
    public float Y;

    //CITY'S INDEX OF IT'S "ORIGINAL" LIST
    public long index;


    public float DistanceTo(TSP_City other) {
        return (float)Math.Sqrt(
            (this.X - other.X) * (this.X - other.X) + 
            (this.Y - other.Y) * (this.Y - other.Y)
        );
    }
}
