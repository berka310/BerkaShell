//Using 
using Microsoft.VisualBasic.FileIO;
using System.ComponentModel.Design;

//Program Class
class berkaShell
{
    //Variables
    static string currentDirectory = Directory.GetCurrentDirectory();
    static string path;
    static string fullPath;

    //Main
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to BerkaShell!");
        ShellMain();
    }

    //Main Shell Loop
    static void ShellMain()
    {
        //Loop
        while (true)
        {
            //Directory Prompt
            Console.Write($"{currentDirectory}> ");

            //Read Input
            string input = Console.ReadLine();
            //Split Input
            string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            //Command Control
            if (parts[0] == "New" || parts[0] == "Delete" || parts[0] == "Goto" || parts[0] == "Clear" || parts[0] == "Exit" || parts[0] == "Move" || parts[0] == "Help")
            {

            }
            else
            {
                Console.WriteLine("Command not Found!");
            }
            try
            {
                //Move Command
                if (parts.Length >= 4 && parts[0] == "Move")
                {

                    string type = parts[1];
                    string target = parts[2];
                    string targetpath = parts[3];
                    if (type == "File" && File.Exists(target))
                    {
                        string destFile = Path.Combine(targetpath, Path.GetFileName(target));
                        File.Move(target, destFile);
                        Console.WriteLine("File moved successfully!");
                    }
                    else if (type == "Folder" && Directory.Exists(target))
                    {
                        string destDir = Path.Combine(targetpath, Path.GetFileName(target));
                        Directory.Move(target, destDir);
                        Console.WriteLine("Folder moved successfully!");
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Critical Error!");
            }

            //Help Command
            if (input == "Help")
            {
                Console.WriteLine("Available Commands:");
                Console.WriteLine("New File <Name> [Path] - Creates a new file with the specified name. Optionally, you can provide a path to create the file in a specific directory.");
                Console.WriteLine("New Folder <Name> [Path] - Creates a new folder with the specified name. Optionally, you can provide a path to create the folder in a specific directory.");
                Console.WriteLine("Delete File <Path> - Deletes the specified file.");
                Console.WriteLine("Delete Folder <Path> - Deletes the specified folder and its contents.");
                Console.WriteLine("Goto <Path> - Changes the current directory to the specified path.");
                Console.WriteLine("Clear - Clears the console screen.");
                Console.WriteLine("Exit - Exits BerkaShell.");
                Console.WriteLine("Move File <SourcePath> <DestinationPath> - Moves a file from the source path to the destination path.");
                Console.WriteLine("Move Folder <SourcePath> <DestinationPath> - Moves a folder from the source path to the destination path.");
            }

            //New Command
            try
            {
                if (parts.Length >= 3 && parts[0] == "New")
                {
                    string type = parts[1];
                    string name = parts[2];
                    if (parts.Length == 4)
                    {
                        path = parts[3];
                        fullPath = Path.Combine(path, name);
                    }
                    string rawdirectory = Path.Combine(currentDirectory, name);
                    if (type == "File")
                    {
                        if (parts.Length == 3)
                        {
                            File.Create(rawdirectory).Dispose();
                            Console.WriteLine($"File Created Succesfully");
                        }
                        else
                        {
                            File.Create(fullPath).Dispose();
                            Console.WriteLine($"File Created Succesfully");
                        }
                    }
                    else if (type == "Folder")
                    {
                        if (parts.Length == 3)
                        {

                            Directory.CreateDirectory(rawdirectory);
                            Console.WriteLine($"Folder Created Succesfully");
                        }
                        else
                        {
                            Directory.CreateDirectory(fullPath);
                            Console.WriteLine($"Folder Created Succesfully");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Command not Found!");
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Critical Error!");
            }
            //Exit Command
            if (input == "Exit")
            {
                Console.WriteLine("Exiting BerkaShell...");
                break;
            }
            //Clear Command
            if (input == "Clear")
            {
                Console.Clear();
            }
            //Delete Command
            try
            {
                if (parts.Length >= 3 && parts[0] == "Delete")
                {
                    string type = parts[1];
                    string path = parts[2];
                    if (type == "File")
                    {
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                            Console.WriteLine("File Deleted Succesfully!");
                        }
                        else Console.WriteLine("File not found!");
                    }
                    else if (type == "Folder")
                    {
                        if (Directory.Exists(path))
                        {
                            if (Directory.EnumerateFileSystemEntries(path).Any())
                            {

                                Directory.Delete(path, true);
                                Console.WriteLine("Folder Deleted Succesfully!");
                            }
                            else
                            {

                                Directory.Delete(path);
                                Console.WriteLine("Folder Deleted Succesfully!");
                            }

                        }
                        else Console.WriteLine("Folder not found!");

                    }
                }
            }

            catch (Exception)
            {
                Console.WriteLine("Critical Error!");
            }

            //Go to Command

            if (parts.Length >= 2 && parts[0] == "Goto")
            {
                string path1 = parts[1];
                string fullPath = Path.GetFullPath(path1);
                if (Directory.Exists(fullPath))
                {
                    currentDirectory = fullPath;
                    Console.WriteLine("Directory Changed Succesfully!");
                }
                else
                {
                    Console.WriteLine("Directory not Found!");
                }
            }
        }
    }
}


