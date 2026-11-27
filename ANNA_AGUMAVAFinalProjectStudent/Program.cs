using ANNA_AGUMAVA_MARIAM_TURMANIDZE_FinalProjectStudent.Helpers;
using ANNA_AGUMAVA_MARIAM_TURMANIDZE_FinalProjectStudent.Menus;
using ANNA_AGUMAVA_MARIAM_TURMANIDZE_FinalProjectStudent.Models;
using ANNA_AGUMAVA_MARIAM_TURMANIDZE_FinalProjectStudent.Session;
using ANNA_AGUMAVAFinalProjectStudent.Services.AuthServices;
using ANNA_AGUMAVAFinalProjectStudent.Services.Service;


/// <summary>
/// Creates the student service and loads its data asynchronously.
/// </summary>
StudentService service = await StudentService.CreateAsync();
/// <summary>
/// Creates the student-management component using the student service.
/// </summary>
StudentManagement studentManagement = new StudentManagement(service);
/// <summary>
/// Creates the authentication service and loads its data asynchronously.
/// </summary>
var auth = await AuthService.CreateAsync();
/// <summary>
/// Creates a session service to track the current user's login and role.
/// </summary>
var session = new SessionService(auth, studentManagement);
/// <summary>
/// Creates the actions that menus can use to perform student-related tasks.
/// </summary>
var actions = new StudentActions(studentManagement);
/// <summary>
/// Creates the menu used for logging in or registering.
/// </summary>
var authMenu = new AuthMenu(auth, session, service.GetStudents());
/// <summary>
/// Creates the menu available to students.
/// </summary>
var studentMenu = new StudentMenu(session, actions);
/// <summary>
/// Creates the menu available to guests.
/// </summary>
var guestMenu = new GuestMenu(session, actions);
/// <summary>
/// Creates the menu available to administrators and teachers.
/// </summary>
var adminMenu = new AdminMenu(session, actions);

Console.WriteLine("---------- TSU Student Management ----------");
// Restore a previous login, if one exists.
if (session.RestoreLogin())
{
    Console.WriteLine($"Welcome Back {session.CurrentUser?.UserName}");
}
// Track whether the user has chosen to exit the application.
bool exited = false;
while (!exited)
{
    if (!session.IsLoggedIn)
    {
        exited = await authMenu.RunAsync();
        if (exited)
        {
            break;
        }
    }
    Roles? role = session.CurrentRole;
    if (role == Roles.Student)
    {
        exited = await studentMenu.RunAsync();
    }
    else if (role == Roles.Guest)
    {
        exited = await guestMenu.RunAsync();
    }
    else if (role == Roles.Admin || role == Roles.Teacher)
    {
        exited = await adminMenu.RunAsync();
    }
    else
    {
        session.LogOut();
    }
}