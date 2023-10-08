using System.Security.Cryptography.X509Certificates;

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

        public void Fight()
        {

        }
    }

    class Squad
    {

    }

    abstract class CombatUnit : ICombatEntity, IDamageable, IDamageProvider
    {
        protected CombatUnit()
        {
            ClassName = "Default";
            Damage = 10;
            Health = 100;
            Armor = 5;
            EntityName = "DefaultEntity";
        }

        public string ClassName { get; protected set; }
        public int Damage { get; protected set; }
        public int Health { get; protected set; }
        public int Armor { get; protected set; }
        public bool IsAlive { get => Health > 0; }
        public abstract string EntityName { get; set; }

        public virtual bool TryTakeDamage(int damage)
        {
            if (IsAlive == true)
            {
                Health -= damage;
                return true;
            }

            return false;
        }

        public virtual void AttackTo(Fighter target)
        {
            if (IsAlive == true && target.IsAlive == true)
            {
                target.TryTakeDamage(Damage);
            }
        }
    }

    abstract class Fighter : CombatUnit, IHeal
    {
        public Fighter()
        {
            ClassName = "Боец (По умолчанию)";
            EntityName = "Боевая единица (По умолчанию)";
            Damage = 10;
            Health = 100;
            Armor = 5;
        }

        public virtual void Heal(int healthPoint)
        {
            Health += healthPoint;
        }
    }

    class Stormtrooper : Fighter
    {
        public Stormtrooper()
        {
            Health = 20;
        }

        public override string EntityName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    class Sniper : Fighter
    {
        public override string EntityName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    class Paratrooper : Fighter
    {
        public override string EntityName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    class Scout : Fighter
    {
        public override string EntityName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    class Heavy : Fighter
    {
        public override string EntityName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    class GrenadeLauncher : Fighter
    {
        public override string EntityName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    class Medic : Fighter
    {
        public override string EntityName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    abstract class FighterVihicles : ICombatEntity, IDamageable, IDamageProvider
    {
        public abstract string EntityName { get; set; }

        public abstract void AttackTo(Fighter target);

        public abstract bool TryTakeDamage(int damage);
    }

    class Tank : FighterVihicles
    {
        public override string EntityName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AttackTo(Fighter target)
        {
            throw new NotImplementedException();
        }

        public override bool TryTakeDamage(int damage)
        {
            throw new NotImplementedException();
        }
    }

    #region Intarfaces

    interface ICombatEntity
    {
        string EntityName { get; set; }
    }

    interface IDamageable
    {
        public abstract bool TryTakeDamage(int damage);
    }

    interface IDamageProvider
    {
        public abstract void AttackTo(Fighter target);
    }

    interface IHeal
    {
        public abstract void Heal(int healthPoint);
    }

    #endregion
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

//Combat Unit - реализовать