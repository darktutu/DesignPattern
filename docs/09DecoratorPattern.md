# C#设计模式之八装饰模式（Decorator Pattern）【结构型】

[链接](https://www.cnblogs.com/PatrickLiu/p/7723225.html)

把评论也粘贴过来了，下面有对模式的例子的讨论。很有用
## 一、引言

   今天我们要讲【结构型】设计模式的第三个模式，该模式是【装饰模式】，英文名称：Decorator Pattern。我第一次看到这个名称想到的是另外一个词语“装修”，我就说说我对“装修”的理解吧，大家一定要看清楚，是“装修”,不是“装饰”。我们长大了，就要结婚，要结婚就涉及到要买房子，买的精装修或者简单装修就可以住的，暂时不谈。我们就谈谈我们购买的是毛坯房。如果我想要房子的内饰是大理石风格的，我们只要在毛坯房的基础之上用大理石风格的材料装修就可以，我们当然不可能为了要一个装修风格，就把刚刚盖好的房子拆了在重新来过。房子装修好了，我们就住了进来，很开心。过了段时间，我们发现我们的房子在冬季比较冷，于是我就想给我们的房子增加保暖的功能，装修好的房子我们可以继续居住，我们只是在房子外面加一层保护层就可以了。又过了一段时间，总是有陌生人光顾，所以我们想让房子更安全，于是我们在外墙和房顶加装安全摄像头，同时门窗也增加安全系统。随着时间的流逝，我们可能会根据我们的需求增加相应的功能，期间，我们的房子可以正常使用，加上什么设施就有了相应的功能。从这一方面来讲，“装修”和“装饰”有类似的概念，接下来就让我们看看装饰模式具体是什么吧！

## 二、装饰模式的详细介绍

### 2.1、动机（Motivate）

   在房子装修的过程中，各种功能可以相互组合，来增加房子的功用。类似的，如果我们在软件系统中，要给某个类型或者对象增加功能，如果使用“继承”的方案来写代码，就会出现子类暴涨的情况。比如：IMarbleStyle是大理石风格的一个功能，IKeepWarm是保温的一个接口定义，IHouseSecurity是房子安全的一个接口，就三个接口来说，House是我们房子，我们的房子要什么功能就实现什么接口，如果房子要的是复合功能，接口不同的组合就有不同的结果，这样就导致我们子类膨胀严重，如果需要在增加功能，子类会成指数增长。这个问题的根源在于我们“过度地使用了继承来扩展对象的功能”，由于继承为类型引入的静态特质（所谓静态特质，就是说如果想要某种功能，我们必须在编译的时候就要定义这个类，这也是强类型语言的特点。静态，就是指在编译的时候要确定的东西；动态，是指运行时确定的东西），使得这种扩展方式缺乏灵活性；并且随着子类的增多（扩展功能的增多），各种子类的组合（扩展功能的组合）会导致更多子类的膨胀（多继承）。如何使“对象功能的扩展”能够根据需要来动态（即运行时）地实现？同时避免“扩展功能的增多”带来的子类膨胀问题？从而使得任何“功能扩展变化”所导致的影响降为最低？

### 2.2、意图（Intent）

   动态地给一个对象增加一些额外的职责。就增加功能而言，Decorator模式比生成子类更为灵活。　　       ——  《设计模式》GoF

### 2.3、结构图（Structure）

       

### 2.4、模式的组成

    在装饰模式中的各个角色有：

　　（1）、抽象构件角色（Component）：给出一个抽象接口，以规范准备接收附加责任的对象。

　　（2）、具体构件角色（Concrete Component）：定义一个将要接收附加责任的类。

　　（3）、装饰角色（Decorator）：持有一个构件（Component）对象的实例，并实现一个与抽象构件接口一致的接口。

　　（4）、具体装饰角色（Concrete Decorator）：负责给构件对象添加上附加的责任。

### 2.5 、装饰模式的具体代码实现

   刚开始一看这个“装饰模式”是有点不太好理解，既然这个模式是面向对象的设计模式，那在现实生活中一定有事例和其对应，其实这种例子也不少，大家好好的挖掘吧，也可以提高我们对面向对象的理解。我继续拿盖房子来说事吧。
 

``` c#
namespace 装饰模式的实现
{
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
    public abstract class DecorationStrategy:House //关键点之二，体现关系为Is-a，有这这个关系，装饰的类也可以继续装饰了
    {
       //通过组合方式引用Decorator类型，该类型实施具体功能的增加
        //这是关键点之一，包含关系，体现为Has-a
        protected House _house;

        //通过构造器注入，初始化平台实现
        protected DecorationStrategy(House house)
        {
           this._house=house;
        }

       //该方法就相当于Decorator类型的Operation方法
       public override void Renovation()
       {
           if(this._house!=null)
            {
                this._house.Renovation();
            }
        }
    }
 
    /// <summary>
    /// PatrickLiu的房子，我要按我的要求做房子，相当于ConcreteComponent类型，这就是我们具体的饺子馅，我个人比较喜欢韭菜馅
    /// </summary>
    public sealed class PatrickLiuHouse:House
    {
        public override void Renovation()
        {
            Console.WriteLine("装修PatrickLiu的房子");
        }
    }
 

   /// <summary>
    /// 具有安全功能的设备，可以提供监视和报警功能，相当于ConcreteDecoratorA类型
    /// </summary>
    public sealed class HouseSecurityDecorator:DecorationStrategy
    {
        public  HouseSecurityDecorator(House house):base(house){}

        public override void Renovation()
        {
            base.Renovation();
            Console.WriteLine("增加安全系统");
        }
    }
 
    /// <summary>
    /// 具有保温接口的材料，提供保温功能，相当于ConcreteDecoratorB类型
    /// </summary>
    public sealed class KeepWarmDecorator:DecorationStrategy
    {
        public  KeepWarmDecorator(House house):base(house){}

        public override void Renovation()
        {
            base.Renovation();
            Console.WriteLine("增加保温的功能");
        }
    }

   public class Program
   {
      static void Main()
      {
         //这就是我们的饺子馅，需要装饰的房子
         House myselfHouse=new PatrickLiuHouse();

         DecorationStrategy securityHouse=new HouseSecurityDecorator(myselfHouse);
         securityHouse.Renovation();
         //房子就有了安全系统了

         //如果我既要安全系统又要保暖呢，继续装饰就行
         DecorationStrategy securityAndWarmHouse=new HouseSecurityDecorator(securityHouse);
         securityAndWarmHouse.Renovation();
      }
   }
}
```
    写了很多备注，大家好好体会一下，里面有两个关键点，仔细把握。

## 三、装饰模式的实现要点：
    
    1、通过采用组合、而非继承的手法，Decorator模式实现了在运行时动态地扩展对象功能的能力，而且可以根据需要扩展多个功能。避免了单独使用继承带来的“灵活性差”和“多子类衍生问题”。

    2、Component类在Decorator模式中充当抽象接口的角色，不应该去实现具体的行为。而且Decorator类对于Component类应该透明——换言之Component类无需知道Decorator类，Decorator类是从外部来扩展Component类的功能。

    3、Decorator类在接口上表现为is-a Component的继承关系，即Decorator类继承了Component类所具有的接口。但在实现上又表现为has-a Component的组合关系，即Decorator类又使用了另外一个Component类。我们可以使用一个或者多个Decorator对象来“装饰”一个Component对象，且装饰后的对象仍然是一个Component对象。

    4、Decorator模式并非解决“多子类衍生的多继承”问题，Decorator模式应用的要点在于解决“主体类在多个方向上的扩展功能”——是为“装饰”的含义。

     3.1】、装饰模式的优点：

             （1）、把抽象接口与其实现解耦。

             （2）、抽象和实现可以独立扩展，不会影响到对方。

             （3）、实现细节对客户透明，对用于隐藏了具体实现细节。

      3.2】、装饰模式的缺点：

           （1）、增加了系统的复杂度

      3.3】、在以下情况下应当使用桥接模式：

          （1）、如果一个系统需要在构件的抽象化角色和具体化角色之间添加更多的灵活性，避免在两个层次之间建立静态的联系。

          （2）、设计要求实现化角色的任何改变不应当影响客户端，或者实现化角色的改变对客户端是完全透明的。

          （3）、需要跨越多个平台的图形和窗口系统上。

          （4）、一个类存在两个独立变化的维度，且两个维度都需要进行扩展。

## 四、.NET 中装饰模式的实现

    在Net框架中，有一个类型很明显的使用了“装饰模式”，这个类型就是Stream。Stream类型是一个抽象接口，它在System.IO命名空间里面，它其实就是Component。FileStream、NetworkStream、MemoryStream都是实体类ConcreteComponent。右边的BufferedStream、CryptoStream是装饰对象，它们都是继承了Stream接口的。

   如图：
       

    Stream就相当于Component，定义装饰的对象，FileStream就是要装饰的对象，BufferedStream是装饰对象。我们看看BufferedStream的定义，部分定义了。
``` c#
 public sealed class BufferedStream : Stream
 {
     private const int _DefaultBufferSize = 4096;
 
     private Stream _stream;
    
 }
 ```
 结构很简单，对比结构图看吧，没什么可说的了。

## 五、总结

     今天的文章就写到这里了，总结一下我对这个模式的看法，这个模式有点像包饺子，ConcreteComponent其实是饺子馅，Decorator就像饺子皮一样，包什么皮就有什么的样子，皮和皮也可以嵌套，当然我们生活中的饺子只是包一层。其实手机也是一个装饰模式使用的好例子，以前我们的手机只是接打电话，然后可以发短信和彩信，我在装饰一个就可以拍照了。我们现在的手机功能很丰富，其结果也类似装饰的结果。随着社会的进步，技术发展，模块化的手机也出现了，其设计原理也和“装饰模式”就更接近了。不光手机，还有我们身边其他很多家用电器也有类似的发展经历，我们努力发现生活中的真理吧，然后再在软件环境中慢慢体会吧。


#6楼
2018-01-16 15:24 | 鲁广广
1
2
3
4
5
6
7
8
9
10
43    /// <summary>
44     /// 具有安全功能的设备，可以提供监视和报警功能，相当于ConcreteDecoratorA类型
45     /// </summary>
46     public class HouseSecurityDecorator:DecorationStrategy
47     {
48         public override void Process()
49         {
50             Console.WriteLine("增加安全系统");
51         }
52     }


好的，第46行，这里继承了DecorationStrategy类，但是DecorationStrategy继承了House类，所以 HouseSecurityDecorator少了抽象类House的抽象方法


#7楼
2018-01-17 10:06 | 雨飞
通过具体装饰子类来继承装饰父类，并且通过构造方法 传递房屋类，来对房屋进行装饰（赋予更多的功能）

   回复 引用
#8楼 [楼主]
2018-01-17 12:34 | 可均可可
@ 雨飞
继承是让子类可以继续嵌套，最重要的是，包含一个实例，完成的动作委托给这个对象来做，可以实现层层嵌套的效果，继承和包含起到了不同的作用。

#9楼
2018-09-10 09:05 | --天天向上--
House myselfHouse=new PatrickLiuHouse();

DecorationStrategy securityHouse=new HouseSecurityDecorator(myselfHouse);
securityHouse.Renovation();
//房子就有了安全系统了

//如果我既要安全系统又要保暖呢，继续装饰就行
DecorationStrategy securityAndWarmHouse=new HouseSecurityDecorator(securityHouse);
securityAndWarmHouse.Renovation();

我的疑惑：在这种模式下，第一次装饰了安全系统，第二次在装饰保暖的时候，又重新装饰了一次安全系统。那么在多次装饰的时候，都会对前面的装饰重新完成一次。似乎太浪费了吧

#10楼 [楼主]
2018-09-10 09:36 | 可均可可
@ --天天向上--
不会的，你可以单步调试一下，装饰模式就是这样实现的，而且不会出现你说的那种浪费。

#11楼
2018-09-10 10:07 | --天天向上--
@ 可均可可
理解了，其实已经是不同的对象了。安全的房子、安全保暖的房子不是同一个对象了。那请教一下：如果是要求同一个对象的话，可以任意增加它的“装饰”，应该怎么做呢？

#12楼
2019-06-01 23:26 | BYRON_2015
@ 可均可可
引用
@--天天向上--
不会的，你可以单步调试一下，装饰模式就是这样实现的，而且不会出现你说的那种浪费。


我调试了一下，每次执行到base.Renovation();其实执行的是Console.WriteLine("装修PatrickLiu的房子");
每增加一个装饰器，就会执行一遍Console.WriteLine("装修PatrickLiu的房子");
所以，我把base.Renovation();注释掉，反而是我想要的结果。
同时，这里还有一个顺序的问题，就像流水作业，装饰器A执行完传给装饰器B，开始以为懂了，越细想反而有点犯糊涂了。

#13楼
2019-06-01 23:36 | BYRON_2015
@ --天天向上--
引用
@可均可可
理解了，其实已经是不同的对象了。安全的房子、安全保暖的房子不是同一个对象了。那请教一下：如果是要求同一个对象的话，可以任意增加它的“装饰”，应该怎么做呢？


我可能跟你开始一样的想法，大概明白了，作者的逻辑是3个不同的对象。我想的是对同一个对象分三步装饰。代码改成：
81 //这就是我们的饺子馅，需要装饰的房子
82 House myselfHouse=new PatrickLiuHouse();
83myselfHouse.Renovation();
84 DecorationStrategy securityHouse=new HouseSecurityDecorator(myselfHouse);
85 securityHouse.Renovation();
86 //房子就有了安全系统了
87
88 //如果我既要安全系统又要保暖呢，继续装饰就行
89 DecorationStrategy securityAndWarmHouse=new HouseSecurityDecorator(myselfHouse);
90 securityAndWarmHouse.Renovation();

同时，需要删除两个装饰器里面对基类方法的调用
58 base.Renovation();//移除掉，不然会重复执行基类的Renovation();

这样就变成了2个装饰器对myselfHouse进行装饰了。似乎更灵活。

#14楼
2019-06-28 15:55 | --天天向上--
@ Byron_2015
经过再次阅读这段楼主写的内容，我现在的理解是：
DecorationStrategy securityHouse=new HouseSecurityDecorator(myselfHouse);
securityHouse.Renovation();
这段内容会在 myselfHouse 现在的基础上，重新建一座新的有安全保护的房子。
“这个模式有点像包饺子，ConcreteComponent其实是饺子馅，Decorator就像饺子皮一样，包什么皮就有什么的样子，皮和皮也可以嵌套，当然我们生活中的饺子只是包一层。” 这个总结和代码实际表现的逻辑其实是不相符的。 按照代码的逻辑，当你包完3层饺子的时候，你已经有了3个饺子了：1个1层皮的，1个2层皮的，1个3层皮的。以前的理解（也是按照楼主的举例的文字描述【房子装饰】）是始终只应该有一个饺子。

#15楼
2019-06-28 16:35 | --天天向上--
@ Byron_2015
不要移除第58行 base.Renovation(); 这里有继承的多态特性。你移除第58行的话，你的房子都不存在，何来装饰？

#16楼
2019-12-17 15:02 | JAKE_CAIEE
我说说一下我的看法，如果有错的地方还请大家指正。装饰模式它的主要思想就是添加组件，就像上文的例子中一样，给房子添加各种各样的功能，但主体一定是房子本身，这一点是毋庸置疑的。接下来我们来分析房子和加进来的功能之间的异同，首先无论是什么，它们都是有house类的功能的，所以它们都是house的子类，这里的子类有可能是孙子类，因为安全功能就垮了两代。然后我们再看看他们的异，你让PatrickLiuHouse子类去继承安全子类的功能这显然是违背了装饰模式的目的，所以PatrickLiuHouse一定是继承最基本的父类的功能，就是上文的House,安全等一些附加的子类都有房子的功能，但是又有多余的功能，所以就要在有一个新的父类，新父类继承house类，安全等附加子类去继承新父类，这个模式的提出告诉我们有的时候父类有可能是传递父类，相对于附加子类就是这样的。

说到这里，我突然有一个疑问，如果按照楼主的main的写法的话，就会出现一个问题，就是是最新的附加功能类，就是问题的保暖类它拥有安全类和房子类，就这样就不行了，主次关系就颠倒了。我们原来希望的是房子拥有安全类和保暖类，并且他们之间的联系应该是很弱，当我不用的时候，就可以去掉它；按照楼主的主体传递的话，他们之间的耦合非常强，因为父类已经变成子类的成员了。最后我的观点还是应该是组件型的，主体永远都是房子，它的附加子类与房子的联系应该是弱的，这样可以随时把没有用的功能去掉 ，把子类中的 base.Renovation();去掉。
House myselfHouse = new PatrickLiuHouse();
myselfHouse.Renovation();
DecorationStrategy securityHouse = new HouseSecurityDecorator(myselfHouse);
securityHouse.Renovation();
//房子就有了安全系统了

//如果我既要安全系统又要保暖，继续装饰就行
DecorationStrategy securityAndWarmHouse = new KeepWarmDecorator(myselfHouse);
securityAndWarmHouse.Renovation();
