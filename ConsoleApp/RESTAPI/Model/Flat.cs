namespace RESTAPI.Model
{
    public class Flat
    {
        public int Nr { get; set; }
        public int Storey { get; set; }
        public int Rooms { get; set; }
        public int Inhabitants { get; set; }
        public double Area { get; set; }
        public double LivableArea { get; set; }
        public House House{ get; set; } 

        public Flat(int nr, int storey, int rooms, int inhabitants, double area, double livableArea, House house)
        {
            Nr = nr;
            Storey = storey;
            Rooms = rooms;
            Inhabitants = inhabitants;
            Area = area;
            LivableArea = livableArea;
            House = house;
        }
    }
}