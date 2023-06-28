namespace Lesson_19
{
    internal class Program
    {
        static void Main()
        {
            const string FireElementalCommand = "1";
            const string FireShowerCommand = "2";
            const string FireballCommand = "3";
            const string FadeInShadowsCommand = "4";
            const string SurgeOfStrengthCommand = "5";
            const string BlackHoleCommand = "6";
            const string YesCommand = "y";
            const string NoCommand = "n";
            const string ExitBattleCommand = "exit";

            string name = "Арканий";
            int healthPlayer;
            int mysteriousEnergy = 0;
            int maxMysteriosEnergy = 100;
            int minPlayerHealth = 400;
            int maxPlayerHealth = 600;
            int numberMoves = 2;
            int numberFadeInShadowsMoves = 0;
            int minFadeInShadowsMoves = 0;
            bool isUsedElementalOfFire = false;
            bool isUsedFadeInShadows = false;
            bool isPlayerAttack = false;
            int damageElementalOfFire = 50;
            int damageFireShower = 25;
            int damageFireball = 75;
            int damageBlackHole = 200;
            int restorationHealth = 200;
            int accumulateMysteriousEnergy = 50;
            string skillFireElemental = $"Призыв элементаля огня (отнимает {damageElementalOfFire} ХР боссу).";
            string skillFireShower = $"Заклинание огненного ливня (отнимает {damageFireShower} ХР боссу, " +
                                     $"\n\tдля использования нужен элементаль огня).";
            string skillFireball = $"Заклинание выброса огненнго шара (отнимает {damageFireball} ХР боссу, " +
                                $"\n\tчародей накапливает {accumulateMysteriousEnergy} единиц мистической энергии).";
            string skillFadeInShadows = $"Заклинание ухода в тень (В тени чародей может использовать прилив сил " +
                                        $"\n\tи становится неуязвим к урону на {numberMoves} хода).";
            string skillSurgeOfStrength = $"Заклинание прилива сил (Восстанавливает {restorationHealth} единиц ХР" +
                                          $"\n\tи {accumulateMysteriousEnergy} единиц мистической энергии чародею).";
            string skillBlackHole = $"Заклинание создания черной дыры (отнимает {damageBlackHole} ХР боссу, " +
                                    $"\n\tдля создания нужно {maxMysteriosEnergy} единиц мистической энергии).";
            string battleTextSkillFireElemental = $"Чародей призывает элементаля огня, который наносит: {damageElementalOfFire} единиц урона.";
            string battleTextSkillFireShower = $"Элементаль вызывает огненный ливень, который наносит: {damageFireShower} единиц урона.";
            string battleTextSkillFireball = $"Чародей делает выброс огненного шара, который наносит: {damageFireball} единиц урона.";
            string battleTextSkillFadeInShadows = $"Чародей растворяется в тенях";
            string battleTextSkillSurgeOfStrength = $"Чародей использует заклиниание \"прилив сил\", которое восстанавливает: {restorationHealth} единиц здоровья.";
            string battleTextSkillBlackHole = $"Чародей создаёт \"черную дыру\", которая наносит: {damageBlackHole} единиц урона.";
            string enemyName = "Архимонд повелитель Зла";
            int healthEnemy;
            int damageEnemy;
            int minDamageEnemy = 50;
            int maxDamageEnemy = 150;
            int minEnemyHealth = 700;
            int maxEnemyHealth = 1000;
            bool isEnemyAttack = false;
            int delayInMiliseconds = 10;
            Random random = new Random();
            healthPlayer = random.Next(minPlayerHealth, maxPlayerHealth + 1);
            healthEnemy = random.Next(minEnemyHealth, maxEnemyHealth + 1);
            bool isRunCombat = true;
            string userInput;
            bool isSkipStory = false;
            bool isToldStartStory = false;
            string[] startStoryText = new string[]
            {
            $"Начинается рассказ новой истории...",
            $"\nВ средеземье жил и не тужил чародей по имени: \"{name}\".",
            $"Как то раз глубокой весной, чародей гуляя по своему любимому саду - увидел таинственное зарево в небе.",
            $"\"Ой не к добру это...\" - произнёс про себя \"{name}\".",
            $"И был прав чародей! Не прошло и часу, как стал подниматься сильный воющий ветер.",
            $"Небо накрыли черные тучи, сверкающие раскатами молний.",
            $"Морозный воздух начал застилать поверхность цветущего сада.",
            $"Любимые цветы чародея покрылись льдом...",
            $"Что за таинсвенная злая сила это сотворила? - произнес \"{name}\".",
            $"Надо разобраться? Кто же это мог быть?! - продолжал чародей.",
            $"Стряхнув пыль и взяв в руку свой старый посох, чародей убедился, что в нём всё ещё есть энергия стихий.",
            $"\"{name}\" разыскал виновного злодея. Им оказалось древнее зло по имени - \"{enemyName}\".",
            $"Ты поплатишься за свои злодеяния: \"{enemyName}\" - произнёс \"{name}\" и вступил в битву!"
            };
            string skipMenu = $"Перед началом великой битвы, хотите ли вы услышать историю с чего всё началось?" +
                              $"\n'{YesCommand}\' - начало истории..." +
                              $"\n\'{NoCommand}\' - пропустить историю...";
            string skillMenu = $"Заклинания чародея:" +
                               $"\n {FireElementalCommand} - {skillFireElemental}" +
                               $"\n {FireShowerCommand} - {skillFireShower}" +
                               $"\n {FireballCommand} - {skillFireball}" +
                               $"\n {FadeInShadowsCommand} - {skillFadeInShadows}" +
                               $"\n {SurgeOfStrengthCommand} - {skillSurgeOfStrength}" +
                               $"\n {BlackHoleCommand} - {skillBlackHole}" +
                               $"\n {ExitBattleCommand} - для побега с поля боя...";
            string continueMessage = "\nНажмите любую клавишу чтобы продолжить...\n";
            string startBattleMessage = "\nДа начнётся великая битва!!!";
            string requestCommandMessage = "\nВведите команду: ";

            Console.WindowHeight = 50;
            Console.WindowWidth = 100;

            while (isSkipStory == false)
            {
                Console.Clear();
                Console.Write(skipMenu);
                Console.Write(requestCommandMessage);
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case YesCommand:
                        isToldStartStory = false;
                        break;

                    case NoCommand:
                        isToldStartStory = true;
                        break;

                    default:
                        Console.WriteLine($"\"{userInput}\" - такой команды нет, попробуйте снова.");
                        Console.WriteLine(continueMessage);
                        Console.ReadKey();
                        continue;
                }

                isSkipStory = true;
            }

            Console.Clear();

            if (isToldStartStory == false)
            {
                foreach (string message in startStoryText)
                {
                    for (int i = 0; i < message.Length; i++)
                    {
                        Console.Write(message[i]);
                        Task.Delay(delayInMiliseconds).Wait();
                    }

                    Console.Write("\n");
                }

                isToldStartStory = true;
                Console.WriteLine(startBattleMessage);
                Console.WriteLine(continueMessage);
                Console.ReadKey();
            }

            while (isRunCombat == true)
            {
                string statusBar = $"Здоровье чародея: [{healthPlayer}] единиц." +
                                   $"\nЗдоровье босса: [{healthEnemy}] единиц." +
                                   $"\nНеуязвимость чародея: [{numberFadeInShadowsMoves}] хода." +
                                   $"\nМистической энергии у чародея: [{mysteriousEnergy}] единиц.\n";
                Console.Clear();
                Console.Write(statusBar);
                Console.WriteLine();

                if (numberFadeInShadowsMoves > minFadeInShadowsMoves)
                {
                    numberFadeInShadowsMoves--;
                }

                while (isPlayerAttack == false)
                {
                    Console.WriteLine(skillMenu);
                    Console.Write(requestCommandMessage);
                    userInput = Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(">------ Ход игрока ------<");
                    Console.ForegroundColor = ConsoleColor.White;

                    switch (userInput)
                    {
                        case ExitBattleCommand:
                            Console.WriteLine($"Вы сбежали с битвы, позор вам!");
                            return;

                        case FireElementalCommand:
                            healthEnemy -= damageElementalOfFire;
                            Console.WriteLine(battleTextSkillFireElemental);
                            isUsedElementalOfFire = true;
                            isPlayerAttack = true;
                            break;

                        case FireShowerCommand:
                            if (isUsedElementalOfFire == true)
                            {
                                healthEnemy -= damageFireShower;
                                Console.WriteLine(battleTextSkillFireShower);
                                isPlayerAttack = true;
                            }
                            else
                            {
                                Console.WriteLine($"Сейчас вы не можете использовать \"{skillFireShower}\", " +
                                                  $"\nдля этого нужно применить заклинание: \"{skillFireElemental}\"");
                            }
                            break;

                        case FireballCommand:
                            healthEnemy -= damageFireball;
                            mysteriousEnergy += accumulateMysteriousEnergy;
                            Console.WriteLine(battleTextSkillFireball);
                            isPlayerAttack = true;
                            break;

                        case FadeInShadowsCommand:
                            isUsedFadeInShadows = true;
                            numberFadeInShadowsMoves = numberMoves;
                            Console.WriteLine(battleTextSkillFadeInShadows);
                            Console.WriteLine($"Чародей в безопасности, неуязвим к урону {numberFadeInShadowsMoves} ходов");
                            isPlayerAttack = true;
                            break;

                        case SurgeOfStrengthCommand:
                            if (isUsedFadeInShadows == true)
                            {
                                healthPlayer += restorationHealth;
                                mysteriousEnergy += accumulateMysteriousEnergy;
                                Console.WriteLine(battleTextSkillSurgeOfStrength);
                                isPlayerAttack = true;
                            }
                            else
                            {
                                Console.WriteLine($"Сейчас вы не можете использовать \"{skillSurgeOfStrength}\", " +
                                                  $"\nдля этого нужно применить заклинание: \"{skillFadeInShadows}\"");
                            }
                            break;

                        case BlackHoleCommand:
                            if (mysteriousEnergy >= maxMysteriosEnergy)
                            {
                                healthEnemy -= damageBlackHole;
                                mysteriousEnergy -= maxMysteriosEnergy;
                                Console.WriteLine(battleTextSkillBlackHole);
                                isPlayerAttack = true;
                            }
                            else
                            {
                                Console.WriteLine($"Сейчас вы не можете использовать \"{skillBlackHole}\", " +
                                                  $"\nдля этого нужно накопить: \"{maxMysteriosEnergy}\" единиц мистической энергии.");
                            }
                            break;

                        default:
                            Console.WriteLine($"\"{userInput}\" - такой команды нет, попробуйте снова.");
                            Console.WriteLine(continueMessage);
                            Console.ReadKey();
                            Console.Clear();
                            Console.Write(statusBar);
                            Console.WriteLine();
                            continue;
                    }

                    Console.WriteLine(continueMessage);
                    Console.ReadKey();
                }

                while (isEnemyAttack == false)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(">------ Ход босса ------<");
                    Console.ForegroundColor = ConsoleColor.White;

                    if (numberFadeInShadowsMoves <= minFadeInShadowsMoves)
                    {
                        damageEnemy = random.Next(minDamageEnemy, maxDamageEnemy);
                        healthPlayer -= damageEnemy;
                        Console.Write($"{enemyName} нанёс чародею {damageEnemy} единиц урона.\n");
                    }
                    else
                    {
                        Console.Write($"\n{enemyName} - не смог нанести урон.");
                    }

                    isEnemyAttack = true;
                }

                isPlayerAttack = false;
                isEnemyAttack = false;

                if (healthPlayer <= 0 || healthEnemy <= 0)
                {
                    isRunCombat = false;
                }

                Console.WriteLine(continueMessage);
                Console.ReadKey();
            }

            if (healthPlayer <= 0 && healthEnemy <= 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"Битва окончена ничьей! Чародей {name} и {enemyName} не добились успеха в битве!");
            }
            else if (healthPlayer <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Битва окончена! Чародей {name} пал смертью храбрых. {enemyName} - победил в этой битве!");
            }
            else if (healthEnemy <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Битва окончена! Чародей {name} победил своего врага {enemyName}! Да здравствует победа!");
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(continueMessage);
            Console.ReadKey();
        }
    }
}