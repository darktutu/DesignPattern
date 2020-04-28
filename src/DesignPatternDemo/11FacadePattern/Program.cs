using System;

namespace _11FacadePattern
{
    /// <summary>
    /// 不使用外观模式的情况
    /// 此时客户端与三个子系统都发送了耦合，使得客户端程序依赖与子系统
    /// 为了解决这样的问题，我们可以使用外观模式来为所有子系统设计一个统一的接口
    /// 客户端只需要调用外观类中的方法就可以了，简化了客户端的操作
    /// 从而让客户和子系统之间避免了紧耦合
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            SystemFacade facade = new SystemFacade();
            facade.Buy();
            Console.Read();
            Console.WriteLine("Hello World!");
        }
    }



    // 身份认证子系统A
    public class AuthoriationSystemA
    {
        public void MethodA()
        {
            Console.WriteLine("执行身份认证");
        }
    }

    // 系统安全子系统B
    public class SecuritySystemB
    {
        public void MethodB()
        {
            Console.WriteLine("执行系统安全检查");
        }
    }

    // 网银安全子系统C
    public class NetBankSystemC
    {
        public void MethodC()
        {
            Console.WriteLine("执行网银安全检测");
        }
    }

    //更高层的Facade
    public class SystemFacade
    {
        private AuthoriationSystemA auth;
        private SecuritySystemB security;
        private NetBankSystemC netbank;

        public SystemFacade()
        {
            auth = new AuthoriationSystemA();
            security = new SecuritySystemB();
            netbank = new NetBankSystemC();
        }

        public void Buy()
        {
            auth.MethodA();//身份认证子系统
            security.MethodB();//系统安全子系统
            netbank.MethodC();//网银安全子系统

            Console.WriteLine("我已经成功购买了！");
        }
    }
}

