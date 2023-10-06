namespace Lesson_45
{
    class Program
    {
        static void Main()
        {
            const string BeginFightMenu = "1";
            const string ExitMenu = "2";

            Console.WindowWidth = 90;
            Console.BufferHeight = 500;

            BattleField battleField = new();
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
                        Console.ReadKey();
                        break;
                }
            }

            Console.WriteLine("Работа программы завершена!");
            Console.ReadKey();
        }
    }

    class BattleField
    {
        private List<Fighter>? _fightersCatalog;
        private Fighter? _fighter1;
        private Fighter? _fighter2;

        public BattleField()
        {
            _fightersCatalog = new()
            {
                new Fighter(),
                new Warrior(),
                new Assasign(),
                new Hunter(),
                new Wizzard()
            };
        }

        public void Work()
        {
            ClearFighters();

            while (IsFigtersNotChosen() == true)
            {
                Console.Clear();

                ShowListFighters();

                ChooseFighter(UserInput.ReadInt("Введите номер бойца: "));
            }

            AnnounceFightersReadyForFight();
            Fight();
            AnnounceWinner();
        }

        private bool IsFigtersNotChosen()
        {
            return _fighter1 == null || _fighter2 == null;
        }

        private void ShowListFighters()
        {
            Console.WriteLine($"Список доступных бойцов для выбора:");

            for (int i = 0; i < _fightersCatalog.Count; i++)
            {
                Console.WriteLine($"{i} - {_fightersCatalog[i].GetInfo()}");
            }
        }

        private void ClearFighters()
        {
            _fighter1 = null;
            _fighter2 = null;
        }

        private void AnnounceFightersReadyForFight()
        {
            Console.WriteLine("\nГотовые к бою отважные герои:");
            Console.WriteLine($"1. {_fighter1.ClassName} ({_fighter1.Name}): DMG: {_fighter1.Damage}, HP: {_fighter1.Health}");
            Console.WriteLine($"2. {_fighter2.ClassName} ({_fighter2.Name}): DMG: {_fighter2.Damage}, HP: {_fighter2.Health}");
        }

        private void Fight()
        {
            Console.WriteLine("\nНачать битву?\nДля продолжения нажмите любую клавишу...\n");
            Console.ReadKey();

            while (_fighter1.IsAlive == true && _fighter2.IsAlive == true)
            {
                _fighter1.Attack(_fighter2);
                _fighter2.Attack(_fighter1);

                Console.WriteLine(new string('-', 70));
                Task.Delay(1000).Wait();
            }
        }

        private void AnnounceWinner()
        {
            if (_fighter1.IsAlive == false && _fighter2.IsAlive == false)
            {
                Console.WriteLine("\nНичья! Оба героя пали на поле боя!");
            }
            else if (_fighter1.IsAlive == true && _fighter2.IsAlive == false)
            {
                Console.WriteLine($"\nПобедитель - {_fighter1.ClassName} ({_fighter1.Name})!");
            }
            else if (_fighter1.IsAlive == false && _fighter2.IsAlive == true)
            {
                Console.WriteLine($"\nПобедитель - {_fighter2.ClassName} ({_fighter2.Name})!");
            }

            Console.ReadKey();
        }

        private void ChooseFighter(int number)
        {
            if (number >= _fightersCatalog.Count || number < 0)
            {
                Console.WriteLine("Нет такого бойца в каталоге!");
                Console.ReadKey();
                return;
            }

            if (_fighter1 == null)
            {
                _fighter1 = (Fighter?)_fightersCatalog[number].Clone();
                Console.WriteLine($"Вы выбрали: {_fighter1.GetInfo()}");
            }
            else if (_fighter2 == null)
            {
                _fighter2 = (Fighter?)_fightersCatalog[number].Clone();
                Console.WriteLine($"Вы выбрали: {_fighter2.GetInfo()}");
            }

            Console.ReadKey();
        }
    }

    class Fighter : IFighterClone
    {
        private int _health;

        public Fighter()
        {
            ClassName = "Боец";
            Health = Randomaizer.GenerateRandomNumber(150, 301);
            Damage = Randomaizer.GenerateRandomNumber(10, 21);
            Name = Randomaizer.GenerateRandomName();
        }

        public Fighter(string className, int health, int damage, string name)
        {
            ClassName = className;
            Health = health;
            Damage = damage;
            Name = name;
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

        public virtual string GetInfo()
        {
            return $"{ClassName}";
        }

        public bool TryTakeDamage(int damage)
        {
            if (Health > 0)
            {
                Health -= damage;
                Console.WriteLine($"{ClassName} ({Name}) получил удар ({damage}) ед., осталось здоровья ({Health})");
                return true;
            }

            return false;
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

        public virtual IFighterClone Clone()
        {
            return new Fighter();
        }
    }

    class Warrior : Fighter
    {
        private readonly int _missDamagePercent = 30;
        private readonly int _maxPercent = 100;

        public Warrior() => ClassName = "Воин";

        public bool TryTakeDamage(int damage)
        {
            int missChance = Randomaizer.GenerateRandomNumber(0, _maxPercent + 1);

            if (missChance < _missDamagePercent)
            {
                Console.WriteLine($"{ClassName} ({Name}) увернулся от удара, осталось здоровья ({Health})");
                return false;
            }

            return base.TryTakeDamage(damage);
        }

        public override IFighterClone Clone()
        {
            return new Warrior();
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

        public override IFighterClone Clone()
        {
            return new Assasign();
        }
    }

    class Hunter : Fighter
    {
        private readonly int _critPercent = 30;
        private readonly int _maxPercent = 100;
        private readonly int _damageModifyPercent = 150;

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

        public override IFighterClone Clone()
        {
            return new Hunter();
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
        private readonly int _minMana = 50;
        private readonly int _maxMana = 100;
        private readonly int _castingManaCost = 20;
        private readonly int _regenerationManaCount = 10;

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

        public override IFighterClone Clone()
        {
            return new Wizzard();
        }

        private bool TryTakeDamage(int damage)
        {
            _mana += _regenerationManaCount;
            return base.TryTakeDamage(damage);
        }


    }

    interface IFighterClone
    {
        IFighterClone Clone();
    }

    static class Randomaizer
    {
        private static readonly Random s_random = new();
        private static readonly string[] s_names =
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

    static class UserInput
    {
        public static int ReadInt(string message, int minValue = int.MinValue, int maxValue = int.MaxValue)
        {
            int result;

            Console.WriteLine(message);

            while (int.TryParse(Console.ReadLine(), out result) == false || result < minValue || result >= maxValue)
            {
                Console.Error.WriteLine("Ошибка!. Попробуйте снова!");
            }

            return result;
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