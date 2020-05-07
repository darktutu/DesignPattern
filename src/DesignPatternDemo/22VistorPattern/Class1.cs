using System;
using System.Collections.Generic;
using System.Text;


namespace Visitor
{
    //抽象访问者角色 Visitor
    public abstract class Visitor
    {
        public abstract void PutTelevision(Television tv);

        public abstract void PutComputer(Computer comp);
    }

    //具体访问者角色 ConcreteVisitor
    public sealed class SizeVisitor : Visitor
    {
        public override void PutTelevision(Television tv)
        {
            Console.WriteLine("按商品大小{0}排放", tv.Size);
        }

        public override void PutComputer(Computer comp)
        {
            Console.WriteLine("按商品大小{0}排放", comp.Size);
        }
    }

    //具体访问者角色 ConcreteVisitor
    public sealed class StateVisitor : Visitor
    {
        public override void PutTelevision(Television tv)
        {
            Console.WriteLine("按商品新旧值{0}排放", tv.State);
        }

        public override void PutComputer(Computer comp)
        {
            Console.WriteLine("按商品新旧值{0}排放", comp.State);
        }
    }

    //抽象节点角色 Element
    public abstract class Goods
    {
        public abstract void Operate(Visitor visitor);

        private int nSize;
        public int Size
        {
            get { return nSize; }
            set { nSize = value; }
        }

        private int nState;
        public int State
        {
            get { return nState; }
            set { nState = value; }
        }
    }

    //具体节点角色 ConcreteElement
    public sealed class Television : Goods
    {
        public override void Operate(Visitor visitor)
        {
            visitor.PutTelevision(this);
        }
    }

    //具体节点角色 ConcreteElement
    public sealed class Computer : Goods
    {
        public override void Operate(Visitor visitor)
        {
            visitor.PutComputer(this);
        }
    }

    //结构对象角色
    public sealed class StoragePlatform
    {
        private IList<Goods> list = new List<Goods>();

        public void Attach(Goods element)
        {
            list.Add(element);
        }

        public void Detach(Goods element)
        {
            list.Remove(element);
        }

        public void Operate(Visitor visitor)
        {
            foreach (Goods g in list)
            {
                g.Operate(visitor);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            StoragePlatform platform = new StoragePlatform();
            platform.Attach(new Television());
            platform.Attach(new Computer());

            SizeVisitor sizeVisitor = new SizeVisitor();
            StateVisitor stateVisitor = new StateVisitor();

            platform.Operate(sizeVisitor);
            platform.Operate(stateVisitor);

            Console.Read();
        }
    }
}