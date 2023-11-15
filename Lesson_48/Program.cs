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
        public Fish() { }

        public string Name { get; }
        public int Age { get; private set; }
        public bool IsAlive { get => Age > 0; }

        public void ShowInfo() { }
        public void Update() { }
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