namespace Lesson_09
{
    internal class Program
    {
        private static void Main()
        {
            const string CommandGoToCafe = "1";
            const string CommandGoToCinema = "2";
            const string CommandGoToPerfumeShop = "3";
            const string CommandGoToElectronicsShop = "4";
            const string CommandGoToSeeOffice = "5";
            const string CommandGoToExit = "exit";
            const string WelcomeMessage = "Вы пришли в торговый центр \"Алибаба\".";

            string userInput;
            bool isTired = false;

            while (isTired == false)
            {
                Console.Clear();

                Console.WriteLine(WelcomeMessage);
                Console.WriteLine("Введите одну из команд для перехода в следующие зоны ТЦ:");
                Console.WriteLine($"{CommandGoToCafe} - чтобы пройти в кафе-ресторан и перекусить");
                Console.WriteLine($"{CommandGoToCinema} - для перехода в кинотеатр \"40 Разбойников\"");
                Console.WriteLine($"{CommandGoToPerfumeShop} - в магазин парфюмерии и косметики \"РИВ ГОШ\"");
                Console.WriteLine($"{CommandGoToElectronicsShop} - зайти в магазин электроники\"DNS\"");
                Console.WriteLine($"{CommandGoToSeeOffice} - поглазеть со стороны на офис IJunior\"");
                Console.WriteLine($"{CommandGoToExit} - для того чтобы выйти из ТЦ");
                Console.Write("\nВаша команда: ");

                userInput = Console.ReadLine().ToLower();

                switch (userInput)
                {
                    case CommandGoToExit:
                        isTired = true;
                        Console.WriteLine("Очевидно что вы устали и решили выйти из ТЦ");
                        Console.WriteLine("Возвращайтесь к нам снова!!!");
                        break;

                    case CommandGoToCafe:
                        Console.WriteLine("Вы пришли в кафе-ресторан, сели за свободный столик и заказали себе покушать");
                        break;

                    case CommandGoToCinema:
                        Console.WriteLine("Вы добрались до кинотеатра и купили билет на фантастический детектив по кодингу в кремниевой долине");
                        break;

                    case CommandGoToPerfumeShop:
                        Console.WriteLine("Вы зашли в парфюмерный магазин и купили себе дезодорант со свежестью альпиского ветра");
                        break;

                    case CommandGoToElectronicsShop:
                        Console.WriteLine("Вы вошли в магазин электроники и сразу же направились к полкам с компьютерным железом");
                        break;

                    case CommandGoToSeeOffice:
                        Console.WriteLine("Вы наблюдаете как менторы школы IJunior хорошо указывают на достижимость целей в задачах по программированию С#");
                        break;

                    default:
                        Console.WriteLine($"К большому сожалению магазин - \"{userInput}\" у нас отстутствует :(");
                        Console.ReadKey();
                        continue;
                }

                Console.WriteLine("\nЧтобы продолжить нажмите любую клавишу...");
                Console.ReadKey();
            }
        }
    }
}