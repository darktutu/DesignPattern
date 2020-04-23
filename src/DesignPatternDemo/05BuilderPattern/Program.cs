using System;
using System.Collections.Generic;

namespace _05BuilderPattern.Copy
{

    /// <summary>
    /// 现在人们的生活水平都提高了，有钱了，我今天就以汽车组装为例子
    /// 每台汽车的组装过程都是一致的，所以我们使用同样的构建过程可以创建不同的表示(即可以组装成不同型号的汽车，不能像例子这样，一会别克，一会奥迪的)
    /// 组装汽车、电脑、手机、电视等等负责对象的这些场景都可以应用建造者模式来设计
    /// </summary>


    /// <summary>
    /// 客户类
    /// </summary>
    class Customer
    {
        static void Main(string[] args)
        {
            Director director = new Director();
            Builder buickCarBuilder = new BuickBuilder();
            Builder aoDiCarBuilder = new AoDiBuilder();

            director.Construct(buickCarBuilder);

            //组装完成，我来驾驶别克了
            Car buickCar = buickCarBuilder.GetCar();
            buickCar.Show();

            // 我老婆就要奥迪了，她比较喜欢大品牌
            director.Construct(aoDiCarBuilder);
            Car aoDiCar = aoDiCarBuilder.GetCar();
            aoDiCar.Show();

            Console.Read();
        }
    }

    /// <summary>
    /// 这个类型才是组装的，Construct方法里面的实现就是创建复杂对象固定算法的实现，该算法是固定的，或者说是相对稳定的
    /// 这个人当然就是老板了，也就是建造者模式中的指挥者
    /// </summary>
    public class Director
    {
        // 组装汽车
        public void Construct(Builder builder)
        {
            builder.BuildCarDoor();
            builder.BuildCarWheel();
            builder.BuildCarEngine();
        }
    }

    /// <summary>
    /// 汽车类
    /// </summary>
    public sealed class Car
    {
        // 汽车部件集合
        private IList<string> parts = new List<string>();

        // 把单个部件添加到汽车部件集合中
        public void Add(string part)
        {
            parts.Add(part);
        }

        public void Show()
        {
            Console.WriteLine("汽车开始在组装.......");
            foreach (string part in parts)
            {
                Console.WriteLine("组件" + part + "已装好");
            }

            Console.WriteLine("汽车组装好了");
        }
    }

    /// <summary>
    /// 抽象建造者，它定义了要创建什么部件和最后创建的结果，但是不是组装的的类型，切记
    /// </summary>
    public abstract class Builder
    {
        // 创建车门
        public abstract void BuildCarDoor();
        // 创建车轮
        public abstract void BuildCarWheel();
        //创建车引擎
        public abstract void BuildCarEngine();
        // 当然还有部件，大灯、方向盘等，这里就省略了

        // 获得组装好的汽车
        public abstract Car GetCar();
    }

    /// <summary>
    /// 具体创建者，具体的车型的创建者，例如：别克
    /// </summary>
    public sealed class BuickBuilder : Builder
    {
        Car buickCar = new Car();
        public override void BuildCarDoor()
        {
            buickCar.Add("Buick's Door");
        }

        public override void BuildCarWheel()
        {
            buickCar.Add("Buick's Wheel");
        }

        public override void BuildCarEngine()
        {
            buickCar.Add("Buick's Engine");
        }

        public override Car GetCar()
        {
            return buickCar;
        }
    }

    /// <summary>
    /// 具体创建者，具体的车型的创建者，例如：奥迪
    /// </summary>
    public sealed class AoDiBuilder : Builder
    {
        Car aoDiCar = new Car();
        public override void BuildCarDoor()
        {
            aoDiCar.Add("Aodi's Door");
        }

        public override void BuildCarWheel()
        {
            aoDiCar.Add("Aodi's Wheel");
        }

        public override void BuildCarEngine()
        {
            aoDiCar.Add("Aodi's Engine");
        }

        public override Car GetCar()
        {
            return aoDiCar;
        }
    }
}


