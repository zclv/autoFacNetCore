using System;
using System.Collections.Generic;
using System.Text;
using NetCore.IService;

namespace Service.Service
{
    public class InjectionTestService: IInjectionTestService
    {
        public string Test()
        {
            return "autoFac NetCore 测试成功！";

        }
    }
}
