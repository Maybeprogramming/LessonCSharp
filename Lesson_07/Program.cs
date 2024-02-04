namespace Lesson_07
{
    internal class Program
    {
        private static void Main()
        {
            int patientsInQueue;
            int waitingTime;
            int patientReceptionTime = 10;
            int minutesPerHour = 60;
            int waitingHours;
            int waitingMinutes;

            Console.WriteLine("Добро пожаловать в поликлинику!!!");
            Console.Write("Сколько людей в очереди на приём перед вами? ");
            patientsInQueue = Convert.ToInt32(Console.ReadLine());

            waitingTime = patientsInQueue * patientReceptionTime;
            waitingHours = waitingTime / minutesPerHour;
            waitingMinutes = waitingTime % minutesPerHour;

            Console.WriteLine($"Вы должны отстоять в очереди {waitingHours} часа {waitingMinutes} минут");
            Console.ReadLine();
        }
    }
}