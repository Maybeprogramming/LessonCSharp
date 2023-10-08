namespace Lesson_46
{
    class Program
    {
        static void Main()
        {
        }
    }

    class Market
    {
        private Seller? _seller;
        private Queue<Buyer>? _buyers;
    }

    class Seller
    {

    }

    class Buyer
    {
        private readonly int _money;
        private Cart? _cart;

        public bool TryBuyProduct()
        {
            return false;
        }
    }

    class Cart
    {
        private List<Product> _products;
    }

    class  Product
    {
        
    }

    static class Randomizer
    {

    }

    static class UserInput
    {

    }

    static class Display
    {

    }
}

//Супермаркет
//Написать программу администрирования супермаркетом.
//В супермаркете есть очередь клиентов.
//У каждого клиента в корзине есть товары,
//также у клиентов есть деньги.
//Клиент, когда подходит на кассу,
//получает итоговую сумму покупки и старается её оплатить.
//Если оплатить клиент не может,
//то он рандомный товар из корзины выкидывает до тех пор,
//пока его денег не хватит для оплаты.
//Клиентов можно делать ограниченное число на старте программы.
//Супермаркет содержит список товаров, из которых клиент выбирает товары для покупки.