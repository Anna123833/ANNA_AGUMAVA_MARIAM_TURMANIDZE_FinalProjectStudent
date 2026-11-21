using ANNA_AGUMAVA_MARIAM_TURMANIDZE_FinalProjectStudent.Helpers;
using ANNA_AGUMAVA_MARIAM_TURMANIDZE_FinalProjectStudent.Session;

namespace ANNA_AGUMAVA_MARIAM_TURMANIDZE_FinalProjectStudent.Menus;
/// <summary>
/// Displays the admin menu and handles student-management operations.
/// </summary>
internal class AdminMenu
{
    /// <summary>
    /// Manages the current user's session.
    /// </summary>
    private readonly SessionService session;
    /// <summary>
    /// Provides student-management actions.
    /// </summary>
    private readonly StudentActions actions;
    /// <summary>
    /// Initializes a new instance of the <see cref="AdminMenu"/> class.
    /// </summary>
    /// <param name="session">The service used to manage the current user session.</param>
    /// <param name="actions">The student-management actions available to the admin.</param>
    public AdminMenu(SessionService session, StudentActions actions)
    {
        this.session = session;
        this.actions = actions;
    }
    /// <summary>
    /// Displays the admin menu and processes selections until the user logs out or exits.
    /// </summary>
    /// <returns>
    /// A task containing <see langword="true"/> if the user exits,
    /// or <see langword="false"/> if the user logs out.
    /// </returns>
    public async Task<bool> RunAsync()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("---Student management TSU---");
            Console.WriteLine("1. Add new student");
            Console.WriteLine("2. See all students");
            Console.WriteLine("3. Search student with Name");
            Console.WriteLine("4. Search student with Id");
            Console.WriteLine("5. Search student by Grade");
            Console.WriteLine("6. Search student by School Name");
            Console.WriteLine("7. Update students Grade");
            Console.WriteLine("8. Search student by Birth Date");
            Console.WriteLine("9. Get Students By Increasing Age");
            Console.WriteLine("10. Get Students By Decreasing Age");
            Console.WriteLine("11. Get Students By Increasing Grade");
            Console.WriteLine("12. Get Students By Decreasing Grade");
            Console.WriteLine("13. Get Best Students");
            Console.WriteLine("14. Delete Student by Id");
            Console.WriteLine("15. Get Students By Course");
            Console.WriteLine("16. Log Out");
            Console.WriteLine("17. Exit");
            Console.WriteLine();
            Console.WriteLine("------------------------");
            Console.WriteLine();
            Console.Write("Select: ");
            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1": await actions.AddStudentAsync(); break;
                case "2": actions.ShowAll(); break;
                case "3": actions.SearchByName(); break;
                case "4": actions.SearchById(); break;
                case "5": actions.SearchByGrade(); break;
                case "6": actions.SearchBySchoolName(); break;
                case "7": await actions.UpdateGradeAsync(); break;
                case "8": actions.SearchByBirthDate(); break;
                case "9": actions.ShowByIncreasingAge(); break;
                case "10": actions.ShowByDecreasingAge(); break;
                case "11": actions.ShowByIncreasingGrade(); break;
                case "12": actions.ShowByDecreasingGrade(); break;
                case "13": actions.ShowBest(); break;
                case "14": await actions.DeleteStudentAsync(); break;
                case "15": actions.ShowByCourse(); break;
                case "16":
                    session.LogOut();
                    Console.WriteLine("Logged out successfully!");
                    return false;
                case "17":
                    Console.WriteLine("Thanks for using our website!");
                    return true;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }
}
