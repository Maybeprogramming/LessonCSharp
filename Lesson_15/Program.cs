namespace Lesson_15
{
    internal class Program
    {
        static void Main()
        {
            string password = "qwerty";
            string userInput;
            int tryPasswordCount = 3;
            int leftTryInputPassword;
            string welcomeMessage = $"Секретная база данных зоны 51";
            string requestInputMessage = $"\nДля доступа к базе введите пароль: ";
            string continueMessege = "\nНажмите клавишу чтобы продолжить...";
            string topSecretFile = $"\n▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒" +
                                    $"\n▒▒▄▀▀▀▀▀▄▒▒▒▒▒▄▄▄▄▄▒▒▒" +
                                    $"\n▒▐░▄░░░▄░▌▒▒▄█▄█▄█▄█▄▒" +
                                    $"\n▒▐░▀▀░▀▀░▌▒▒▒▒▒░░░▒▒▒▒" +
                                    $"\n▒▒▀▄░═░▄▀▒▒▒▒▒▒░░░▒▒▒▒" +
                                    $"\n▒▒▐░▀▄▀░▌▒▒▒▒▒▒░░░▒▒▒▒";
            string topSecretMessege = "\nПришельцы существуют!!!";
            string incorrectInputPassworMessage = $"\nВы неверно ввели пароль!";

            for (int i = 0; i < tryPasswordCount; i++)
            {
                Console.Clear();
                Console.WriteLine(welcomeMessage);
                Console.Write(requestInputMessage);
                userInput = Console.ReadLine();

                if (userInput == password)
                {
                    Console.WriteLine(topSecretFile);
                    Console.WriteLine(topSecretMessege);
                    Console.WriteLine(continueMessege);
                    Console.ReadLine();
                }
                else
                {
                    leftTryInputPassword = tryPasswordCount - (i + 1);
                    Console.WriteLine(incorrectInputPassworMessage);
                    Console.WriteLine($"У вас осталось {leftTryInputPassword} попыток.");
                    Console.WriteLine(continueMessege);
                    Console.ReadLine();
                }
            }

            Console.WriteLine("\nРабота программы завершена.");
            Console.ReadKey();
        }
    }
}