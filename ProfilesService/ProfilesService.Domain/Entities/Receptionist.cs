namespace ProfilesService.Domain.Entities
{
    public class Receptionist
    {
        public Guid Id { get; private set; }
        public Guid AccountId { get; private set; }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        private Receptionist() { }

        public Receptionist(Guid accountId, string firstName, string lastName)
        {
            Id = Guid.NewGuid();
            AccountId = accountId;
            FirstName = firstName;
            LastName = lastName;
        }
        public void Update(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
