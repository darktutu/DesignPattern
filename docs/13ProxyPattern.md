C#设计模式之十二代理模式（Proxy Pattern）【结构型】
一、引言

   今天我们要讲【结构型】设计模式的第七个模式，也是“结构型”设计模式中的最后一个模式，该模式是【代理模式】，英文名称是：Proxy Pattern。还是老套路，先从名字上来看看。“代理”可以理解为“代替”，代替“主人”做一些事情，为什么需要“代理”，是因为某些原因（比如：安全方面的原因），不想让“主人”直接面对这些繁琐、复杂的问题，但是这些事情是经“主人”同意或者授意的，如同“主人”亲自完成的一样。这个模式很简单，生活中的例子也很多。举例说明，歌星、影星的经纪人就是现实生活中一个代理模式的很好例子，还有操作系统中的防火墙，也是代理的例子，要访问系统，先过防火墙这关，否则免谈。还有很多了，就不一一列举了，大家在生活中慢慢的体会吧。

二、代理模式的详细介绍

2.1、动机（Motivate）

   在面向对象系统中，有些对象由于某种原因（比如对象创建的开销很大，或者某些操作需要安全控制，或者需要进程外的访问等），直接访问会给使用者、或者系统结构带来很多麻烦。如何在不失去透明操作对象的同时来管理/控制这些对象特有的复杂性？增加一层间接层是软件开发中常见的解决方式。

2.2、意图（Intent）

   为其他对象提供一种代理以控制对这个对象的访问。　　　　　　                                ——《设计模式》GoF

2.3、结构图（Structure）

      

2.4、模式的组成
    
    代理模式所涉及的角色有三个：

    （1）、抽象主题角色（Subject）：声明了真实主题和代理主题的公共接口，这样一来在使用真实主题的任何地方都可以使用代理主题。

    （2）、代理主题角色（Proxy）：代理主题角色内部含有对真实主题的引用，从而可以操作真实主题对象；代理主题角色负责在需要的时候创建真实主题对象；代理角色通常在将客户端调用传递到真实主题之前或之后，都要执行一些其他的操作，而不是单纯地将调用传递给真实主题对象。

    （3）、真实主题角色（RealSubject）：定义了代理角色所代表的真实对象。

    附：在WCF或者WebService的开发过程中，我们在客户端添加服务引用的时候，在客户程序中会添加一些额外的类，在客户端生成的类扮演着代理主题角色，我们客户端也是直接调用这些代理角色来访问远程服务提供的操作。这个是远程代理的一个典型例子。

2.5、代理模式的分类：

    代理模式按照使用目的可以分为以下几种：

    （1）、远程（Remote）代理：为一个位于不同的地址空间的对象提供一个局域代表对象。这个不同的地址空间可以是本电脑中，也可以在另一台电脑中。最典型的例子就是——客户端调用Web服务或WCF服务。

    （2）、虚拟（Virtual）代理：根据需要创建一个资源消耗较大的对象，使得对象只在需要时才会被真正创建。

    （3）、Copy-on-Write代理：虚拟代理的一种，把复制（或者叫克隆）拖延到只有在客户端需要时，才真正采取行动。

    （4）、保护（Protect or Access）代理：控制一个对象的访问，可以给不同的用户提供不同级别的使用权限。

    （5）、防火墙（Firewall）代理：保护目标不让恶意用户接近。

    （6）、智能引用（Smart Reference）代理：当一个对象被引用时，提供一些额外的操作，比如将对此对象调用的次数记录下来等。

    （7）、Cache代理：为某一个目标操作的结果提供临时的存储空间，以便多个客户端可以这些结果。

   在上面所有种类的代理模式中，虚拟代理、远程代理、智能引用代理和保护代理较为常见的代理模式。

2.6、代理模式的具体实现

    说起“代理模式”，其实很容易，现实生活中的例子也很多。明星的经纪人，国家的发言人都是代理的好例子。我们就用明星经纪人这个事情来介绍“代理模式”的实现吧。

``` c#
namespace 代理模式的实现
{
    /// <summary>
    /// 大明星都有钱，有钱了，就可以请自己的经纪人了，有了经纪人，很多事情就不用自己亲力亲为。弄点绯闻，炒作一下子通过经纪人就可以名正言顺的的操作了，万一搞不好，自己也可以否认。
    /// </summary>
    class Client
    {
        static void Main(string[] args)
        {
            //近期，Fan姓明星关注度有点下降，来点炒作
            AgentAbstract fan = new AgentPerson();
            fan.Speculation("偶尔出来现现身，为炒作造势");

            Console.WriteLine();

            //过了段时间，又不行了，再炒作一次
            fan.Speculation("这段时间不火了，开始离婚炒作");


            Console.Read();
        }
    }


    //该类型就是抽象Subject角色，定义代理角色和真实主体角色共有的接口方法
    public abstract class AgentAbstract
    {

        //该方法执行具体的炒作---该方法相当于抽象Subject的Request方法
        public virtual void Speculation(string thing)
        {
            Console.WriteLine(thing);
        }
    }

    //该类型是Fan姓明星，有钱有势，想炒什么炒什么---相当于具体的RealSubject角色
    public sealed class FanStar : AgentAbstract
    {
        //有钱有势，有背景啊
        public FanStar() { }

        //要有名气，定期要炒作---就是RealSubject类型的Request方法
        public override void Speculation(string thing)
        {
            Console.WriteLine(thing);
        }
    }

    //该类型是代理类型----相当于具体的Proxy角色
    public sealed class AgentPerson : AgentAbstract
    {
        //这是背后的老板，
        private FanStar boss;

        //老板在后面发号施令
        public AgentPerson()
        {
            boss = new FanStar();
        }

        //炒作的方法，执行具体的炒作---就是Proxy类型的Request方法
        public override void Speculation(string thing)
        {
            Console.WriteLine("前期弄点绯闻，拍点野照");
            base.Speculation(thing);
            Console.WriteLine("然后开发布会，伤心哭泣，继续捞钱");
        }
    }
}
```

   这个模式很简单，就话不多说了。

三、代理模式的实现要点：
    
    “增加一层间接层”是软件系统中对许多复杂问题的一种常见解决方法。在面向对象系统中，直接使用某些对象会来带很多问题，作为间接层的Proxy对象便是解决这一问题的常用手段。具体Proxy设计模式的实现方法、实现粒度都相差很大，有些可能对单个对象做细粒度的控制，如copy-on-write技术，有些可能对组件模块提供抽象代理层，在架构层次对对象做Proxy。

　　Proxy并不一定要求保持接口的一致性，只要能够实现间接控制，有时候损及一些透明性是可以接受的。

    3.1】、代理模式的优点：

          （1）、代理模式能够将调用用于真正被调用的对象隔离，在一定程度上降低了系统的耦合度；

          （2）、代理对象在客户端和目标对象之间起到一个中介的作用，这样可以起到对目标对象的保护。代理对象可以在对目标对象发出请求之前进行一个额外的操作，例如权限检查等。

           不同类型的代理模式也具有独特的优点，例如：

         （1）、远程代理为位于两个不同地址空间对象的访问提供了一种实现机制，可以将一些消耗资源较多的对象和操作移至性能更好的计算机上，提高系统的整体运行效率。

         （2）、虚拟代理通过一个消耗资源较少的对象来代表一个消耗资源较多的对象，可以在一定程度上节省系统的运行开销。

         （3）、缓冲代理为某一个操作的结果提供临时的缓存存储空间，以便在后续使用中能够共享这些结果，优化系统性能，缩短执行时间。

         （4）、保护代理可以控制对一个对象的访问权限，为不同用户提供不同级别的使用权限。

    3.2】、代理模式的缺点：

          （1）、由于在客户端和真实主题之间增加了一个代理对象，所以会造成请求的处理速度变慢

          （2）、实现代理类也需要额外的工作，从而增加了系统的实现复杂度。

    3.3】、代理模式的使用场景：

           代理模式的类型较多，不同类型的代理模式有不同的优缺点，它们应用于不同的场合：

            （1）、 当客户端对象需要访问远程主机中的对象时可以使用远程代理。

            （2）、当需要用一个消耗资源较少的对象来代表一个消耗资源较多的对象，从而降低系统开销、缩短运行时间时可以使用虚拟代理，例如一个对象需要很长时间才能完成加载时。

            （3）、当需要为某一个被频繁访问的操作结果提供一个临时存储空间，以供多个客户端共享访问这些结果时可以使用缓冲代理。通过使用缓冲代理，系统无须在客户端每一次访问时都重新执行操作，只需直接从临时缓冲区获取操作结果即可。

            （4）、 当需要控制对一个对象的访问，为不同用户提供不同级别的访问权限时可以使用保护代理。

            （5）、当需要为一个对象的访问（引用）提供一些额外的操作时可以使用智能引用代理。

四、.NET 中代理模式的实现

    代理模式在Net的FCL中的实现也不少，框架级别的有，类级别的也有。框架级别的有WCF,Remoting,他们都需要生成本地的代理，然后通过代理访问进程外或者机器外的对象。类级别的有StringBuilder类型，StringBuilder其实就是一种代理，我们本意是想访问字符串的，StringBuilder就是一种可变字符串的代理，而且StringBuilder也没有和String保持接口的一致性。

五、总结

      到今天为止，我们设计模式的三个部分讲完两个部分了，第一个部分是“创建型”的设计模式，解决对象创建的问题，对对象创建的解耦。第二部分就是“结构型”的设计模式，所谓结构型设计模式模式，顾名思义讨论的是类和对象的结构 ，主要用来处理类或对象的组合。它包括两种类型，一是类结构型模式，指的是采用继承机制来组合接口或实现；二是对象结构型模式，指的是通过组合对象的方式来实现新的功能。它包括适配器模式、桥接模式、装饰者模式、组合模式、外观模式、享元模式和代理模式。设计模式到现在也说了不少了，但是看起来很多模式都很类似，之间好像很容转换，有时候条件不同了，的确模式也可以转换，但是不能肆意的转换。为了避免思想的混乱，我们把“结构型”这个几个设计模式，再总结一次，把握核心，理解使用场景。

    适配器模式注重转换接口，将不吻合的接口适配对接

    桥接模式注重分离接口与其实现，支持多维度变化

    组合模式注重统一接口，将“一对多”的关系转化为“一对一”的关系

    装饰者模式注重稳定接口，在此前提下为对象扩展功能

    外观模式注重简化接口，简化组件系统与外部客户程序的依赖关系

    享元模式注重保留接口，在内部使用共享技术对对象存储进行优化

    代理模式注重假借接口，增加间接层来实现灵活控制


  从下篇文章就开始写“行为型”设计模式，今天就到此结束了。