namespace APBD3.Models
{
    [Serializable]
    public class Student
    {
        public Student(string firstName, string lastName, string index, string birthdate, string studies, string mode, string email, string fathersName, string mothersName)
        {
            FirstName = firstName;
            LastName = lastName;
            this.index = index;
            this.birthdate = birthdate;
            this.studies = studies;
            this.mode = mode;
            this.email = email;
            this.fathersName = fathersName;
            this.mothersName = mothersName;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string index { get; set; }
        public string birthdate { get; set; }
        public string studies { get; set; }
        public string mode { get; set; }
        public string email { get; set; }
        public string fathersName { get; set; }
        public string mothersName { get; set; }
    }
}
