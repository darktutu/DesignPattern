using System;

namespace _02SampleFactory
{
    class Program
    {
        static void Main(string[] args)
        {

            // 客户想点一个西红柿炒蛋        
            Food food = FoodSimpleFactory.CreateFood("西红柿炒蛋");
            food.Print();

            // 客户想点一个土豆肉丝
            food = FoodSimpleFactory.CreateFood("土豆肉丝");
            food.Print();

            Console.Read();
        }
    }
}
