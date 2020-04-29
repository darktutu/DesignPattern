using System;

namespace _14TemplateMethodPattern
{
    class Program
    {

        /// <summary>
        /// 好吃不如饺子，舒服不如倒着，我最喜欢吃我爸爸包的饺子，今天就拿吃饺子这件事来看看模板方法的实现吧
        /// </summary>
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //现在想吃绿色面的，猪肉大葱馅的饺子
            AbstractClass fan = new ConcreteClass();
            fan.EatDumplings();

            Console.WriteLine();
            //过了段时间，我开始想吃橙色面的，韭菜鸡蛋馅的饺子
            fan = new ConcreteClass2();
            fan.EatDumplings();


            Console.Read();
        }
    }


    //该类型就是抽象类角色--AbstractClass，定义做饺子的算法骨架，这里有三步骤，当然也可以有多个步骤，根据实际需要而定
    public abstract class AbstractClass
    {
        //该方法就是模板方法，方法里面包含了做饺子的算法步骤，模板方法可以返回结果，也可以是void类型，视具体情况而定
        public void EatDumplings()
        {
            //和面
            MakingDough();
            //包馅
            MakeDumplings();
            //煮饺子
            BoiledDumplings();

            Console.WriteLine("饺子真好吃！");
        }

        //要想吃饺子第一步肯定是“和面”---该方法相当于算法中的某一步
        public abstract void MakingDough();

        //要想吃饺子第二部是“包饺子”---该方法相当于算法中的某一步
        public abstract void MakeDumplings();

        //要想吃饺子第三部是“煮饺子”---该方法相当于算法中的某一步
        public abstract void BoiledDumplings();
    }

    //该类型是具体类角色--ConcreteClass，我想吃绿色面皮，猪肉大葱馅的饺子
    public sealed class ConcreteClass : AbstractClass
    {
        //要想吃饺子第一步肯定是“和面”---该方法相当于算法中的某一步
        public override void MakingDough()
        {
            //我想要面是绿色的，绿色健康嘛，就可以在此步定制了
            Console.WriteLine("在和面的时候加入芹菜汁，和好的面就是绿色的");
        }

        //要想吃饺子第二部是“包饺子”---该方法相当于算法中的某一步
        public override void MakeDumplings()
        {
            //我想吃猪肉大葱馅的，在此步就可以定制了
            Console.WriteLine("农家猪肉和农家大葱，制作成馅");
        }

        //要想吃饺子第三部是“煮饺子”---该方法相当于算法中的某一步
        public override void BoiledDumplings()
        {
            //我想吃大铁锅煮的饺子，有家的味道，在此步就可以定制了
            Console.WriteLine("用我家的大铁锅和大木材煮饺子");
        }
    }

    //该类型是具体类角色--ConcreteClass2，我想吃橙色面皮，韭菜鸡蛋馅的饺子
    public sealed class ConcreteClass2 : AbstractClass
    {
        //要想吃饺子第一步肯定是“和面”---该方法相当于算法中的某一步
        public override void MakingDough()
        {
            //我想要面是橙色的，加入胡萝卜汁就可以。在此步定制就可以了。
            Console.WriteLine("在和面的时候加入胡萝卜汁，和好的面就是橙色的");
        }

        //要想吃饺子第二部是“包饺子”---该方法相当于算法中的某一步
        public override void MakeDumplings()
        {
            //我想吃韭菜鸡蛋馅的，在此步就可以定制了
            Console.WriteLine("农家鸡蛋和农家韭菜，制作成馅");
        }

        //要想吃饺子第三部是“煮饺子”---该方法相当于算法中的某一步
        public override void BoiledDumplings()
        {
            //此处没要求
            Console.WriteLine("可以用一般煤气和不粘锅煮就可以");
        }
    }
}
