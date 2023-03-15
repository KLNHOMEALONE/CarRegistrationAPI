namespace CarRegistrationAPI.Entities
{
    public class Certificate
    {
        public int Id { get; set; }
        public int Ownerid { get; set; }
        public string Registrationplate { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public string Vin { get; set; }
        public string Type { get; set; }
        public int Maxmass { get; set; }
        public DateTime Issuedate { get; set; }
        public string Issuer { get; set; }
    }
}
