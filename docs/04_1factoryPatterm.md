# 详解设计模式之工厂模式(简单工厂+工厂方法+抽象工厂)

转自[博客园](https://www.cnblogs.com/toutou/p/4899388.html)

看了工厂模式，但是并没有理解模式带来的好处。没事多思考吧，没见到应用长江

## 简单工厂模式
### 1.介绍： 

简单工厂模式是属于创建型模式，又叫做静态工厂方法（Static Factory Method）模式，但不属于23种GOF设计模式之一。简单工厂模式是由一个工厂对象决定创建出哪一种产品类的实例。简单工厂模式是工厂模式家族中最简单实用的模式，可以理解为是不同工厂模式的一个特殊实现。

### 2.延伸： 

试想一下，当我们在coding的时候，在A类里面只要NEW了一个B类的对象，那么A类就会从某种程度上依赖B类。如果在后期需求发生变化或者是维护的时候，需要修改B类的时候，我们就需要打开源代码修改所有与这个类有关的类了，做过重构的朋友都知道，这样的事情虽然无法完全避免，但确实是一件让人心碎的事情。

### 3.模拟场景： 

欧美主导的以赛车为主题的系列电影《速度与激情》系列相信大家都看过，里面的男主角(zhǔ jué，加个拼音，经常听到有人说什么主脚主脚的，虽然之前我也不确定是zhǔ jué还是主脚，但是我没念过主脚，我在不确定的情况下我都是念男一号)范·迪塞尔在每一集里面做不同的事情都是开不同的车子，相信大家都觉得很酷吧。
人家酷也没办法，谁叫人家是大佬呢。这里我们试想一下，如果这是一套程序，我们该怎么设计？每次不同的画面或者剧情范·迪塞尔都需要按照导演的安排开不一样的车，去参加赛车需要开的是跑车，可能导演就会说下一场戏：范·迪塞尔下一场戏需要开跑车(参数)，要去参加五环首届跑车拉力赛，这时候场务(工厂类)接到导演的命令(跑车参数)后需要从车库开出一辆跑车(具体产品)交到范·迪塞尔手上让他去准备五环首届跑车拉力赛。这套程序的整个生命周期就算完成了。(什么？没完成？难不成你还真想来个五环首届跑车拉力赛了啊:)

根据导演不同的指令，开的车是不一样的，但是车都是在车库中存在的。车都属于同一种抽象，车库里所有的车都有自己的特征，这些特征就是条件。导演发出指令的时候，只要告诉场务特征，场务就知道提什么车。这就简单工厂模式的典型案例。
### 5.代码演示： 
``` C#
namespace CNBlogs.DesignPattern.Common
{
    /// <summary>
    /// 抽象产品类： 汽车
    /// </summary>
    public interface ICar
    {
        void GetCar();
    }
}

namespace CNBlogs.DesignPattern.Common
{
    public enum CarType
    {
        SportCarType = 0,
        JeepCarType = 1,
        HatchbackCarType = 2
    }

    /// <summary>
    /// 具体产品类： 跑车
    /// </summary>
    public class SportCar : ICar
    {
        public void GetCar()
        {
            Console.WriteLine("场务把跑车交给范·迪塞尔");
        }
    }

    /// <summary>
    /// 具体产品类： 越野车
    /// </summary>
    public class JeepCar : ICar
    {
        public void GetCar()
        {
            Console.WriteLine("场务把越野车交给范·迪塞尔");
        }
    }

    /// <summary>
    /// 具体产品类： 两箱车
    /// </summary>
    public class HatchbackCar : ICar
    {
        public void GetCar()
        {
            Console.WriteLine("场务把两箱车交给范·迪塞尔");
        }
    }
}

namespace CNBlogs.DesignPattern.Common
{
    public class Factory
    {
        public ICar GetCar(CarType carType)
        {
            switch (carType)
            {
                case CarType.SportCarType:
                    return new SportCar();
                case CarType.JeepCarType:
                    return new JeepCar();
                case CarType.HatchbackCarType:
                    return new HatchbackCar();
                default:
                    throw new Exception("爱上一匹野马,可我的家里没有草原. 你走吧！");
            }
        }
    }
}

//------------------------------------------------------------------------------
// <copyright file="Program.cs" company="CNBlogs Corporation">
//     Copyright (C) 2015-2016 All Rights Reserved
//     原博文地址： http://www.cnblogs.com/toutou/
//     作      者: 请叫我头头哥
// </copyright> 
//------------------------------------------------------------------------------
namespace CNBlogs.DesignPattern
{
    using System;
    using CNBlogs.DesignPattern.Common;

    class Program
    {
        static void Main(string[] args)
        {
            ICar car;
            try
            {
                Factory factory = new Factory();

                Console.WriteLine("范·迪塞尔下一场戏开跑车。");
                car = factory.GetCar(CarType.SportCarType);
                car.GetCar();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
```

简单工厂的简单案例就这么多，真正在项目实战的话可能还有需要改进和扩展的地方。因需求而定吧。

### 6.简单工厂的优点/缺点： 

优点：简单工厂模式能够根据外界给定的信息，决定究竟应该创建哪个具体类的对象。明确区分了各自的职责和权力，有利于整个软件体系结构的优化。
缺点：很明显工厂类集中了所有实例的创建逻辑，容易违反GRASPR的高内聚的责任分配原则

## 工厂方法模式

### 1.介绍： 

工厂方法模式Factory Method，又称多态性工厂模式。在工厂方法模式中，核心的工厂类不再负责所有的产品的创建，而是将具体创建的工作交给子类去做。该核心类成为一个抽象工厂角色，仅负责给出具体工厂子类必须实现的接口，而不接触哪一个产品类应当被实例化这种细节。
### 2.定义： 

工厂方法模式是简单工厂模式的衍生，解决了许多简单工厂模式的问题。首先完全实现‘开－闭 原则’，实现了可扩展。其次更复杂的层次结构，可以应用于产品结果复杂的场合。
### 3.延伸： 

在上面简单工厂的引入中，我们将实例化具体对象的工作全部交给了专门负责创建对象的工厂类(场务)中，这样就可以在我们得到导演的命令后创建对应的车(产品)类了。但是剧组的导演是性情比较古怪的，可能指令也是无限变化的。这样就有了新的问题，一旦导演发出的指令时我们没有预料到的，就必须得修改源代码。这也不是很合理的。工厂方法就是为了解决这类问题的。
### 4.模拟场景： 

还是上面范·迪塞尔要去参加五环首届跑车拉力赛的场景。因为要拍摄《速度与激情8》，导演组车的种类增多了，阵容也更加豪华了，加上导演古怪的性格可能每一场戏绝对需要试驾几十种车。如果车库没有的车(具体产品类)可以由场务(具体工厂类)直接去4S店取，这样没增加一种车(具体产品类)就要对应的有一个场务(具体工厂类)，他们互相之间有着各自的职责，互不影响，这样可扩展性就变强了。

### 6.代码演示：
``` c# 
namespace CNBlogs.DesignPattern.Common
{
    public interface IFactory
    {
        ICar CreateCar();
    }
}

namespace CNBlogs.DesignPattern.Common
{
    public interface ICar
    {
        void GetCar();
    }
}

namespace CNBlogs.DesignPattern.Common
{
    /// <summary>
    ///  具体工厂类： 用于创建跑车类
    /// </summary>
    public class SportFactory : IFactory
    {
        public ICar CreateCar()
        {
            return new SportCar();
        }
    }

    /// <summary>
    ///  具体工厂类： 用于创建越野车类
    /// </summary>
    public class JeepFactory : IFactory
    {
        public ICar CreateCar()
        {
            return new JeepCar();
        }
    }

    /// <summary>
    ///  具体工厂类： 用于创建两厢车类
    /// </summary>
    public class HatchbackFactory : IFactory
    {
        public ICar CreateCar()
        {
            return new HatchbackCar();
        }
    }
}

namespace CNBlogs.DesignPattern.Common
{
    /// <summary>
    /// 具体产品类： 跑车
    /// </summary>
    public class SportCar : ICar
    {
        public void GetCar()
        {
            Console.WriteLine("场务把跑车交给范·迪塞尔");
        }
    }

    /// <summary>
    /// 具体产品类： 越野车
    /// </summary>
    public class JeepCar : ICar
    {
        public void GetCar()
        {
            Console.WriteLine("场务把越野车交给范·迪塞尔");
        }
    }

    /// <summary>
    /// 具体产品类： 两箱车
    /// </summary>
    public class HatchbackCar : ICar
    {
        public void GetCar()
        {
            Console.WriteLine("场务把两箱车交给范·迪塞尔");
        }
    }
}


//------------------------------------------------------------------------------
// <copyright file="Program.cs" company="CNBlogs Corporation">
//     Copyright (C) 2015-2016 All Rights Reserved
//     原博文地址： http://www.cnblogs.com/toutou/
//     作      者: 请叫我头头哥
// </copyright> 
//------------------------------------------------------------------------------
namespace CNBlogs.DesignPattern
{
    using System.IO;
    using System.Configuration;
    using System.Reflection;
    using CNBlogs.DesignPattern.Common;

    class Program
    {
        static void Main(string[] args)
        {
            // 工厂类的类名写在配置文件中可以方便以后修改
            string factoryType = ConfigurationManager.AppSettings["FactoryType"];

            // 这里把DLL配置在数据库是因为以后数据可能发生改变
            // 比如说现在的数据是从sql server取的，以后需要从oracle取的话只需要添加一个访问oracle数据库的工程就行了
            string dllName = ConfigurationManager.AppSettings["DllName"];

            // 利用.NET提供的反射可以根据类名来创建它的实例，非常方便
            var currentAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            string codeBase = currentAssembly.CodeBase.ToLower().Replace(currentAssembly.ManifestModule.Name.ToLower(), string.Empty);
            IFactory factory = Assembly.LoadFrom(Path.Combine(codeBase, dllName)).CreateInstance(factoryType) as IFactory;
            ICar car = factory.CreateCar();
            car.GetCar();
        }
    }
}
```

### 7.工厂方法的优点/缺点： 

* 优点：
    * 子类提供挂钩。基类为工厂方法提供缺省实现，子类可以重 写新的实现，也可以继承父类的实现。-- 加一层间接性，增加了灵活性
    * 屏蔽产品类。产品类的实现如何变化，调用者都不需要关心，只需关心产品的接口，只要接口保持不变，系统中的上层模块就不会发生变化。
    * 典型的解耦框架。高层模块只需要知道产品的抽象类，其他的实现类都不需要关心，符合迪米特法则，符合依赖倒置原则，符合里氏替换原则。
    * 多态性：客户代码可以做到与特定应用无关，适用于任何实体类。
* 缺点：需要Creator和相应的子类作为factory method的载体，如果应用模型确实需要creator和子类存在，则很好；否则的话，需要增加一个类层次。(不过说这个缺点好像有点吹毛求疵了)

## 抽象工厂模式

### 1.介绍： 

抽象工厂模式是所有形态的工厂模式中最为抽象和最具一般性的一种形态。抽象工厂模式是指当有多个抽象角色时，使用的一种工厂模式。抽象工厂模式可以向客户端提供一个接口，使客户端在不必指定产品的具体的情况下，创建多个产品族中的产品对象。根据里氏替换原则，任何接受父类型的地方，都应当能够接受子类型。因此，实际上系统所需要的，仅仅是类型与这些抽象产品角色相同的一些实例，而不是这些抽象产品的实例。换言之，也就是这些抽象产品的具体子类的实例。工厂类负责创建抽象产品的具体子类的实例。
### 2.定义： 

为创建一组相关或相互依赖的对象提供一个接口，而且无需指定他们的具体类。
### 3.模拟场景： 

我们还是继续范·迪塞尔的例子，往往这些大牌生活中经常参加一些活动，或是商务活动或是公益活动。不管参加什么活动，加上老范(范·迪塞尔名字太长，以下文中简称老范)的知名度，他的车肯定不少，可能光跑车或者光越野车就有多辆。比如说有跑车(多辆，跑车系列的具体产品)、越野车(多辆，越野车系列的具体产品)、两箱车(多辆，两箱车系列的具体产品)。可能很多大牌明星都是如此的。假设老范家里，某一个车库(具体工厂)只存放某一系列的车(比如说跑车车库只存放跑车一系列具体的产品)，每次要某一辆跑车的时候肯定要从这个跑车车库里开出来。用了OO(Object Oriented,面向对象)的思想去理解，所有的车库(具体工厂)都是车库类(抽象工厂)的某一个，而每一辆车又包括具体的开车时候所背的包(某一具体产品。包是也是放在车库里的，不同的车搭配不同的包，我们把车和车对应的背包称作出去参加活动的装备)，这些具体的包其实也都是背包(抽象产品)，具体的车其实也都是车(另一个抽象产品)。
### 4.场景分析： 

上面的场景可能有点稀里糊涂的，但是用OO的思想结合前面的简单工厂和工厂方法的思路去理解的话，也好理解。

下面让我们来捋一捋这个思路：

抽象工厂：虚拟的车库，只是所有车库的一个概念。在程序中可能是一个借口或者抽象类，对其他车库的规范，开车和取包。
具体工厂：具体存在的车库，用来存放车和车对应的背包。在程序中继承抽象工厂，实现抽象工厂中的方法，可以有具体的产品。
抽象产品：虚拟的装备(车和对应的背包)，也只是所有装备的一个概念。在程序中可能是多个接口或者多个抽象类，对具体的装备起到规范。
具体产品：活动参加的具体装备，它指的是组成装备的某一辆车或者背包。它继承自某一个抽象产品。
### 6.代码演示：
``` c#
namespace CNBlogs.DesignPattern.Common
{
    /// <summary>
    /// 抽象工厂类
    /// </summary>
    public abstract class AbstractEquipment
    {
        /// <summary>
        /// 抽象方法： 创建一辆车
        /// </summary>
        /// <returns></returns>
        public abstract AbstractCar CreateCar();

        /// <summary>
        /// 抽象方法： 创建背包
        /// </summary>
        /// <returns></returns>
        public abstract AbstractBackpack CreateBackpack();
    }
}

namespace CNBlogs.DesignPattern.Common
{
    /// <summary>
    /// 抽象产品: 车抽象类
    /// </summary>
    public abstract class AbstractCar
    {
        /// <summary>
        /// 车的类型属性
        /// </summary>
        public abstract string Type
        {
            get;
        }

        /// <summary>
        /// 车的颜色属性
        /// </summary>
        public abstract string Color
        {
            get;
        }
    }

    /// <summary>
    /// 抽象产品: 背包抽象类
    /// </summary>
    public abstract class AbstractBackpack
    {
        /// <summary>
        /// 包的类型属性
        /// </summary>
        public abstract string Type
        {
            get;
        }

        /// <summary>
        /// 包的颜色属性
        /// </summary>
        public abstract string Color
        {
            get;
        }
    }
}

namespace CNBlogs.DesignPattern.Common
{
    /// <summary>
    /// 运动装备
    /// </summary>
    public class SportEquipment : AbstractEquipment
    {
        public override AbstractCar CreateCar()
        {
            return new SportCar();
        }

        public override AbstractBackpack CreateBackpack()
        {
            return new SportBackpack();
        }
    }

    /// <summary>
    /// 越野装备  这里就不添加了，同运动装备一个原理，demo里只演示一个，实际项目中可以按需添加
    /// </summary>
    //public class JeepEquipment : AbstractEquipment
    //{
    //    public override AbstractCar CreateCar()
    //    {
    //        return new JeeptCar();
    //    }

    //    public override AbstractBackpack CreateBackpack()
    //    {
    //        return new JeepBackpack();
    //    }
    //}
}

namespace CNBlogs.DesignPattern.Common
{
    /// <summary>
    /// 跑车
    /// </summary>
    public class SportCar : AbstractCar
    {
        private string type = "Sport";
        private string color = "Red";

        /// <summary>
        /// 重写基类的Type属性
        /// </summary>
        public override string Type
        {
            get
            {
                return type;
            }
        }

        /// <summary>
        /// 重写基类的Color属性
        /// </summary>
        public override string Color
        {
            get
            {
                return color;
            }
        }
    }

    /// <summary>
    /// 运动背包
    /// </summary>
    public class SportBackpack : AbstractBackpack
    {
        private string type = "Sport";
        private string color = "Red";

        /// <summary>
        /// 重写基类的Type属性
        /// </summary>
        public override string Type
        {
            get
            {
                return type;
            }
        }

        /// <summary>
        /// 重写基类的Color属性
        /// </summary>
        public override string Color
        {
            get
            {
                return color;
            }
        }
    }
}
//具体产品可以有很多很多， 至于越野类的具体产品这里就不列出来了。

namespace CNBlogs.DesignPattern.Common
{
    public class CreateEquipment
    {
        private AbstractCar fanCar;
        private AbstractBackpack fanBackpack;
        public CreateEquipment(AbstractEquipment equipment)
        {
            fanCar = equipment.CreateCar();
            fanBackpack = equipment.CreateBackpack();
        }

        public void ReadyEquipment()
        {
            Console.WriteLine(string.Format("老范背着{0}色{1}包开着{2}色{3}车。", 
                fanBackpack.Color, 
                fanBackpack.Type,
                fanCar.Color,
                fanCar.Type
                ));
        }
    }
}

//------------------------------------------------------------------------------
// <copyright file="Program.cs" company="CNBlogs Corporation">
//     Copyright (C) 2015-2016 All Rights Reserved
//     原博文地址： http://www.cnblogs.com/toutou/
//     作      者: 请叫我头头哥
// </copyright> 
//------------------------------------------------------------------------------
namespace CNBlogs.DesignPattern
{
    using System;
    using System.Configuration;
    using System.Reflection;

    using CNBlogs.DesignPattern.Common;

    class Program
    {
        static void Main(string[] args)
        {
            // ***具体app.config配置如下*** //
            //<add key="assemblyName" value="CNBlogs.DesignPattern.Common"/>
            //<add key="nameSpaceName" value="CNBlogs.DesignPattern.Common"/>
            //<add key="typename" value="SportEquipment"/>
            // 创建一个工厂类的实例
            string assemblyName = ConfigurationManager.AppSettings["assemblyName"];
            string fullTypeName = string.Concat(ConfigurationManager.AppSettings["nameSpaceName"], ".", ConfigurationManager.AppSettings["typename"]);
            AbstractEquipment factory = (AbstractEquipment)Assembly.Load(assemblyName).CreateInstance(fullTypeName);
            CreateEquipment equipment = new CreateEquipment(factory);
            equipment.ReadyEquipment();
            Console.Read();
        }
    }
}
```

抽象工厂模式符合了六大原则中的开闭原则、里氏代换原则、依赖倒转原则等等

### 7.抽象工厂的优点/缺点： 

* 优点：
    * 抽象工厂模式隔离了具体类的生产，使得客户并不需要知道什么被创建。
    * 当一个产品族中的多个对象被设计成一起工作时，它能保证客户端始终只使用同一个产品族中的对象。
    * 增加新的具体工厂和产品族很方便，无须修改已有系统，符合“开闭原则”。
* 缺点：增加新的产品等级结构很复杂，需要修改抽象工厂和所有的具体工厂类，对“开闭原则”的支持呈现倾斜性。(不过说这个缺点好像有点吹毛求疵了)