using ANNA_AGUMAVA_MARIAM_TURMANIDZE_FinalProjectStudent.Models;
using ANNA_AGUMAVAFinalProjectStudent.Models;

namespace ANNA_AGUMAVAFinalProjectStudent.Interfaces;

internal interface IAuthService
{
    /// <summary>
    /// Finds a user by their unique ID.
    /// </summary>
    /// <param name="id">The ID of the user to find.</param>
    /// <returns>The matching user, or <see langword="null"/> if no user is found.</returns>
    Users? GetUserById(int id);

    /// <summary>
    /// Registers a new user if the email address is not already in use.
    /// </summary>
    /// <param name="userName">The new user's username.</param>
    /// <param name="password">The new user's password.</param>
    /// <param name="email">The new user's email address.</param>
    /// <param name="roles">The role assigned to the new user.</param>
    /// <param name="phoneNumber">The new user's phone number.</param>
    /// <param name="studentId">The ID of the student associated with the user.</param>
    /// <param name="students">The list of students.</param>
    /// <returns><see langword="true"/> if registration succeeds; otherwise, <see langword="false"/>.</returns>
    Task<bool> RegisterAsync(string userName, string password, string email, Roles roles, string phoneNumber, int studentId, List<Student> students);

    /// <summary>
    /// Deletes a registered user when the supplied credentials match.
    /// </summary>
    /// <param name="userName">The user's username.</param>
    /// <param name="password">The user's password.</param>
    /// <param name="email">The user's email address.</param>
    /// <returns><see langword="true"/> if the user is deleted; otherwise, <see langword="false"/>.</returns>
    Task<bool> DeleteRegistratedUser(string userName, string password, string email);

    /// <summary>
    /// Finds a user whose username, password, and email match the supplied credentials.
    /// </summary>
    /// <param name="userName">The username to check.</param>
    /// <param name="password">The password to check.</param>
    /// <param name="email">The email address to check.</param>
    /// <returns>The matching user, or <see langword="null"/> if no match is found.</returns>
    Users? GetUser(string userName, string password, string email);
}
