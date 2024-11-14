using System;
using System.IO;
namespace BinaryConverter;
class Program
{
    static void Main()
    {
        string file = "C:\\Users\\idiocy\\Desktop\\students.dat";
        Sort(Convert(file));
    }
    static List<Student>? Convert(string file)
    {
        if (File.Exists(file))
        {
            List<Student> students = new();
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            using (BinaryReader br = new BinaryReader(fs))
            {
                while (fs.Position < fs.Length)
                {
                    Student student = new Student();
                    student.Name = br.ReadString();
                    student.Group = br.ReadString();
                    student.DateOfBirth = DateTime.FromBinary(br.ReadInt64());
                    student.AverageScore = br.ReadDecimal();
                    students.Add(student);
                }
            }
            return students;
        }
        return null;
    }
    static void Sort(List<Student> students)
    {
        string path = "C:\\Users\\idiocy\\Desktop\\Students";
        if (!Directory.Exists(path)) { Directory.CreateDirectory(path); }
        foreach (Student student in students)
        {
            string pathGroup = Path.Combine(path, student.Group) + ".txt";
            using (StreamWriter sw = File.AppendText(pathGroup))
            {
            if (!File.Exists(pathGroup)) { File.Create(pathGroup); }
            sw.WriteLine($"{student.Name} ({student.DateOfBirth}): {student.AverageScore};");
            }
        }
    }
}