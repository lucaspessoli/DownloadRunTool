using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Net;
using System.Linq.Expressions;

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

    public static void DownloadFileLocal(String link, String fileDirectory)
    {
        using (WebClient client = new WebClient())
        {
            try
            {
                Console.WriteLine("Downloading...");
                client.DownloadFile(link, fileDirectory);
                Console.WriteLine("Sucess! File downloaded and stored at: " + fileDirectory);
            }
            catch (Exception e)
            {
                Console.WriteLine("error downloading your file, error: " + e.Message);
                Console.WriteLine("Possible Reasons: no adm privileges found, try open the software as a administrator, your download host is offline or invalid directory");
                Console.WriteLine("Exiting in 5 seconds...");
                Thread.Sleep(5000);
                Environment.Exit(1);
            }
        }
    }
    public static void DownloadMenu() {
        Console.WriteLine("insert your download link: ");
        String link = Console.ReadLine();

        Console.WriteLine("name your archive: (do NOT use these characters: < > : ' / \\ | ? * or any other special chars");
        String archiveName = Console.ReadLine();

        Console.WriteLine("insert the archive type. Example: (txt,exe,zip... etc)");
        String archiveType = Console.ReadLine();

        Console.WriteLine("insert your directory (empty to C:\\)");
        String directory = Console.ReadLine();
        if (directory == "")
        {
            directory = "C:\\";
        }

        LinkAndArchiveTypeValidation(ref link, ref archiveType);
        string fileDirectory = directory + archiveName + "." + archiveType;
        DownloadFileLocal(link, fileDirectory);
    }

    public static void ExecuteMenu()
    {
        Console.WriteLine("Execute menu...");
    }

    static void Main()
    {
        bool runMenu = true;
        while (runMenu){
            Console.WriteLine("[1] - Download Tool -? download a file with personal name,file type etc");
            Console.WriteLine("[2] - Execute file -?");
            String menuAnswer = Console.ReadLine();
            switch (menuAnswer)
            {
                case "1":
                    DownloadMenu();
                    break;
                case "2":
                    ExecuteMenu();
                    break;
                default:
                    Console.WriteLine("Opção inesperada!");
                    break;
            }
            Console.WriteLine("return to menu? (Y/n)");
            String answer = Console.ReadLine();
            if(answer != "" ||  answer != "Y" || answer != "y")
            {
                runMenu = false;
            }
        }

        Console.WriteLine("Thanks for using the tool! Exiting in 5 seconds...");
        Thread.Sleep(5000);

        Console.WriteLine("-------------");
    }
}