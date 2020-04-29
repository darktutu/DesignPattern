C#设计模式之十五迭代器模式（Iterator Pattern）【行为型】
一、引言

   今天我们开始讲“行为型”设计模式的第三个模式，该模式是【迭代器模式】，英文名称是：Iterator Pattern。还是老套路，先从名字上来看看。“迭代器模式”我第一次看到这个名称，我的理解是，迭代是遍历的意思，迭代器可以理解为是遍历某某的工具，遍历什么呢？在软件设计中，当然遍历的是集合对象，所以说迭代器模式是遍历集合的一种通用的算法。如果集合只有一种类型，那这个模式就没用了，就是因为集合对象包含数组、列表，字典和哈希表等各种对象，如果为每一种集合对象都实现一套遍历算法，也不太现实，因此为了解决遍历集合有一个统一的接口这个事情，所以就提出了“迭代器”这个模式。

二、迭代器模式的详细介绍

2.1、动机（Motivate）

   在软件构建过程中，集合对象内部结构常常变化各异。但对于这些集合对象，我们希望在不暴露其内部结构的同时，可以让外部客户代码透明地访问其中包含的元素；同时这种“透明遍历”也为“同一种算法在多种集合对象上进行操作”提供了可能。

　使用面向对象技术将这种遍历机制抽象为“迭代器对象”为“应对变化中的集合对象”提供了一种优雅的方式。

2.2、意图（Intent）

   提供一种方法顺序访问一个聚合对象中的各个元素，而又不暴露该对象的内部表示。　　　　　　                                ——《设计模式》GoF

2.3、结构图

       

2.4、模式的组成
    
    从迭代器模式的结构图可以看出，它涉及到四个角色，它们分别是：

    （1）、抽象迭代器(Iterator)：抽象迭代器定义了访问和遍历元素的接口，一般声明如下方法：用于获取第一个元素的first()，用于访问下一个元素的next()，用于判断是否还有下一个元素的hasNext()，用于获取当前元素的currentItem()，在其子类中将实现这些方法。

    （2）、具体迭代器(ConcreteIterator)：具体迭代器实现了抽象迭代器接口，完成对集合对象的遍历，同时在对聚合进行遍历时跟踪其当前位置。

    （3）、抽象聚合类(Aggregate)：抽象聚合类用于存储对象，并定义创建相应迭代器对象的接口，声明一个createIterator()方法用于创建一个迭代器对象。

    （4）、具体聚合类(ConcreteAggregate)：具体聚合类实现了创建相应迭代器的接口，实现了在抽象聚合类中声明的createIterator()方法，并返回一个与该具体聚合相对应的具体迭代器ConcreteIterator实例。

2.5、迭代器模式的代码实现

    迭代器模式在现实生活中也有类似的例子，比如：在部队中，我们可以让某一队伍当中的某人出列，或者让队列里面的每个人依次报名，其实这个过程就是一个遍历的过程。没什么可说的，具体实现代码如下：

``` c#
namespace 迭代器模式的实现
{
    // 部队队列的抽象聚合类--该类型相当于抽象聚合类Aggregate
    public interface ITroopQueue
    {
        Iterator GetIterator();
    }
 
    // 迭代器抽象类
    public interface Iterator
    {
        bool MoveNext();
        Object GetCurrent();
        void Next();
        void Reset();
    }
 
    //部队队列具体聚合类--相当于具体聚合类ConcreteAggregate
    public sealed class ConcreteTroopQueue:ITroopQueue
    {
        private string[] collection;

        public ConcreteTroopQueue()
        {
            collection = new string[] { "黄飞鸿","方世玉","洪熙官","严咏春" };
        }
 
        public Iterator GetIterator()
        {
            return new ConcreteIterator(this);
        }
 
        public int Length
        {
            get { return collection.Length; }
        }
 
        public string GetElement(int index)
        {
            return collection[index];
        }
    }
 
    // 具体迭代器类
    public sealed class ConcreteIterator : Iterator
    {
        // 迭代器要集合对象进行遍历操作，自然就需要引用集合对象
        private ConcreteTroopQueue _list;
        private int _index;
 
        public ConcreteIterator(ConcreteTroopQueue list)
        {
            _list = list;
            _index = 0;
        }
 
        public bool MoveNext()
        {
            if (_index < _list.Length)
            {
                return true;
            }
            return false;
        }
 
        public Object GetCurrent()
        {
            return _list.GetElement(_index);
        }
 
        public void Reset()
        {
            _index = 0;
        }
 
        public void Next()
        {
            if (_index < _list.Length)
            {
                _index++;
            }
 
        }
    }
 
    // 客户端（Client）
    class Program
    {
        static void Main(string[] args)
        {
            Iterator iterator;
            ITroopQueue list = new ConcreteTroopQueue();
            iterator = list.GetIterator();
 
            while (iterator.MoveNext())
            {
                string ren = (string)iterator.GetCurrent();
                Console.WriteLine(ren);
                iterator.Next();
            }
 
            Console.Read();
        }
    }
}
```
  
三、迭代器模式的实现要点：
    
     1、迭代抽象：访问一个聚合对象的内容而无需暴露它的内部表示。

     2、迭代多态：为遍历不同的集合结构提供一个统一的接口，从而支持同样的算法在不同的集合结构上进行操作。

     3、迭代器的健壮性考虑：遍历的同时更改迭代器所在的集合结构，会导致问题。
    
     3.1】、迭代器模式的优点：

          （1）、迭代器模式使得访问一个聚合对象的内容而无需暴露它的内部表示，即迭代抽象。

          （2）、迭代器模式为遍历不同的集合结构提供了一个统一的接口，从而支持同样的算法在不同的集合结构上进行操作

    3.2】、迭代器模式的缺点：

          （1）、迭代器模式在遍历的同时更改迭代器所在的集合结构会导致出现异常。所以使用foreach语句只能在对集合进行遍历，不能在遍历的同时更改集合中的元素。

     3.3】、迭代器模式的使用场景：

       （1）、访问一个聚合对象的内容而无需暴露它的内部表示。

       （2）、支持对聚合对象的多种遍历。

       （3）、为遍历不同的聚合结构提供一个统一的接口(即, 支持多态迭代)。

四、.NET 中迭代器模式的实现

     在mscorlib程序集里有这样一个命名空间，该命名空间就是：System.Collections，在该命名空间里面早已有了迭代器模式的实现。对于聚集接口和迭代器接口已经存在了，其中IEnumerator扮演的就是迭代器的角色，它的实现如下：

``` c#
public interface IEnumerator
 {
      object Current
      {
           get;
      }

     bool MoveNext();

     void Reset();
 }
````

     属性Current返回当前集合中的元素，Reset()方法恢复初始化指向的位置，MoveNext()方法返回值true表示迭代器成功前进到集合中的下一个元素，返回值false表示已经位于集合的末尾。能够提供元素遍历的集合对象，在.Net中都实现了IEnumerator接口。

     IEnumerable则扮演的就是抽象聚集的角色，只有一个GetEnumerator()方法，如果集合对象需要具备跌代遍历的功能，就必须实现该接口。
``` c#
public interface IEnumerable
{
    IEumerator GetEnumerator();
}
```
    抽象聚合角色（Aggregate）和抽象迭代器角色（Iterator）分别是IEnumerable接口和IEnumerator接口，具体聚合角色（ConcreteAggregate）有Queue类型， BitArray等类型，代码如下：

``` c#
public sealed class BitArray : ICollection, IEnumerable, ICloneable
    {
        [Serializable]
        private class BitArrayEnumeratorSimple : IEnumerator, ICloneable
        {
            private BitArray bitarray;

            private int index;

            private int version;

            private bool currentElement;

            public virtual object Current
            {
                get
                {
                    if (this.index == -1)
                    {
                        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
                    }
                    if (this.index >= this.bitarray.Count)
                    {
                        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
                    }
                    return this.currentElement;
                }
            }

            internal BitArrayEnumeratorSimple(BitArray bitarray)
            {
                this.bitarray = bitarray;
                this.index = -1;
                this.version = bitarray._version;
            }

            public object Clone()
            {
                return base.MemberwiseClone();
            }

            public virtual bool MoveNext()
            {
                if (this.version != this.bitarray._version)
                {
                    throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
                }
                if (this.index < this.bitarray.Count - 1)
                {
                    this.index++;
                    this.currentElement = this.bitarray.Get(this.index);
                    return true;
                }
                this.index = this.bitarray.Count;
                return false;
            }

            public void Reset()
            {
                if (this.version != this.bitarray._version)
                {
                    throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
                }
                this.index = -1;
            }
        }

        private const int BitsPerInt32 = 32;

        private const int BytesPerInt32 = 4;

        private const int BitsPerByte = 8;

        private int[] m_array;

        private int m_length;

        private int _version;

        [NonSerialized]
        private object _syncRoot;

        private const int _ShrinkThreshold = 256;

        [__DynamicallyInvokable]
        public bool this[int index]
        {
            [__DynamicallyInvokable]
            get
            {
                return this.Get(index);
            }
            [__DynamicallyInvokable]
            set
            {
                this.Set(index, value);
            }
        }

        [__DynamicallyInvokable]
        public int Length
        {
            [__DynamicallyInvokable]
            get
            {
                return this.m_length;
            }
            [__DynamicallyInvokable]
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
                }
                int arrayLength = BitArray.GetArrayLength(value, 32);
                if (arrayLength > this.m_array.Length || arrayLength + 256 < this.m_array.Length)
                {
                    int[] array = new int[arrayLength];
                    Array.Copy(this.m_array, array, (arrayLength > this.m_array.Length) ? this.m_array.Length : arrayLength);
                    this.m_array = array;
                }
                if (value > this.m_length)
                {
                    int num = BitArray.GetArrayLength(this.m_length, 32) - 1;
                    int num2 = this.m_length % 32;
                    if (num2 > 0)
                    {
                        this.m_array[num] &= (1 << num2) - 1;
                    }
                    Array.Clear(this.m_array, num + 1, arrayLength - num - 1);
                }
                this.m_length = value;
                this._version++;
            }
        }

        public int Count
        {
            get
            {
                return this.m_length;
            }
        }

        public object SyncRoot
        {
            get
            {
                if (this._syncRoot == null)
                {
                    Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
                }
                return this._syncRoot;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public bool IsSynchronized
        {
            get
            {
                return false;
            }
        }

        private BitArray()
        {
        }

        [__DynamicallyInvokable]
        public BitArray(int length) : this(length, false)
        {
        }

        [__DynamicallyInvokable]
        public BitArray(int length, bool defaultValue)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
            }
            this.m_array = new int[BitArray.GetArrayLength(length, 32)];
            this.m_length = length;
            int num = defaultValue ? -1 : 0;
            for (int i = 0; i < this.m_array.Length; i++)
            {
                this.m_array[i] = num;
            }
            this._version = 0;
        }

        [__DynamicallyInvokable]
        public BitArray(byte[] bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }
            if (bytes.Length > 268435455)
            {
                throw new ArgumentException(Environment.GetResourceString("Argument_ArrayTooLarge", new object[]
                {
                    8
                }), "bytes");
            }
            this.m_array = new int[BitArray.GetArrayLength(bytes.Length, 4)];
            this.m_length = bytes.Length * 8;
            int num = 0;
            int num2 = 0;
            while (bytes.Length - num2 >= 4)
            {
                this.m_array[num++] = ((int)(bytes[num2] & 255) | (int)(bytes[num2 + 1] & 255) << 8 | (int)(bytes[num2 + 2] & 255) << 16 | (int)(bytes[num2 + 3] & 255) << 24);
                num2 += 4;
            }
            switch (bytes.Length - num2)
            {
            case 1:
                goto IL_103;
            case 2:
                break;
            case 3:
                this.m_array[num] = (int)(bytes[num2 + 2] & 255) << 16;
                break;
            default:
                goto IL_11C;
            }
            this.m_array[num] |= (int)(bytes[num2 + 1] & 255) << 8;
            IL_103:
            this.m_array[num] |= (int)(bytes[num2] & 255);
            IL_11C:
            this._version = 0;
        }

        [__DynamicallyInvokable]
        public BitArray(bool[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }
            this.m_array = new int[BitArray.GetArrayLength(values.Length, 32)];
            this.m_length = values.Length;
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i])
                {
                    this.m_array[i / 32] |= 1 << i % 32;
                }
            }
            this._version = 0;
        }

        [__DynamicallyInvokable]
        public BitArray(int[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }
            if (values.Length > 67108863)
            {
                throw new ArgumentException(Environment.GetResourceString("Argument_ArrayTooLarge", new object[]
                {
                    32
                }), "values");
            }
            this.m_array = new int[values.Length];
            this.m_length = values.Length * 32;
            Array.Copy(values, this.m_array, values.Length);
            this._version = 0;
        }

        [__DynamicallyInvokable]
        public BitArray(BitArray bits)
        {
            if (bits == null)
            {
                throw new ArgumentNullException("bits");
            }
            int arrayLength = BitArray.GetArrayLength(bits.m_length, 32);
            this.m_array = new int[arrayLength];
            this.m_length = bits.m_length;
            Array.Copy(bits.m_array, this.m_array, arrayLength);
            this._version = bits._version;
        }

        [__DynamicallyInvokable]
        public bool Get(int index)
        {
            if (index < 0 || index >= this.Length)
            {
                throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
            }
            return (this.m_array[index / 32] & 1 << index % 32) != 0;
        }

        [__DynamicallyInvokable]
        public void Set(int index, bool value)
        {
            if (index < 0 || index >= this.Length)
            {
                throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
            }
            if (value)
            {
                this.m_array[index / 32] |= 1 << index % 32;
            }
            else
            {
                this.m_array[index / 32] &= ~(1 << index % 32);
            }
            this._version++;
        }

        [__DynamicallyInvokable]
        public void SetAll(bool value)
        {
            int num = value ? -1 : 0;
            int arrayLength = BitArray.GetArrayLength(this.m_length, 32);
            for (int i = 0; i < arrayLength; i++)
            {
                this.m_array[i] = num;
            }
            this._version++;
        }

        [__DynamicallyInvokable]
        public BitArray And(BitArray value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            if (this.Length != value.Length)
            {
                throw new ArgumentException(Environment.GetResourceString("Arg_ArrayLengthsDiffer"));
            }
            int arrayLength = BitArray.GetArrayLength(this.m_length, 32);
            for (int i = 0; i < arrayLength; i++)
            {
                this.m_array[i] &= value.m_array[i];
            }
            this._version++;
            return this;
        }

        [__DynamicallyInvokable]
        public BitArray Or(BitArray value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            if (this.Length != value.Length)
            {
                throw new ArgumentException(Environment.GetResourceString("Arg_ArrayLengthsDiffer"));
            }
            int arrayLength = BitArray.GetArrayLength(this.m_length, 32);
            for (int i = 0; i < arrayLength; i++)
            {
                this.m_array[i] |= value.m_array[i];
            }
            this._version++;
            return this;
        }

        [__DynamicallyInvokable]
        public BitArray Xor(BitArray value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            if (this.Length != value.Length)
            {
                throw new ArgumentException(Environment.GetResourceString("Arg_ArrayLengthsDiffer"));
            }
            int arrayLength = BitArray.GetArrayLength(this.m_length, 32);
            for (int i = 0; i < arrayLength; i++)
            {
                this.m_array[i] ^= value.m_array[i];
            }
            this._version++;
            return this;
        }

        [__DynamicallyInvokable]
        public BitArray Not()
        {
            int arrayLength = BitArray.GetArrayLength(this.m_length, 32);
            for (int i = 0; i < arrayLength; i++)
            {
                this.m_array[i] = ~this.m_array[i];
            }
            this._version++;
            return this;
        }

        public void CopyTo(Array array, int index)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
            }
            if (array.Rank != 1)
            {
                throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
            }
            if (array is int[])
            {
                Array.Copy(this.m_array, 0, array, index, BitArray.GetArrayLength(this.m_length, 32));
                return;
            }
            if (array is byte[])
            {
                int arrayLength = BitArray.GetArrayLength(this.m_length, 8);
                if (array.Length - index < arrayLength)
                {
                    throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
                }
                byte[] array2 = (byte[])array;
                for (int i = 0; i < arrayLength; i++)
                {
                    array2[index + i] = (byte)(this.m_array[i / 4] >> i % 4 * 8 & 255);
                }
                return;
            }
            else
            {
                if (!(array is bool[]))
                {
                    throw new ArgumentException(Environment.GetResourceString("Arg_BitArrayTypeUnsupported"));
                }
                if (array.Length - index < this.m_length)
                {
                    throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
                }
                bool[] array3 = (bool[])array;
                for (int j = 0; j < this.m_length; j++)
                {
                    array3[index + j] = ((this.m_array[j / 32] >> j % 32 & 1) != 0);
                }
                return;
            }
        }

        public object Clone()
        {
            return new BitArray(this.m_array)
            {
                _version = this._version,
                m_length = this.m_length
            };
        }

        [__DynamicallyInvokable]
        public IEnumerator GetEnumerator()
        {
            return new BitArray.BitArrayEnumeratorSimple(this);
        }

        private static int GetArrayLength(int n, int div)
        {
            if (n <= 0)
            {
                return 0;
            }
            return (n - 1) / div + 1;
        }
    }
```
   还有很多类型，就不一一列举了，大家可以查看源代码，每个元素都可以在迭代器模式的构造图上找到对应的元素。

   具体的类型和代码截图如下：

      
  
五、总结

    今天到此就把迭代器模式写完了，该模式不是很难，结构也不是很复杂，Net框架里面也有现成的实现。并且在 Net 2.0里面还有升级的实现，要想学习该模式，可以好好看看该模式在Net 框架中的实现，受益匪浅。