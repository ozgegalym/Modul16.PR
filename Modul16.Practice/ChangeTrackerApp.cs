using System;

class ChangeTrackerApp
{
    private ChangeTracker changeTracker;

    public ChangeTrackerApp()
    {
        changeTracker = new ChangeTracker();
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("1. Настройка отслеживания");
            Console.WriteLine("2. Запуск отслеживания");
            Console.WriteLine("3. Остановка отслеживания");
            Console.WriteLine("4. Выход");

            Console.Write("Выберите действие (1-4): ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ConfigureTracking();
                    break;

                case "2":
                    StartTracking();
                    break;

                case "3":
                    StopTracking();
                    break;

                case "4":
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Неверный выбор. Пожалуйста, выберите снова.");
                    break;
            }
        }
    }

    private void ConfigureTracking()
    {
        Console.Write("Введите путь к отслеживаемой директории: ");
        string directoryPath = Console.ReadLine();
        Console.Write("Введите путь к лог-файлу: ");
        string logFilePath = Console.ReadLine();

        changeTracker.Configure(directoryPath, logFilePath);
        Console.WriteLine("Конфигурация завершена.");
    }

    private void StartTracking()
    {
        try
        {
            changeTracker.Start();
            Console.WriteLine("Отслеживание запущено.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    private void StopTracking()
    {
        changeTracker.Stop();
        Console.WriteLine("Отслеживание остановлено.");
    }
}
