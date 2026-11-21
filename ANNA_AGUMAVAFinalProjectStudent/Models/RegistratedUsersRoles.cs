namespace ANNA_AGUMAVA_MARIAM_TURMANIDZE_FinalProjectStudent.Models;
/// <summary>
/// Defines the roles available to registered users.
/// </summary>
internal enum Roles
{
    /// <summary>A user with guest-level access.</summary>
    Guest,
    /// <summary>A user with administrative access.</summary>
    Admin,
    /// <summary>A user with student-level access.</summary>
    Student,
    /// <summary>A user with teacher-level access.</summary>
    Teacher,
}
/// <summary>
/// Provides a role property for a registered user.
/// </summary>
internal class RegistratedUsersRoles
{
    /// <summary>Gets or sets the user's role.</summary>
    public Roles UserRoles { get; set; }
    /// <summary>Initializes a new instance with the default role.</summary>
    protected RegistratedUsersRoles()
    {
        UserRoles = new Roles();
    }
}
