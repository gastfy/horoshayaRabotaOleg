using System.Diagnostics;

namespace ConsoleApp10
{
    internal class Menu
    {
        private int POS;
        private int min;//я не знаю зачем мне эти переменные но раз надо так надо
        private int max;//прыгну со скалы и разобьюсь о волны, навсегда оставшись..
        public Menu(int pos)
        {
            min = pos;
            max = pos + 1;
            POS = pos;
            PrintMenuArrows();
        }
        public void Update(int pos, bool isUp)
        {
            if (isUp)
            {
                Console.SetCursorPosition(0, pos + 1);
                Console.Write("  ");
            }
            else
            {
                Console.SetCursorPosition(0, pos - 1);
                Console.Write("  ");
            }
            POS = pos;
            
            PrintMenuArrows();
        }
        private void PrintMenuArrows()
        {
            
            Console.SetCursorPosition(0, POS);
            Console.Write("->");
        }
    }
    internal static class getFiles
    {
        public static void getDirectories(string path)
        {
            Console.Clear();           
            int posInDirs = 0;
            Menu secondMenu = new Menu(posInDirs);
            while (true)
            {
                int help = 0;
                string[] alldirs = Directory.GetFileSystemEntries(path);
                foreach (string item in alldirs)
                {
                    DateTime birthdate = Directory.GetCreationTime(item);
                    if (Path.HasExtension(item))
                    {
                        Console.SetCursorPosition(3, help);
                        string[] all = item.Split("\\");
                        string realItem = all[all.Length - 1];
                        Console.Write(realItem.Replace(Path.GetExtension(item), ""));
                        Console.SetCursorPosition(60, help);
                        Console.Write(birthdate);
                        Console.SetCursorPosition(100, help);
                        Console.Write(Path.GetExtension(item));
                        help++;
                    }
                    else
                    {
                        string[] all = item.Split("\\");
                        string realItem = all[all.Length - 1];
                        Console.SetCursorPosition(3, help);
                        Console.Write(realItem);
                        Console.SetCursorPosition(60, help);
                        Console.Write(birthdate);
                        help++;
                    }
                }
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.Enter)
                {
                    alldirs.DefaultIfEmpty();
                    Open(alldirs[posInDirs]);
                    break;
                }
                if(key.Key == ConsoleKey.Escape) { Console.Clear(); break; }
                if (key.Key == ConsoleKey.DownArrow)
                {
                    if (posInDirs == alldirs.Length - 1)
                    {
                        secondMenu.Update(posInDirs, false);
                    }
                    else
                    {
                        secondMenu.Update(posInDirs += 1, false);
                    }
                }
                else if (key.Key == ConsoleKey.UpArrow)
                {
                    if (posInDirs != 0)
                    {
                        secondMenu.Update(posInDirs -= 1, true);
                    }
                    else
                    {
                        secondMenu.Update(posInDirs, true);
                    }
                }

            }
        }


        private static void Open(string npath)
        {
            if (Directory.Exists(npath))
            {
                Console.Clear();
                getDirectories(npath);
            }
            else if (File.Exists(npath))
            {
                Process.Start(new ProcessStartInfo { FileName = npath, UseShellExecute = true });
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> dirs = new List<string>();
            int position = 0;
            Menu FirstMenu = new Menu(position);
            while (true)
            {
                int a = 0;
                DriveInfo[] drives = DriveInfo.GetDrives();
                foreach (DriveInfo drive in drives)
                {
                    Console.SetCursorPosition(3, a);
                    Console.WriteLine($"{drive.Name} - доступного места: {drive.AvailableFreeSpace}");
                    dirs.Add(drive.Name.Split("\\")[0]);
                    a++;
                }
                ConsoleKeyInfo key = Console.ReadKey();
                if(key.Key == ConsoleKey.Escape) { break; }
                if(key.Key == ConsoleKey.Enter)
                {
                    getFiles.getDirectories($"{dirs[position]}\\");
                }
                if (key.Key == ConsoleKey.DownArrow)
                {
                    if(position == drives.Length-1)
                    {
                        FirstMenu.Update(position, false);
                    }
                    else
                    {
                        FirstMenu.Update(position += 1, false);
                    }
                }
                else if(key.Key == ConsoleKey.UpArrow)
                {
                    if(position != 0)
                    {
                        FirstMenu.Update(position -= 1, true);
                    }
                    else
                    {
                        FirstMenu.Update(position, true);
                    }
                }
               
            }
        }
    }
}
//маленькой частью моря