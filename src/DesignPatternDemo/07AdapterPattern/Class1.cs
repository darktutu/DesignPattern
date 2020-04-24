using System;
using System.Collections.Generic;
using System.Text;

namespace _07AdapterPattern.类的适配器模式
{
    /// <summary>
    /// 这里手机充电器为例，我们的家的插座是两相电的，但是手机的插座接头是三相电的
    /// </summary>
    class Class1
    {
        static void Main(string[] args)
        {
            //好了，现在可以充电了
            ITwoHoleTarget change = new ThreeToTwoAdapter();
            change.Request();
            Console.ReadLine();
        }
    }


    /// <summary>
    /// 我家只有2个孔的插座，也就是适配器模式中的目标角色（Target），这里只能是接口，也是类适配器的限制
    /// </summary>
    public interface ITwoHoleTarget
    {
        void Request();
    }

    /// <summary>
    /// 3个孔的插头，源角色——需要适配的类（Adaptee）
    /// </summary>
    public abstract class ThreeHoleAdaptee
    {
        public void SpecificRequest()
        {
            Console.WriteLine("我是三个孔的插头");
        }
    }

    /// <summary>
    /// 适配器类，接口要放在类的后面，在此无法适配更多的对象，这是类适配器的不足
    /// </summary>
    public class ThreeToTwoAdapter : ThreeHoleAdaptee, ITwoHoleTarget
    {
        /// <summary>
        /// 实现2个孔插头接口方法
        /// </summary>
        public void Request()
        {
            // 调用3个孔插头方法
            this.SpecificRequest();
        }
    }

}
