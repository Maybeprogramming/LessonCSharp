namespace Lesson_48
{
    class Program
    {
        static void Main() { }
    }

    class Aquarium
    {
        private List<Fish> _fishes;

        public Aquarium() 
        {
            _fishes = FillFishes();
        }

        public int MaxFishesCount { get; }

        public void Work() { }

        public void UpdateFishesLifeCicle()
        {
            foreach (Fish fish in _fishes)
            {
                fish.Update();
            }
        }

        public void GetInfoAboutFishes() { }
        private void AddFish() { }
        private void GetFish() { }
        private List<Fish> FillFishes()
        {
            return new List<Fish> 
            { 
                new Fish() 
            };
        }
    }

    class Fish
    {
        private int _age;

        public Fish() { }

        public string Name { get; }
        public int Age 
        { 
            get => _age; 
            private set => SetAge(value); 
        }
        public int DeadAge { get; private set; }
        public bool IsAlive { get => Age == DeadAge; }

        public void ShowInfo() 
        {
            Console.Write($"Рыба: [{Name}] возраст: [{Age}]\n");
        }

        public void Update() 
        { 
            if(IsAlive == true)
            {
                ++Age;
            }
        }

        private int SetAge(int value)
        {
            if (value >= DeadAge)
            {
                _age = DeadAge;
            }
            else
            {
                _age = value;
            }

            return _age;
        }
    }
}

//Аквариум
//Есть аквариум, в котором плавают рыбы.
//В этом аквариуме может быть максимум определенное кол-во рыб.
//Рыб можно добавить в аквариум или рыб можно достать из аквариума.
//(программу делать в цикле для того, чтобы рыбы могли “жить”)
//Все рыбы отображаются списком,
//у рыб также есть возраст.
//За 1 итерацию рыбы стареют на определенное кол-во жизней и могут умереть.
//Рыб также вывести в консоль, чтобы можно было мониторить показатели.