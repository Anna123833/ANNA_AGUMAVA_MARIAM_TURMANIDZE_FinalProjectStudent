using ANNA_AGUMAVA_MARIAM_TURMANIDZE_FinalProjectStudent.AuthServices;
using ANNA_AGUMAVA_MARIAM_TURMANIDZE_FinalProjectStudent.Models;
using ANNA_AGUMAVA_MARIAM_TURMANIDZE_FinalProjectStudent.User;
namespace ANNA_AGUMAVA_MARIAM_TURMANIDZE_FinalProjectStudent.Session;
/// <summary>
/// Manages the current user's login session.
/// </summary>
internal class SessionService
{
    private static readonly string sessionFilePath = GetSessionFilePath();
    private Users? currentUser = null;
    private readonly AuthService authService;
    /// <summary>
    /// Gets the currently logged-in user.
    /// </summary>
    public Users? CurrentUser => currentUser;
    /// <summary>
    /// Gets the current user's role, if logged in.
    /// </summary>
    public Roles? CurrentRole => currentUser?.UserRoles;
    /// <summary>
    /// Gets the current user's phone number, if logged in.
    /// </summary>
    public string? CurrentPhonenmber => currentUser?.PhoneNumber;
    /// <summary>
    /// Gets whether a user is currently logged in.
    /// </summary>
    public bool IsLoggedIn
    {
        get
        {
            if(currentUser is not null)
            {
                return true;
            }
            return false;
        }
    }
    /// <summary>
    /// Initializes the session service.
    /// </summary>
    /// <param name="authService">Service used to authenticate and retrieve users.</param>
    public SessionService(AuthService authService)
    {
        this.authService = authService;
    }
    /// <summary>
    /// Gets the path to the session file and creates its directory if needed.
    /// </summary>
    private static string GetSessionFilePath()
    {
        string projectPath = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName ?? string.Empty;
        string folderPath = Path.Combine(projectPath, "Data");
        Directory.CreateDirectory(folderPath);
        return Path.Combine(folderPath, "session.json");
    }
    /// <summary>
    /// Restores a session using the user ID stored in the session file.
    /// </summary>
    /// <returns><see langword="true"/> if the session was restored; otherwise, <see langword="false"/>.</returns>
    public bool RestoreLogin()
    {
        if (!File.Exists(sessionFilePath))
        {
            return false;
        }
        if (!int.TryParse(File.ReadAllText(sessionFilePath).Trim(), out int userId))
        {
            return false;
        }
        Users? user = authService.GetUserById(userId);
        if(user is null)
        {
            File.Delete(sessionFilePath);
            return false;
        }
        currentUser = user;
        return true;
    }
    /// <summary>
    /// Authenticates a user and stores their ID in the session file.
    /// </summary>
    /// <param name="userName">The user's username.</param>
    /// <param name="password">The user's password.</param>
    /// <param name="email">The user's email address.</param>
    /// <returns><see langword="true"/> if login succeeds; otherwise, <see langword="false"/>.</returns>
    public bool Login(string userName, string password, string email)
    {
        Users? users = authService.GetUser(userName, password, email);
        if(users is not null)
        {
            currentUser = users;
            File.WriteAllText(sessionFilePath, users.Id.ToString());
            Roles role = users.UserRoles;
            string phoneNumber = users.PhoneNumber;
            return true;
        }
        return false;
    }
    /// <summary>
    /// Logs out the current user and deletes the session file.
    /// </summary>
    /// <returns><see langword="true"/> if a user was logged out; otherwise, <see langword="false"/>.</returns>
    public bool LogOut()
    {
        if (IsLoggedIn)
        {
            currentUser = null;
            if (File.Exists(sessionFilePath))
            {
                File.Delete(sessionFilePath);
            }
        }
        return false;
    }
}
