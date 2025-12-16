using ProfilesService.Domain.ValueObjects;

namespace ProfilesService.Domain.Entities
{
    public class Doctor
    {
        public Guid Id { get; private set; }
        public Guid AccountId { get; private set; }

        public Guid SpecializationId { get; private set; }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public int CareerYearStart { get; private set; }
        public DoctorStatus Status { get; private set; }

        private Doctor() { }

        public Doctor(Guid accountId, Guid specializationId, string firstName, string lastName, int careerYearStart, DoctorStatus doctorStatus)
        {
            Id = Guid.NewGuid();
            AccountId = accountId;
            SpecializationId = specializationId;
            FirstName = firstName;
            LastName = lastName;
            CareerYearStart = careerYearStart;
            Status = doctorStatus;
        }

        public void Update(string firstName, string lastName, int careerYearStart, DoctorStatus status)
        {
            FirstName = firstName;
            LastName = lastName;
            CareerYearStart = careerYearStart;
            Status = status;
        }
    }
}
