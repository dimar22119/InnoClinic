namespace ProfilesService.Domain.Entities
{
    public class Patient
    {
        public Guid Id { get; private set; }
        public Guid AccountId { get; private set; }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime BirthDate { get; private set; }

        private Patient() { }

        public Patient(Guid accountId, string firstName, string lastName, DateTime birthDate)
        {
            Id = Guid.NewGuid();
            AccountId = accountId;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
        }

        public void Update(string firstName, string lastName, DateTime birthDate)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
        }
    }
}
