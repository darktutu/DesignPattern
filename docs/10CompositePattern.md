一、引言

      今天我们要讲【结构型】设计模式的第四个模式，该模式是【组合模式】，英文名称是：Composite Pattern。当我们谈到这个模式的时候，有一个物件和这个模式很像，也符合这个模式要表达的意思，那就是“俄罗斯套娃”。“俄罗斯套娃”就是大的瓷器娃娃里面装着一个小的瓷器娃娃，小的瓷器娃娃里面再装着更小的瓷器娃娃，直到最后一个不能再装更小的瓷器娃娃的那个瓷器娃娃为止（有点绕，下面我会配图，一看就明白）。在我们的操作系统中有文件夹的概念，文件夹可以包含文件夹，可以嵌套多层，最里面包含的是文件，这个概念和“俄罗斯套娃”很像。当然还有很多的例子，例如我们使用系统的时候，会使用到“系统菜单”，这个东西是树形结构。这些例子包含的这些东西或者说是对象，可以分为两类，一类是：容器对象，可以包含其他的子对象；另一类是：叶子对象，这类对象是不能在包含其他对象的对象了。在软件设计中，我们该怎么处理这种情况呢？是每类对象分别对待，还是提供一个统一的操作方式呢。组合模式给我们提供了一种解决此类问题的一个途径，接下来我们就好好的介绍一下“组合模式”吧。

       

二、组合模式的详细介绍

2.1、动机（Motivate）

     客户代码过多地依赖于对象容器（对象容器是对象的容器，细细评味）复杂的内部实现结构，对象容器内部实现结构（而非抽象接口）的变化将引起客户代码的频繁变化，带来了代码的维护性、扩展性等方面的弊端。如何将“客户代码与复杂的对象容器结构”解耦？如何让对象容器自己来实现自身的复杂结构，从而使得客户代码就像处理简单对象一样来处理复杂的对象容器？

2.2、意图（Intent）

     将对象组合成树形结构以表示“部分-整体”的层次结构。Composite使得用户对单个对象和组合对象的使用具有一致性。　　       ——  《设计模式》GoF

2.3、结构图（Structure）

      

2.4、模式的组成
    
    组合模式中涉及到三个角色：

    （1）、抽象构件角色（Component）：这是一个抽象角色，它给参加组合的对象定义出了公共的接口及默认行为，可以用来管理所有的子对象（在透明式的组合模式是这样的）。在安全式的组合模式里，构件角色并不定义出管理子对象的方法，这一定义由树枝结构对象给出。

    （2）、树叶构件角色（Leaf）：树叶对象是没有下级子对象的对象，定义出参加组合的原始对象的行为。（原始对象的行为可以理解为没有容器对象管理子对象的方法，或者 【原始对象行为】+【管理子对象的行为（Add，Remove等）】=面对客户代码的接口行为集合）

    （3）、树枝构件角色（Composite）：代表参加组合的有下级子对象的对象，树枝对象给出所有管理子对象的方法实现，如Add、Remove等。

   组合模式实现的最关键的地方是——简单对象和复合对象必须实现相同的接口。这就是组合模式能够将组合对象和简单对象进行一致处理的原因。

2.5、组合模式的具体代码实现

        组合模式有两种实现方式，一种是：透明式的组合模式，另外一种是：安全式的组合模式。在这里我就详细说一下何为“透明式”，何为“安全式”。所谓透明式是指“抽象构件角色”定义的接口行为集合包含两个部分，一部分是叶子对象本身所包含的行为（比如Operation），另外一部分是容器对象本身所包含的管理子对象的行为(Add,Remove)。这个抽象构件必须同时包含这两类对象所有的行为，客户端代码才会透明的使用，无论调用容器对象还是叶子对象，接口方法都是一样的，这就是透明，针对客户端代码的透明，但是也有他自己的问题，叶子对象不会包含自己的子对象，为什么要有Add,Remove等类似方法呢，调用叶子对象这样的方法可能（注意：我这里说的是可能，因为有些人会把这些方法实现为空，不做任何动作，当然也不会有异常抛出了，不要抬杠）会抛出异常，这样就不安全了，然后人们就提出了“安全式的组合模式”。所谓安全式是指“抽象构件角色”只定义叶子对象的方法，确切的说这个抽象构件只定义两类对象共有的行为，然后容器对象的方法定义在“树枝构件角色”上，这样叶子对象有叶子对象的方法，容器对象有容器对象的方法，这样责任很明确，当然调用肯定不会抛出异常了。大家可以根据自己的情况自行选择是实现为“透视式”还是“安全式”的，以下我们会针对这两种情况都有实现，具体实现如下：
``` c#
namespace 透明式的组合模式的实现
{
    /// <summary>
    /// 该抽象类就是文件夹抽象接口的定义，该类型就相当于是抽象构件Component类型
    /// </summary>
    public abstract class Folder
    {
        //增加文件夹或文件
        public abstract void Add(Folder folder);

        //删除文件夹或者文件
        public abstract void Remove(Folder folder);

        //打开文件或者文件夹--该操作相当于Component类型的Operation方法
        public abstract void Open();
    }

    /// <summary>
    /// 该Word文档类就是叶子构件的定义，该类型就相当于是Leaf类型，不能在包含子对象
    /// </summary>
    public sealed class Word : Folder
    {
        //增加文件夹或文件
        public override void Add(Folder folder)
        {
            throw new Exception("Word文档不具有该功能");
        }

        //删除文件夹或者文件
        public override void Remove(Folder folder)
        {
            throw new Exception("Word文档不具有该功能");
        }

        //打开文件--该操作相当于Component类型的Operation方法
        public override void Open()
        {
            Console.WriteLine("打开Word文档，开始进行编辑");
        }
    }

    /// <summary>
    /// SonFolder类型就是树枝构件，由于我们使用的是“透明式”，所以Add,Remove都是从Folder类型继承下来的
    /// </summary>
    public class SonFolder : Folder
    {
        //增加文件夹或文件
        public override void Add(Folder folder)
        {
            Console.WriteLine("文件或者文件夹已经增加成功");
        }

        //删除文件夹或者文件
        public override void Remove(Folder folder)
        {
            Console.WriteLine("文件或者文件夹已经删除成功");
        }

        //打开文件夹--该操作相当于Component类型的Operation方法
        public override void Open()
        {
            Console.WriteLine("已经打开当前文件夹");
        }
    }

    public class Program
    {
        static void Main()
        {

            Folder myword = new Word();

            myword.Open();//打开文件，处理文件

            myword.Add(new SonFolder());//抛出异常
            myword.Remove(new SonFolder());//抛出异常


            Folder myfolder = new SonFolder();
            myfolder.Open();//打开文件夹

            myfolder.Add(new SonFolder());//成功增加文件或者文件夹
            myfolder.Remove(new SonFolder());//成功删除文件或者文件夹

            Console.Read();
        }
    }
}
```

   以上代码就是“透明式的组合模式”实现，以下代码就是“安全式的组合模式”实现：


``` c#
namespace 安全式的组合模式的实现
{
    /// <summary>
    /// 该抽象类就是文件夹抽象接口的定义，该类型就相当于是抽象构件Component类型
    /// </summary>
    public abstract class Folder //该类型少了容器对象管理子对象的方法的定义，换了地方，在树枝构件也就是SonFolder类型
    {
        //打开文件或者文件夹--该操作相当于Component类型的Operation方法
        public abstract void Open();
    }

    /// <summary>
    /// 该Word文档类就是叶子构件的定义，该类型就相当于是Leaf类型，不能在包含子对象
    /// </summary>
    public sealed class Word : Folder  //这类型现在很干净
    {
        //打开文件--该操作相当于Component类型的Operation方法
        public override void Open()
        {
            Console.WriteLine("打开Word文档，开始进行编辑");
        }
    }

    /// <summary>
    /// SonFolder类型就是树枝构件，现在由于我们使用的是“安全式”，所以Add,Remove都是从此处开始定义的
    /// </summary>
    public abstract class SonFolder : Folder //这里可以是抽象接口，可以自己根据自己的情况而定
    {
        //增加文件夹或文件
        public abstract void Add(Folder folder);

        //删除文件夹或者文件
        public abstract void Remove(Folder folder);

        //打开文件夹--该操作相当于Component类型的Operation方法
        public override void Open()
        {
            Console.WriteLine("已经打开当前文件夹");
        }
    }

    /// <summary>
    /// NextFolder类型就是树枝构件的实现类
    /// </summary>
    public sealed class NextFolder : SonFolder
    {
        //增加文件夹或文件
        public override void Add(Folder folder)
        {
            Console.WriteLine("文件或者文件夹已经增加成功");
        }

        //删除文件夹或者文件
        public override void Remove(Folder folder)
        {
            Console.WriteLine("文件或者文件夹已经删除成功");
        }

        //打开文件夹--该操作相当于Component类型的Operation方法
        public override void Open()
        {
            Console.WriteLine("已经打开当前文件夹");
        }
    }

    public class Program
    {
        static void Main()
        {
            //这是安全的组合模式
            Folder myword = new Word();

            myword.Open();//打开文件，处理文件


            Folder myfolder = new NextFolder();
            myfolder.Open();//打开文件夹

            //此处要是用增加和删除功能，需要转型的操作，否则不能使用
            ((SonFolder)myfolder).Add(new NextFolder());//成功增加文件或者文件夹
            ((SonFolder)myfolder).Remove(new NextFolder());//成功删除文件或者文件夹

            Console.Read();
        }
    }
}
```

   这个模式不是很难，仔细体会实现关键点，最重要理解模式的意图，结合结构图，大家好好体会一下。

三、组合模式的实现要点：
    
    1、Composite模式采用树形结构来实现普遍存在的对象容器，从而将“一对多”的关系转化为“一对一”的关系，使得客户代码可以一致地处理对象和对象容器，无需关心处理的是单个的对象，还是组合的对象容器。

    2、将“客户代码与复杂的对象容器结构”解耦是Composite模式的核心思想，解耦之后，客户代码将与纯粹的抽象接口——而非对象容器的复杂内部实现结构——发生依赖关系，从而更能“应对变化”。

    3、Composite模式中，是将“Add和Remove等和对象容器相关的方法”定义在“表示抽象对象的Component类”中，还是将其定义在“表示对象容器的Composite类”中，是一个关乎“透明性”和“安全性”的两难问题，需要仔细权衡。这里有可能违背面向对象的“单一职责原则”，但是对于这种特殊结构，这又是必须付出的代价。ASP.Net控件的实现在这方面为我们提供了一个很好的示范。

    4、Composite模式在具体实现中，可以让父对象中的子对象反向追朔；如果父对象有频繁的遍历需求，可使用缓存技巧来改善效率。

    3.1】、组合模式的优点：

          （1）、组合模式使得客户端代码可以一致地处理对象和对象容器，无需关系处理的单个对象，还是组合的对象容器。

          （2）、将”客户代码与复杂的对象容器结构“解耦。

          （3）、可以更容易地往组合对象中加入新的构件。

     3.2】、组合模式的缺点：

          （1）、使得设计更加复杂。客户端需要花更多时间理清类之间的层次关系。（这个是几乎所有设计模式所面临的问题）。

      3.3】、在以下情况下应该考虑使用组合模式：

           （1）、需要表示一个对象整体或部分的层次结构。

           （2）、希望用户忽略组合对象与单个对象的不同，用户将统一地使用组合结构中的所有对象。

四、.NET 中组合模式的实现

       其实组合模式在FCL里面运用还是很多的，不知道大家是不是有所感觉，这个模式大多数是运用在控件上或者是和界面操作、展示相关的操作上。这个模式在.NET 中最典型的应用就是应用与WinForms和Web的开发中，在.NET类库中，都为这两个平台提供了很多现有的控件，然而System.Windows.Forms.dll中System.Windows.Forms.Control类就应用了组合模式，因为控件包括Label、TextBox等这样的简单控件，这些控件可以理解为叶子对象，同时也包括GroupBox、DataGrid这样复合的控件或者叫容器控件，每个控件都需要调用OnPaint方法来进行控件显示，为了表示这种对象之间整体与部分的层次结构，微软把Control类的实现应用了组合模式（确切地说应用了透明式的组合模式）。

五、总结

      我写文章，怎么也要3个小时，也要读上好几遍，防止有错字错句的出现。我也想把握的理解更好融进我写的文章中，但是能力有限，欢迎大家来批评指正，我也会从中收益。今天的文章就写到这里了，模式这个东西就像“独孤九剑”，不要死记硬背，要多看看别人的，多写写代码，要理解场景和意图，多写多练吧，你就有可能成为一代大侠。模式学无止境，我也是刚刚开始。