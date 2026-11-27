namespace ANNA_AGUMAVA_MARIAM_TURMANIDZE_FinalProjectStudent.Models;
    /// <summary>
    /// Represents a student, including their school details and enrolled course.
    /// </summary>
internal class Student : Students
{
    // The ID assigned to the next student.
    private static int nextId = 1;

    /// <summary>
    /// Initializes a student and assigns only next ID.
    /// </summary>
    public Student() : base()
    {
        this.Id = nextId++;
    }


    /// <summary>
    /// Initializes a student and assigns the next available identifier.
    /// </summary>
    /// <param name="studentName"></param>
    /// <param name="grade"></param>
    /// <param name="schoolName"></param>
    /// <param name="birthDate"></param>
    /// <param name="studentCourse"></param>
    public Student(string studentName, char grade, string schoolName, DateOnly birthDate, LearningCourse studentCourse) :base(studentName, grade, schoolName, birthDate, studentCourse)
    {
        this.Id = nextId++;
    }


    /// <summary>
    /// Sets the identifier that will be assigned to the next student.
    /// </summary>
    public static void SetNextId(int value)
    {
        nextId = value;
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