

using Microsoft.VisualBasic.FileIO;

namespace BerkaShell;

class berkaShell
{
    static string currentDirectory = Directory.GetCurrentDirectory();
    static string path;
    static string fullPath;

    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to BerkaShell!");
        ShellMain();
    }

    static void ShellMain()
    {

        while (true)
        {
            Console.Write($"{currentDirectory}> ");

            string input = Console.ReadLine();
            string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

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
                    if(parts.Length == 3) {
                        
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
            if (input == "Exit")
            {
                Console.WriteLine("Exiting BerkaShell...");
                break;
            }
            if (input == "Clear")
            {
                Console.Clear();
            }
            if(parts.Length >= 3 && parts[0]=="Delete")
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
