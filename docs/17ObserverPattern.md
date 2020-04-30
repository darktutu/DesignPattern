# C#设计模式之十六观察者模式（Observer Pattern）【行为型】

订阅的逻辑就是观察者模式的一种应用。service bus中的topic就是一个实际例子。

## 一、引言

   今天是2017年11月份的最后一天，也就是2017年11月30日，利用今天再写一个模式，争取下个月（也就是12月份）把所有的模式写完，2018年，新的一年写一些新的东西。今天我们开始讲“行为型”设计模式的第四个模式，该模式是【观察者模式】，英文名称是：Observer Pattern。还是老套路，先从名字上来看看。“观察者模式”我第一次看到这个名称，我的理解是，既然有“观察者”，那肯定就有“被观察者”了，“观察者”监视着“被观察者”，如果“被观察者”有所行动，“观察者”就会做出相应的动作来回应，哈哈，听起来是不是有点像“谍战”的味道。我所说的谍战不是天朝内的那种，比如：手撕鬼子，我说的是“谍影重重”的那类优秀影片，大家懂得。“观察者模式”在现实生活中，实例其实是很多的，比如：八九十年代我们订阅的报纸，我们会定期收到报纸，因为我们订阅了。银行可以给储户发手机短信，也是“观察者模式”很好的使用的例子，因为我们订阅了银行的短信业务，当我们账户余额发生变化就会收到通知，还有很多，我就不一一列举了，发挥大家的想象吧。好了，接下来，就让我们看看该模式具体是怎么实现的吧。

## 二、观察者模式的详细介绍

### 2.1、动机（Motivate）

   在软件构建过程中，我们需要为某些对象建立一种“通知依赖关系”——一个对象（目标对象）的状态发生改变，所有的依赖对象（观察者对象）都将得到通知。如果这样的依赖关系过于紧密，将使软件不能很好地抵御变化。

   使用面向对象技术，可以将这种依赖关系弱化，并形成一种稳定的依赖关系。从而实现软件体系结构的松耦合。

### 2.2、意图（Intent）

   定义对象间的一种一对多的依赖关系，以便当一个对象的状态发生改变时，所有依赖于它的对象都得到通知并自动更新。　　　　　　                                ——《设计模式》GoF

### 2.3、结构图

       

### 2.4、模式的组成
    
    可以看出，在观察者模式的结构图有以下角色：

    （1）、抽象主题角色（Subject）：抽象主题把所有观察者对象的引用保存在一个列表中，并提供增加和删除观察者对象的操作，抽象主题角色又叫做抽象被观察者角色，一般由抽象类或接口实现。

    （2）、抽象观察者角色（Observer）：为所有具体观察者定义一个接口，在得到主题通知时更新自己，一般由抽象类或接口实现。

    （3）、具体主题角色（ConcreteSubject）：实现抽象主题接口，具体主题角色又叫做具体被观察者角色。

    （4）、具体观察者角色（ConcreteObserver）：实现抽象观察者角色所要求的接口，以便使自身状态与主题的状态相协调。

### 2.5、观察者模式的代码实现

    观察者模式在显示生活中也有类似的例子，比如：我们订阅银行短信业务，当我们账户发生改变，我们就会收到相应的短信。类似的还有微信订阅号，今天我们就以银行给我发送短信当我们账户余额发生变化的时候为例来讲讲观察者模式的实现，很简单，现实生活正例子也很多，理解起来也很容易。我们看代码吧，实现代码如下：

``` c#
namespace 观察者模式的实现
{
    //银行短信系统抽象接口，是被观察者--该类型相当于抽象主体角色Subject
    public abstract class BankMessageSystem
    {
        protected IList<Depositor> observers;

        //构造函数初始化观察者列表实例
        protected BankMessageSystem()
        {
            observers = new List<Depositor>();
        }

        //增加预约储户
        public abstract void Add(Depositor depositor);

        //删除预约储户
        public abstract void Delete(Depositor depositor);

        //通知储户
        public void Notify()
        {
            foreach (Depositor depositor in observers)
            {
                if (depositor.AccountIsChanged)
                {
                    depositor.Update(depositor.Balance, depositor.OperationDateTime);
                    //账户发生了变化，并且通知了，储户的账户就认为没有变化
                    depositor.AccountIsChanged = false;
                }
            }
        }
    }

    //北京银行短信系统，是被观察者--该类型相当于具体主体角色ConcreteSubject
    public sealed class BeiJingBankMessageSystem : BankMessageSystem
    {
        //增加预约储户
        public override void Add(Depositor depositor)
        {
            //应该先判断该用户是否存在，存在不操作，不存在则增加到储户列表中，这里简化了
            observers.Add(depositor);
        }

        //删除预约储户
        public override void Delete(Depositor depositor)
        {
            //应该先判断该用户是否存在，存在则删除，不存在无操作，这里简化了
            observers.Remove(depositor);
        }
    }

    //储户的抽象接口--相当于抽象观察者角色（Observer）
    public abstract class Depositor
    {
        //状态数据
        private string _name;
        private int _balance;
        private int _total;
        private bool _isChanged;

        //初始化状态数据
        protected Depositor(string name, int total)
        {
            this._name = name;
            this._balance = total;//存款总额等于余额
            this._isChanged = false;//账户未发生变化
        }

        //储户的名称，假设可以唯一区别的
        public string Name
        {
            get { return _name; }
            private set { this._name = value; }
        }

        public int Balance
        {
            get { return this._balance; }
        }

        //取钱
        public void GetMoney(int num)
        {
            if (num <= this._balance && num > 0)
            {
                this._balance = this._balance - num;
                this._isChanged = true;
                OperationDateTime = DateTime.Now;
            }
        }

        //账户操作时间
        public DateTime OperationDateTime { get; set; }

        //账户是否发生变化
        public bool AccountIsChanged
        {
            get { return this._isChanged; }
            set { this._isChanged = value; }
        }

        //更新储户状态
        public abstract void Update(int currentBalance, DateTime dateTime);
    }

    //北京的具体储户--相当于具体观察者角色ConcreteObserver
    public sealed class BeiJingDepositor : Depositor
    {
        public BeiJingDepositor(string name, int total) : base(name, total) { }

        public override void Update(int currentBalance, DateTime dateTime)
        {
            Console.WriteLine(Name + ":账户发生了变化，变化时间是" + dateTime.ToString() + ",当前余额是" + currentBalance.ToString());
        }
    }


    // 客户端（Client）
    class Program
    {
        static void Main(string[] args)
        {
            //我们有了三位储户，都是武林高手，也比较有钱
            Depositor huangFeiHong = new BeiJingDepositor("黄飞鸿", 3000);
            Depositor fangShiYu = new BeiJingDepositor("方世玉", 1300);
            Depositor hongXiGuan = new BeiJingDepositor("洪熙官", 2500);

            BankMessageSystem beijingBank = new BeiJingBankMessageSystem();
            //这三位开始订阅银行短信业务
            beijingBank.Add(huangFeiHong);
            beijingBank.Add(fangShiYu);
            beijingBank.Add(hongXiGuan);

            //黄飞鸿取100块钱
            huangFeiHong.GetMoney(100);
            beijingBank.Notify();

            //黄飞鸿和方世玉都取了钱
            huangFeiHong.GetMoney(200);
            fangShiYu.GetMoney(200);
            beijingBank.Notify();

            //他们三个都取了钱
            huangFeiHong.GetMoney(320);
            fangShiYu.GetMoney(4330);
            hongXiGuan.GetMoney(332);
            beijingBank.Notify();

            Console.Read();
        }
    }
}
```

 观察者模式有些麻烦的地方就是关于状态的处理，我这里面涉及了一些状态的处理，大家可以细细体会一下，模式还是要多多练习，多多写，里面的道理就不难理解了。
   
## 三、观察者模式的实现要点：
    
    使用面向对象的抽象，Observer模式使得我们可以独立地改变目标与观察者（面向对象中的改变不是指改代码，而是指扩展、子类化、实现接口），从而使二者之间的依赖关系达致松耦合。

    目标发送通知时，无需指定观察者，通知（可以携带通知信息作为参数）会自动传播。观察者自己决定是否需要订阅通知，目标对象对此一无所知。

    在C#的event中，委托充当了抽象的Observer接口，而提供事件的对象充当了目标对象。委托是比抽象Observer接口更为松耦合的设计。

     3.1】、观察者模式的优点：

          （1）、观察者模式实现了表示层和数据逻辑层的分离，并定义了稳定的更新消息传递机制，并抽象了更新接口，使得可以有各种各样不同的表示层，即观察者。

          （2）、观察者模式在被观察者和观察者之间建立了一个抽象的耦合，被观察者并不知道任何一个具体的观察者，只是保存着抽象观察者的列表，每个具体观察者都符合一个抽象观察者的接口。

          （3）、观察者模式支持广播通信。被观察者会向所有的注册过的观察者发出通知。

    3.2】、观察者模式的缺点：

         （1）、如果一个被观察者有很多直接和间接的观察者时，将所有的观察者都通知到会花费很多时间。

         （2）、虽然观察者模式可以随时使观察者知道所观察的对象发送了变化，但是观察者模式没有相应的机制使观察者知道所观察的对象是怎样发生变化的。

         （3）、如果在被观察者之间有循环依赖的话，被观察者会触发它们之间进行循环调用，导致系统崩溃，在使用观察者模式应特别注意这点。


## 四、.NET 中观察者模式的实现

     我上面写了一点，“在C#的event中，委托充当了抽象的Observer接口，而提供事件的对象充当了目标对象。委托是比抽象Observer接口更为松耦合的设计。”，其实在Net里面实现的观察者模式做了一些改变，用委托或者说是事件来实现观察者模式。事件我们都很明白，我们可以注册控件的事件，当触发控件的动作时候，相应的事件就会执行，在事件的执行过程中我们就可以做相关的提醒业务。这里关于观察者模式在Net里面的实现就不说了，如果大家不明白，可以多看看相关委托或者事件的相关资料。

## 五、总结

    终于写完了，这个模式主要是花在了代码的书写上。因为我写每篇文章的时候，模式实现代码都是当时现想的，要组织代码关系，让其更合理，所以时间就花了不少，但是是理解更好了。该模式不是很难，结构也不是很复杂，唯一让我们多多注意的是状态的管理。这个模式结合实例理解是很容易的，模式的使用我们不能照搬，要理解，当然多多的联系和写代码也是必不可少的，我们使用模式的一贯宗旨是通过重构和迭代，在我们的代码中实现相应的模式。