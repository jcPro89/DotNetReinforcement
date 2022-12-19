internal class Program
{
    private static void Main()
    {
        int myYearOfBirth = 1989;
        int myAge = DateTime.Now.Year - myYearOfBirth;
        var myHeight = 1.86m; //literal decimal 
        int myWeight = 67;

        Console.Write($"I'm {myAge} years old. "); //String interpolation        
        Console.WriteLine($"Next year, I'll be {++myAge}."); //addition assignment with increment before retrieving the variable's value

        /* Quotient (left of the assignment operator) AND 
         * either the dividend or divisor must be of type decimal (or both) */
        decimal myImc = myWeight / (myHeight*myHeight); 
        Console.WriteLine($"I'm {myHeight}m tall. I have an IMC of {myImc}.");

        Console.WriteLine("My diminutives are \"Jona\", \"Jony\" and \"Jonita\"\n"); // double-quotation mark character escape \"

        string dataDirectory = "C:\\instrument"; // backslash character escape \\
        Console.WriteLine($"The paths for my application settings and output is {dataDirectory}, and these are the subdirectories:\n");        
        dataDirectory = @"C:\instrument\data  C:\instrument\config
            C:\instrument\log"; // Verbatim string literal
        Console.WriteLine($@"And again...The path for my application settings and output is {dataDirectory}");

        PrintMyCategories();
        _ = Console.Read();
    }

    private static void PrintMyCategories()
    {
        Console.WriteLine("----------------------------");
        Console.WriteLine("My categories");
        Console.WriteLine("----------------------------\n");

        Console.WriteLine("ITEM\t\t\tDESCRIPTION\n"); //tab character escape \t
        Console.WriteLine("Biblioteca multimedia\tMovies, personal and familily footage and pictures");
        Console.WriteLine("elJona\t\t\tHobbies, health, profile, money, time management, personal docs");
        Console.WriteLine("Familia\t\t\tFamily backups, house");
        Console.WriteLine("JW\t\t\tResponsibilities, docs, privileges");
        Console.WriteLine("Profesional\t\tCourses, certificates, job tools");
        Console.WriteLine("SOFTWARE\t\tInstallers, utilities");
    }
}