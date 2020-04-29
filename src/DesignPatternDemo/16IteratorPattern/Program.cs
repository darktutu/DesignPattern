using System;

namespace _16IteratorPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            Iterator iterator;
            ITroopQueue list = new ConcreteTroopQueue();
            iterator = list.GetIterator();

            while (iterator.MoveNext())
            {
                string ren = (string)iterator.GetCurrent();
                Console.WriteLine(ren);
                iterator.Next();
            }

            Console.Read();
        }


    }
    // 部队队列的抽象聚合类--该类型相当于抽象聚合类Aggregate
    public interface ITroopQueue
    {
        Iterator GetIterator();
    }

    // 迭代器抽象类
    public interface Iterator
    {
        bool MoveNext();
        Object GetCurrent();
        void Next();
        void Reset();
    }

    //部队队列具体聚合类--相当于具体聚合类ConcreteAggregate
    public sealed class ConcreteTroopQueue : ITroopQueue
    {
        private string[] collection;

        public ConcreteTroopQueue()
        {
            collection = new string[] { "黄飞鸿", "方世玉", "洪熙官", "严咏春" };
        }

        public Iterator GetIterator()
        {
            return new ConcreteIterator(this);
        }

        public int Length
        {
            get { return collection.Length; }
        }

        public string GetElement(int index)
        {
            return collection[index];
        }
    }

    // 具体迭代器类
    public sealed class ConcreteIterator : Iterator
    {
        // 迭代器要集合对象进行遍历操作，自然就需要引用集合对象
        private ConcreteTroopQueue _list;
        private int _index;

        public ConcreteIterator(ConcreteTroopQueue list)
        {
            _list = list;
            _index = 0;
        }

        public bool MoveNext()
        {
            if (_index < _list.Length)
            {
                return true;
            }
            return false;
        }

        public Object GetCurrent()
        {
            return _list.GetElement(_index);
        }

        public void Reset()
        {
            _index = 0;
        }

        public void Next()
        {
            if (_index < _list.Length)
            {
                _index++;
            }

        }
    }

}