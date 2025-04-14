// КЛАСС, КОТОРЫЙ СОХРАНЯЕТ И ВОССТАНАВЛИВАЕТ СОСТОЯНИЕ
using System;
using System.IO;

public class StateHandler
{
    private const string ConfigFile = "state.json";
// МЕТОД ДЛЯ СОХРАНЕНИЯ СОСТОЯНИЯ ТЕКУЩЕЙ ДИРЕКТОРИИ
    public static void SaveState(string currentDirectory)
    {
        try
        {
            File.WriteAllText(ConfigFile, currentDirectory);
        }
        catch (Exception ex)
        {
            ErrorHandler.HandleError(ex);
        }
    }

    public static string LoadState()
    {
        try
        {
            if (File.Exists(ConfigFile))
            {
                return File.ReadAllText(ConfigFile);
            }
        }
        catch (Exception ex)
        {
            ErrorHandler.HandleError(ex);
        }
        return null;
    }
}