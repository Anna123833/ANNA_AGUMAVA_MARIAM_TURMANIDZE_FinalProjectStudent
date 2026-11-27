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
    private string userName = string.Empty;
    private string passwordHash = string.Empty;
    private string email = string.Empty;
    public string phoneNumber = string.Empty;
    public int studentId;
    private Roles userRoles;
    /// <summary>
    /// Gets or sets the user's name.
    /// </summary>
    public string UserName
    {
        get => userName;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("UserName can not be null");
            }
            userName = value.Trim();
        }
    }


    /// <summary>
    /// Gets or sets the user's password hash.
    /// </summary>
    public string PasswordHash
    {
        get => passwordHash;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Passwword hash can not be null");
            }
            passwordHash = value;
        }
    }


    /// <summary>
    /// Gets or sets the user's email address.
    /// </summary>
    public string Email
    {
        get => email;
        set 
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Email cannot be empty.");
            }
            if(!value.Contains('@') && !value.Contains('.'))
            {
                throw new ArgumentException("Email must contain @ and .");
            }
            email = value.Trim();
        }
    }


    /// <summary>
    /// Gets or sets the user's phone number.
    /// </summary>
    public string PhoneNumber
    {
        get => phoneNumber;
        set
        {
            if (string.IsNullOrWhiteSpace(value) || !value.All(char.IsDigit))
            {
                throw new ArgumentException("Invalid phone number! Enter again!");
            }
            phoneNumber = value;
        }
    }


    /// <summary>
    /// Gets or sets the user's Student ID.
    /// </summary>
    public int StudentId
    {
        get => studentId;
        set => studentId = value;
    }


    /// <summary>Gets or sets the user's role.</summary>
    public Roles UserRoles
    {
        get => userRoles;
        set => userRoles = value;
    }


    /// <summary>Initializes a new instance with the default role.</summary>
    protected RegistratedUsersRoles()
    {
        UserRoles = new Roles();
    }
    public RegistratedUsersRoles(string userName, string email, string phoneNumber, Roles roles, string PasswordHash)
    {
        this.UserName = userName;
        this.PasswordHash = PasswordHash;
        this.Email = email;
        this.PhoneNumber = phoneNumber;
        this.UserRoles = roles;
    }
}
