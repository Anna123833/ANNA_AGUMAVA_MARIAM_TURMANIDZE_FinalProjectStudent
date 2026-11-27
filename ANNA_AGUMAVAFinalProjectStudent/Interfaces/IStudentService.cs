using ANNA_AGUMAVA_MARIAM_TURMANIDZE_FinalProjectStudent.Models;
using ANNA_AGUMAVAFinalProjectStudent.Services.Service;

namespace ANNA_AGUMAVAFinalProjectStudent.Interfaces;
/// <summary>
/// Defines operations for loading, storing, and persisting student data.
/// </summary>
internal interface IStudentService
{
    /// <summary>
    /// Gets the student list managed by the service.
    /// </summary>
    /// <returns>The list of all students.</returns>
    List<Student> GetStudents();


    /// <summary>
    /// Saves the current student list to the data file.
    /// </summary>
    /// <returns>A task that represents the asynchronous save operation.</returns>
    Task SaveDataAsync();


    /// <summary>
    /// Creates a new student, adds it to the list, and saves it.
    /// </summary>
    /// <param name="studentName">The student's name.</param>
    /// <param name="schoolName">The student's school.</param>
    /// <param name="grade">The student's grade (A to F).</param>
    /// <param name="birthDate">The student's birth date.</param>
    /// <param name="course">The student's course.</param>
    /// <returns>A task that represents the asynchronous add operation.</returns>
    Task AddNewStudentAsync(string studentName, string schoolName, char grade, DateOnly birthDate, LearningCourse course);


    /// <summary>
    /// Loads students asynchronously from the JSON file.
    /// </summary>
    /// <returns>A task representing the asynchronous operation, containing a list of students.</returns>
    Task<List<Student>> LoadStudents();
}
