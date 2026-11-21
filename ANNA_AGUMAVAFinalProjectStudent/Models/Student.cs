namespace ANNA_AGUMAVA_MARIAM_TURMANIDZE_FinalProjectStudent.Models;
    /// <summary>
    /// Represents a student, including their school details and enrolled course.
    /// </summary>
internal class Student : IStudents
{
    // Stores the student's name.
    private string studentName { get; set; } = string.Empty;
    // The ID assigned to the next student.
    private static int nextId = 1;
    // Stores this student's ID.
    private int id;
    // The student's roll number.
    public string RollNumber { get; set; } = string.Empty;
    // Stores the student's school name.
    private string schoolName { get; set; } = string.Empty;
    // Stores the student's grade.
    private char grade { get; set; }
    // Stores the student's date of birth.
    private DateOnly birthDate { get; set; }
    // Stores the course the student is enrolled in.
    private LearningCourse course;

    /// <summary>
    /// Gets or sets the student's name.
    /// </summary>
    public string StudentName
    {
        get => studentName;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                Console.WriteLine("Student name cannot be empty or contain only whitespace.");
            }
            studentName = value.Trim();
        }
    }
    /// <summary>
    /// Gets or sets the student's unique identifier.
    /// </summary>
    public int Id
    {
        get => id;
        set
        {
            if (value < 1)
            {
                throw new ArgumentException("Id must be a positive integer.");
            }

            id = value;
        }
    }
    /// <summary>
    /// Gets or sets the student's grade, from A through F.
    /// </summary>
    public char Grade
    {
        get => grade;
        set 
        {
            char g = char.ToUpper(value);
            if(g < 'A' || g > 'F')
            {
                throw new ArgumentException("Grade must be a letter between A and F.");
            }
            grade = g;
        }
    }
    /// <summary>
    /// Gets or sets the student's school name.
    /// </summary>
    public string SchoolName
    {
        get => schoolName;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                Console.WriteLine("School name cannot be empty or contain only whitespace.");
            }
            schoolName = value.Trim();
        }
    }
    /// <summary>
    /// Gets or sets the student's date of birth.
    /// </summary>
    public DateOnly BirthDate
    {
        get => birthDate;
        set
        {
            if (value > DateOnly.FromDateTime(DateTime.Now))
            {
                throw new ArgumentException("Birth date cannot be in the future.");
            }
            else if(value < DateOnly.FromDateTime(DateTime.Now.AddYears(-100)))
            {
                throw new ArgumentException("Birth date is not valid.");
            }
            birthDate = value;
        }
    }
    /// <summary>
    /// Gets the student's age in years.
    /// </summary>
    public int Age
    {
        get
        {
            int age = DateTime.Today.Year - BirthDate.Year;
            if (BirthDate > DateOnly.FromDateTime(DateTime.Today).AddYears(-age)){
                age--;
            }
            return age;
        }
    }
    /// <summary>
    /// Gets or sets the student's learning course.
    /// </summary>
    public LearningCourse Course { 
        get => course;
        set
        {
            if(!Enum.IsDefined(typeof(LearningCourse), value))
            {
                throw new ArgumentException("Invalid course selection.");
            }
            course = value;
        }
    }

    /// <summary>
    /// Sets the identifier that will be assigned to the next student.
    /// </summary>
    /// <param name="value">The next identifier to assign.</param>
    public static void SetNextId(int value)
    {
        nextId = value;
    }
    /// <summary>
    /// Prints and returns a summary of the student's details.
    /// </summary>
    /// <returns>A string containing the student's name, ID, grade, school, birth date, and course.</returns>
    public string GetInfo()
    {
        Console.WriteLine($"Student Name: {StudentName}, Id: {Id}, Grade: {Grade}, School Name: {SchoolName}, Birth Date: {BirthDate}, Course: {Course}");
        return $"{StudentName}, {Id}, {Grade}, {SchoolName}, {BirthDate}, {Course}";
    }
    /// <summary>
    /// Initializes a student and assigns the next available identifier.
    /// </summary>
    public Student()
    {
        this.Id = nextId++;
    }
    /// <summary>
    /// Initializes a student with the specified details and assigns the next available identifier.
    /// </summary>
    /// <param name="studentName"></param>
    /// <param name="grade"></param>
    /// <param name="schoolName"></param>
    /// <param name="birthDate"></param>
    /// <param name="studentCourse"></param>
    public Student(string studentName, char grade, string schoolName, DateOnly birthDate, LearningCourse studentCourse)
    {
        this.Id = nextId++;
        this.StudentName = studentName;
        this.Grade = grade;
        this.SchoolName = schoolName;
        this.BirthDate = birthDate;
        this.Course = studentCourse;
    }
    /// <summary>
    /// Returns a readable summary of the student's details.
    /// </summary>
    /// <returns>A string representing the student.</returns>
    public override string ToString()
    {
        return $"Id: {Id}, Roll Number: {RollNumber}, Student Name: {StudentName}, Age: {Age}, Grade: {Grade}, School Name: {SchoolName}, Birth Date: {BirthDate}, Course: {Course}";
    }
}