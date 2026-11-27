namespace ANNA_AGUMAVA_MARIAM_TURMANIDZE_FinalProjectStudent.Models;

/// <summary>
/// Represents a course that a student can take.
/// </summary>
internal enum LearningCourse
{
    ICTLiteracy = 1,
    BasicsOfProgramming,
    ObjectOrientedProgramming,
    ComputerArchitectureAndOrganization,
    DataStructures,
    Database,
    DataAnalysisAndStatistics,
    OperatingSystems,
    ModelingAndSimulation,
    Algorithms,
    SoftwareEngineering,
    MathematicalProgramming,
    NetworkTechnologiesAndCommunications,
    WebProgramming,
    FunctionalProgramming,
    OperationsResearch,
    ProjectPreparation,
    ArtificialIntelligence,
    ComputerLawAndEthics,
    TeamProjects
}
/// <summary>
/// Describes the properties and information required of a student.
/// </summary>
internal abstract class Students
{
    private string studentName = string.Empty;
    private int id;
    private char grade;
    private string schoolName = string.Empty;
    private DateOnly birthDate;
    private LearningCourse course;


    /// <summary>Gets or sets the student's name.</summary>
    public string StudentName
    {
        get => studentName;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Student can not be empty.");
            }
            studentName = value.Trim();
        }
    }


    /// <summary>Gets or sets the student's ID.</summary>
    public int Id
    {
        get => id;
        set
        {
            if (value < 1)
            {
                throw new ArgumentException("Id can not be 0 or negative integer");
            }
            id = value;
        }
    }


    /// <summary>Gets or sets the student's roll number.</summary>
    public string RollNumber { get; set; } = string.Empty;


    /// <summary>Gets or sets the student's grade.</summary>
    public char Grade
    {
        get => grade;
        set
        {
            char g = char.ToUpper(value);
            if (g < 'A' || g > 'F')
            {
                throw new ArgumentException("Grade must be from A to F");
            }
            grade = g;
        }
    }


    /// <summary>Gets or sets the name of the student's school.</summary>
    public string SchoolName
    {
        get => schoolName;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("School Name can not be empty.");
            }
            schoolName = value.Trim();
        }
    }



    /// <summary>Gets or sets the student's date of birth and validates it.</summary>
    public DateOnly BirthDate
    {
        get => birthDate; 
        set
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            if (value > today)
            {
                throw new ArgumentException("Birth date can not be in the future");
            }
            if(value < today.AddYears(-110))
            {
                throw new ArgumentException("Invalid Birth date");
            }
            birthDate = value;
        }
    }



    /// <summary>Gets the student's age.</summary>
    public int Age { 
        get
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            int age = today.Year - birthDate.Year;
            if(BirthDate > today.AddYears(-age))
            {
                age--;
            }
            return age;
        }
    }



    /// <summary>Gets or sets the course the student is taking.</summary>
    public LearningCourse Course { 
        get => course; 
        set 
        { 
            if(!Enum.IsDefined(typeof(LearningCourse), value))
            {
                throw new ArgumentException("Invalid course selection");
            }
            course = value;
        } 
    }
    public Students()
    {

    }
    public Students(string studentName, char grade, string schoolName, DateOnly birthDate, LearningCourse course)
    {
        this.StudentName = studentName;
        this.Grade = grade;
        this.SchoolName = schoolName;
        this.BirthDate = birthDate;
        this.Course = course;
    }
}
