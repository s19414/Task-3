using APBD3.Models;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace APBD3.Services
{
    public static class Services
    {
        private const string DATABASE_PATH = @"Data\data.csv";
        public static List<Student> studentList;
        
        //API FUNCTIONS
        public static string getStudentsJson()
        {
            getStudentsFromDB();
            return JsonSerializer.Serialize(studentList);
        }

        public static string getStudentJson(string indexNumber)
        {
            getStudentsFromDB();
            foreach (Student student in studentList)
            {
                if (student.index.Equals(indexNumber))
                {
                    return JsonSerializer.Serialize<Student>(student);
                }
            }
            return null;
        }

        public static bool updateStudent(string indexNumber, Student updatedStudent)
        {
            getStudentsFromDB();
            foreach(Student student in studentList)
            {
                if (indexNumber.Equals(student.index)) { 
                    studentList.Remove(student);
                    studentList.Add(updatedStudent);
                    updateDBFromStudentList();
                    return true;
                }
            }
            return false;
        }

        public static bool addNewStudent(Student newStudent)
        {
            getStudentsFromDB();
            string newIndex = newStudent.index;
            //does new student have properly formatted index?
            string indexPattern = @"^s\d{4,7}$";
            if(!Regex.IsMatch(newIndex, indexPattern)){
                return false;
            }
            //is new student index a duplicate?
            foreach(Student student in studentList)
            {
                if (newIndex.Equals(student.index)) {
                    return false;
                }
            }
            //add new student to DB
            addStudentToDB(newStudent);
            return true;
        }

        public static bool deleteStudent(string indexNumber)
        {
            getStudentsFromDB();
            foreach(Student student in studentList)
            {
                if (student.index.Equals(indexNumber)) {
                    studentList.Remove(student);
                    updateDBFromStudentList();
                    return true;
                }
            }
            return false;
        }
        //HELPER FUNCTIONS
        //Populates studentList with database entries. Should be called before any operations on studentList
        public static void getStudentsFromDB()
        {
            studentList = new List<Student>();
            foreach ( string row in File.ReadLines(DATABASE_PATH))
            {
                string[] splitRow = row.Split(",");
                studentList.Add(new Student(splitRow[0], splitRow[1], splitRow[2], splitRow[3], splitRow[4],
                    splitRow[5], splitRow[6], splitRow[7], splitRow[8]));
            }
            
        }

        //rewrites data.csv based on studentList
        public static void updateDBFromStudentList()
        {
            File.WriteAllText(DATABASE_PATH, "");
            foreach (Student student in studentList)
            {
                addStudentToDB(student);
            }
        }

        //Append new student to database
        public static void addStudentToDB(Student student)
        {
            string newRow = student.FirstName + "," + student.LastName + "," + student.index + "," + student.birthdate + "," +
                student.studies + "," + student.mode + "," + student.email + "," + student.fathersName + "," + student.mothersName + "\n";
            File.AppendAllText(DATABASE_PATH, newRow);
        }

        //Are all Student fields populated?
        public static bool studentHasEmptyFields(Student student)
        {
            if (student.FirstName.Equals("") || student.LastName.Equals("") || student.index.Equals("") ||
                student.birthdate.Equals("") || student.studies.Equals("") || student.mode.Equals("") ||
                student.email.Equals("") || student.fathersName.Equals("") || student.mothersName.Equals(""))
            {
                return true;
            }
            return false;
        }
    }
}
