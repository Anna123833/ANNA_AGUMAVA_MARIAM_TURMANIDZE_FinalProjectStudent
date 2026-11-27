using ANNA_AGUMAVA_MARIAM_TURMANIDZE_FinalProjectStudent.Models;

namespace ANNA_AGUMAVAFinalProjectStudent.Models;
/// <summary>
/// Represents a registered user and their account details.
/// </summary>
internal class Users : RegistratedUsersRoles
{
    private static int id = 1;
    /// <summary>
    /// Gets or sets the user's unique identifier.
    /// </summary>
    public int Id { get; set; } = id++;

    /// <summary>
    /// Sets the value used for the next user's identifier.
    /// </summary>
    /// <param name="value"></param>
    public static void SetNextId(int value)
    {
        id = value;
    }
    /// <summary>
    /// Initializes a new, empty user.
    /// </summary>
    public Users() { }

    /// <summary>
    /// Initializes a new user with the specified details and role.
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="email"></param>
    /// <param name="phoneNumber"></param>
    /// <param name="roles"></param>
    /// <param name="PasswordHash"></param>

    public Users(string userName, string email, string phoneNumber, Roles roles, string PasswordHash) : base(userName, email, phoneNumber, roles, PasswordHash)
    {
        this.UserName = userName;
        this.PasswordHash = PasswordHash;
        this.Email = email;
        this.PhoneNumber = phoneNumber;
        this.UserRoles = roles;
    }
}
