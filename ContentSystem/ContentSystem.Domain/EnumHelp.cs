﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentSystem.Domain
{
    public class EnumHelp
    {

        /// <summary>
        /// 启用状态枚举
        /// </summary>
        public enum EnabledEnum
        {
            无效 = 0,
            有效 = 1,
        }

        /// <summary>
        /// 用户组类别枚举
        /// </summary>
        public enum GroupTypeEnum
        {
            后台 = 1,
            APP = 2,
        }
    }
}
