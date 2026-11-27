using ANNA_AGUMAVA_MARIAM_TURMANIDZE_FinalProjectStudent.Models;
using ANNA_AGUMAVAFinalProjectStudent.Interfaces;
using System.Text.Json;

namespace ANNA_AGUMAVAFinalProjectStudent.Services.Service;
/// <summary>
/// Loads, stores, and persists student data.
/// </summary>
internal class StudentService : IStudentService
{
    private readonly List<Student> students;
    private const string fileName = "students.json";
    private readonly string filePath;
    private JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions()
    {
        WriteIndented = true,
    };


    /// <summary>
    /// Initializes the service with a file path and student list.
    /// </summary>
    /// <param name="filePath">Path to the JSON data file.</param>
    /// <param name="students">Students managed by this service.</param>
    public StudentService(string filePath, List<Student> students)
    {
        this.filePath = filePath;
        this.students = students;
    }


    /// <summary>
    /// Creates a service and loads students from the default data file.
    /// </summary>
    /// 

    public static async Task<StudentService> CreateAsync()
    {
        string filePath = GetFilePath();
        StudentService service = new StudentService(filePath, []);
        List<Student> loadedStudents = await service.LoadStudents();
        service.students.AddRange(await service.GetStudentsFromFileAsync());
        int maxId = loadedStudents.Count == 0 ? 0 : loadedStudents.Max(s => s.Id);
        Student.SetNextId(maxId + 1);
        return service;
    }


    /// <summary>
    /// Gets the student list managed by this service.
    /// </summary>
    public List<Student> GetStudents()
    {
        return students;
    }


    public async Task<List<Student>> LoadStudents()
    {
        try
        {
            if (!File.Exists(filePath))
            {
                return new List<Student>();
            }
            string json = await File.ReadAllTextAsync(filePath);
            return JsonSerializer.Deserialize<List<Student>>(json) ?? new List<Student>();
        }
        catch(JsonException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (IOException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine(ex.Message);
        }
        return new List<Student>();
    }

    /// <summary>
    /// Builds the path to the student data file and creates its directory if needed.
    /// </summary>
    private static string GetFilePath()
    {
        string folderName = "Data";
        string projectPath = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName ?? string.Empty;
        string folderPath = Path.Combine(projectPath, folderName);
        Directory.CreateDirectory(folderPath);
        string filePath = Path.Combine(folderPath, fileName);
        return filePath;
    }
    /// <summary>
    /// Loads students from the JSON file, creating an empty file if one does not exist.
    /// </summary>
    private async Task<List<Student>> GetStudentsFromFileAsync()
    {
        if (File.Exists(filePath))
        {
            string json = await File.ReadAllTextAsync(filePath);
            if (string.IsNullOrWhiteSpace(json))
            {
                return [];
            }
            List<Student> loaded = JsonSerializer.Deserialize<List<Student>>(json) ?? [];
            int maxRoll = loaded.Count == 0 ? 0 : loaded.Max(s => s.Id);
            Student.SetNextId(maxRoll + 1);
            return loaded;
        }
        List<Student> result = [];
        await File.WriteAllTextAsync(filePath, JsonSerializer.Serialize(result, jsonSerializerOptions));
        return result;
    }
    /// <summary>
    /// Saves the current student list to the JSON file.
    /// </summary>
    public async Task SaveDataAsync()
    {
        string json = JsonSerializer.Serialize(students, jsonSerializerOptions);
        await File.WriteAllTextAsync(filePath, json);
    }
    /// <summary>
    /// Creates, adds, and saves a student.
    /// </summary>
    /// <param name="studentName">The student's name.</param>
    /// <param name="schoolName">The student's school.</param>
    /// <param name="grade">The student's grade.</param>
    /// <param name="birthDate">The student's birth date.</param>
    /// <param name="course">The student's course.</param>
    public async Task AddNewStudentAsync(string studentName, string schoolName, char grade, DateOnly birthDate, LearningCourse course)
    {
        Student student = new Student(studentName, grade, schoolName, birthDate, course);
        int studentsInCourse = students.Count(s => s?.Course == course);
        student.RollNumber = $"R{(int)student.Course}_{studentsInCourse /  20 + 1}";
        students.Add(student);
        await SaveDataAsync();
    }
}
