using System;
using System.Collections.Generic;

namespace _17ObserverPattern
{
    //银行短信系统抽象接口，是被观察者--该类型相当于抽象主体角色Subject
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
            Console.WriteLine("Hello World!");
        }
    }

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
          
        }
    }
}

