namespace Nexus.Entities
{
    public class State
    {
        public int StateID { get; set; }
        public string StateName { get; set; }

        // Navigation property
        public ICollection<City> Cities { get; set; }
    }

}
