#include <iostream>
#include <filesystem>
#include <cstdlib>
#include <sstream>
#include <vector>
#include <string>
#include <fstream>

// Function to handle the command loop of the shell
void BerkaShell()
{
	std::filesystem::path path;
	std::filesystem::path currentPath = std::filesystem::current_path();
	//Command Loop
	while (true)
	{
		// Directory prompt
		std::cout << currentPath.string() << "> ";
		// Read user input
		std::string command;
		std::getline(std::cin, command);
		std::istringstream iss(command);
		std::string token;
		std::vector<std::string> args;
		while (iss >> token) {
			args.push_back(token);
		}

		//Commands

		// Check if command is empty
		if (args.size() == 0)
		{
			continue;
		}

		// Help command
		else if (args.size() <= 1&&args[0]=="help")
		{
			std::cout << "Available commands:" << std::endl;
			std::cout << "list [path] - List files and directories in the specified path (or current directory if no path is provided)" << std::endl;
			std::cout << "run [file] - Run the specified file" << std::endl;
			std::cout << "rename [source] [destination] - Rename a file or directory" << std::endl;
			std::cout << "move [source] [destination] - Move a file or directory to a new location" << std::endl;
			std::cout << "delete [path] - Delete the specified file or directory" << std::endl;
			std::cout << "goto [path] - Change the current directory to the specified path" << std::endl;
			std::cout << "new [type] [name] [path (optional)] - Create a new file or folder of the specified type and name at the specified path (or current directory if no path is provided)" << std::endl;
			std::cout << "clear - Clear the console" << std::endl;
			std::cout << "exit - Exit the shell" << std::endl;
		}

		// List command
		else if (args.size() <= 2&&args[0]=="list")
		{
			if (args.size() == 1)
			{
				try {
					for (const auto& entry : std::filesystem::directory_iterator(currentPath))
					{
						std::cout << entry.path().filename().string() << std::endl;
					}
				}
				catch (std::exception)
				{
					std::cout << "Critical Error!" << std::endl;
				}
			}
			if (args.size() == 2)
			{
				try {
					for (const auto& entry : std::filesystem::directory_iterator(args[1]))
					{
						std::cout << entry.path().filename().string() << std::endl;
					}
				}
				catch (std::exception)
				{
					std::cout << "Critical Error!" << std::endl;
				}
			}
			
		}



		// Run command
		else if (args.size() == 2 && args[0] == "run")
		{
			std::filesystem::path fileToRun = args[1];

			if (std::filesystem::exists(fileToRun))
			{
				try {
					system(fileToRun.string().c_str());
					std::cout << "Program executed successfully" << std::endl;
				}
				catch (...) {
					std::cout << "Error running file!" << std::endl;
				}
			}
			else {
				std::cout << "File not found!" << std::endl;
			}
		}

		// Rename command
		else if (args.size() <= 3&&args[0] == "rename")
		{
			std::filesystem::path source = args[1];
			std::filesystem::path destination = args[2];
			if (std::filesystem::exists(source))
			{
				try
				{
					std::filesystem::path destination = source.parent_path() / args[2];
					std::filesystem::rename(source, destination);
					std::cout << "Renamed successfully" << std::endl;
				}
				catch(std::exception)
				{
					std::cout << "Critical Error!" << std::endl;
				}
			}
		}

		// Move command 
		else if (args.size() <= 3 && args[0] == "move")
		{
			std::filesystem::path source = args[1];
			std::filesystem::path destination = args[2];
			if (std::filesystem::exists(source))
			{
				try
		        {
					std::filesystem::rename(source, destination/source.filename());
					std::cout << "Moved successfully" << std::endl;
				}
				catch(std::exception)
				{
					std::cout << "Critical Error!" << std::endl;
				}
			}
		}

		// Delete command
		try {
			 if (args.size() <= 2 && args[0] == "delete")
			{
				path = args[1];
				if (args.size() == 2)
				{
					if (std::filesystem::is_directory(path))
					{
						std::filesystem::remove_all(path);
						std::cout << "Folder deleted successfully" << std::endl;
					}
					else {
						if (std::filesystem::remove(path))
						{
							std::cout << "File deleted successfully" << std::endl;
						}
					}
				}
			}
		}
		catch (std::exception)
		{
			std::cout << "Critical Error!" << std::endl;
		}


		// Go to command
		if (args.size() == 2 && args[0] == "goto")
		{
			if (std::filesystem::is_directory(args[1]))
			{
				currentPath = args[1];
				std::cout << "Directory changed successfully" << std::endl;
			}
			else
			{
				std::cout << "Directory does not exist!" << std::endl;
			}
		}
		try {
			// New command
			if (args.size() <= 4 && args[0] == "new")
			{
				// Parts
				std::string type = args[1];
				std::string name = args[2];
				if (args.size() == 4)
				{
					path = args[3];
				}
				// Create file
				if (type == "file")
				{
					if (args.size() == 4)
					{
						std::ofstream file((path / name).string());
						std::cout << "File created succesfully" << std::endl;
						file.close();
					}
					if (args.size() == 3)
					{
						std::ofstream file((currentPath / name).string());
						std::cout << "File created succesfully" << std::endl;
						file.close();
					}
				}
				// Create folder
				if (type == "folder")
				{
					if (args.size() == 4)
					{
						if (std::filesystem::create_directory((path / name).string()))
						{
							std::cout << "Folder created succesfully" << std::endl;
						}
					}
					if (args.size() == 3)
					{
						if (std::filesystem::create_directory(currentPath / name))
						{
							std::cout << "Folder created succesfully" << std::endl;
						}
					}
				}
			}
		}
			catch (std::exception)
			{
				std::cout << "Critical Error!" << std::endl;
			}

		
		//Exit command
		if (args.size() <= 1&&args[0]=="exit")
		{
			std::cout << "Exiting BerkaShell. Goodbye!" << std::endl;
			break;
		}
		//Clear command
		if (args.size()<=1&&args[0]=="clear")
		{
			std::cout << "\033[2J\033[1;1H";
		}

	}
}


// main function to start the shell
int main()
{
	std::cout << "Welcome to BerkaShell!" << std::endl;
	BerkaShell();
	return 0;
}