// ОСНОВНОЙ КЛАСС, КОТОРЫЙ РАБОТАЕТ С ФАЙЛОВОЙ СИСТЕМОЙ
using System;
using System.IO;

public class FileManager
{
    private string currentDirectory;

    public FileManager(string initialDirectory = null)
    {
        currentDirectory = initialDirectory ?? Directory.GetCurrentDirectory();
    }

    public string GetCurrentDirectory() => currentDirectory;

    // ОТОБРАЖЕНИЕ СОДЕРЖИМОГО ТЕКУЩЕЙ ДИРЕКТОРИИ
    public void DisplayDirectoryContents()
    {
        try
        {
            Console.WriteLine($"Текущая директория: {currentDirectory}");
            Console.WriteLine("Содержимое:");

            foreach (var item in Directory.GetFileSystemEntries(currentDirectory))
            {
                var fileInfo = new FileInfo(item);
                Console.WriteLine($"{(fileInfo.Attributes.HasFlag(FileAttributes.Directory) ? "[DIR]" : "[FILE]")} " +
                                  $"{fileInfo.Name} | Размер: {fileInfo.Length} байт | " +
                                  $"Создан: {fileInfo.CreationTime} | Изменен: {fileInfo.LastWriteTime}");
            }
        }
        catch (Exception ex)
        {
            ErrorHandler.HandleError(ex);
        }
    }

    // ПЕРЕХОД В ДРУГУЮ ДИРЕКТОРИЮ
    public void ChangeDirectory(string path)
    {
        try
        {
            var newPath = Path.Combine(currentDirectory, path);
            if (Directory.Exists(newPath))
            {
                currentDirectory = newPath;
                Console.WriteLine($"Перешли в директорию: {currentDirectory}");
            }
            else
            {
                Console.WriteLine("Директория не существует");
            }
        }
        catch (Exception ex)
        {
            ErrorHandler.HandleError(ex);
        }
    }

    // КОПИРАВАНИЕ ФАЙЛОВ И КАТАЛОГОВ
    public void Copy(string source, string destination)
    {
        try
        {
            var sourcePath = Path.Combine(currentDirectory, source);
            var destPath = Path.Combine(currentDirectory, destination);

            if (File.Exists(sourcePath))
            {
                CopyFile(sourcePath, destPath);
            }
            else if (Directory.Exists(sourcePath))
            {
                CopyDirectory(sourcePath, destPath);
            }
            else
            {
                Console.WriteLine("Источник не существует");
            }
        }
        catch (Exception ex)
        {
            ErrorHandler.HandleError(ex);
        }
    }

    private void CopyFile(string sourcePath, string destPath)
    {
        if (File.Exists(destPath))
        {
            Console.Write("Файл уже существует. Изменить?: ");
            if (Console.ReadLine().ToLower() != "y") return;
        }
        File.Copy(sourcePath, destPath, overwrite: true);
        Console.WriteLine($"Файл скопирован: {destPath}");
    }

    private void CopyDirectory(string sourceDir, string destDir)
    {
        if (Directory.Exists(destDir))
        {
            Console.Write("Каталог уже существует. Изменить?: ");
            if (Console.ReadLine().ToLower() != "y") return;
        }

        Directory.CreateDirectory(destDir);

        foreach (var file in Directory.GetFiles(sourceDir))
        {
            File.Copy(file, Path.Combine(destDir, Path.GetFileName(file)), overwrite: true);
        }

        foreach (var dir in Directory.GetDirectories(sourceDir))
        {
            CopyDirectory(dir, Path.Combine(destDir, Path.GetFileName(dir)));
        }

        Console.WriteLine($"Каталог скопирован: {destDir}");
    }

    // УДАЛЕНИЕ ФАЙЛОВ И КАТАЛОГОВ
    public void Delete(string path)
    {
        try
        {
            var fullPath = Path.Combine(currentDirectory, path);

            if (File.Exists(fullPath))
            {
                DeleteFile(fullPath);
            }
            else if (Directory.Exists(fullPath))
            {
                DeleteDirectory(fullPath);
            }
            else
            {
                Console.WriteLine("Указанный путь не существует(не создан)");
            }
        }
        catch (Exception ex)
        {
            ErrorHandler.HandleError(ex);
        }
    }

    private void DeleteFile(string filePath)
    {
        Console.Write("Вы уверены, что хотите удалить файл?(удалится без возвратно): ");
        if (Console.ReadLine().ToLower() == "y")
        {
            File.Delete(filePath);
            Console.WriteLine($"Файл удален: {filePath}");
        }
    }

    private void DeleteDirectory(string dirPath)
    {
        Console.Write("Вы уверены, что хотите удалить каталог?(удалится без возвратно): ");
        if (Console.ReadLine().ToLower() == "y")
        {
            Directory.Delete(dirPath, recursive: true);
            Console.WriteLine($"Каталог удален: {dirPath}");
        }
    }

    // ПОЛУЧЕНИЕ ИНФОРМАЦИИ О ФАЙЛАХ И КАТАЛОГАХ
    public void GetFileInfo(string path)
    {
        try
        {
            var fullPath = Path.Combine(currentDirectory, path);

            if (File.Exists(fullPath))
            {
                var fileInfo = new FileInfo(fullPath);
                Console.WriteLine($"Имя: {fileInfo.Name}");
                Console.WriteLine($"Размер: {fileInfo.Length} байт");
                Console.WriteLine($"Создан: {fileInfo.CreationTime}");
                Console.WriteLine($"Изменен: {fileInfo.LastWriteTime}");
                Console.WriteLine($"Атрибуты: {fileInfo.Attributes}");
            }
            else if (Directory.Exists(fullPath))
            {
                Console.WriteLine($"Итоговый размер каталога: {GetDirectorySize(fullPath)} ");
            }
            else
            {
                Console.WriteLine("Путь не существует");
            }
        }
        catch (Exception ex)
        {
            ErrorHandler.HandleError(ex);
        }
    }

    private long GetDirectorySize(string path)
    {
        long size = 0;
        foreach (var file in Directory.GetFiles(path, "*", SearchOption.AllDirectories))
        {
            size += new FileInfo(file).Length;
        }
        return size;
    }
}