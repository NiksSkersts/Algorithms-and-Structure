namespace RESTAPI.Model
{
    public class House
    {
        public int Nr { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalIndex { get; set; }

        public House(int nr, string street, string city, string country, string postalIndex)
        {
            Nr = nr;
            Street = street;
            City = city;
            Country = country;
            PostalIndex = postalIndex;
        }
    }
}