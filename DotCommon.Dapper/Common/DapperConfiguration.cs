using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DotCommon.Dapper.Common
{
    public class DapperConfiguration
    {
        /// <summary>Provides the singleton access instance.
        /// </summary>
        public static DapperConfiguration Instance { get; private set; }

        public ConfigSetting Setting { get; private set; }
        private DapperConfiguration() { }

        public static DapperConfiguration Create()
        {
            Instance = new DapperConfiguration();
            return Instance;
        }

        public DapperConfiguration Init(ConfigSetting setting = null)
        {
            if (setting == null)
            {
                setting = new ConfigSetting();
            }
            Setting = setting;
            return this;
        }


    }
}
