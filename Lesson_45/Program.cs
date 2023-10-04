﻿namespace Lesson_45
{
    class Program
    {
        static void Main()
        {
            const string BeginFightMenu = "1";
            const string ExitMenu = "2";

            Console.WindowWidth = 90;
            Console.BufferHeight = 500;

            BattleField battleField = new BattleField();
            bool isRun = true;

            while (isRun == true)
            {
                Console.Clear();
                Console.WriteLine(
                    $"Меню:\n" +
                    $"{BeginFightMenu} - Начать подготовку битвы\n" +
                    $"{ExitMenu} - Покинуть поле битвы.\n" +
                    $"Введите команду для продолжения: ");

                switch (Console.ReadLine())
                {
                    case BeginFightMenu:
                        battleField.Work();
                        break;

                    case ExitMenu:
                        isRun = false;
                        break;

                    default:
                        Console.WriteLine("Такой команды нет!!!");
                        break;
                }
            }

            Console.WriteLine("Работа программы завершена!");
            Console.ReadKey();
        }
    }

    class BattleField
    {
        private List<Fighter> _fighters;

        public void Work()
        {
            const string ChooseFighterCommand = "0";
            const string ChooseWarriorCommand = "1";
            const string ChooseAssasignCommand = "2";
            const string ChooseHunterCommand = "3";
            const string ChooseWizzardCommand = "4";

            _fighters = new List<Fighter>();

            while (_fighters.Count < 2)
            {
                Console.Clear();

                Console.WriteLine(
                    $"Доступные классы героев:\n" +
                    $"{ChooseFighterCommand} - Боец\n" +
                    $"{ChooseWarriorCommand} - Воин\n" +
                    $"{ChooseAssasignCommand} - Разбойник\n" +
                    $"{ChooseHunterCommand} - Охотник\n" +
                    $"{ChooseWizzardCommand} - Волшебник\n" +
                    $"Введите номер для выбора {_fighters.Count + 1} класса героя:");

                switch (Console.ReadLine())
                {
                    case ChooseFighterCommand:
                        ChooseFighter(new Fighter());
                        break;

                    case ChooseWarriorCommand:
                        ChooseFighter(new Warrior());
                        break;

                    case ChooseAssasignCommand:
                        ChooseFighter(new Assasign());
                        break;

                    case ChooseHunterCommand:
                        ChooseFighter(new Hunter());
                        break;

                    case ChooseWizzardCommand:
                        ChooseFighter(new Wizzard());
                        break;

                    default:
                        Console.WriteLine("Нет такой команды!");
                        break;
                }
            }
            
            AnnounceFightersReadyForFight();
            Fight();
            AnnounceWinner();            
        }

        private void AnnounceFightersReadyForFight()
        {
            Console.WriteLine("Готовые к бою отважные герои:");

            for (int i = 0; i < _fighters.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_fighters[i].ClassName} ({_fighters[i].Name}): DMG: {_fighters[i].Damage}, HP: {_fighters[i].Health}");
            }
        }

        private void Fight()
        {
            Console.WriteLine("Начать битву?\nДля продолжения нажмите любую клавишу...\n\n");
            Console.ReadKey();

            while (_fighters[0].IsAlive == true && _fighters[1].IsAlive == true)
            {
                _fighters[0].Attack(_fighters[1]);
                _fighters[1].Attack(_fighters[0]);

                Console.WriteLine(new string('-', 70));
                Task.Delay(1000).Wait();
            }
        }

        private void AnnounceWinner()
        {
            if (_fighters[0].IsAlive == false && _fighters[1].IsAlive == false)
            {
                Console.WriteLine("\nНичья! Оба героя пали на поле боя!");
            }
            else if (_fighters[0].IsAlive == true && _fighters[1].IsAlive == false)
            {
                Console.WriteLine($"\nПобедитель - {_fighters[0]} ({_fighters[0].Name})!");
            }
            else if (_fighters[0].IsAlive == false && _fighters[1].IsAlive == true)
            {
                Console.WriteLine($"\nПобедитель - {_fighters[1]} ({_fighters[1].Name})!");
            }

            Console.ReadKey();
        }

        private void ChooseFighter(Fighter fighter) => _fighters.Add(fighter); 

        private void ChooseFighter(Warrior warrior) => _fighters.Add(warrior);

        private void ChooseFighter(Assasign assasign) => _fighters.Add(assasign);

        private void ChooseFighter(Hunter hunter) => _fighters.Add(hunter);

        private void ChooseFighter(Wizzard wizzard) => _fighters.Add(wizzard);
    }

    class Fighter
    {
        private int _health;

        public Fighter()
        {
            ClassName = "Боец";
            Health = Randomaizer.GenerateRandomNumber(150, 301);
            Damage = Randomaizer.GenerateRandomNumber(10, 21);
            Name = Randomaizer.GenerateRandomName();
        }

        public string ClassName { get; protected set; }
        public int Health
        {
            get => _health;
            protected set => SetHealth(value);
        }
        public int Damage { get; protected set; }
        public bool IsAlive => Health > 0;
        public string Name { get; protected set; }

        public virtual void Attack(Fighter target)
        {
            if (IsAlive == false)
            {
                return;
            }
            else
            {
                Console.WriteLine($"{ClassName} ({Name}) произвёл удар в сторону {target.ClassName} ({target.Name})");

                if (target.IsAlive == true)
                {
                    target.TryTakeDamage(Damage);
                }
            }
        }

        public virtual void Healing(int healingPoint)
        {
            Health += healingPoint;
            Console.WriteLine($"{ClassName} ({Name}) подлечил здоровье на ({healingPoint}) ед. Здоровье : ({Health})");
        }

        public virtual bool TryTakeDamage(int damage)
        {
            if (Health > 0)
            {
                Health -= damage;
                Console.WriteLine($"{ClassName} ({Name}) получил удар ({damage}) ед., осталось здоровья ({Health})");
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return $"{ClassName}";
        }

        private void SetHealth(int value)
        {
            if (value > 0)
            {
                _health = value;
            }
            else
            {
                _health = 0;
            }
        }
    }

    class Warrior : Fighter
    {
        private int _missDamagePercent = 30;
        private int _maxPercent = 100;

        public Warrior() => ClassName = "Воин";

        public override bool TryTakeDamage(int damage)
        {
            int missChance = Randomaizer.GenerateRandomNumber(0, _maxPercent + 1);

            if (missChance < _missDamagePercent)
            {
                Console.WriteLine($"{ClassName} ({Name}) увернулся от удара, осталось здоровья ({Health})");
                return false;
            }

            return base.TryTakeDamage(damage);
        }
    }

    class Assasign : Fighter
    {
        public Assasign() => ClassName = "Разбойник";

        public override void Attack(Fighter target)
        {
            if (IsAlive == false || target.IsAlive == false)
            {
                return;
            }

            Console.WriteLine($"{ClassName} ({Name}) произвёл удар в сторону {target.ClassName} ({target.Name})");

            if (target.TryTakeDamage(Damage))
            {
                int damageDivider = 10;
                int healingPoint = Damage / damageDivider;
                Healing(healingPoint);
            }
        }
    }

    class Hunter : Fighter
    {
        private int _critPercent = 30;
        private int _maxPercent = 100;
        private int _damageModifyPercent = 150;

        public Hunter() => ClassName = "Охотник";

        public override void Attack(Fighter target)
        {
            if (IsAlive == false)
            {
                return;
            }
            else
            {
                int currentDamage = CalculateCriteDamage();
                Console.WriteLine($"{ClassName} ({Name}) произвёл удар в сторону {target.ClassName} ({target.Name})");

                if (target.IsAlive == true)
                {
                    target.TryTakeDamage(currentDamage);
                }
            }
        }

        private int CalculateCriteDamage()
        {
            int critChance = Randomaizer.GenerateRandomNumber(0, _maxPercent + 1);

            if (critChance < _critPercent)
            {
                return Damage * _damageModifyPercent / _maxPercent;
            }

            return Damage;
        }
    }

    class Wizzard : Fighter
    {
        private int _mana;
        private int _minMana = 50;
        private int _maxMana = 100;
        private int _castingManaCost = 20;
        private int _regenerationManaCount = 10;

        public Wizzard()
        {
            ClassName = "Волшебник";
            _mana = Randomaizer.GenerateRandomNumber(_minMana, _maxMana + 1);
        }

        public int Mana { get => _mana; }

        public override void Attack(Fighter target)
        {
            if (_mana >= _castingManaCost)
            {
                _mana -= _castingManaCost;
                base.Attack(target);
            }
            else
            {
                _mana += _regenerationManaCount;
                Console.WriteLine($"{ClassName} ({Name}) не хватает маны для удара {target.ClassName} ({target.Name})");
            }
        }

        public override bool TryTakeDamage(int damage)
        {
            _mana += _regenerationManaCount;
            return base.TryTakeDamage(damage);
        }
    }

    static class Randomaizer
    {
        private static Random s_random = new Random();
        private static string[] s_names =
            {
                "Варвар",
                "Космонафт",
                "Миледи",
                "Вульфич",
                "Страйк",
                "Герандич",
                "Фрея",
                "Крыса",
                "Нинка",
                "Царь",
                "Забота",
                "Прожариватель",
                "Овощ",
                "Имба",
                "Нагибатель",
                "Топчик",
                "Холивар",
                "Бывалый",
                "Пирожок",
                "Котейка",
                "Оливер",
                "Викрам",
                "Архидея",
                "Метрономщик",
                "Зимник",
                "Волкодав",
                "Богатырь",
                "Вафлич",
                "Вурдолакыч",
                "Зяблик",
                "Кудахта",
                "Чувиха",
                "Мордорка",
                "Куряха",
                "Смоляха",
                "Крендель",
                "Остряк",
                "Крушила",
                "Очкович",
                "Щавель",
                "Днище",
                "Нубичка",
                "Жираф",
                "Подлиза",
                "Лимурчик",
                "Попрыгун",
                "Тряпкович"
            };

        public static string GenerateRandomName()
        {
            return s_names[s_random.Next(0, s_names.Length - 1)];
        }

        public static int GenerateRandomNumber(int minValue, int maxValue)
        {
            return s_random.Next(minValue, maxValue);
        }
    }
}

//Гладиаторские бои
//Создать 5 бойцов,
//пользователь выбирает 2 бойцов
//и они сражаются друг с другом до смерти.
//У каждого бойца могут быть свои статы.
//Каждый игрок должен иметь особую способность для атаки,
//которая свойственна только его классу!
//Если можно выбрать одинаковых бойцов,
//то это не должна быть одна и та же ссылка на одного бойца,
//чтобы он не атаковал сам себя.
//Пример, что может быть уникальное у бойцов.
//Кто-то каждый 3 удар наносит удвоенный урон,
//другой имеет 30% увернуться от полученного урона,
//кто-то при получении урона немного себя лечит.
//Будут новые поля у наследников.
//У кого-то может быть мана и это только его особенность.


// Исправлено:
// 1) Константы для меню начала боя + свитч
// 2) Убрана лишняя строка в майн
// 3) Ширина консоли 90 символов
// 4) СтартФайт->Файт
// 5) Отделил методы строками
// 6) Одинаковое оформление If else
// 7) Заменил оскорбительные имена