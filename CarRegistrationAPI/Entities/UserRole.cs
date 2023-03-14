namespace CarRegistrationAPI.Entities
{
    public class UserRole
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string normalized_name     { get; set; }
        public string concurrency_stamp  { get; set; }
    }
}
