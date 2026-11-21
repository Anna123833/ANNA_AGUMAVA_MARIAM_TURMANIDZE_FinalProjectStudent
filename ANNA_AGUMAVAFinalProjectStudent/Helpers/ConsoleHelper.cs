using System.Globalization;
using ANNA_AGUMAVA_MARIAM_TURMANIDZE_FinalProjectStudent.Models;

namespace ANNA_AGUMAVA_MARIAM_TURMANIDZE_FinalProjectStudent.Helpers;

internal static class ConsoleHelper
{
    /// <summary>
    /// This method reads a line of text from the console after displaying a prompt. It returns the entered text, or an empty string if the user presses Enter without typing anything.
    /// </summary>
    /// <param name="prompt"></param>
    /// <returns></returns>
    public static string ReadText(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine() ?? "";
    }

    /// <summary>
    /// This method repeatedly prompts the user for input until they provide a non-empty, non-whitespace string. It trims any leading or trailing whitespace from the input before returning it.
    /// </summary>
    /// <param name="prompt"></param>
    /// <returns></returns>
    public static string ReadRequired(string prompt)
    {
        while (true)
        {
            string value = ReadText(prompt).Trim();
            if (!string.IsNullOrWhiteSpace(value))
            {
                return value;
            }
            Console.WriteLine("This field cannot be empty or contain only whitespace.");
        }
    }

    /// <summary>
    /// This method prompts the user for input and attempts to parse it as an integer. If the input is not a valid integer, it displays an error message and prompts the user again until a valid integer is entered.
    /// </summary>
    /// <param name="prompt"></param>
    /// <returns></returns>
    public static int ReadInt(string prompt)
    {
        int value;
        Console.Write(prompt);
        while (!int.TryParse(Console.ReadLine(), out value))
        {
            Console.Write("Invalid number, try again: ");
        }
        return value;
    }

    /// <summary>
    /// This method prompts the user for input and ensures that the input is a single character representing a grade between 'A' and 'F'. It continues to prompt the user until valid input is provided.
    /// </summary>
    /// <param name="prompt"></param>
    /// <returns></returns>
    public static char ReadGrade(string prompt)
    {
        while (true)
        {
            string input = ReadText(prompt).Trim().ToUpper();
            if (input.Length == 1 && input[0] >= 'A' && input[0] <= 'F')
            {
                return input[0];
            }
            Console.WriteLine("Enter only ONE character from 'A' to 'F'.");
        }
    }
    /// <summary>
    /// Reads a date from the console, repeating until the input matches the yyyy-MM-dd format.
    /// </summary>
    /// <param name="prompt"></param>
    /// <returns></returns>
    public static DateOnly ReadDate(string prompt)
    {
        DateOnly date;
        while (!DateOnly.TryParseExact(ReadText(prompt), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
        {
            Console.WriteLine("Invalid date format. Use yyyy-MM-dd.");
        }
        return date;
    }

    /// <summary>
    /// Prompts the user to press Enter and waits for input.
    /// </summary>
    public static void Pause()
    {
        Console.WriteLine("Press Enter To Continue");
        Console.ReadLine();
    }
    /// <summary>
    /// Prints the specified student's information to the console.
    /// </summary>
    /// <param name="student"></param>
    public static void PrintStudent(Student student)
    {
        Console.WriteLine($"Name: {student.StudentName}");
        Console.WriteLine($"Id: {student.Id}");
        Console.WriteLine($"Roll Number: {student.RollNumber}");
        Console.WriteLine($"Grade: {student.Grade}");
        Console.WriteLine($"School Name: {student.SchoolName}");
        Console.WriteLine($"Birth Date: {student.BirthDate}");
        Console.WriteLine($"Course: {student.Course}");
        Console.WriteLine();
    }
}
