# C#设计模式之十九策略模式（Stragety Pattern）【行为型】
## 一、引言

   今天我们开始讲“行为型”设计模式的第七个模式，该模式是【策略模式】，英文名称是：Stragety Pattern。在现实生活中，策略模式的例子也非常常见，例如，在一个公司中，会有各种工作人员，比如：有的是普通员工，有的是软件架构师，有的是部门经理，当然也会有公司的CEO。这些工作人员负责的工作不同，担负的责任不同，自然得到的报酬也就不同了。每种工作人员都有自己的工资，但是每个工种的工作人员的工资的计算方法又是不一样的。如果所有人的工资都一样，肯定会天下大乱的。如果不采用策略模式来实现这个需求的话，我们可能会这样来做，我们会定义一个工资类，该类有一个属性来标识工作人员的类型，并且有一个计算工资的CalculateSalary()方法，在该方法体内需要对工作人员类型进行判断，通过if-else语句来针对不同的工作人员类型来计算其所得工资。这样的实现确实可以解决这个场景，但是这样的设计不利于扩展，如果系统后期需要增加一种新的工种时，此时不得不回去修改CalculateSalary方法来多添加一个判断语句，这样明显违背了“开放——封闭”原则。此时，我们可以考虑使用策略模式来解决这个问题，既然工资计算方法是这个场景中的变化部分，此时自然可以想到对工资算法进行抽象，不同工种的工资可以用不用的策略算法具体实现，想要得到某个工作人员的工资，用其相应的工资算法策略来计算就可以了。

## 二、策略模式的详细介绍

2.1、动机（Motivate）

   在软件构建过程中，某些对象使用的算法可能多种多样，经常改变，如果将这些算法都编码到对象中，将会使对象变得异常复杂；而且有时候支持不使用的算法也是一个性能负担。如何在运行时根据需要透明地更改对象的算法？将算法与对象本身解耦，从而避免上述问题？

2.2、意图（Intent）

   定义一系列算法，把它们一个个封装起来，并且使它们可互相替换。该模式使得算法可独立于使用它的客户而变化。　　　　　　                                ——《设计模式》GoF

2.3、结构图（Structure）

       

2.4、模式的组成
    
    可以看出，在策略模式的结构图有以下角色：

    （1）、环境角色（Context）：持有一个Strategy类的引用。

         需要使用ConcreteStrategy提供的算法。

         内部维护一个Strategy的实例。

         负责动态设置运行时Strategy具体的实现算法。

         负责跟Strategy之间的交互和数据传递

    （2）、抽象策略角色（Strategy）：定义了一个公共接口，各种不同的算法以不同的方式实现这个接口，Context使用这个接口调用不同的算法，一般使用接口或抽象类实现。

    （3）、具体策略角色（ConcreteStrategy）：实现了Strategy定义的接口，提供具体的算法实现。

2.5、策略模式的代码实现

    在现实生活中，策略模式的例子也是很多的，例如：一个公司会有很多工作种类，每个工作种类负责的工作不同，自然每个工种的工资计算方法也会有千差万别，我们今天就以工资的计算为例来说明策略模式的使用，我们直接上代码，但是实际编码中切记别这样，我们要通过迭代的方式使用模式。实现代码如下：

``` c#
namespace 策略模式的实现
{
    //环境角色---相当于Context类型
    public sealed class SalaryContext
    {
        private ISalaryStrategy _strategy;

        public SalaryContext(ISalaryStrategy strategy)
        {
            this._strategy = strategy;
        }

        public ISalaryStrategy ISalaryStrategy
        {
            get { return _strategy; }
            set { _strategy = value; }
        }

        public void GetSalary(double income)
        {
            _strategy.CalculateSalary(income);
        }
    }

    //抽象策略角色---相当于Strategy类型
    public interface ISalaryStrategy
    {
        //工资计算
        void CalculateSalary(double income);
    }

    //程序员的工资--相当于具体策略角色ConcreteStrategyA
    public sealed class ProgrammerSalary : ISalaryStrategy
    {
        public void CalculateSalary(double income)
        {
            Console.WriteLine("我的工资是：基本工资(" + income + ")底薪(" + 8000 + ")+加班费+项目奖金（10%）");
        }
    }

    //普通员工的工资---相当于具体策略角色ConcreteStrategyB
    public sealed class NormalPeopleSalary : ISalaryStrategy
    {
        public void CalculateSalary(double income)
        {
            Console.WriteLine("我的工资是：基本工资(" + income + ")底薪(3000)+加班费");
        }
    }

    //CEO的工资---相当于具体策略角色ConcreteStrategyC
    public sealed class CEOSalary : ISalaryStrategy
    {
        public void CalculateSalary(double income)
        {
            Console.WriteLine("我的工资是：基本工资(" + income + ")底薪(20000)+项目奖金(20%)+公司股票");
        }
    }


    public class Client
    {
        public static void Main(String[] args)
        {
            //普通员工的工资
            SalaryContext context = new SalaryContext(new NormalPeopleSalary());
            context.GetSalary(3000);

            //CEO的工资
            context.ISalaryStrategy = new CEOSalary();
            context.GetSalary(6000);

            Console.Read();
        }
    }
}
```

## 三、策略模式的实现要点：

    Strategy及其子类为组件提供了一系列可重用的算法，从而可以使得类型在运行时方便地根据需要在各个算法之间进行切换，所谓封装算法，支持算法的变化。Strategy模式提供了用条件判断语句以外的另一种选择，消除条件判断语句，就是在解耦合。含有许多条件判断语句的代码通常都需要Strategy模式。

　　与State类似，如果Strategy对象没有实例变量，那么各个上下文可以共享一个Strategy对象，从而节省对象开销。Strategy模式适用的是算法结构中整个算法的改变，而不是算法中某个部分的改变。

　　Template Method方法：执行算法的步骤协议是本身放在抽象类里面的，允许一个通用的算法操作多个可能实现

　　Strategy模式：执行算法的协议是在具体类，每个具体实现有不同通用算法来做。

    （1）、策略模式的主要优点有：

        1】、策略类之间可以自由切换。由于策略类都实现同一个接口，所以使它们之间可以自由切换。
    
        2】、易于扩展。增加一个新的策略只需要添加一个具体的策略类即可，基本不需要改变原有的代码。
     
        3】、避免使用多重条件选择语句，充分体现面向对象设计思想。

　（2）、策略模式的主要缺点有：

        1】、客户端必须知道所有的策略类，并自行决定使用哪一个策略类。这点可以考虑使用IOC容器和依赖注入的方式来解决，关于IOC容器和依赖注入（Dependency Inject）的文章可以参考：[IoC 容器和Dependency Injection 模式](https://www.cnblogs.com/lusd/articles/3175062.html)。
        
        2】、策略模式会造成很多的策略类。

    （3）、在下面的情况下可以考虑使用策略模式：

        1】、一个系统需要动态地在几种算法中选择一种的情况下。那么这些算法可以包装到一个个具体的算法类里面，并为这些具体的算法类提供一个统一的接口。
    
        2】、如果一个对象有很多的行为，如果不使用合适的模式，这些行为就只好使用多重的if-else语句来实现，此时，可以使用策略模式，把这些行为转移到相应的具体策略类里面，就可以避免使用难以维护的多重条件选择语句，并体现面向对象涉及的概念。

## 四、.NET 策略模式的实现

     在.NET Framework中也不乏策略模式的应用例子。例如，在.NET中，为集合类型ArrayList和List<T>提供的排序功能，其中实现就利用了策略模式，定义了IComparer接口来对比较算法进行封装，实现IComparer接口的类可以是顺序，或逆序地比较两个对象的大小，具体.NET中的实现可以使用反编译工具查看List<T>.Sort(IComparer<T>)的实现。其中List<T>就是承担着环境角色，而IComparer<T>接口承担着抽象策略角色，具体的策略角色就是实现了IComparer<T>接口的类，List<T>类本身实现了存在实现了该接口的类，我们可以自定义继承与该接口的具体策略类。

## 五、总结

    今天开始有点晚，写完也比较晚。策略模式不是很难，可以说很简单，或许大家已经在实际编码中使用过该模式了。还是老话，我们要向清楚的使用每一个模式，要理解他们的优缺点，要深刻理解他们使用的场合。我们使用模式切记不能上来就使用模式，我们应该通过迭代的方式来写代码。我们编码的时候，第一印象很重要，第一次怎么想的就怎么写，如果有需求的改变，且改变比较频繁，然后我们仔细分析变化点，再找合适的模式来解决相应的问题。