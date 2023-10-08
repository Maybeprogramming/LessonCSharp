namespace Lesson_47
{
    class Program
    {
        static void Main()
        {
        }
    }

    class BattleField
    {
        Squad squad1 = new Squad();
        Squad squad2 = new Squad();

        public void BeginWar()
        {

        }
    }

    class Squad
    {

    }

    abstract class Fighter
    {

    }

    class Stormtrooper : Fighter
    {

    }

    class Sniper: Fighter
    {

    }

    class Paratrooper: Fighter
    {

    }

    class Scout: Fighter
    {

    }

    class Heavy : Fighter
    {

    }

    class GrenadeLauncher: Fighter
    {

    }

    class Medic: Fighter
    {

    }
}

//Война
//Есть 2 взвода.
//1 взвод страны один,
//2 взвод страны два.
//Каждый взвод внутри имеет солдат.
//Нужно написать программу, которая будет моделировать бой этих взводов.
//Каждый боец - это уникальная единица,
//он может иметь уникальные способности или же уникальные характеристики,
//такие как повышенная сила.
//Побеждает та страна, во взводе которой остались выжившие бойцы.
//Не важно, какой будет бой, рукопашный, стрелковый.

//Штурмовик - Stormtrooper
//Снайпер - Sniper
//Десантник - Paratrooper
//Разведчик - Scout
//Пулеметчик - Heavy
//Гранатометчик - Grenade launcher
//Медик - Medic