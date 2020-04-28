using System;

namespace _09DecoratorPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            // 这就是我们的饺子馅，需要装饰的房子
            House myselfHouse = new PatrickLiuHouse();

            DecorationStrategy securityHouse = new HouseSecurityDecorator(myselfHouse);
            securityHouse.Renovation();
            //房子就有了安全系统了

            //如果我既要安全系统又要保暖呢，继续装饰就行
            DecorationStrategy securityAndWarmHouse = new HouseSecurityDecorator(securityHouse);
            securityAndWarmHouse.Renovation();
            Console.WriteLine("Hello World!");
        }
    }


    /// <summary>
    /// 该抽象类就是房子抽象接口的定义，该类型就相当于是Component类型，是饺子馅，需要装饰的，需要包装的
    /// </summary>
    public abstract class House
    {
        //房子的装修方法--该操作相当于Component类型的Operation方法
        public abstract void Renovation();
    }

    /// <summary>
    /// 该抽象类就是装饰接口的定义，该类型就相当于是Decorator类型，如果需要具体的功能，可以子类化该类型
    /// </summary>
    public abstract class DecorationStrategy : House //关键点之二，体现关系为Is-a，有这这个关系，装饰的类也可以继续装饰了
    {
        //通过组合方式引用Decorator类型，该类型实施具体功能的增加
        //这是关键点之一，包含关系，体现为Has-a
        protected House _house;

        //通过构造器注入，初始化平台实现
        protected DecorationStrategy(House house)
        {
            this._house = house;
        }

        //该方法就相当于Decorator类型的Operation方法
        public override void Renovation()
        {
            if (this._house != null)
            {
                this._house.Renovation();
            }
        }
    }

    /// <summary>
    /// PatrickLiu的房子，我要按我的要求做房子，相当于ConcreteComponent类型，这就是我们具体的饺子馅，我个人比较喜欢韭菜馅
    /// </summary>
    public sealed class PatrickLiuHouse : House
    {
        public override void Renovation()
        {
            Console.WriteLine("装修PatrickLiu的房子");
        }
    }


    /// <summary>
    /// 具有安全功能的设备，可以提供监视和报警功能，相当于ConcreteDecoratorA类型
    /// </summary>
    public sealed class HouseSecurityDecorator : DecorationStrategy
    {
        public HouseSecurityDecorator(House house) : base(house) { }

        public override void Renovation()
        {
            base.Renovation();
            Console.WriteLine("增加安全系统");
        }
    }

    /// <summary>
    /// 具有保温接口的材料，提供保温功能，相当于ConcreteDecoratorB类型
    /// </summary>
    public sealed class KeepWarmDecorator : DecorationStrategy
    {
        public KeepWarmDecorator(House house) : base(house) { }

        public override void Renovation()
        {
            base.Renovation();
            Console.WriteLine("增加保温的功能");
        }
    }


}

