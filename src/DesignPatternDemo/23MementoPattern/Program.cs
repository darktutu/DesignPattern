using System;
using System.Collections.Generic;

namespace _23MementoPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            List<ContactPerson> persons = new List<ContactPerson>()
            {
                new ContactPerson() { Name="黄飞鸿", MobileNumber = "13533332222"},
                new ContactPerson() { Name="方世玉", MobileNumber = "13966554433"},
                new ContactPerson() { Name="洪熙官", MobileNumber = "13198765544"}
            };

            //手机名单发起人
            MobileBackOriginator mobileOriginator = new MobileBackOriginator(persons);
            mobileOriginator.Show();

            // 创建备忘录并保存备忘录对象
            MementoManager manager = new MementoManager();
            manager.ContactPersonMemento = mobileOriginator.CreateMemento();

            // 更改发起人联系人列表
            Console.WriteLine("----移除最后一个联系人--------");
            mobileOriginator.ContactPersonList.RemoveAt(2);
            mobileOriginator.Show();

            // 恢复到原始状态
            Console.WriteLine("-------恢复联系人列表------");
            mobileOriginator.RestoreMemento(manager.ContactPersonMemento);
            mobileOriginator.Show();

            Console.Read();
            Console.WriteLine("Hello World!");
        }
    }
    // 联系人--需要备份的数据，是状态数据，没有操作
    public sealed class ContactPerson
    {
        //姓名
        public string Name { get; set; }

        //电话号码
        public string MobileNumber { get; set; }
    }

    // 发起人--相当于【发起人角色】Originator
    public sealed class MobileBackOriginator
    {
        // 发起人需要保存的内部状态
        private List<ContactPerson> _personList;


        public List<ContactPerson> ContactPersonList
        {
            get
            {
                return this._personList;
            }

            set
            {
                this._personList = value;
            }
        }
        //初始化需要备份的电话名单
        public MobileBackOriginator(List<ContactPerson> personList)
        {
            if (personList != null)
            {
                this._personList = personList;
            }
            else
            {
                throw new ArgumentNullException("参数不能为空！");
            }
        }

        // 创建备忘录对象实例，将当期要保存的联系人列表保存到备忘录对象中
        public ContactPersonMemento CreateMemento()
        {
            return new ContactPersonMemento(new List<ContactPerson>(this._personList));
        }

        // 将备忘录中的数据备份还原到联系人列表中
        public void RestoreMemento(ContactPersonMemento memento)
        {
            this.ContactPersonList = memento.ContactPersonListBack;
        }

        public void Show()
        {
            Console.WriteLine("联系人列表中共有{0}个人，他们是:", ContactPersonList.Count);
            foreach (ContactPerson p in ContactPersonList)
            {
                Console.WriteLine("姓名: {0} 号码: {1}", p.Name, p.MobileNumber);
            }
        }
    }

    // 备忘录对象，用于保存状态数据，保存的是当时对象具体状态数据--相当于【备忘录角色】Memeto
    public sealed class ContactPersonMemento
    {
        // 保存发起人创建的电话名单数据，就是所谓的状态
        public List<ContactPerson> ContactPersonListBack { get; private set; }

        public ContactPersonMemento(List<ContactPerson> personList)
        {
            ContactPersonListBack = personList;
        }
    }

    // 管理角色，它可以管理【备忘录】对象，如果是保存多个【备忘录】对象，当然可以对保存的对象进行增、删等管理处理---相当于【管理者角色】Caretaker
    public sealed class MementoManager
    {
        //如果想保存多个【备忘录】对象，可以通过字典或者堆栈来保存，堆栈对象可以反映保存对象的先后顺序
        //比如：public Dictionary<string, ContactPersonMemento> ContactPersonMementoDictionary { get; set; }
        public ContactPersonMemento ContactPersonMemento { get; set; }
    }

}