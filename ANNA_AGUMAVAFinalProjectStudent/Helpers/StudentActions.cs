using ANNA_AGUMAVA_MARIAM_TURMANIDZE_FinalProjectStudent.Models;
using ANNA_AGUMAVAFinalProjectStudent.Services.Service;

namespace ANNA_AGUMAVA_MARIAM_TURMANIDZE_FinalProjectStudent.Helpers;
/// <summary>
/// Provides console-based actions for managing and displaying students.
/// </summary>
internal class StudentActions
{
    /// <summary>
    /// Provides student data retrieval and management operations.
    /// </summary>
    private readonly StudentManagement studentManagement;
    /// <summary>
    /// Initializes a new instance of the <see cref="StudentActions"/> class.
    /// </summary>
    /// <param name="studentManagement">The service used to manage student records.</param>
    public StudentActions(StudentManagement studentManagement)
    {
        this.studentManagement = studentManagement;
    }
    /// <summary>
    /// Displays all students, or a message if no students are available.
    /// </summary>
    public void ShowAll()
    {
        Console.WriteLine("---- See all Students ----");
        IReadOnlyList<Student> allStudents = studentManagement.GetAllStudents();
        if (allStudents.Count == 0)
        {
            Console.WriteLine("No Students found!");
        }
        else
        {
            foreach (Student student in allStudents)
            {
                ConsoleHelper.PrintStudent(student);
            }
        }
        ConsoleHelper.Pause();
    }
    /// <summary>
    /// Prompts for a student's name and displays the matching student or students, if found.
    /// </summary>
    public void SearchByName()
    {
        Console.WriteLine("---- Search Student with Name ----");
        string name = ConsoleHelper.ReadText("Name: ");
        ShowResults(studentManagement.GetStudentsByName(name));
    }
    /// <summary>
    /// Prompts for a student ID and displays the matching student, if found.
    /// </summary>
    public void SearchById()
    {
        Console.WriteLine("---- Search Student with Id ----");
        int id = ConsoleHelper.ReadInt("Id: ");
        ShowResult(studentManagement.GetStudentById(id));
    }
    /// <summary>
    /// Prompts for a grade and displays a matching student or students, if found.
    /// </summary>
    public void SearchByGrade()
    {
        Console.WriteLine("---- Search student by Grade ----");
        char grade = ConsoleHelper.ReadGrade("Grade: ");
        ShowResults(studentManagement.GetStudentsByGrade(grade));
    }
    /// <summary>
    /// Prompts for a school name and displays a matching student or students, if found.
    /// </summary>
    public void SearchBySchoolName()
    {
        Console.WriteLine("---- Search student by School Name ----");
        string schoolName = ConsoleHelper.ReadText("School Name: ");
        ShowResults(studentManagement.GetStudentsBySchoolName(schoolName));
    }
    /// <summary>
    /// Prompts for a Roll number and displays a matching student or students, if found.
    /// </summary>
    public void SearchByRollNumber()
    {
        Console.WriteLine("---- Search student by Roll Number ----");
        string rollNumber = ConsoleHelper.ReadText("Roll Number R{course}_{group}: ");
        ShowResults(studentManagement.GetStudentsByRollNumber(rollNumber));
    }
    /// <summary>
    /// Prompts for a birth date and displays a matching student or students, if found.
    /// </summary>
    public void SearchByBirthDate()
    {
        Console.WriteLine("---- Search Student with Birth Date ----");
        DateOnly birthDate = ConsoleHelper.ReadDate("Birth Date (yyyy-MM-dd): ");
        ShowResults(studentManagement.GetStudentsByBirthDate(birthDate));
    }
    /// <summary>
    /// Displays the best students according to the student-management service.
    /// </summary>
    public void ShowBest()
    {
        Console.WriteLine("---- Get Best Students ----");
        List<string> best = studentManagement.GetBestStudents();
        if (best.Count == 0)
        {
            Console.WriteLine("No Students found!");
        }
        ConsoleHelper.Pause();
    }
    /// <summary>
    /// Displays students in order of increasing age.
    /// </summary>
    public void ShowByIncreasingAge()
    {
        Console.WriteLine("---- Get Students By Increasing Age ----");
        PrintWithAge(studentManagement.GetStudentsByIncreasingAge());
    }
    /// <summary>
    /// Displays students in order of decreasing age.
    /// </summary>
    public void ShowByDecreasingAge()
    {
        Console.WriteLine("---- Get Students By Decreasing Age ----");
        PrintWithAge(studentManagement.GetStudentsByDecreasingAge());
    }
    /// <summary>
    /// Displays students in order of increasing grade.
    /// </summary>
    public void ShowByIncreasingGrade()
    {
        Console.WriteLine("---- Get Students By Increasing Grade ----");
        PrintWithGrade(studentManagement.GetStudentsByIncreasingGrade());
    }
    /// <summary>
    /// Displays students in order of decreasing grade.
    /// </summary>
    public void ShowByDecreasingGrade()
    {
        Console.WriteLine("---- Get Students By Decreasing Grade ----");
        PrintWithGrade(studentManagement.GetStudentsByDecreasingGrade());
    }
    /// <summary>
    /// Prompts for a course and displays students enrolled in it.
    /// </summary>
    public void ShowByCourse()
    {
        Console.WriteLine("---- Get Students By Course ----");
        LearningCourse course = studentManagement.ReadCourse();
        List<Student> students = studentManagement.GetStudentsByCourse(course);
        if (students.Count == 0)
        {
            Console.WriteLine($"No students found for the course: {course}");
        }
        else
        {
            foreach (Student student in students)
            {
                Console.WriteLine($"Name: {student.StudentName}, Id: {student.Id}, Roll Number: {student.RollNumber}, Grade: {student.Grade}");
            }
        }
        ConsoleHelper.Pause();
    }
    /// <summary>
    /// Prompts for student details and adds a new student.
    /// </summary>
    /// <returns>A task representing the asynchronous add operation.</returns>
    public async Task AddStudentAsync()
    {
        Console.WriteLine("---- Add new Student ----");
        Student student = new Student();
        student.StudentName = ConsoleHelper.ReadRequired("Name: ");
        student.Grade = ConsoleHelper.ReadGrade("Grade A-F: ");
        student.SchoolName = ConsoleHelper.ReadRequired("School Name: ");
        while (true)
        {
            DateOnly birthDate = ConsoleHelper.ReadDate("Birth Date (yyyy-MM-dd): ");
            try
            {
                student.BirthDate = birthDate;
                break;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        student.Course = studentManagement.ReadCourse();
        await studentManagement.AddStudentAsync(student);
        Console.WriteLine($"'{student.StudentName}' was added to the List of Students");
        ConsoleHelper.Pause();
    }
    /// <summary>
    /// Prompts for a student ID and updates that student's grade.
    /// </summary>
    /// <returns>A task representing the asynchronous update operation.</returns>
    public async Task UpdateGradeAsync()
    {
        Console.WriteLine("---- Update Grade ----");
        int id = ConsoleHelper.ReadInt("Id: ");
        Student? student = studentManagement.GetStudentById(id);
        if (student == null)
        {
            Console.WriteLine("No Student found");
        }
        else
        {
            while (true)
            {
                char newGrade = ConsoleHelper.ReadGrade("New grade A-F: ");
                try
                {
                    await studentManagement.UpdateStudentGradeAsync(student.Id, newGrade);
                    Console.WriteLine($"Grade: {student.Grade}");
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        ConsoleHelper.Pause();
    }
    /// <summary>
    /// Prompts for a student ID and deletes the corresponding student.
    /// </summary>
    /// <returns>A task representing the asynchronous delete operation.</returns>
    public async Task DeleteStudentAsync()
    {
        Console.WriteLine("---- Delete Student by Id ----");
        int id = ConsoleHelper.ReadInt("Id: ");
        if (await studentManagement.DeleteStudentByIdAsync(id))
        {
            Console.WriteLine($"Student with Id {id} has been deleted.");
        }
        else
        {
            Console.WriteLine($"No Student found with Id {id}.");
        }
        ConsoleHelper.Pause();
    }
    /// <summary>
    /// Displays a student or a message if no matching student was found.
    /// </summary>
    /// <param name="student">The student to display, or <see langword="null"/> if there is no match.</param>
    private static void ShowResult(Student? student)
    {
        if (student == null)
        {
            Console.WriteLine("No Student found!");
        }
        else
        {
            ConsoleHelper.PrintStudent(student);
        }
        ConsoleHelper.Pause();
    }
    private static void ShowResults(List<Student?> students)
    {
        if (students == null || students.Count == 0)
        {
            Console.WriteLine("No Student found!");
        }
        else
        {
            foreach (Student? student in students)
            {
                ConsoleHelper.PrintStudent(student);
            }
        }
        ConsoleHelper.Pause();
    }
    /// <summary>
    /// Displays students and their ages.
    /// </summary>
    /// <param name="students">The students to display.</param>
    private static void PrintWithAge(List<Student> students)
    {
        foreach (Student student in students)
        {
            Console.WriteLine($"Name: {student.StudentName}, Age: {student.Age}");
        }
        ConsoleHelper.Pause();
    }
    /// Displays students and their grades.
    /// </summary>
    /// <param name="students">The students to display.</param>
    private static void PrintWithGrade(List<Student> students)
    {
        foreach (Student student in students)
        {
            Console.WriteLine($"Name: {student.StudentName}, Grade: {student.Grade}");
        }
        ConsoleHelper.Pause();
    }
}
