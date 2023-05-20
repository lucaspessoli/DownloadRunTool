using System.Diagnostics.CodeAnalysis;
using System.Threading;


partial class Program
{

    public static void LinkValidation(ref String link, ref String archiveType)
    {

        public static string LinkHttpsFormatter(String insertedLink)
        {
            string webPrefix = "htpps://";
            insertedLink = webPrefix + insertedLink;
            return insertedLink;
        }

        public static string ArchiveFormatter(String type)
        {
            char charToBeRemoved = '.';
            int index = type.IndexOf(charToBeRemoved);
            type = type.Remove(index, 1); //1 is the quantity of characters to be replaced
            return type;

        }

        if (!link.Contains("https://"))
        {
            link = LinkHttpsFormatter(link);
        }
        if (!link.Contains("."))
        {
            Console.WriteLine("did u forgot the .?");
        }
        if (!link.Contains("www"))
        {
            Console.WriteLine("no www prefix found, exiting program...");
            Environment.Exit(1);
        }
        if (archiveType.Contains("."))
        {
            archiveType = ArchiveFormatter(archiveType);
        }
    }
    static void Main()
    {
        Console.WriteLine("insert your download link: ");
        String link = Console.ReadLine();

        Console.WriteLine("insert the archive type. Example: (txt,exe,zip... etc)");
        String archiveType = Console.ReadLine();

        LinkValidation(ref link, ref archiveType);

        Console.WriteLine("-------------");
    }
}