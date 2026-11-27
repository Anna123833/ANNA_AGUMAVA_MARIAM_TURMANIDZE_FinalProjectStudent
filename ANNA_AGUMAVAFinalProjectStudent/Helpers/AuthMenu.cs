using ANNA_AGUMAVA_MARIAM_TURMANIDZE_FinalProjectStudent.Helpers;
using ANNA_AGUMAVA_MARIAM_TURMANIDZE_FinalProjectStudent.Models;
using ANNA_AGUMAVA_MARIAM_TURMANIDZE_FinalProjectStudent.Session;
using ANNA_AGUMAVAFinalProjectStudent.Services.AuthServices;

namespace ANNA_AGUMAVA_MARIAM_TURMANIDZE_FinalProjectStudent.Menus;

/// <summary>
/// Displays authentication options and handles registration, login, and account deletion.
/// </summary>
internal class AuthMenu
{
    /// <summary>
    /// Provides registration and account-deletion operations.
    /// </summary>
    private readonly AuthService auth;
    /// <summary>
    /// Manages the current user's session.
    /// </summary>
    private readonly SessionService session;

    /// <summary>
    /// The list of students used for validating student registration.  
    /// </summary>
    private readonly List<Student> student;
    /// <summary>
    /// Initializes a new instance of the <see cref="AuthMenu"/> class.
    /// </summary>
    /// <param name="auth">The service used for authentication operations.</param>
    /// <param name="session">The service used to manage user sessions.</param>
    /// <param name="students">The list of students used for validating student registration.</param>
    public AuthMenu(AuthService auth, SessionService session, List<Student> students)
    {
        this.auth = auth;
        this.session = session;
        this.student = students;
    }
    /// <summary>
    /// Displays the authentication menu and processes the user's selections.
    /// </summary>
    /// <returns>
    /// A task containing <see langword="true"/> if the user chooses to exit;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public async Task<bool> RunAsync()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("1. Register");
            Console.WriteLine("2. Log in");
            Console.WriteLine("3. Delete Account");
            Console.WriteLine("4. Exit");
            Console.WriteLine();
            Console.WriteLine("-------------------------------");
            Console.WriteLine();
            Console.Write("Select: ");
            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    if (await RegisterAsync())
                    {
                        return false;
                    }
                    break;
                case "2":
                    if (LogIn())
                    {
                        return false;
                    }
                    break;
                case "3":
                    await DeleteAccountAsync();
                    break;
                case "4":
                    Console.WriteLine("Thanks for using our website!");
                    return true;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }
    /// <summary>
    /// Reads and validates the username, password, and email from the console.
    /// </summary>
    /// <param name="username">When this method returns, contains the entered username.</param>
    /// <param name="password">When this method returns, contains the entered password.</param>
    /// <param name="email">When this method returns, contains the entered email.</param>
    /// <returns><see langword="true"/> if all credentials are provided; otherwise, <see langword="false"/>.</returns>
    private bool ReadCredentials(out string username, out string password, out string email)
    {
        username = ConsoleHelper.ReadText("Username: ");
        password = ConsoleHelper.ReadText("Password: ");
        email = ConsoleHelper.ReadText("Email: ");
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(email))
        {
            Console.WriteLine("Username, Password and Email cannot be empty or contain only whitespace.");
            return false;
        }
        return true;
    }

    /// <summary>
    /// Registers a new user and attempts to log them in automatically.
    /// </summary>
    /// <returns>
    /// A task containing <see langword="true"/> if registration and automatic login succeed;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    private async Task<bool> RegisterAsync()
    {
        if (!ReadCredentials(out string username, out string password, out string email))
        {
            return false;
        }

        string phoneNumber = ConsoleHelper.ReadRequired("Phone Number: ");

        Roles role;
        while (true)
        {
            Console.Write("Role (Guest, Admin, Student, Teacher): ");
            if (Enum.TryParse(Console.ReadLine(), true, out role) && Enum.IsDefined(role))
            {
                break;
            }
            Console.WriteLine("Invalid role. Please enter a valid role.");
        }
        int studentId = 0;
        if (role == Roles.Student)
        {
            while (true)
            {
                Console.WriteLine("Enter Student ID (must be a positive integer): ");
                if (int.TryParse(Console.ReadLine(), out studentId) && studentId > 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a positive integer for Student ID.");
                }
            }
        }
        try
        {
            if (!await auth.RegisterAsync(username, password, email, role, phoneNumber, studentId, student))
            {
                Console.WriteLine("User already exists!");
                return false;
            }
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Registration failed {ex.Message}");
            return false;
        }

        Console.WriteLine("Registered Successfully!");
        if (session.Login(username, password, email))
        {
            PrintLoginInfo();
            return true;
        }
        Console.WriteLine("Registered, but automatic login failed. Please log in manually.");
        return false;
    }

    /// <summary>
    /// Reads credentials and attempts to log in the user.
    /// </summary>
    /// <returns><see langword="true"/> if login succeeds; otherwise, <see langword="false"/>.</returns>
    private bool LogIn()
    {
        if (!ReadCredentials(out string username, out string password, out string email))
        {
            return false;
        }
        if (session.Login(username, password, email))
        {
            PrintLoginInfo();
            return true;
        }
        Console.WriteLine("Wrong Username or Password");
        return false;
    }
    /// <summary>
    /// Reads credentials and attempts to delete the corresponding user account.
    /// </summary>
    /// <returns>A task representing the asynchronous account-deletion operation.</returns>
    private async Task DeleteAccountAsync()
    {
        if (!ReadCredentials(out string username, out string password, out string email))
        {
            return;
        }
        Console.WriteLine(await auth.DeleteRegistratedUser(username, password, email) ? "Deleted Successfully" : "User not found!");
    }
    /// <summary>
    /// Prints information about the logged-in user.
    /// </summary>
    private void PrintLoginInfo()
    {
        Console.WriteLine("Login Successful!");
        Console.WriteLine($"Current User: {session.CurrentRole}");
        Console.WriteLine($"Phone Number: {session.CurrentPhonenmber}");
        if(session.CurrentRole == Roles.Student)
        {
            Console.WriteLine($"Student ID: {session.CurrentStudentId}");
        }
    }
}
