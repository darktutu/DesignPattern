using System;

namespace _10CompositePattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}


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