using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Net;


partial class Program
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

    public static void LinkAndArchiveTypeValidation(ref String link, ref String archiveType)
    {
        //Formatting link
        if (!link.Contains("https://"))
        {
            link = LinkHttpsFormatter(link);
        }

        if (!link.Contains("."))
        {
            Console.WriteLine("did u forgot the .?");
            Environment.Exit(1);
        }

        //Formatting Archive
        if (archiveType.Contains("."))
        {
            archiveType = ArchiveFormatter(archiveType);
        }
    }

    public static void NameValidation(ref String name)
    {
        if (name.Contains("$"))
        {
            Console.WriteLine("Invalid name, please follow the default windows archive naming rules");
        }
    }

    static void Main()
    {
        Console.WriteLine("insert your download link: ");
        String link = Console.ReadLine();

        Console.WriteLine("insert the archive type. Example: (txt,exe,zip... etc)");
        String archiveType = Console.ReadLine();

        Console.WriteLine("name your archive: (do NOT use these characters: < > : ' / \\ | ? * or any other special chars");
        String archiveName = Console.ReadLine();

        Console.WriteLine("insert your directory (empty to C:\\)");
        String directory = Console.ReadLine();
        if (directory == "")
        {
            directory = "C:\\";
        }

        LinkAndArchiveTypeValidation(ref link, ref archiveType);

        string fileDirectory = directory + archiveName + "." + archiveType;

        using (WebClient client = new WebClient())
        {
            client.DownloadFile(link, fileDirectory);
        }

        Console.WriteLine("-------------");
    }
}