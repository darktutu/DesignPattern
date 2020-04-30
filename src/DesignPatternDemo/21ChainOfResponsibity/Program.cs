using System;

namespace _21ChainOfResponsibity
{
    class Program
    {
        static void Main(string[] args)
        {
            PurchaseRequest requestDao = new PurchaseRequest(8000.0, "单刀5把");
            PurchaseRequest requestHuaJi = new PurchaseRequest(10000.0, "10把方天画戟");
            PurchaseRequest requestJian = new PurchaseRequest(80000.0, "5把金丝龙鳞闪电劈");

            Approver manager = new Manager("黄飞鸿");
            Approver financial = new FinancialManager("黄麒英");
            Approver ceo = new CEO("十三姨");

            // 设置职责链
            manager.NextApprover = financial;
            financial.NextApprover = ceo;

            // 处理请求
            manager.ProcessRequest(requestDao);
            manager.ProcessRequest(requestHuaJi);
            manager.ProcessRequest(requestJian);

            Console.ReadLine();
            Console.WriteLine("Hello World!");
        }
    }

    // 采购请求
    public sealed class PurchaseRequest
    {
        // 金额
        public double Amount { get; set; }

        // 产品名字
        public string ProductName { get; set; }

        public PurchaseRequest(double amount, string productName)
        {
            Amount = amount;
            ProductName = productName;
        }
    }

    //抽象审批人,Handler---相当于“抽象处理者角色”
    public abstract class Approver
    {
        //下一位审批人，由此形成一条链
        public Approver NextApprover { get; set; }

        //审批人的名称
        public string Name { get; set; }

        public Approver(string name)
        {
            this.Name = name;
        }

        //处理请求
        public abstract void ProcessRequest(PurchaseRequest request);
    }

    //部门经理----相当于“具体处理者角色” ConcreteHandler
    public sealed class Manager : Approver
    {
        public Manager(string name) : base(name) { }

        public override void ProcessRequest(PurchaseRequest request)
        {
            if (request.Amount <= 10000.0)
            {
                Console.WriteLine("{0} 部门经理批准了对原材料{1}的采购计划！", this.Name, request.ProductName);
            }
            else if (NextApprover != null)
            {
                NextApprover.ProcessRequest(request);
            }
        }
    }

    //财务经理---相当于“具体处理者角色”ConcreteHandler
    public sealed class FinancialManager : Approver
    {
        public FinancialManager(string name) : base(name) { }

        public override void ProcessRequest(PurchaseRequest request)
        {
            if (request.Amount > 10000.0 && request.Amount <= 50000.0)
            {
                Console.WriteLine("{0} 财务经理批准了对原材料{1}的采购计划！", this.Name, request.ProductName);
            }
            else if (NextApprover != null)
            {
                NextApprover.ProcessRequest(request);
            }
        }
    }

    //总裁---相当于“具体处理者角色” ConcreteHandler
    public sealed class CEO : Approver
    {
        public CEO(string name) : base(name) { }

        public override void ProcessRequest(PurchaseRequest request)
        {
            if (request.Amount > 50000.0 && request.Amount < 300000.0)
            {
                Console.WriteLine("{0} 总裁批准了对原材料 {1} 的采购计划！", this.Name, request.ProductName);
            }
            else
            {
                Console.WriteLine("这个采购计划的金额比较大，需要一次董事会会议讨论才能决定！");
            }
        }
    }
}