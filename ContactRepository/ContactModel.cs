namespace ContactRepo
{
    public class ContactModel
    {
        public ContactModel()
        {
        }

        public int Id { get; internal set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool Active { get; set; }
    }
}