using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Net;
using System.Linq.Expressions;
using System.Diagnostics;
using System.Collections;

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
        if (type.Contains("."))
        {
            char charToBeRemoved = '.';
            int index = type.IndexOf(charToBeRemoved);
            type = type.Remove(index, 1); //1 is the quantity of characters to be replaced
        }
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
            Console.WriteLine("did u forgot the .? quiting...");
            Environment.Exit(1);
        }

        archiveType = ArchiveFormatter(archiveType);
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
            }
        }
    }
    public static void DownloadMenu()
    {
        Console.WriteLine("Insert your download link: ");
        String link = Console.ReadLine();

        Console.WriteLine("Name your archive: (do NOT use these characters: < > : ' / \\ | ? * or any other special chars");
        String archiveName = Console.ReadLine();

        Console.WriteLine("Insert the archive type. Example: (txt,exe,zip... etc)");
        String archiveType = Console.ReadLine();

        Console.WriteLine("Insert your directory (empty to C:\\)");
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
        List<String> typeList = new List<String>()
        {
            ".txt",
            ".exe",
            ".zip",
            ".jpg",
            ".png",
            ".gif"
        };
        List<String> execNames = new List<String>()
        {
            "notepad.exe",
            "",
            "C:\\Program Files\\WinRAR\\WinRAR.exe",
            "mspaint.exe",
            "mspaint.exe",
            "mspaint.exe"
        };

        Console.WriteLine("Insert your file directory: (ex: C:\\)");
        String fileDir = Console.ReadLine();

        Console.WriteLine("Insert your file name:");
        string fileName = Console.ReadLine();

        Console.WriteLine("Insert your file sufix: (ex: zip exe  etc...");
        Console.WriteLine("Obs: use 'bt' for .bat");
        String sufixFile = Console.ReadLine();


        sufixFile = ArchiveFormatter(sufixFile);
        sufixFile = "." + sufixFile;
        foreach (String list in typeList)
        {
            int i = typeList.IndexOf(list);
            if (sufixFile.Contains(list))
            {
                if(sufixFile == ".exe")
                {
                    Process.Start(fileDir + fileName + sufixFile);
                }
                else
                {
                    Process.Start(execNames[i], fileDir + fileName + sufixFile);
                }
            }
        }
    }

    static void Main()
    {
        bool runMenu = true;
        while (runMenu)
        {
            Console.Clear();
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
                    Console.WriteLine("Unexpected option!");
                    break;
            }
            Console.WriteLine("Return to menu? (y/n)");
            String answer = Console.ReadLine();
            if (answer == "N" || answer == "n")
            {
                runMenu = false;
            }
        }

        Console.WriteLine("Thanks for using the tool! Exiting in 5 seconds...");
        Thread.Sleep(5000);

        Console.WriteLine("-------------");
    }
}