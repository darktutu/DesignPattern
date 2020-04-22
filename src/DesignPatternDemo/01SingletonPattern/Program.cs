using System;

namespace _01SingletonPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }


    public sealed class Singleton1
    {
        private static readonly Singleton1 instance = new Singleton1();

        // 显式静态构造函数告诉C＃编译器
        // 不要将类型标记为BeforeFieldInit
        // 如果没有静态类的应用可以去掉这个构造函数
        static Singleton1()
        {
        }

        private Singleton1()
        {
        }

        public static Singleton1 Instance
        {
            get
            {
                return instance;
            }
        }
    }

    public sealed class Singleton
    {
        private static readonly Lazy<Singleton> lazy = new Lazy<Singleton>(() => new Singleton());

        public static Singleton Instance { get { return lazy.Value; } }

        private Singleton()
        {
        }
    }


}
