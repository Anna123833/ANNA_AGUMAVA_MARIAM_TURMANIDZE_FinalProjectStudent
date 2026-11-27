using ANNA_AGUMAVA_MARIAM_TURMANIDZE_FinalProjectStudent.Models;

namespace ANNA_AGUMAVAFinalProjectStudent.Interfaces;
/// <summary>
/// Defines operations for managing student records, including adding, searching, sorting, updating, and deleting.
/// </summary>
internal interface IStudentManagement
{
    /// <summary>
    /// Adds a student, assigns a roll number, and saves the data if a service is available.
    /// </summary>
    /// <param name="student">The student to add.</param>
    /// <returns>A task that represents the asynchronous add operation.</returns>
    Task AddStudentAsync(Student student);


    /// <summary>
    /// Returns a read-only view of all students.
    /// </summary>
    /// <returns>A read-only list of all students.</returns>
    IReadOnlyList<Student> GetAllStudents();


    /// <summary>
    /// Returns students ordered by increasing age.
    /// </summary>
    /// <returns>A list of students sorted from youngest to oldest.</returns>
    List<Student> GetStudentsByIncreasingAge();


    /// <summary>
    /// Returns students ordered by decreasing age.
    /// </summary>
    /// <returns>A list of students sorted from oldest to youngest.</returns>
    List<Student> GetStudentsByDecreasingAge();


    /// <summary>
    /// Returns students ordered by decreasing grade.
    /// </summary>
    /// <returns>A list of students sorted from the best grade to the worst.</returns>
    List<Student> GetStudentsByDecreasingGrade();


    /// <summary>
    /// Returns students ordered by increasing grade.
    /// </summary>
    /// <returns>A list of students sorted from the worst grade to the best.</returns>
    List<Student> GetStudentsByIncreasingGrade();


    /// <summary>
    /// Returns up to 16 students with an A grade and prints their names.
    /// </summary>
    /// <returns>A list of the names of the best students.</returns>
    List<string> GetBestStudents();


    /// <summary>
    /// Updates a student's grade and saves the data if a service is available.
    /// </summary>
    /// <param name="id">The student's ID.</param>
    /// <param name="newGrade">The grade to assign.</param>
    /// <returns><see langword="true"/> if the student was found; otherwise, <see langword="false"/>.</returns>
    Task<bool> UpdateStudentGradeAsync(int id, char newGrade);


    /// <summary>
    /// Returns all students with the specified grade.
    /// </summary>
    /// <param name="grade">The grade to search for.</param>
    /// <returns>A list of matching students.</returns>
    List<Student?> GetStudentsByGrade(char grade);

    /// <summary>
    /// Returns all students with the specified name.
    /// </summary>
    /// <param name="studentName">The name to search for.</param>
    /// <returns>A list of matching students.</returns>
    List<Student?> GetStudentsByName(string studentName);


    /// <summary>
    /// Returns all students from the specified school.
    /// </summary>
    /// <param name="schoolName">The school name to search for.</param>
    /// <returns>A list of matching students.</returns>
    List<Student?> GetStudentsBySchoolName(string schoolName);


    /// <summary>
    /// Returns all students with the specified roll number.
    /// </summary>
    /// <param name="rollNumber">The roll number to search for.</param>
    /// <returns>A list of matching students.</returns>
    List<Student?> GetStudentsByRollNumber(string rollNumber);


    /// <summary>
    /// Returns the student with the specified ID.
    /// </summary>
    /// <param name="id">The student's ID.</param>
    /// <returns>The matching student, or <see langword="null"/> if none is found.</returns>
    Student? GetStudentById(int id);


    /// <summary>
    /// Returns all students with the specified birth date.
    /// </summary>
    /// <param name="birthdateDate">The birth date to search for.</param>
    /// <returns>A list of matching students.</returns>
    List<Student?> GetStudentsByBirthDate(DateOnly birthdateDate);


    /// <summary>
    /// Returns the first student enrolled in the specified course.
    /// </summary>
    /// <param name="course">The course to search for.</param>
    /// <returns>The first matching student, or <see langword="null"/> if none is found.</returns>
    Student? GetStudentByCourse(LearningCourse course);


    /// <summary>
    /// Deletes a student by ID and saves the data if a service is available.
    /// </summary>
    /// <param name="id">The ID of the student to delete.</param>
    /// <returns><see langword="true"/> if a student was deleted; otherwise, <see langword="false"/>.</returns>
    Task<bool> DeleteStudentByIdAsync(int id);


    /// <summary>
    /// Returns all students enrolled in the specified course.
    /// </summary>
    /// <param name="course">The course to search for.</param>
    /// <returns>A list of students in the course.</returns>
    List<Student> GetStudentsByCourse(LearningCourse course);


    /// <summary>
    /// Displays available courses and prompts the user to select one.
    /// </summary>
    /// <returns>The selected course.</returns>
    LearningCourse ReadCourse();
}
