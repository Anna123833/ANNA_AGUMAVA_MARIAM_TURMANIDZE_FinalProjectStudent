using ANNA_AGUMAVA_MARIAM_TURMANIDZE_FinalProjectStudent.Service;

namespace ANNA_AGUMAVA_MARIAM_TURMANIDZE_FinalProjectStudent.Models;
/// <summary>
/// Manages student records, including searching, sorting, and persistence.
/// </summary>
internal class StudentManagement
{
    private readonly List<Student> students;
    private readonly StudentService? service;
    /// <summary>Initializes student management using data from a service.</summary>
    /// <param name="service">The service used to load and save student data.</param>
    public StudentManagement(StudentService service)
    {
        this.service = service;
        students = service.GetStudents();
    }
    /// <summary>Initializes student management with the provided student list.</summary>
    /// <param name="students">The list of students to manage.</param>
    public StudentManagement(List<Student> students)
    {
        this.students = students;
    }
    /// <summary>
    /// Adds a student, assigns a roll number, and saves the data if a service is available.
    /// </summary>
    /// <param name="student"></param>
    /// <returns></returns>
    public async Task AddStudentAsync(Student student)
    {
        int studentsInCourse = students.Count(s => s.Course == student.Course);
        student.RollNumber = $"R{(int)student.Course}_{studentsInCourse / 20 + 1}";
        students.Add(student);
        if(service != null)
        {
            await service.SaveDataAsync();
        };
    }

    /// <summary>Returns a read-only view of all students.</summary>
    public IReadOnlyList<Student> GetAllStudents()
    {
        return students.AsReadOnly();
    }
    /// <summary>Returns students ordered by increasing age.</summary>
    public List<Student> GetStudentsByIncreasingAge()
    {
        var sortedstudentsByIncreasingAge = students.OrderBy(s=>s.Age).ToList();
        return sortedstudentsByIncreasingAge;
    }
    /// <summary>Returns students ordered by decreasing age.</summary>
    public List<Student> GetStudentsByDecreasingAge()
    {
        var sortedstudentsByDecreasingAge = students.OrderByDescending(s => s.Age).ToList();
        return sortedstudentsByDecreasingAge;
    }
    /// <summary>Returns students ordered by decreasing grade.</summary>
    public List<Student> GetStudentsByDecreasingGrade()
    {
        var sortedstudentsByDecreasingGrade = students.OrderBy(s => s.Grade).ToList();
        return sortedstudentsByDecreasingGrade;
    }
    /// <summary>Returns students ordered by increasing grade.</summary>
    public List<Student> GetStudentsByIncreasingGrade()
    {
        var sortedstudentsByIncreasingGrade = students.OrderByDescending(s => s.Grade).ToList();
        return sortedstudentsByIncreasingGrade;
    }
    /// <summary>Returns up to 16 students with an A grade and prints their names.</summary>
    public List<string> GetBestStudents()
    {
        var beststudents = students.Where(s => s.Grade == 'A').Select(s => s.StudentName).Take(16).ToList();

        for (var i = 0; i < beststudents.Count; i++)
        {
            Console.WriteLine($"{i+1}. {beststudents[i]} ");
        }
        return beststudents;
    }
    /// <summary>Updates a student's grade and saves the data if a service is available.</summary>
    /// <param name="id">The student's ID.</param>
    /// <param name="newGrade">The grade to assign.</param>
    /// <returns><see langword="true"/> if the student was found; otherwise, <see langword="false"/>.</returns>
    public async Task<bool> UpdateStudentGradeAsync(int id, char newGrade)
    {
        Student? student = GetStudentById(id);
        if (student == null)
        {
            return false;
        }
        student.Grade = newGrade;
        if(service != null)
        {
            await service.SaveDataAsync();
        }
        return true;
    }
    /// <summary>Returns the first student with the specified grade.</summary>
    public Student? GetStudentByGrade(char grade)
    {
        Student? result = null;
        foreach (Student item in students)
        {
            if (item.Grade == grade)
            {
                result = item;
                break;
            }
        }
        return result;
    }
    /// <summary>Returns the first student with the specified name.</summary>
    public Student? GetStudentByName(string studentName)
    {
        Student? result = null;
        foreach (Student item in students)
        {
            if (item.StudentName == studentName)
            {
                result = item;
                break;
            }
        }
        return result;
    }
    /// <summary>Returns the first student from the specified school.</summary>
    public Student? GetStudentBySchoolName(string schoolName)
    {
        Student? result = null;
        foreach (Student item in students)
        {
            if (item.SchoolName == schoolName)
            {
                result = item;
                break;
            }
        }
        return result;
    }
    /// <summary>Returns the student with the specified ID.</summary>
    public Student? GetStudentById(int id)
    {
        Student? result = null;
        foreach (Student item in students)
        {
            if (item.Id == id)
            {
                result = item;
                break;
            }
        }
        return result;
    }
    /// <summary>Returns the first student with the specified birth date.</summary>

    public Student? GetStudentByBirthDate(DateOnly birthdateDate)
    {
        Student? result = null;
        foreach (Student item in students)
        {
            if (item.BirthDate == birthdateDate)
            {
                result = item;
                break;
            }
        }
        return result;
    }
    /// <summary>Returns the first student enrolled in the specified course.</summary>
    public Student? GetStudentByCourse(LearningCourse course)
    {
        Student? result = null;
        foreach (Student item in students)
        {
            if (item.Course == course)
            {
                result = item;
                break;
            }
        }
        return result;
    }
    /// <summary>Deletes a student by ID and saves the data if a service is available.</summary>
    /// <param name="id">The ID of the student to delete.</param>
    /// <returns><see langword="true"/> if a student was deleted; otherwise, <see langword="false"/>.</returns>
    public async Task<bool> DeleteStudentByIdAsync(int id)
    {
        Student? student = GetStudentById(id);
        if (student == null)
        {
            return false;
        }
        students.Remove(student);
        if (service != null)
        {
            await service.SaveDataAsync();
        }
        return true;
    }
    /// <summary>Returns all students enrolled in the specified course.</summary>
    public List<Student> GetStudentsByCourse(LearningCourse course)
    {
        return students.Where(s => s.Course == course).ToList();
    }
    /// <summary>Displays available courses and prompts the user to select one.</summary>
    /// <returns>The selected course.</returns>
    public LearningCourse ReadCourse()
    {
        LearningCourse[] courses = Enum.GetValues<LearningCourse>();
        for(int i = 0; i < courses.Length; i++)
        {
            Console.WriteLine($"{i + 1}.{courses[i]}");
        }
        Console.WriteLine("Select Course: 1 ICTLiteracy,  2 BasicsOfProgramming,  3 ObjectOrientedProgramming,  4 ComputerArchitectureAndOrganization,  5 DataStructures,  6 Database,  7 DataAnalysisAndStatistics,  8 OperatingSystems,  9 ModelingAndSimulation,  10 Algorithms,  11 SoftwareEngineering,  12 MathematicalProgramming,  13 NetworkTechnologiesAndCommunications,  14 WebProgramming,  15 FunctionalProgramming,  16 OperationsResearch,  17 ProjectPreparation,  18 ArtificialIntelligence,  19 ComputerLawAndEthics,  20 TeamProjects ");
        Console.WriteLine("Select Course: ");
        int choice;
        while(!int.TryParse(Console.ReadLine(), out choice) || choice<1 || choice>courses.Length)
        {
            Console.WriteLine($"Wrong Choice, select a number again from 1 to {courses.Length}");
        }
        return courses[choice - 1];
    }

}