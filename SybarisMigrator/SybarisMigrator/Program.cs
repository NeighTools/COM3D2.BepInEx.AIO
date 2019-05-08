using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace SybarisMigrator
{
    public static class Program
    {
        private static readonly string[] FilesToRemove =
        {
                "Sybaris.Loader.dll", "COM3D2.UnityInjector.Patcher.dll", "Mono.Cecil.dll", "ExIni.dll", "opengl32.dll", "UnityInjector.dll"
        };

        public static void Main(string[] args)
        {
            DoMigrate();
        }

        private static void WriteLine(ConsoleColor col, string message)
        {
            ConsoleColor old = Console.ForegroundColor;
            Console.ForegroundColor = col;
            Console.WriteLine(message);
            Console.ForegroundColor = old;
        }

        private static void Write(ConsoleColor col, string message)
        {
            ConsoleColor old = Console.ForegroundColor;
            Console.ForegroundColor = col;
            Console.Write(message);
            Console.ForegroundColor = old;
        }

        private static bool IsPatched()
        {
            return File.Exists("sybaris_migrator.lock");
        }

        private static void PrintMigrated()
        {
            Console.Clear();

            WriteLine(ConsoleColor.Green, new string('=', Console.WindowWidth - 2));
            WriteLine(ConsoleColor.Green, "Info");
            WriteLine(ConsoleColor.Green, new string('=', Console.WindowWidth - 2));
            Console.WriteLine();
            Console.WriteLine("Migration is complete!");
            Console.WriteLine("Sybaris Migrator detects that the migration is complete.");
            Console.WriteLine("You can run the game normally.");
            Console.WriteLine();
            WriteLine(ConsoleColor.Yellow, "NOTE: If you install any Sybaris AIO packs, you must re-run the tool again.");
            Console.WriteLine();
            Console.WriteLine("Press any key to close...");
            Console.ReadKey(true);
        }

        private static void DoMigrate()
        {
            Console.Clear();

            WriteLine(ConsoleColor.Green, new string('=', Console.WindowWidth - 2));
            WriteLine(ConsoleColor.Green, "Sybaris Migrator");
            WriteLine(ConsoleColor.Green, new string('=', Console.WindowWidth - 2));
            Console.WriteLine();

            Console.WriteLine("Welcome to Sybaris Migrator!");
            Console.WriteLine("This tool will automatically migrate your (possible) Sybaris 2 installation to BepInEx.");
            Console.WriteLine();
            Console.WriteLine("NOTE: Please run this EXE from the game's main directory if it's not there yet.");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Press any key to begin migration...");
            Console.ReadKey(true);

            Console.Clear();
            WriteLine(ConsoleColor.Green, new string('=', Console.WindowWidth - 2));
            WriteLine(ConsoleColor.Green, "Step 1: Detecting Sybaris files");
            WriteLine(ConsoleColor.Green, new string('=', Console.WindowWidth - 2));
            Console.WriteLine();

            Console.WriteLine("First, the migrator will try to find files that will conflict with BepInEx.");
            Console.WriteLine("The migrator WILL NOT DELETE anything!");
            Console.WriteLine();
            Console.WriteLine("Press any key to search for files...");
            Console.ReadKey(true);

            Console.Clear();

            Console.WriteLine("Searching for Sybaris files. This will take a moment.");

            var invalidFiles = new HashSet<string>(FilesToRemove, StringComparer.InvariantCultureIgnoreCase);
            var filesToDelete = new HashSet<string>();
            foreach (string file in Directory.EnumerateFiles(".", "*", SearchOption.AllDirectories))
            {
                string path = Path.GetDirectoryName(file).ToLowerInvariant();
                string name = Path.GetFileName(file);

                if (path.Contains("bepinex") || path.Contains("sybaris_old"))
                    continue;

                if (!invalidFiles.Contains(name))
                    continue;
                Console.WriteLine($"Found {file}");
                filesToDelete.Add(file);
            }

            bool deleteBepInExFolder = false;
            if (File.Exists("BepInEx/core/BepInEx.dll"))
            {
                var fileInfo = FileVersionInfo.GetVersionInfo("BepInEx/core/BepInEx.dll");
                if (int.Parse(fileInfo.FileVersion.Substring(0, 1)) < 5)
                    deleteBepInExFolder = true;
                fileInfo = null;
                GC.Collect();
            }

            Console.Clear();
            WriteLine(ConsoleColor.Green, new string('=', Console.WindowWidth - 2));
            WriteLine(ConsoleColor.Green, "Step 2: Moving the files");
            WriteLine(ConsoleColor.Green, new string('=', Console.WindowWidth - 2));
            Console.WriteLine();

            if (filesToDelete.Count == 0 && !deleteBepInExFolder)
            {
                Console.WriteLine("No conflicting files found! Your game is clean!");
                Console.WriteLine();

                Console.WriteLine("Press any key to finish...");
                Console.ReadKey(true);
                PrintMigrated();
                return;
            }

            Console.WriteLine("The migrator found the following conflicting files:");
            foreach (string s in filesToDelete)
                Console.WriteLine($"* {s}");
            Console.WriteLine();

            if (deleteBepInExFolder)
            {
                Console.WriteLine("The migrator also found an incompatible installation of BepInEx.");
            }

            Console.Write("Migrator will now move these files to ");
            Write(ConsoleColor.Cyan, "sybaris_old");
            Console.WriteLine(" folder.");
            Console.WriteLine();

            WriteLine(ConsoleColor.Yellow,
                      "The next operation is permanent. To reverse it you will have to manually move the files out of sybaris_old.");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Press ENTER to migrate");
            Console.WriteLine("Press ESC to exit");

            ConsoleKeyInfo info;
            do
            {
                info = Console.ReadKey(true);
            } while (info.Key != ConsoleKey.Enter && info.Key != ConsoleKey.Escape);

            if (info.Key == ConsoleKey.Escape)
                Environment.Exit(0);

            if (!Directory.Exists("sybaris_old"))
                Directory.CreateDirectory("sybaris_old");

            foreach (string file in filesToDelete)
            {
                string dest = Path.GetFullPath(Path.Combine("sybaris_old", file));
                Directory.CreateDirectory(Path.GetDirectoryName(dest));
                if(File.Exists(dest))
                    File.Delete(dest);
                File.Move(Path.GetFullPath(file), dest);
            }

            if (deleteBepInExFolder)
            {
                foreach (string file in Directory.EnumerateFiles("BepInEx", "*", SearchOption.AllDirectories))
                {
                    string dest = Path.GetFullPath(Path.Combine("sybaris_old", file));
                    Directory.CreateDirectory(Path.GetDirectoryName(dest));
                    if(File.Exists(dest))
                        File.Delete(dest);
                    File.Move(Path.GetFullPath(file), dest);
                }

                try
                {
                    Directory.Delete(Path.GetFullPath("BepInEx"), true);
                }
                catch (Exception) { }

                try
                {
                    Directory.Delete(Path.GetFullPath("BepInEx"));
                }
                catch (Exception) { }
            }

            PrintMigrated();
        }
    }
}