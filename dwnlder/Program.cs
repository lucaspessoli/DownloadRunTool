using System.Net;
using System.Diagnostics;
using System.IO;
using System.Text;


partial class Program
{
    public static List<String> typeList = new List<String>()
    {
        ".txt",
        ".exe",
        ".zip",
        ".jpg",
        ".png",
        ".gif"
    };
    public static List<String> execNames = new List<String>()
    {
        "notepad.exe",
        "",
        "C:\\Program Files\\WinRAR\\WinRAR.exe",
        "msPaint.exe",
        "msPaint.exe",
        "msPaint.exe"
    };

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
                LogRegister("File downloaded at: " + fileDirectory, false);
            }
            catch (Exception e)
            {
                Console.WriteLine("error downloading your file, error: " + e.Message);
                Console.WriteLine("Possible Reasons: no adm privileges found, try open the software as a administrator, your download host is offline or invalid directory");
                LogRegister(e.Message, true);
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
        Console.WriteLine("Insert your file directory: (ex: C:\\)");
        String fileDir = Console.ReadLine();
        fileDir = fileDir + "\\";

        Console.WriteLine("Insert your file sufix: (ex: zip exe  etc...");
        String sufixFile = Console.ReadLine();

        sufixFile = ArchiveFormatter(sufixFile);
        sufixFile = "." + sufixFile;
        string fileName = FileReaderAtFolder(fileDir, sufixFile);

        foreach (String list in typeList)
        {
            int i = typeList.IndexOf(list);
            if (sufixFile.Contains(list))
            {
                try
                {
                    if (sufixFile == ".exe")
                    {
                        Process.Start(fileName);
                        LogRegister("Arquivo aberto: " + fileName, false);
                    }
                    else
                    {
                        Process.Start(execNames[i], fileName);
                        LogRegister("Arquivo aberto: " + fileName, false);
                    }
                }catch(Exception e)
                {
                    LogRegister("Erro: " + e, true);
                }

            }
        }
    }

    public static string getDate()
    {
        DateTime dateObj = DateTime.Now;
        String dateNow = dateObj.ToString("dd/MM/yyy HH:mm");
        dateNow = ("[" + dateNow + "] - ");

        return dateNow;
    }

    public static string FileReaderAtFolder(string folder, string archiveType)
    {
        string fileName = "";
        try
        {
            archiveType = "*" + archiveType;
            string[] tempVectorDocuments = System.IO.Directory.GetFiles(folder, archiveType);
            List<String> documents = new List<String>();
            if (tempVectorDocuments.Length != 0)
            {
                foreach (String item in tempVectorDocuments)
                {
                    documents.Add(item);
                }
                foreach (string item in documents)
                {
                    int i = documents.IndexOf(item);
                    Console.WriteLine("["+i+"] File:" + item);
                }
                Console.WriteLine("Insert the file number that u want to run");
                int fileNumber = int.Parse(Console.ReadLine());
                fileName = documents[fileNumber];
            }
            else
            {
                Console.WriteLine("No files at your folder!");
            }
        }catch(Exception e)
        {
            Console.WriteLine(e);
        }
        return fileName;
    }

    public static void LogRegister(string log, bool isError)
    {
        if (isError)
        {
            string path = "C:\\logsError.txt";
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine(getDate() + "Error: " + log);
            }
        }
        else
        {
            string path = "C:\\logs.txt";
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine(getDate() + log);
            }
        }
    }

    public static string getData(string filePath)
    {
        string contentData = "";
        if (File.Exists(filePath))
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                contentData = sr.ReadToEnd();
            }
        }
        else
        {
            Console.WriteLine("path wasn't found!");
        }
        return contentData;
    }

    public static void ShowTutorial()
    {
        Console.Clear();
        Console.WriteLine("Welcome to tutorial.");
        Thread.Sleep(400);
        bool runTutorial = true;
        string tutAnswer = "";
        while (runTutorial)
        {
            Console.WriteLine("which tutorial do u want?\n[1] - Downloader\n[2] - File Runner\n[3] - Exit");
            string answer = Console.ReadLine();
            switch (answer)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("1. To start off, the your download adress link");
                    Thread.Sleep(1500);
                    Console.WriteLine("2. Insert your archiveType, like exe, zip, txt... etc");
                    Thread.Sleep(1500);
                    Console.WriteLine("3. Finally insert the directory that will store your file, like: C:\\myFolder and your download would start");
                    Thread.Sleep(1500);
                    Console.WriteLine("\nDo you want to continue tutorial? (y/n)");
                    tutAnswer = Console.ReadLine().ToLower();
                    if(tutAnswer == "n")
                    {
                        runTutorial = false;
                    }
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("1. To start off. Insert your directory that contains your file. Like: C:\\folderThatContainMyFiles");
                    Thread.Sleep(400);
                    Console.WriteLine("2. Finally insert your archiveType, like exe, zip, txt... etc and select your file to open");
                    Console.WriteLine("\nDo you want to continue tutorial? (y/n)");
                    tutAnswer = Console.ReadLine().ToLower();
                    if (tutAnswer == "n")
                    {
                        runTutorial = false;
                    }
                    break;
                case "3":
                    runTutorial = false;
                    break;
            }
        }
    }

    static void Main()
    {
        bool runMenu;
        if (getData("C:\\util.txt").Contains("version: 1.19"))
        {
            runMenu = true;
            if(getData("C:\\util.txt").Contains("firstRun: true"))
            {
                Console.WriteLine("Seems that is your first time here. Do you want a tutorial? (y/n)");
                string answer = Console.ReadLine().ToLower();
                if(answer.Equals("y"))
                {
                    ShowTutorial();
                }
            }
        }
        else
        {
            runMenu = false;
            Console.WriteLine("OUTDATED TOOL VERSION, QUITING...");
        }
        while (runMenu)
        {
            Console.Clear();
            Console.WriteLine("highly recommended running this software as a administrator!");
            Console.WriteLine("github.com/lucaspessoli\n\n");
            Console.WriteLine("[1] - Download Tool -? download a file with personal name,file type etc");
            Console.WriteLine("[2] - Execute file -? run a personal file of your choice");
            Console.WriteLine("[3] - Tutorial");
            String menuAnswer = Console.ReadLine();
            switch (menuAnswer)
            {
                case "1":
                    DownloadMenu();
                    break;
                case "2":
                    ExecuteMenu();
                    break;
                case "3":
                    ShowTutorial();
                    break;
                default:
                    Console.WriteLine("Unexpected option!");
                    break;
            }
            Console.WriteLine("Return to menu? (y/n)");
            String answer = Console.ReadLine().ToLower();
            if (answer == "n")
            {
                runMenu = false;
            }
        }

        Console.WriteLine("Thanks for using the tool! Exiting in 5 seconds...");
        Thread.Sleep(5000);

        Console.WriteLine("-------------");
    }
}