# C#设计模式之五原型模式（Prototype Pattern）【创建型】

文章依然是copy的，这篇文章指出了别的文章里说的错误。
* 错误一：原型模式会减少内存使用量，
我自己有疑问但是没有去验证。懒癌无疑

* 疑问：c# 中 string 在浅拷贝的时候和深copy 到底是什么样子的
疑问来源 [文章](https://www.cnblogs.com/zhili/p/PrototypePattern.html) 中的
>例如，如果一个对象有一个指向字符串的字段，并且我们对该对象做了一个浅拷贝，那么这两个对象将引用同一个字符串，而深拷贝是
>对对象实例中字段引用的对象也进行拷贝，如果一个对象有一个指向字符串的字段，并且我们对该对象进行了深拷贝的话，那么我们将
>创建一个对象和一个新的字符串，新的对象将引用新的字符串。也就是说，执行深拷贝创建的新对象和原来对象不会共享任何东西，改
>变一个对象对另外一个对象没有任何影响，而执行浅拷贝创建的新对象与原来对象共享成员，改变一个对象，另外一个对象的成员也会
>改变

深浅复制相关 [文章](https://www.cnblogs.com/chenwolong/p/MemberwiseClone.html)

## 一、引言

      在开始今天的文章之前先说明一点，欢迎大家来指正。很多人说原型设计模式会节省机器内存，他们说是拷贝出来的对象，这些对象其实都是原型的复制，不会使用内存。我认为这是不对的，因为拷贝出来的每一个对象都是实际存在的，每个对象都有自己的独立内存地址，都会被GC回收。如果就浅拷贝来说，可能会公用一些字段，深拷贝是不会的，所以说原型设计模式会提高内存使用率，不一定。具体还要看当时的设计，如果拷贝出来的对象缓存了，每次使用的是缓存的拷贝对象，那就另当别论了，再说该模式本身解决的不是内存使用率的问题。
     现在说说原型模式的要解决的问题吧，在软件系统中，当创建一个类的实例的过程很昂贵或很复杂，并且我们需要创建多个这样类的实例时，如果我们用new操作符去创建这样的类实例，这就会增加创建类的复杂度和创建过程与客户代码复杂的耦合度。如果采用工厂模式来创建这样的实例对象的话，随着产品类的不断增加，导致子类的数量不断增多，也导致了相应工厂类的增加，维护的代码维度增加了，因为有产品和工厂两个维度了，反而增加了系统复杂程度，所以在这里使用工厂模式来封装类创建过程并不合适。由于每个类实例都是相同的，这个相同指的是类型相同，但是每个实例的状态参数会有不同，如果状态数值也相同就没意义了，有一个这样的对象就可以了。当我们需要多个相同的类实例时，可以通过对原来对象拷贝一份来完成创建，这个思路正是原型模式的实现方式。

## 二、原型模式的详细介绍

### 2.1、动机（Motivate）

        在软件系统中，经常面临着“某些结构复杂的对象”的创建工作；由于需求的变化，这些对象经常面临着剧烈的变化，但是它们却拥有比较稳定一致的接口。如何应对这种变化？如何向“客户程序（使用这些对象的程序）”隔离出“这些易变对象”，从而使得“依赖这些易变对象的客户程序”不随着需求改变而改变？

### 2.2、意图（Intent）

      使用原型实例指定创建对象的种类，然后通过拷贝这些原型来创建新的对象。                 --《设计模式》Gof

### 2.3、结构图（Structure）

      

### 2.4、模式的组成

可以看出，在原型模式的结构图有以下角色：

    （1）、原型类（Prototype）：原型类，声明一个Clone自身的接口；

    （2）、具体原型类（ConcretePrototype）：实现一个Clone自身的操作。

　   在原型模式中，Prototype通常提供一个包含Clone方法的接口，具体的原型ConcretePrototype使用Clone方法完成对象的创建。

### 2.5 原型模式的具体实现

      《大话西游之大圣娶亲》这部电影，没看过的人不多吧，里面有这样一个场景。牛魔王使用无敌牛虱大战至尊宝，至尊宝的应对之策就是，从脑后把下一撮猴毛，吹了口仙气，无数猴子猴孙现身，来大战牛魔王的无敌牛虱。至尊宝的猴子猴孙就是该原型模式的最好体现。至尊宝创建自己的一个副本，不用还要重新孕育五百年，然后出世，再学艺，最后来和老牛大战，估计黄花菜都凉了。他有3根救命猴毛，轻轻一吹，想要多少个自己就有多少个，方便，快捷。
  
``` c#
/// <summary>
/// 原型设计模式，每个具体原型是一类对象的原始对象，通过每个原型对象克隆出来的对象也可以进行设置，在原型的基础之上丰富克隆出来的对象，所以要设计好抽象原型的接口
/// </summary>
namespace 设计模式之原型模式
{
    /// <summary>
    /// 客户类
    /// </summary>
    class Customer
    {
        static void Main(string[] args)
        {
            Prototype xingZheSun = new XingZheSunPrototype();
            Prototype xingZheSun2 = xingZheSun.Clone();
            Prototype xingZheSun3 = xingZheSun.Clone();

            Prototype sunXingZhe = new SunXingZhePrototype();
            Prototype sunXingZhe2 = sunXingZhe.Clone();
            Prototype sunXingZhe3 = sunXingZhe.Clone();
            Prototype sunXingZhe4 = sunXingZhe.Clone();
            Prototype sunXingZhe5 = sunXingZhe.Clone();

            //1号孙行者打妖怪
            sunXingZhe.Fight();
            //2号孙行者去化缘
            sunXingZhe2.BegAlms();

            //战斗和化缘也可以分类，比如化缘，可以分：水果类化缘，饭食类化缘；战斗可以分为：天界宠物下界成妖的战斗，自然修炼成妖的战斗，大家可以自己去想吧，原型模式还是很有用的

            Console.Read();
        }
    }

    /// <summary>
    /// 抽象原型，定义了原型本身所具有特征和动作，该类型就是至尊宝
    /// </summary>
    public abstract class Prototype
    {
        // 战斗--保护师傅
        public abstract void Fight();
        // 化缘--不要饿着师傅
        public abstract void BegAlms();

        // 吹口仙气--变化一个自己出来
        public abstract Prototype Clone();
    }

    /// <summary>
    /// 具体原型，例如：行者孙，他只负责化斋饭食和与天界宠物下界的妖怪的战斗
    /// </summary>
    public sealed class XingZheSunPrototype:Prototype
    {
        // 战斗--保护师傅--与自然修炼成妖的战斗
        public override void Fight()
        {
            Console.WriteLine("腾云驾雾，各种武艺");
        }
        // 化缘--不要饿着师傅--饭食类
        public override void BegAlms()
        {
            Console.WriteLine("什么都能要来");
        }

        // 吹口仙气--变化一个自己出来
        public override Prototype Clone()
        {
            return (XingZheSunPrototype)this.MemberwiseClone();
        }
    }

    /// <summary>
    /// 具体原型，例如：孙行者，他只负责与自然界修炼成妖的战斗和化斋水果
    /// </summary>
    public sealed class SunXingZhePrototype : Prototype
    {
        // 战斗--保护师傅-与天界宠物战斗
        public override void Fight()
        {
            Console.WriteLine("腾云驾雾，各种武艺");
        }
        // 化缘--不要饿着师傅---水果类
        public override void BegAlms()
        {
            Console.WriteLine("什么都能要来");
        }

        // 吹口仙气--变化一个自己出来
        public override Prototype Clone()
        {
            return (SunXingZhePrototype)this.MemberwiseClone();
        }
    }
}
```


   上面代码中都有详细的注释代码，这里就不过多解释。

## 三、原型模式的实现要点： 
 
       Prototype模式同样用于隔离类对象的使用者和具体类型（易变类）之间的耦合关系，它同样要求这些“易变类”拥有“稳定的接口”。

　　Prototype模式对于“如何创建易变类的实体对象”（创建型模式除了Singleton模式以外，都是用于解决创建易变类的实体对象的问题的）采用“原型克隆”的方法来做，它使得我们可以非常灵活地动态创建“拥有某些稳定接口”的新对象——所需工作仅仅是注册一个新类的对象（即原型），然后在任何需要的地方不断地Clone。

　　Prototype模式中的Clone方法可以利用.NET中的Object类的MemberwiseClone()方法或者序列化来实现深拷贝。

    3.1】、原型模式的优点：

        （1）、原型模式向客户隐藏了创建新实例的复杂性

        （2）、原型模式允许动态增加或较少产品类。

        （3）、原型模式简化了实例的创建结构，工厂方法模式需要有一个与产品类等级结构相同的等级结构，而原型模式不需要这样。

        （4）、产品类不需要事先确定产品的等级结构，因为原型模式适用于任何的等级结构

   3.2】、原型模式的缺点：

       （1）、每个类必须配备一个克隆方法

       （2）、配备克隆方法需要对类的功能进行通盘考虑，这对于全新的类不是很难，但对于已有的类不一定很容易，特别当一个类引用不支持串行化的间接对象，或者引用含有循环结构的时候。

   3.3】、原型模式使用的场景：

          （1）、资源优化场景

                      类初始化需要消化非常多的资源，这个资源包括数据、硬件资源等。

         （2）、性能和安全要求的场景

                    通过new产生一个对象需要非常繁琐的数据准备或访问权限，则可以使用原型模式。

         （3）、一个对象多个修改者的场景

                    一个对象需要提供给其他对象访问，而且各个调用者可能都需要修改其值时，可以考虑使用原型模式拷贝多个对象供调用者使用。

           在实际项目中，原型模式很少单独出现，一般是和工厂方法模式一起出现，通过clone的方法创建一个对象，然后由工厂方法提供给调用者。

### 四、.NET 中原型模式的实现

      在.NET中，微软已经为我们提供了原型模式的接口实现，该接口就是ICloneable，其实这个接口就是抽象原型，提供克隆方法，相当于与上面代码中Prototype抽象类，其中的Clone()方法来实现原型模式，如果我们想我们自定义的类具有克隆的功能，首先定义类实现ICloneable接口的Clone方法。其实在.NET中实现了ICloneable接口的类有很多，如下图所示（图中只截取了部分，可以用ILSpy反编译工具进行查看）：

``` c#
namespace System
{
    [ComVisible(true)]
    public interface ICloneable
    {
        object Clone();
    }
}
```
在Net的FCL里面实现ICloneable接口的类如图，自己可以去查看每个类自己的实现，在此就不贴出来了。



## 五、总结

       到今天为止，所有的创建型设计模式就写完了。学习设计模式应该是有一个循序渐进的过程，当我们写代码的时候不要一上来就用什么设计模式，而是通过重构来使用设计模式。创建型的设计模式写完了，我们就总结一下。Singleton单件模式解决的是实体对象个数的问题。除了Singleton之外，其他创建型模式解决的都是new所带来的耦合关系。　Factory Method，Abstract Factory，Builder都需要一个额外的工厂类来负责实例化“易变对象”，而Prototype则是通过原型（一个特殊的工厂类）来克隆“易变对象”。（其实原型就是一个特殊的工厂类，它只是把工厂和实体对象耦合在一起了）。如果遇到“易变类”，起初的设计通常从Factory Method开始，当遇到更多的复杂变化时，再考虑重构为其他三种工厂模式（Abstract Factory，Builder，Prototype）。
　一般来说，如果可以使用Factory Method，那么一定可以使用Prototype。但是Prototype的使用情况一般是在类比较容易克隆的条件之上，如果是每个类实现比较简单，都可以只用实现MemberwiseClone，没有引用类型的深拷贝，那么就更适合了。