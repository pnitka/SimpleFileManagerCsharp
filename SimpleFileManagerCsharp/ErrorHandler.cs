// КЛАСС, КОТОРЫЙ ОБРАБАТЫВАЕТ ОШИБКИ 
public static class ErrorHandler
{
    public static void HandleError(Exception ex)
    {
        Console.WriteLine($"Ошибка(и): {ex.Message}");
    }
}