using ANNA_AGUMAVA_MARIAM_TURMANIDZE_FinalProjectStudent.Models;
using ANNA_AGUMAVAFinalProjectStudent.Models;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ANNA_AGUMAVAFinalProjectStudent.Services.AuthServices;
/// <summary>
/// Provides authentication functionality, including user registration,
/// credential lookup, password hashing, and user-data persistence.
/// </summary>
internal class AuthService
{

    private List<Users> users = new List<Users>();
    private const string fileName = "users.json";

    /// <summary>
    /// Gets or sets the password after validating its required length and character types.
    /// </summary>
    private string password = "";
    /// <summary>
    /// Gets or sets the password after validating its required length and character types.
    /// </summary>
    public string Password
    {
        get => password;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Pasword can not be null or Whitespace, it is required!", nameof(value));
            }
            if (value.Length < 8)
            {
                throw new ArgumentException("Password must be minimum 8 characters long!", nameof(value));
            }
            if (!value.Any(char.IsUpper))
            {
                throw new ArgumentException("Pasword must contain at least one Uppercase letter", nameof(value));
            }
            if (!value.Any(char.IsLower))
            {
                throw new ArgumentException("Pasword must contain at least one Lowercase letter", nameof(value));
            }
            if (!value.Any(char.IsSymbol) && !value.Any(char.IsPunctuation))
            {
                throw new ArgumentException("Pasword must contain at least one Symbol", nameof(value));
            }
            password = value;
        }
    }
    private static bool IsAllowedToRegister(Roles role, string userName, int studentId, List<Student> students)
    {
        switch (role)
        {
            case Roles.Student:
                return students.Any(s => s != null && s?.Id == studentId && userName == s?.StudentName);

            default:
                return true;
        }
    }

    private readonly string filePath;
    /// <summary>
    /// Finds a user by their unique ID.
    /// </summary>
    /// <param name="id">The ID of the user to find.</param>
    /// <returns>The matching user, or <see langword="null"/> if no user is found.</returns>
    public Users? GetUserById(int id)
    {
        return users.FirstOrDefault(u => u.Id == id);
    }

    /// <summary>
    /// It combines folderpath with filename to create the File Path
    /// </summary>
    /// <returns></returns>
    private static string GetFilePath()
    {
        string projectPath = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName ?? string.Empty;
        string folderPath = Path.Combine(projectPath, "Data");
        Directory.CreateDirectory(folderPath);
        return Path.Combine(folderPath, fileName);
    }
    private static readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions()
    {
        WriteIndented = true,
        Converters = { new JsonStringEnumConverter() }
    };
    private AuthService(string filePath, List<Users> users)
    {
        this.filePath = filePath;
        this.users = users;
    }
    private static readonly byte[] fixedSalt = Encoding.UTF8.GetBytes("MyProject");
    /// <summary>
    /// Creates an authentication service and loads saved users asynchronously.
    /// </summary>
    /// <returns>A task containing the initialized authentication service.</returns>
    public static async Task<AuthService> CreateAsync()
    {
        string filePath = GetFilePath();
        List<Users> users = await LoadingUsersAsync(filePath);
        return new AuthService(filePath, users);
    }
    private static async Task<List<Users>> LoadingUsersAsync(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return [];
        }
        try
        {
            string json = await File.ReadAllTextAsync(filePath);
            List<Users> loaded = JsonSerializer.Deserialize<List<Users>>(json, jsonSerializerOptions) ?? [];
            int maxId = loaded.Count == 0 ? 0 : loaded.Max(u => u.Id);
            Users.SetNextId(maxId + 1);
            return loaded;
        }
        catch (JsonException ex)
        {
            Console.WriteLine(ex.Message);
            return [];
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
            return [];
        }
        catch (IOException ex)
        {
            Console.WriteLine(ex.Message);
            return [];
        }
    }
    private async Task SaveUsersAsync()
    {
        string json = JsonSerializer.Serialize(users, jsonSerializerOptions);
        await File.WriteAllTextAsync(filePath, json);
    }
    private static string GetHashPasswords(string password)
    {
        return HashPassword(password, fixedSalt);
    }



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
    /// <returns>
    /// <see langword="true"/> if registration succeeds; otherwise,
    /// <see langword="false"/>.
    /// </returns>
    public async Task<bool> RegisterAsync(string userName, string password, string email, Roles roles, string phoneNumber, int studentId, List<Student> students)
    {
        Password = password;
        Users? ExistingUser = users.FirstOrDefault(u => u.Email == email);
        if (users.Any(e => e.Email == email))
        {
            return false;
        }
        if (!IsAllowedToRegister(roles, userName, studentId, students))
        {
            throw new ArgumentException($"Cannot register user with role {roles} and username {userName} because the student ID {studentId} does not match any existing student.");
        }
        Users newUser = new Users(userName, email, phoneNumber, roles, GetHashPasswords(password));
        if (roles == Roles.Student)
        {
            newUser.StudentId = studentId;
        }
        users.Add(newUser);
        await SaveUsersAsync();
        return true;
    }


    /// <summary>
    /// Deletes a registered user when the supplied credentials match.
    /// </summary>
    /// <param name="userName">The user's username.</param>
    /// <param name="password">The user's password.</param>
    /// <param name="email">The user's email address.</param>
    /// <returns>
    /// <see langword="true"/> if the user is deleted; otherwise,
    /// <see langword="false"/>.
    /// </returns>
    public async Task<bool> DeleteRegistratedUser(string userName, string password, string email)
    {
        Users? user = GetUser(userName, password, email);
        if (user is null)
        {
            return false;
        }
        users.Remove(user);
        await SaveUsersAsync();
        return true;
    }
    /// <summary>
    /// Finds a user whose username, password, and email match the supplied credentials.
    /// </summary>
    /// <param name="userName">The username to check.</param>
    /// <param name="password">The password to check.</param>
    /// <param name="email">The email address to check.</param>
    /// <returns>The matching user, or <see langword="null"/> if no match is found.</returns>
    public Users? GetUser(string userName, string password, string email)
    {
        return users.FirstOrDefault(u => u.UserName == userName && u.PasswordHash == GetHashPasswords(password) && u.Email == email);
    }
    /// <summary>
    /// Hashes a password using the supplied salt and returns the result as Base64.
    /// </summary>
    /// <param name="password">The password to hash.</param>
    /// <param name="salt">The salt to append to the password before hashing.</param>
    /// <returns>The salt and password hash encoded as Base64.</returns>
    static string HashPassword(string password, byte[] salt)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] saltedPassword = new byte[passwordBytes.Length + salt.Length];

            Buffer.BlockCopy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
            Buffer.BlockCopy(salt, 0, saltedPassword, passwordBytes.Length, salt.Length);

            byte[] hashedBytes = sha256.ComputeHash(saltedPassword);

            byte[] hashedPasswordWithSalt = new byte[hashedBytes.Length + salt.Length];
            Buffer.BlockCopy(salt, 0, hashedPasswordWithSalt, 0, salt.Length);
            Buffer.BlockCopy(hashedBytes, 0, hashedPasswordWithSalt, salt.Length, hashedBytes.Length);

            return Convert.ToBase64String(hashedPasswordWithSalt);
        }
    }
}


