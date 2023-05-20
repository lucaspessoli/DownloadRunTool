using System.Diagnostics.CodeAnalysis;
using System.Threading;


partial class Program
{
    static void Main()
    {
        Console.WriteLine("insert your download link: ");
        String link = Console.ReadLine();
        if (link.Contains("https://") && link.Contains(".com"))
        {
            Console.WriteLine("Link válido!");
        }
        else
        {
            Console.WriteLine("Link invalido!");
        }
    }
}