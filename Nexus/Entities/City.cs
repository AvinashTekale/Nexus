namespace Nexus.Entities
{
    public class City
    {
        public int CityID { get; set; }
        public string CityName { get; set; }

        // Foreign key
        public int StateID { get; set; }

        // Navigation property
        public State State { get; set; }
    }

}
