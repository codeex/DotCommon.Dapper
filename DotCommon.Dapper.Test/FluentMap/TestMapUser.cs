using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotCommon.Dapper.FluentMap;

namespace DotCommon.Dapper.Test.FluentMap
{
    public class TestMapUser : EntityMap<TestUser>
    {
        public TestMapUser()
        {
            Table("T1");
            Map(x => x.UserId).Column("uid");
            HasPrefix("tb_");
            Map(x => x.Age).Ignore();
        }
    }
}
