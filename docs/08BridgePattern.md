
# C#设计模式之七桥接模式（Bridge Pattern）【结构型】

[原帖](https://www.cnblogs.com/PatrickLiu/p/7699301.html)
## 一、引言

   今天我们要讲【结构型】设计模式的第二个模式，该模式是【桥接模式】，也有叫【桥模式】的，英文名称：Bridge Pattern。大家第一次看到这个名称会想到什么呢？我第一次看到这个模式根据名称猜肯定是连接什么东西的。因为桥在我们现实生活中经常是连接着A地和B地，再往后来发展，桥引申为一种纽带，比如：丝绸之路是连接亚洲和欧洲的桥梁。有了桥，我们出行方便了，从一个地方到另一个地方在有桥的情况更方便了（此处不许抬杠，当然是需要桥的情况）。桥是针对桥的使用环境来说的，解决了跨越和衔接的问题。在设计模式中的【桥模式】也有类似的概念，是连接了两个不同维度的东西，而且这两个维度又有强烈的变化，什么叫强烈呢，经常变化，什么是经常呢？哈哈，自己理解吧。

## 二、桥接模式的详细介绍

### 2.1、动机（Motivate）

   在很多游戏场景中，会有这样的情况：【装备】本身会有的自己固有的逻辑，比如枪支，会有型号的问题，同时现在很多的游戏又在不同的介质平台上运行和使用，这样就使得游戏的【装备】具有了两个变化的维度——一个变化的维度为“平台的变化”，另一个变化的维度为“型号的变化”。如果我们要写代码实现这款游戏，难道我们针对每种平台都实现一套独立的【装备】吗？复用在哪里？如何应对这种“多维度的变化”？如何利用面向对象技术来使得【装备】可以轻松地沿着“平台”和“型号”两个方向变化，而不引入额外的复杂度？

### 2.2、意图（Intent）

   将抽象部分与实现部分分离，使它们都可以独立地变化。                                                                  --《设计模式》Gof

   桥模式不能只是认为是抽象和实现的分离，它其实并不仅限于此。其实两个都是抽象的部分，更确切的理解，应该是将一个事物中多个维度的变化分离。

### 2.3、结构图（Structure）

       

### 2.4、模式的组成

      桥接模式的结构包括Abstraction、RefinedAbstraction、Implementor、ConcreteImplementorA和ConcreteImplementorB五个部分，其中：

       （1）、抽象化角色(Abstraction)：抽象化给出的定义，并保存一个对实现化对象（Implementor）的引用。

       （2）、修正抽象化角色(Refined Abstraction)：扩展抽象化角色，改变和修正父类对抽象化的定义。

       （3）、实现化角色(Implementor)：这个角色给出实现化角色的接口，但不给出具体的实现。必须指出的是，这个接口不一定和抽象化角色的接口定义相同，实际上，这两个接口可以非常不一样。实现化角色应当只给出底层操作，而抽象化角色应当只给出基于底层操作的更高一层的操作。

       （4）、具体实现化角色(Concrete Implementor)：这个角色给出实现化角色接口的具体实现。

　　在桥接模式中，两个类Abstraction和Implementor分别定义了抽象与行为类型的接口，通过调用两接口的子类实现抽象与行为的动态组合。

### 2.5 、桥接模式的具体代码实现

   今天我们就以数据库为例来写该模式的实现。每种数据库都有自己的版本，但是每种数据库在不同的平台上实现又是不一样的。比如：微软的SqlServer数据库，该数据库它有2000版本、2005版本、2006版本、2008版本，后面还会有更新的版本。并且这些版本都是运行在Windows操作系统下的，如果要提供Lunix操作系统下的SqlServer怎么办呢？如果又要提供IOS操作系统下的SqlServer数据库该怎么办呢？这个情况就可以使用桥接模式，也就是Brige模式。我们就来看看具体的实现吧！ 


``` c#
namespace 桥接模式的实现
{
    /// <summary>
    /// 该抽象类就是抽象接口的定义，该类型就相当于是Abstraction类型
    /// </summary>
    public abstract class Database
    {
        //通过组合方式引用平台接口，此处就是桥梁，该类型相当于Implementor类型
        protected PlatformImplementor _implementor;

        //通过构造器注入，初始化平台实现
        protected Database(PlatformImplementor implementor)
        {
            this._implementor = implementor;
        }

        //创建数据库--该操作相当于Abstraction类型的Operation方法
        public abstract void Create();
    }

    /// <summary>
    /// 该抽象类就是实现接口的定义，该类型就相当于是Implementor类型
    /// </summary>
    public abstract class PlatformImplementor
    {
        //该方法就相当于Implementor类型的OperationImpl方法
        public abstract void Process();
    }

    /// <summary>
    /// SqlServer2000版本的数据库，相当于RefinedAbstraction类型
    /// </summary>
    public class SqlServer2000 : Database
    {
        //构造函数初始化
        public SqlServer2000(PlatformImplementor implementor) : base(implementor) { }

        public override void Create()
        {
            this._implementor.Process();
        }
    }

    /// <summary>
    /// SqlServer2005版本的数据库，相当于RefinedAbstraction类型
    /// </summary>
    public class SqlServer2005 : Database
    {
        //构造函数初始化
        public SqlServer2005(PlatformImplementor implementor) : base(implementor) { }

        public override void Create()
        {
            this._implementor.Process();
        }
    }

    /// <summary>
    /// SqlServer2000版本的数据库针对Unix操作系统具体的实现，相当于ConcreteImplementorA类型
    /// </summary>
    public class SqlServer2000UnixImplementor : PlatformImplementor
    {
        public override void Process()
        {
            Console.WriteLine("SqlServer2000针对Unix的具体实现");
        }
    }

    /// <summary>
    /// SqlServer2005版本的数据库针对Unix操作系统的具体实现，相当于ConcreteImplementorB类型
    /// </summary>
    public sealed class SqlServer2005UnixImplementor : PlatformImplementor
    {
        public override void Process()
        {
            Console.WriteLine("SqlServer2005针对Unix的具体实现");
        }
    }

    public class Program
    {
        static void Main()
        {
            PlatformImplementor SqlServer2000UnixImp = new SqlServer2000UnixImplementor();
            //还可以针对不同平台进行扩展，也就是子类化，这个是独立变化的

            Database SqlServer2000Unix = new SqlServer2000(SqlServer2000UnixImp);
            //数据库版本也可以进行扩展和升级，也进行独立的变化。

            //以上就是两个维度的变化。

         //就可以针对Unix执行操作了
         SqlServer2000Unix.Create();
        }
    }
}
```
    代码都很简单，也有详细的备注，就不多说了。

## 三、桥接模式的实现要点：
    
    1．Bridge模式使用“对象间的组合关系”解耦了抽象和实现之间固有的绑定关系，使得抽象和实现可以沿着各自的维度来变化。

    2．所谓抽象和实现沿着各自维度的变化，即“子类化”它们，得到各个子类之后，便可以任意组合它们，从而获得不同平台上的不同型号。

    3．Bridge模式有时候类似于多继承方案，但是多继承方案往往违背了类的单一职责原则（即一个类只有一个变化的原因），复用性比较差。Bridge模式是比多继承方案更好的解决方法。

    4．Bridge模式的应用一般在“两个非常强的变化维度”，有时候即使有两个变化的维度，但是某个方向的变化维度并不剧烈——换言之两个变化不会导致纵横交错的结果，并不一定要使用Bridge模式。

    3.1】、桥接模式的优点：

          （1）、把抽象接口与其实现解耦。

          （2）、抽象和实现可以独立扩展，不会影响到对方。

          （3）、实现细节对客户透明，对用于隐藏了具体实现细节。

   3.2】、桥接模式的缺点：

         （1）、增加了系统的复杂度

   3.3】、桥接模式的使用场景：

         （1）、如果一个系统需要在构件的抽象化角色和具体化角色之间添加更多的灵活性，避免在两个层次之间建立静态的联系。

         （2）、设计要求实现化角色的任何改变不应当影响客户端，或者实现化角色的改变对客户端是完全透明的。

         （3）、需要跨越多个平台的图形和窗口系统上。

         （4）、 一个类存在两个独立变化的维度，且两个维度都需要进行扩展。

## 四、.NET 中桥接模式的实现

    学习中。。。，如果谁有好的代码分享，也可以贴出来。

## 五、总结

  今天的文章就写到这里了，现在小结一下。桥接模式它是连接客户端代码和具体实现代码的一座桥梁，同时它也隔离了实现代码的改变对客户代码的影响。在【意图】中所说的抽象和实现，这两个部分其实都是高度抽象的，前面“抽象”是指定义的针对客户端的接口，客户端其实使用的是Abstract类型或者是RefinedAbstract类型，这两个类型只是接口，具体的实现委托给了Implementor类型了，Abstract类型子类化的扩展也演变成Implementor子类化的变化。我个人的理解，Abstract类型和其子类型在客户端代码和真正实现的代码之间起到了桥梁的作用，隔离了Implementor实现代码的变化，让客户端更稳定，所以【意图】才说是讲抽象部分和它的实现部分隔离。大家好好理解一下吧，刚开始有点绕。