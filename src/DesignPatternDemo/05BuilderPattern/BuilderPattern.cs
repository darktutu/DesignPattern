using System;
using System.Collections.Generic;
using System.Text;

namespace _05BuilderPattern.Hand
{
    public class Director
    {
        public void Construct(Builder builder)
        {
            builder.BuildCarDoor();
            builder.BuildCarWheel();
            builder.BuildCarEngine();
        }
    }

    public abstract class Builder
    {

        public abstract void BuildCarDoor();
        public abstract void BuildCarWheel();
        public abstract void BuildCarEngine();

        public abstract Car GetCar();
    }


    public class Car
    {
        private List<string> parts = new List<string>();
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

    public class BuickBuilder : Builder
    {
        Car car = new Car();
        public override void BuildCarDoor()
        {
            car.Add("Buick Door");
        }

        public override void BuildCarEngine()
        {
            car.Add("Buick Door");
        }

        public override void BuildCarWheel()
        {
            car.Add("Buick Door");
        }

        public override Car GetCar()
        {
            return car;
        }
    }

    public class AoDiBuilder : Builder
    {
        Car car = new Car();
        public override void BuildCarDoor()
        {
            car.Add("AoDi Door");
        }

        public override void BuildCarEngine()
        {
            car.Add("AoDi Engine");
        }

        public override void BuildCarWheel()
        {
            car.Add("AoDi Wheel");
        }

        public override Car GetCar()
        {
            return car;
        }
    }
}
