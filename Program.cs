using System;
using System.IO;

namespace PR7
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] paths = WindowsExplorer.GetAndShowDrives();
            string currentPath = null;
            Arrow arrow = Arrow.GetNew(paths.Length);
            while (true)
            {
                //стрелочки - вверх/вниз
                //клавиши-команды записаны в поле _functional
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow:
                        arrow.RePrint(Arrow.Move.Up);
                        break;
                    case ConsoleKey.DownArrow:
                        arrow.RePrint(Arrow.Move.Down);
                        break;
                    case ConsoleKey.Enter:
                        string[] maybePaths = 
                            WindowsExplorer.Enter(paths[arrow.RelativePosition]);
                        PrintAdditionalFunctionality();
                        if (maybePaths == null)
                            break;
                        currentPath = paths[arrow.RelativePosition];
                        paths = maybePaths;
                        arrow = Arrow.GetNew(paths.Length);
                        break;
                    case ConsoleKey.Escape:
                        string root = WindowsExplorer.GetParentRoot(currentPath);
                        paths = WindowsExplorer.Enter(root);
                        if (root != null)
                            PrintAdditionalFunctionality();
                        currentPath = root;
                        arrow = Arrow.GetNew(paths.Length);
                        break;
                    case ConsoleKey.Delete:
                        ExplorerInteraction.Delete(paths[arrow.RelativePosition]); //основной метод
                        //доп действия
                        WindowsExplorer.Enter(currentPath);
                        PrintAdditionalFunctionality();
                        break;
                    case ConsoleKey.F1:
                        if (currentPath == null)
                            break;
                        ExplorerInteraction.CreateFolder(Path.Combine(currentPath,ScanfInput())); //основной метод
                        //доп действия
                        WindowsExplorer.Enter(currentPath);
                        PrintAdditionalFunctionality();
                        break;
                    case ConsoleKey.F2:
                        ExplorerInteraction.CreateFile(Path.Combine(currentPath, ScanfInput()));//основной метод
                        //доп действия
                        WindowsExplorer.Enter(currentPath);
                        PrintAdditionalFunctionality();
                        break;
                }
            }
        }

        static int _x_statrt = 75;
        static string[] _functional =
            {
                "Esc - назад",
                "Enter - открыть",
                "F1 - создать папку",
                "F2 - создать файл",
                "Del - удалить"
            };

        //считать имя создаваемой папки/файла
        static string ScanfInput()
        {
            int oldLeft = Console.CursorLeft,
                oldPos = Console.CursorTop;

            Console.SetCursorPosition(_x_statrt + 2, _functional.Length + 2);

            string toRet = Console.ReadLine();

            Console.SetCursorPosition(oldLeft, oldPos);

            return toRet;
        }

        //Печатает функционал в углу экрана
        static void PrintAdditionalFunctionality()
        {
            int oldLeft = Console.CursorLeft,
                oldPos = Console.CursorTop;

            for (int i = 0; i < _functional.Length; i++)
            {
                Console.SetCursorPosition(_x_statrt, i);
                Console.Write($"||{_functional[i]}");
            }

            Console.SetCursorPosition(oldLeft, oldPos);
        }
    }
}
