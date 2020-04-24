# 设计模式：原型模式
## 原型模式的定义：

　　原型模式：使用原型实例指定待创建对象的类型，并且通过复制这个原型来创建新的对象。

## 原型模式的结构：

　　原型模式主要包含3个角色：

　　　　

　　　　（1）Prototype(抽象原型类)：声明克隆方法的接口，是所有具体原型类的公共父类，它可是抽象类也可以是接口，甚至可以是具体实现类。

　　　　（2）ConcretePrototype(具体原型类)：它实现抽象原型类中声明的克隆方法，在克隆方法中返回自己的一个克隆对象。

　　　　（3）Client(客户端)：在客户类中，让一个原型对象克隆自身从而创建一个新的对象。

深克隆与浅克隆：

　　　　浅克隆：当原型对象被复制时，只复制它本身和其中包含的值类型的成员变量，而引用类型的成员变量并没有复制。

　　　　

　　　　深克隆：除了对象本身被复制外，对象所包含的所有成员变量也将被复制。

　　　　

## 原型模式的实现：

　　在使用某OA系统时，有些岗位的员工发现他们每周的工作都大同小异，因此在填写工作周报时很多内容都是重复的，为了提高工作周报的创建效率，大家迫切地希望有一种机制能够快速创建相同或者相似的周报，包括创建周报的附件。试使用原型模式对该OA系统中的工作周报创建模块进行改进。

 

　　

 

　　代码如下：

　　WeeklyLog：周报类

``` c#
    [Serializable]
    public class WeeklyLog
    {
        public Attachment Attachment { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Content { get; set; }
        /// <summary>
        /// 使用MemberwiseClone()实现浅克隆
        /// </summary>
        /// <returns></returns>
        //public WeeklyLog Clone()
        //{
        //    return (WeeklyLog)this.MemberwiseClone();
        //}

        //使用序列化的方式实现深克隆
        public WeeklyLog Clone()
        {
            WeeklyLog clone = null;
            FileStream fs = new FileStream("temp.dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, this);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to Serialize . Reason :" + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
            FileStream fs1 = new FileStream("temp.dat", FileMode.Open);
            BinaryFormatter formatter1 = new BinaryFormatter();
            try
            {
                clone = (WeeklyLog)formatter.Deserialize(fs1);//反序列化
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to deserialize. Reasion:" + e.Message);
                throw;
            }
            finally
            {
                fs1.Close();
            }

            return clone;
        }
    }
```
　　Attachmeht:附件类

``` c#
    [Serializable]
    public class Attachment
    {
        public string Name { get; set; }


        public void Dowmload()
        {
            Console.WriteLine("下载附件，文件名为{0}", Name);
        }
    }

```
客户端代码：

``` c#
        static void Main(string[] args)
        {
            WeeklyLog log, log_new;
            log = new WeeklyLog();
            Attachment attchment = new Attachment();
            log.Attachment = attchment;
            log_new = log.Clone();
            System.Console.WriteLine("周报是否相同?{0}",log==log_new ? "是":"否");
            System.Console.WriteLine("附件是否相同？{0}",log.Attachment == log_new.Attachment ? "是":"否");
            System.Console.ReadKey();
        }
```
原型管理器：

　　将多个原型对象存储在一个集合中供客户端使用，它是一个专门负责克隆对象的工厂，其中定义了一个集合用于存储原型对象，如果需要某个原型对象的一个克隆，可以通过复制集合中对应的原型对象来获得。

　　

　　代码如下：

``` c#
    public class PrototypeManager
    {
        Hashtable ht = new Hashtable();
        public PrototypeManager()
        {
            ht.Add("A", new ConcretePrototypeA());
            ht.Add("B", new ConcretePrototypeB());
        }

        public void Add(string key, Prototype prototype)
        {
            ht.Add(key, prototype);
        }

        public Prototype Get(string key)
        {
            Prototype clone = null;
            clone = ((Prototype)ht[key]).Clone();
            return clone;
        }
    }


    public abstract class Prototype
    {
        public abstract Prototype Clone();
    }

    public class ConcretePrototypeA : Prototype
    {
        public override Prototype Clone()
        {
            return (ConcretePrototypeA)this.MemberwiseClone();
        }
    }

    public class ConcretePrototypeB : Prototype
    {
        public override Prototype Clone()
        {
            return (ConcretePrototypeB)this.MemberwiseClone();
        }
    }
```
  　　在实际开发当中，可以将PrototypeManager设计为单例模式，确保系统中有且只有一个PrototypeManager对象，有利于节省系统资源，还可以更好的对原型管理器对象进行控制。。。。

## 原型模式的优缺点：

　　优点：（1）：当创建对象的实例较为复杂的时候，使用原型模式可以简化对象的创建过程，通过复制一个已有的实例可以提高实例的创建效率。

　　　　　（2）：扩展性好，由于原型模式提供了抽象原型类，在客户端针对抽象原型类进行编程，而将具体原型类写到配置文件中，增减或减少产品对原有系统都没有影响。

　　　　　（3）：原型模式提供了简化的创建结构，工厂方法模式常常需要有一个与产品类等级结构相同的工厂等级结构，而原型模式不需要这样，圆形模式中产品的复制是通过封装在类中的克隆方法实现的，无需专门的工厂类来创建产品。

　　　　　（4）：可以使用深克隆方式保存对象的状态，使用原型模式将对象复制一份并将其状态保存起来，以便在需要的时候使用(例如恢复到历史某一状态)，可辅助实现撤销操作。

　　缺点：（1）：需要为每一个类配置一个克隆方法，而且该克隆方法位于类的内部，当对已有类进行改造的时候，需要修改代码，违反了开闭原则。

　　　　　（2）：在实现深克隆时需要编写较为复杂的代码，而且当对象之间存在多重签到引用时，为了实现深克隆，每一层对象对应的类都必须支持深克隆，实现起来会比较麻烦。

## 原型模式的适用环境：

　　1：创建新对象成本较大（例如初始化时间长，占用CPU多或占太多网络资源），新对象可以通过复制已有对象来获得，如果相似对象，则可以对其成员变量稍作修改。

　　2：系统要保存对象的状态，而对象的状态很小。

　　3：需要避免使用分层次的工厂类来创建分层次的对象，并且类的实例对象只有一个或很少的组合状态，通过复制原型对象得到新实例可以比使用构造函数创建一个新实例更加方便。