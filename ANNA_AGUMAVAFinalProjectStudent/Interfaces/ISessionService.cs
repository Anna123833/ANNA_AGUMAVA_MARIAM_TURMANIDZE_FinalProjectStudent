using ANNA_AGUMAVA_MARIAM_TURMANIDZE_FinalProjectStudent.Models;
using ANNA_AGUMAVAFinalProjectStudent.Models;

namespace ANNA_AGUMAVAFinalProjectStudent.Interfaces;
/// <summary>
/// Defines operations and state for managing the current user's login session.
/// </summary>
internal interface ISessionService
{

    /// <summary>
    /// Gets the currently logged-in user.
    /// </summary>
    Users? CurrentUser { get; }

    /// <summary>
    /// Gets the currently logged-in student.
    /// </summary>
    Student? CurrentStudent { get; }

    /// <summary>
    /// Gets the current user's role, if logged in.
    /// </summary>
    Roles? CurrentRole { get; }

    /// <summary>
    /// Gets the current student's ID.
    /// </summary>
    int? CurrentStudentId { get; }

    /// <summary>
    /// Gets the current student's grade, if logged in.
    /// </summary>
    char? CurrentStudentGrade { get; }

    /// <summary>
    /// Gets the current user's phone number, if logged in.
    /// </summary>
    string? CurrentPhonenmber { get; }

    /// <summary>
    /// Gets whether a user is currently logged in.
    /// </summary>
    bool IsLoggedIn { get; }


    /// <summary>
    /// Restores a session using the user ID stored in the session file.
    /// </summary>
    /// <returns><see langword="true"/> if the session was restored; otherwise, <see langword="false"/>.</returns>
    bool RestoreLogin();


    /// <summary>
    /// Authenticates a user and stores their ID in the session file.
    /// </summary>
    /// <param name="userName">The user's username.</param>
    /// <param name="password">The user's password.</param>
    /// <param name="email">The user's email address.</param>
    /// <returns><see langword="true"/> if login succeeds; otherwise, <see langword="false"/>.</returns>
    bool Login();


    /// <summary>
    /// Logs out the current user and deletes the session file.
    /// </summary>
    /// <returns><see langword="true"/> if a user was logged out; otherwise, <see langword="false"/>.</returns>
    bool LogOut();
}
