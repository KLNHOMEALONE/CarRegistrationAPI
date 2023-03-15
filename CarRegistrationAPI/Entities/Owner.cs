namespace CarRegistrationAPI.Entities
{
    public class Owner
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Middlename { get; set; }
        public DateTime Birthdate { get; set; }
        public string Address { get; set; }
    }
}
