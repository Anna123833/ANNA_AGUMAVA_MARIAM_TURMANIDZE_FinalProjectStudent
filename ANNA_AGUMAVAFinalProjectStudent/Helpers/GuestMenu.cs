using ANNA_AGUMAVA_MARIAM_TURMANIDZE_FinalProjectStudent.Helpers;
using ANNA_AGUMAVA_MARIAM_TURMANIDZE_FinalProjectStudent.Session;

namespace ANNA_AGUMAVA_MARIAM_TURMANIDZE_FinalProjectStudent.Menus;
/// <summary>
/// Displays the menu available to guest users and handles their selections.
/// </summary>
internal class GuestMenu
{
    /// <summary>
    /// Manages the current user's session.
    /// </summary>
    private readonly SessionService session;
    /// <summary>
    /// Provides actions for viewing and searching student records.
    /// </summary>
    private readonly StudentActions actions;

    /// <summary>
    /// Initializes a new instance of the <see cref="GuestMenu"/> class.
    /// </summary>
    /// <param name="session">The service used to manage the current user session.</param>
    /// <param name="actions">The actions available to guest users.</param>
    public GuestMenu(SessionService session, StudentActions actions)
    {
        this.session = session;
        this.actions = actions;
    }

    /// <summary>
    /// Displays the guest menu and processes selections until the user logs out or exits.
    /// </summary>
    /// <returns>
    /// A task containing <see langword="true"/> if the user exits,
    /// or <see langword="false"/> if the user logs out.
    /// </returns>
    public Task<bool> RunAsync()
    {
        /// <summary>
        /// Runs the student menu until the user logs out or exits the application.
        /// </summary>
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("---TSU Student management---");
            Console.WriteLine("1. See all students");
            Console.WriteLine("2. Search student with Name");
            Console.WriteLine("3. Get Best Students");
            Console.WriteLine("4. Log Out");
            Console.WriteLine("5. Exit");
            Console.WriteLine();
            Console.WriteLine("---------------------");
            Console.WriteLine();
            Console.Write("Select: ");
            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1": actions.ShowAll(); break;
                case "2": actions.SearchByName(); break;
                case "3": actions.ShowBest(); break;
                case "4":
                    session.LogOut();
                    Console.WriteLine("Logged out successfully!");
                    return Task.FromResult(false);
                case "5":
                    Console.WriteLine("Thanks for using our website!");
                    return Task.FromResult(true);
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }
}
