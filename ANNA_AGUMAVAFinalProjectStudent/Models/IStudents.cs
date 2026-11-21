/// <summary>
/// Defines the learning courses available to students.
/// </summary>
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
/// Describes the properties and information method required of a student.
/// </summary>
internal interface IStudents
{
    /// <summary>Gets or sets the student's name.</summary>
    public string StudentName { get; set; }
    /// <summary>Gets or sets the student's ID.</summary>
    public int Id { get; set; }
    /// <summary>Gets or sets the student's roll number.</summary>
    public string RollNumber { get; set; }
    /// <summary>Gets or sets the student's grade.</summary>
    public char Grade { get; set; }
    /// <summary>Gets or sets the name of the student's school.</summary>
    public string SchoolName { get; set; }
    /// <summary>Gets or sets the student's date of birth.</summary>
    public DateOnly BirthDate { get; set; }
    /// <summary>Gets the student's age.</summary>
    public int Age { get; }
    /// <summary>Gets or sets the course the student is taking.</summary>
    public LearningCourse Course { get; set; }
    /// <summary>Returns a text description of the student.</summary>
    /// <returns>Information about the student.</returns>
    string GetInfo();
}