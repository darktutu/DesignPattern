using System;

namespace _13ProxyPattern
{
    class Program
    {
        /// <summary>
        /// 大明星都有钱，有钱了，就可以请自己的经纪人了，有了经纪人，很多事情就不用自己亲力亲为。弄点绯闻，炒作一下子通过经纪人就可以名正言顺的的操作了，万一搞不好，自己也可以否认。
        /// </summary>
        static void Main(string[] args)
        {
            //近期，Fan姓明星关注度有点下降，来点炒作
            AgentAbstract fan = new AgentPerson();
            fan.Speculation("偶尔出来现现身，为炒作造势");

            Console.WriteLine();

            //过了段时间，又不行了，再炒作一次
            fan.Speculation("这段时间不火了，开始离婚炒作");


            Console.Read();
            Console.WriteLine("Hello World!");
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
}
