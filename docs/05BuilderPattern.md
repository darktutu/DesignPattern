# C#设计模式之四建造者模式（Builder Pattern）【创建型】
## 一、引言

  今天我们要讲讲Builder模式，也就是建造者模式，当然也有叫生成器模式的，英文名称是Builder Pattern。在现实生活中，我们经常会遇到一些构成比较复杂的物品，比如：电脑，它就是一个复杂的物品，它主要是由CPU、主板、硬盘、显卡、机箱等组装而成的。手机当然也是复杂物品，由主板，各种芯片，RAM 和ROM  摄像头之类的东西组成。但是无论是电脑还是手机，他们的组装过程是固定的，就拿手机来说，组装流水线是固定的，不变的，但是把不同的主板和其他组件组装在一起就会生产出不同型号的手机。那么在软件系统中是不是也会存在这样的对象呢？答案是肯定的。在软件系统中我们也会遇到类似的复杂对象，并且这个复杂对象的各个部分按照一定的算法组合在一起，此时该对象的创建工作就可以使用Builder模式了，下面我就来详细看看这个模式吧。

## 二、建造者模式的详细介绍

### 2.1、动机（Motivate）

  在软件系统中，有时候面临着“一个复杂对象”的创建工作，其通常由各个部分的子对象用一定的算法构成；由于需求的变化，这个复杂对象的各个部分经常面临着剧烈的变化，但是将它们组合在一起的算法却相对稳定。如何应对这种变化？如何提供一种“封装机制”来隔离出“复杂对象的各个部分”的变化，从而保持系统中的“稳定构建算法”不随着需求改变而改变？

### 2.2、意图（Intent）

  将一个复杂对象的构建与其表示相分离，使得同样的构建过程可以创建不同的表示。                   ——《设计模式》GoF

### 2.3、结构图（Structure）
    
### 2.4、模式的组成

 （1）、抽象建造者角色（Builder）：为创建一个Product对象的各个部件指定抽象接口，以规范产品对象的各个组成成分的建造。一般而言，此角色规定要实现复杂对象的哪些部分的创建，并不涉及具体的对象部件的创建。
 （2）、具体建造者（ConcreteBuilder）

     1）实现Builder的接口以构造和装配该产品的各个部件。即实现抽象建造者角色Builder的方法。

     2）定义并明确它所创建的表示，即针对不同的商业逻辑，具体化复杂对象的各部分的创建

     3) 提供一个检索产品的接口

     4) 构造一个使用Builder接口的对象即在指导者的调用下创建产品实例

  （3）、指导者（Director）：调用具体建造者角色以创建产品对象的各个部分。指导者并没有涉及具体产品类的信息，真正拥有具体产品的信息是具体建造者对象。它只负责保证对象各部分完整创建或按某种顺序创建。

  （4）、产品角色（Product）：建造中的复杂对象。它要包含那些定义组件的类，包括将这些组件装配成产品的接口。

### 2.5 建筑者模式的具体实现

  现在人们生活水平都提高了，家家都有了家庭轿车，那今天我们就以汽车组装为例来说明Builder模式的实现。
``` c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// 现在人们的生活水平都提高了，有钱了，我今天就以汽车组装为例子
/// 每台汽车的组装过程都是一致的，所以我们使用同样的构建过程可以创建不同的表示(即可以组装成不同型号的汽车，不能像例子这样，一会别克，一会奥迪的)
/// 组装汽车、电脑、手机、电视等等负责对象的这些场景都可以应用建造者模式来设计
/// </summary>
namespace 设计模式之建造者模式
{
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
 
```

 上面代码中都有详细的注释代码，这里就不过多解释。

## 三、建造者模式的实现要点

   在建造者模式中，指挥者是直接与客户端打交道的，指挥者将客户端创建产品的请求划分为对各个部件的建造请求，再将这些请求委派到具体建造者角色，具体建造者角色是完成具体产品的构建工作的，却不为客户所知道。 建造者模式主要用于“分步骤来构建一个复杂的对象”，其中“分步骤”是一个固定的组合过程，而复杂对象的各个部分是经常变化的。 产品不需要抽象类，由于建造模式的创建出来的最终产品可能差异很大，所以不大可能提炼出一个抽象产品类。 在前面文章中介绍的抽象工厂模式解决了“系列产品”的需求变化，而建造者模式解决的是 “产品部分” 的需要变化。 由于建造者隐藏了具体产品的组装过程，所以要改变一个产品的内部表示，只需要再实现一个具体的建造者就可以了，从而能很好地应对产品组成组件的需求变化。

  ### 3.1】、建造者模式的优点：

    （1）、使用建造者模式可以使客户端不必知道产品内部组成的细节。

    （2）、具体的建造者类之间是相互独立的，容易扩展。

    （3）、由于具体的建造者是独立的，因此可以对建造过程逐步细化，而不对其他的模块产生任何影响。

 ### 3.2】、建造者模式的缺点：

    （1）、产生多余的Build对象以及Dirextor类。

 ### 3.3】、创建者模式的使用场景：

    （1）、当创建复杂对象的算法应该独立于该对象的组成部分以及它们的装配方式时。

    （2）、相同的方法，不同的执行顺序，产生不同的事件结果时。

    （3）、多个部件或零件,都可以装配到一个对象中，但是产生的运行结果又不相同时。

    （4）、产品类非常复杂，或者产品类中的调用顺序不同产生了不同的效能。

    （5）、创建一些复杂的对象时，这些对象的内部组成构件间的建造顺序是稳定的，但是对象的内部组成构件面临着复杂的变化。

## 四、.NET 中建造者模式的实现

   在微软的类库里面大量使用了设计模式，如果要想学习设计模式，仔细看看微软的类库是很有帮助的。今天的设计模式在FCL里面也有实现，该类型的名字就是System.Text.StringBuilder(存在mscorlib.dll程序集中)，它就是一个建造者模式的实现，从名称也可以看出来。不过它的实现属于建造者模式的演化，此时的建造者模式没有指挥者角色和抽象建造者角色，StringBuilder类即扮演着具体建造者的角色，也同时扮演了指挥者和抽象建造者的角色，StringBuilder类扮演着建造string对象的具体建造者角色，其中的ToString()方法用来返回具体产品给客户端（相当于上面代码中GetProduct方法）。其中Append方法用来创建产品的组件(相当于上面代码中BuildPartA和BuildPartB方法)，因为string对象中每个组件都是字符，所以也就不需要指挥者的角色的代码（指的是Construct方法,用来调用创建每个组件的方法来完成整个产品的组装），因为string字符串对象中每个组件都是一样的,都是字符,所以Append方法也充当了指挥者Construct方法的作用。

## 五、总结

  今天就到这里了，还需要重申的是，学习设计模式不能死学，就像StringBuilder一样，他和Gof23种设计模式中定义的情形有很大的不同，但是它也是Builder模式，因为它们要解决的问题和使用场景是吻合的。我们写代码的时候，不要太居于形式，要看使用的契机和模式是否吻合，根据具体的情况我们的模式也会发生变化。当我们看得越多，写的越多时候，你的变化就越自然了。