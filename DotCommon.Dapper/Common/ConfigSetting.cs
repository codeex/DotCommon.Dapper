using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotCommon.Dapper.Common
{
    public class ConfigSetting
    {
        public int MaxSqlServerBuilder { get; set; } = 1000;

        public int MaxMySqlBuilder { get; set; } = 1000;

    }
}
