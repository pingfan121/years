﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace db
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Runtime.Remoting.Messaging;

    public partial class testEntities : DbContext
    {
        public testEntities()
            : base("name=testEntities")
        {
        }

        public static testEntities GetDbContext
        {
            get
            {
                object efDbContext = CallContext.GetData("DbContext");
                if (efDbContext == null)
                {
                    efDbContext = new testEntities();
                    //存入到这个线程缓存中
                    CallContext.SetData("DbContext", efDbContext);
                }
                return efDbContext as testEntities;
            }
          
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<yao_wan> yao_wan { get; set; }
    }
}