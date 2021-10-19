namespace RESTAPI.Model
{
    public class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int PersonalId { get; set; }
        public string DateOfBirth { get; set; }
        public int Number { get; set; }
        public string Email { get; set; }
        public Flat Flat { get; set; }

        public Person(string name, string surname, int personalId, string dateOfBirth, int number, string email, Flat flat)
        {
            Name = name;
            Surname = surname;
            PersonalId = personalId;
            DateOfBirth = dateOfBirth;
            Number = number;
            Email = email;
            Flat = flat;
        }
    }
}