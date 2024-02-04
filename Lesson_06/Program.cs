namespace Lesson_06
{
    internal class Program
    {
        private static void Main()
        {
            int userCoins;
            int userCristals;
            int cristalPrice = 10;

            Console.WriteLine("Добро пожаловать в лавку драгоценных кристаллов!!!");
            Console.Write("Сколько у вас монет? ");
            userCoins = Convert.ToInt32(Console.ReadLine());

            Console.Write($"Предлагаем вам купить драгоценные кристалы по цене: {cristalPrice} монет за 1 кристал");
            Console.Write("\nСколько вы желаете купить драгоценных кристаллов? ");
            userCristals = Convert.ToInt32(Console.ReadLine());

            userCoins -= cristalPrice * userCristals;
            Console.WriteLine($"У вас {userCoins} монет и {userCristals} драгоценных кристалов");

            Console.ReadLine();
        }
    }
}