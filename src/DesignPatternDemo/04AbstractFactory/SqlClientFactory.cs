using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace _04AbstractFactory
{
    ///// 扮演着具体工厂的角色，用来创建连接SQL Server数据所需要的对象
    //public sealed class SqlClientFactory : DbProviderFactory, IServiceProvider
    //{
    //    // Fields
    //    public static readonly SqlClientFactory Instance = new SqlClientFactory();

    //    // 构造函数
    //    private SqlClientFactory()
    //    {
    //    }

    //    // 重写抽象工厂中的方法
    //    public override DbCommand CreateCommand()
    //    {  // 创建具体产品
    //        return Instance.CreateCommand();
    //    }

    //    public override DbCommandBuilder CreateCommandBuilder()
    //    {
    //        return new SqlCommandBuilder();
    //    }

    //    public override DbConnection CreateConnection()
    //    {
    //        return new SqlConnection();
    //    }

    //    public override DbConnectionStringBuilder CreateConnectionStringBuilder()
    //    {
    //        return new SqlConnectionStringBuilder();
    //    }

    //    public override DbDataAdapter CreateDataAdapter()
    //    {
    //        return new SqlDataAdapter();
    //    }

    //    public override DbDataSourceEnumerator CreateDataSourceEnumerator()
    //    {
    //        return SqlDataSourceEnumerator.Instance;
    //    }

    //    public override DbParameter CreateParameter()
    //    {
    //        return new SqlParameter();
    //    }

    //    public override CodeAccessPermission CreatePermission(PermissionState state)
    //    {
    //        return new SqlClientPermission(state);
    //    }

    //    public object GetService(Type serviceType)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
