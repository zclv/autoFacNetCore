using System;
using System.Collections.Generic;
using System.Text;
using NetCore.Dependency;

namespace NetCore.IService
{
    public interface IInjectionTestService: IDependency
    {
        string Test();
    }
}
