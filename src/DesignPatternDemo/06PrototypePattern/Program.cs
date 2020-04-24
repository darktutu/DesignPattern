using System;

namespace _06PrototypePattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Prototype xingZheSun = new XingZheSunPrototype();
            Prototype xingZheSun2 = xingZheSun.Clone();
            Prototype xingZheSun3 = xingZheSun.Clone();

            Prototype sunXingZhe = new SunXingZhePrototype();
            Prototype sunXingZhe2 = sunXingZhe.Clone();
            Prototype sunXingZhe3 = sunXingZhe.Clone();
            Prototype sunXingZhe4 = sunXingZhe.Clone();
            Prototype sunXingZhe5 = sunXingZhe.Clone();

            //1号孙行者打妖怪
            sunXingZhe.Fight();
            //2号孙行者去化缘
            sunXingZhe2.BegAlms();

            //战斗和化缘也可以分类，比如化缘，可以分：水果类化缘，饭食类化缘；战斗可以分为：天界宠物下界成妖的战斗，自然修炼成妖的战斗，大家可以自己去想吧，原型模式还是很有用的

            Console.Read();
        }
    }



    /// <summary>
    /// 原型设计模式，每个具体原型是一类对象的原始对象，通过每个原型对象克隆出来的对象也可以进行设置，在原型的基础之上丰富克隆出来的对象，所以要设计好抽象原型的接口
    /// </summary>

    /// <summary>
    /// 抽象原型，定义了原型本身所具有特征和动作，该类型就是至尊宝
    /// </summary>
    public abstract class Prototype
    {
        // 战斗--保护师傅
        public abstract void Fight();
        // 化缘--不要饿着师傅
        public abstract void BegAlms();

        // 吹口仙气--变化一个自己出来
        public abstract Prototype Clone();
    }

    /// <summary>
    /// 具体原型，例如：行者孙，他只负责化斋饭食和与天界宠物下界的妖怪的战斗
    /// </summary>
    public sealed class XingZheSunPrototype : Prototype
    {
        // 战斗--保护师傅--与自然修炼成妖的战斗
        public override void Fight()
        {
            Console.WriteLine("腾云驾雾，各种武艺");
        }
        // 化缘--不要饿着师傅--饭食类
        public override void BegAlms()
        {
            Console.WriteLine("什么都能要来");
        }

        // 吹口仙气--变化一个自己出来
        public override Prototype Clone()
        {
            return (XingZheSunPrototype)this.MemberwiseClone();
        }
    }

    /// <summary>
    /// 具体原型，例如：孙行者，他只负责与自然界修炼成妖的战斗和化斋水果
    /// </summary>
    public sealed class SunXingZhePrototype : Prototype
    {
        // 战斗--保护师傅-与天界宠物战斗
        public override void Fight()
        {
            Console.WriteLine("腾云驾雾，各种武艺");
        }
        // 化缘--不要饿着师傅---水果类
        public override void BegAlms()
        {
            Console.WriteLine("什么都能要来");
        }

        // 吹口仙气--变化一个自己出来
        public override Prototype Clone()
        {
            return (SunXingZhePrototype)this.MemberwiseClone();
        }
    }

}