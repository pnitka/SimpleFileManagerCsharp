using System;

class Program
{
    static void Main(string[] args)
    {
        var initialState = StateHandler.LoadState();
        var fileManager = new FileManager(initialState);

        while (true)
        {
            Console.WriteLine("\nВыберите действие:");
            Console.WriteLine("1. Просмотр содержимого");
            Console.WriteLine("2. Перейти в директорию");
            Console.WriteLine("3. Копировать");
            Console.WriteLine("4. Удалить");
            Console.WriteLine("5. Информация о файле/каталоге");
            Console.WriteLine("6. Выход");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    fileManager.DisplayDirectoryContents();
                    break;
                case "2":
                    Console.Write("Введите имя директории: ");
                    fileManager.ChangeDirectory(Console.ReadLine());
                    break;
                case "3":
                    Console.Write("Введите источник: ");
                    var source = Console.ReadLine();
                    Console.Write("Введите назначение: ");
                    var destination = Console.ReadLine();
                    fileManager.Copy(source, destination);
                    break;
                case "4":
                    Console.Write("Введите путь для удаления: ");
                    fileManager.Delete(Console.ReadLine());
                    break;
                case "5":
                    Console.Write("Введите путь: ");
                    fileManager.GetFileInfo(Console.ReadLine());
                    break;
                case "6":
                    StateHandler.SaveState(fileManager.GetCurrentDirectory());
                    return;
                default:
                    Console.WriteLine("Неверный выбор.");
                    break;
            }
        }
    }
}