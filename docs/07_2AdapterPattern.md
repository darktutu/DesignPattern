# C#设计模式之六适配器模式（Adapter Pattern）【结构型】
## 一、引言

   从今天开始我们开始讲【结构型】设计模式，【结构型】设计模式有如下几种：适配器模式、桥接模式、装饰模式、组合模式、外观模式、享元模式、代理模式。【创建型】的设计模式解决的是对象创建的问题，那【结构型】设计模式解决的是类和对象的组合关系的问题。今天我们就开始讲【结构型】设计模式里面的第一个设计模式，中文名称：适配器模式，英文名称：Adapter Pattern。说起这个模式其实很简单，在现实生活中也有很多实例，比如：我们手机的充电器，充电器的接头，有的是把两相电转换为三相电的，当然也有把三相电转换成两相电的。我们经常使用笔记本电脑，笔记本电脑的工作电压和我们家里照明电压是不一致的，当然也就需要充电器把照明电压转换成笔记本的工作电压，只有这样笔记本电脑才可以正常工作。太多了，就不一一列举了。我们只要记住一点，适配就是转换，把不能在一起工作的两样东西通过转换，让他们可以在一起工作。

## 二、适配器模式的详细介绍

### 2.1、动机（Motivate）

   在软件系统中，由于应用环境的变化，常常需要将“一些现存的对象”放在新的环境中应用，但是新环境要求的接口是这些现存对象所不满足的。如何应对这种“迁移的变化”？如何既能利用现有对象的良好实现，同时又能满足新的应用环境所要求的接口？

### 2.2、意图（Intent）

   将一个类的接口转换成客户希望的另一个接口。Adapter模式使得原本由于接口不兼容而不能一起工作的那些类可以一起工作。                                                                   --《设计模式》Gof

### 2.3、结构图（Structure）

   适配器有两种结构

　　1】、-对象适配器（更常用）

         

　　   对象适配器使用的是对象组合的方案，它的Adapter核Adaptee的关系是组合关系。

　　   OO中优先使用组合模式，组合模式不适用再考虑继承。因为组合模式更加松耦合，而继承是紧耦合的，父类的任何改动都要导致子类的改动。

　　2】、-类适配器
       
         

### 2.4、模式的组成

      可以看出，在适配器模式的结构图有以下角色：

      （1）、目标角色（Target）：定义Client使用的与特定领域相关的接口。
   
      （2）、客户角色（Client）：与符合Target接口的对象协同。
 
      （3）、被适配角色（Adaptee)：定义一个已经存在并已经使用的接口，这个接口需要适配。
 
      （4）、适配器角色（Adapte) ：适配器模式的核心。它将对被适配Adaptee角色已有的接口转换为目标角色Target匹配的接口。对Adaptee的接口与Target接口进行适配.

### 2.5 适配器模式的具体实现

   由于适配器模式有两种实现结构，今天我们针对每种都实现了自己的方式。

   1、对象的是适配器模式实现
``` c#
namespace 对象的适配器模式
{
    ///<summary>
    ///家里只有两个孔的插座，也懒得买插线板了，还要花钱，但是我的手机是一个有3个小柱子的插头，明显直接搞不定，那就适配吧
    ///</summary>
    class Client
    {
        static void Main(string[] args)
        {
            //好了，现在就可以给手机充电了
            TwoHoleTarget homeTwoHole = new ThreeToTwoAdapter();
            homeTwoHole.Request();
            Console.ReadLine();
        }
    }

    /// <summary>
    /// 我家只有2个孔的插座，也就是适配器模式中的目标(Target)角色，这里可以写成抽象类或者接口
    /// </summary>
    public class TwoHoleTarget
    {
        // 客户端需要的方法
        public virtual void Request()
        {
            Console.WriteLine("两孔的充电器可以使用");
        }
    }

    /// <summary>
    /// 手机充电器是有3个柱子的插头，源角色——需要适配的类（Adaptee）
    /// </summary>
    public class ThreeHoleAdaptee
    {
        public void SpecificRequest()
        {
            Console.WriteLine("我是3个孔的插头也可以使用了");
        }
    }

    /// <summary>
    /// 适配器类，TwoHole这个对象写成接口或者抽象类更好，面向接口编程嘛
    /// </summary>
    public class ThreeToTwoAdapter : TwoHoleTarget
    {
        // 引用两个孔插头的实例,从而将客户端与TwoHole联系起来
        private ThreeHoleAdaptee threeHoleAdaptee = new ThreeHoleAdaptee();
        //这里可以继续增加适配的对象。。

        /// <summary>
        /// 实现2个孔插头接口方法
        /// </summary>
        public override void Request()
        {
            //可以做具体的转换工作
            threeHoleAdaptee.SpecificRequest();
            //可以做具体的转换工作
        }
    }
}
```
   2、类的适配器模式实现
``` c#   
namespace 设计模式之适配器模式
{
    /// <summary>
    /// 这里手机充电器为例，我们的家的插座是两相电的，但是手机的插座接头是三相电的
    /// </summary>
    class Client
    {
        static void Main(string[] args)
        {
            //好了，现在可以充电了
            ITwoHoleTarget change = new ThreeToTwoAdapter();
            change.Request();
            Console.ReadLine();
        }
    }
 
    /// <summary>
    /// 我家只有2个孔的插座，也就是适配器模式中的目标角色（Target），这里只能是接口，也是类适配器的限制
    /// </summary>
    public interface ITwoHoleTarget
    {
        void Request();
    }
 
    /// <summary>
    /// 3个孔的插头，源角色——需要适配的类（Adaptee）
    /// </summary>
    public abstract class ThreeHoleAdaptee
    {
        public void SpecificRequest()
        {
            Console.WriteLine("我是三个孔的插头");
        }
    }
 
    /// <summary>
    /// 适配器类，接口要放在类的后面，在此无法适配更多的对象，这是类适配器的不足
    /// </summary>
    public class ThreeToTwoAdapter:ThreeHoleAdaptee,ITwoHoleTarget
    {
        /// <summary>
        /// 实现2个孔插头接口方法
        /// </summary>
        public void Request()
        {
            // 调用3个孔插头方法
            this.SpecificRequest();
        }
    }
}
```
   代码都很简答，谁都可以看得懂，也有详细的备注。

## 三、适配器模式的实现要点：
    
 1、Adapter模式主要应用于“希望复用一些现存的类，但是接口又与复用环境要求不一致的情况”，在遗留代码复用、类库迁移等方面非常有用。
 
 2、GoF23定义了两种Adapter模式的实现结构：对象适配器和类适配器。类适配器采用“多继承”的实现方式，在C#语言中，如果被适配角色是类，Target的实现只能是接口，因为C#语言只支持接口的多继承的特性。在C#语言中类适配器也很难支持适配多个对象的情况，同时也会带来了不良的高耦合和违反类的职责单一的原则，所以一般不推荐使用。对象适配器采用“对象组合”的方式，更符合松耦合精神，对适配的对象也没限制，可以一个，也可以多个，但是，使得重定义Adaptee的行为较困难，这就需要生成Adaptee的子类并且使得Adapter引用这个子类而不是引用Adaptee本身。Adapter模式可以实现的非常灵活，不必拘泥于GoF23中定义的两种结构。例如，完全可以将Adapter模式中的“现存对象”作为新的接口方法参数，来达到适配的目的。
 
 3、Adapter模式本身要求我们尽可能地使用“面向接口的编程”风格，这样才能在后期很方便地适配。

      适配器模式用来解决现有对象与客户端期待接口不一致的问题，下面详细总结下适配器两种形式的优缺点。

     1】、类的适配器模式：

         优点：

               （1）、可以在不修改原有代码的基础上来复用现有类，很好地符合 “开闭原则”

               （2）、可以重新定义Adaptee(被适配的类)的部分行为，因为在类适配器模式中，Adapter是Adaptee的子类

               （3）、仅仅引入一个对象，并不需要额外的字段来引用Adaptee实例（这个即是优点也是缺点）。

         缺点：

               （1）、用一个具体的Adapter类对Adaptee和Target进行匹配，当如果想要匹配一个类以及所有它的子类时，类的适配器模式就不能胜任了。因为类的适配器模式中没有引入Adaptee的实例，光调用this.SpecificRequest方法并不能去调用它对应子类的SpecificRequest方法。

               （2）、采用了 “多继承”的实现方式，带来了不良的高耦合。

        2】、对象的适配器模式

             优点：

                  （1）、可以在不修改原有代码的基础上来复用现有类，很好地符合 “开闭原则”（这点是两种实现方式都具有的）

                  （2）、采用 “对象组合”的方式，更符合松耦合。

            缺点：

                  （1）、使得重定义Adaptee的行为较困难，这就需要生成Adaptee的子类并且使得Adapter引用这个子类而不是引用Adaptee本身。

     3】、适配器模式使用的场景：

              （1）、系统需要复用现有类，而该类的接口不符合系统的需求

              （2）、想要建立一个可重复使用的类，用于与一些彼此之间没有太大关联的一些类，包括一些可能在将来引进的类一起工作。

              （3）、对于对象适配器模式，在设计里需要改变多个已有子类的接口，如果使用类的适配器模式，就要针对每一个子类做一个适配器，而这不太实际。

## 四、.NET 中适配器模式的实现

    说道适配器模式在Net中的实现就很多了，比如：System.IO里面的很多类都有适配器的影子，当我们操作文件的时候，其实里面调用了COM的接口实现。以下两点也是适配器使用的案例：
1.在.NET中复用COM对象：

　　  COM对象不符合.NET对象的接口，使用tlbimp.exe来创建一个Runtime Callable Wrapper（RCW）以使其符合.NET对象的接口，COM Interop就好像是COM和.NET之间的一座桥梁。

2..NET数据访问类（Adapter变体）：

　　 各种数据库并没有提供DataSet接口，使用DbDataAdapter可以将任何个数据库访问/存取适配到一个DataSet对象上，DbDataAdapter在数据库和DataSet之间做了很好的适配。当然还有SqlDataAdapter类型了，针对微软SqlServer类型的数据库在和DataSet之间进行适配。

## 五、总结

     今天的文章就写到这里了，在结束今天写作之前，有一句话还是要说的，虽然以前说过。每种设计模式都有自己的适用场景，它是为了解决一类问题，没有所谓的缺点，没有一种设计模式可以解决所有情况的。我们使用设计模式的态度是通过不断地重构来使用模式，不要一上来就使用设计模式，为了模式而模式。如果软件没有需求的变化，我们不使用模式都没有问题。遇到问题，我们就按着常规来写，有了需求变化，然后我们去抽象，了解使用的场景，然后在选择合适的设计模式。