using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetrieveStudentsDataWithSearchingSorting
{

    class Student
    {
        public string Name { get; set; }
        public string Class { get; set; }
    }

    class Program
    {
        static void Main()
        {
            List<Student> students = LoadStudentData("A:/FileHandling/StudentData.txt");

            if (students.Count == 0)
            {
                Console.WriteLine("No student data found.");
            }
            else
            {
                Console.WriteLine("Student Data:");
                DisplayStudentData(students);

                Console.WriteLine("\nSorted Students Data By Name:");
                students = SortStudentsByName(students);
                DisplayStudentData(students);

                Console.WriteLine("\nSearch for a student by name:");
                string searchName = Console.ReadLine();
                SearchAndDisplayStudent(students, searchName);
            }
        }

        static List<Student> LoadStudentData(string filePath)
        {
            List<Student> students = new List<Student>();

            try
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 2)
                    {
                        students.Add(new Student
                        {
                            Name = parts[0].Trim(),
                            Class = parts[1].Trim()
                        });
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while reading the file: {e.Message}");
            }

            return students;
        }

        static void DisplayStudentData(List<Student> students)
        {
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("|      Name        |        Class        |");
            Console.WriteLine("------------------------------------------");
            foreach (var student in students)
            {
                Console.WriteLine($"| {student.Name,-15} | {student.Class,-20} |");
            }
            Console.WriteLine("------------------------------------------");
        }

        static List<Student> SortStudentsByName(List<Student> students)
        {
            return students.OrderBy(student => student.Name).ToList();
        }

        static void SearchAndDisplayStudent(List<Student> students, string searchName)
        {
            var searchResults = students.Where(student => student.Name.ToLower().Contains(searchName.ToLower())).ToList();

            if (searchResults.Count > 0)
            {
                Console.WriteLine($"Search Results for '{searchName}':");
                DisplayStudentData(searchResults);
            }
            else
            {
                Console.WriteLine($"No students found with the name '{searchName}'.");
            }
        }
    }
}