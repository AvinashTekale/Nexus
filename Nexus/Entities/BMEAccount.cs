namespace Nexus.Entities
{
    public class BMEAccount:BaseEntity
    {

        public required string Name { get; set; }

        public required string Phone { get; set; }

        public required int HospitalId { get; set; }

        public virtual Hospital Hospital { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

    }
}
