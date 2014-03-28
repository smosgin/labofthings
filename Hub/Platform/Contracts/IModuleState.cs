﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.AddIn.Contract;
using System.AddIn.Pipeline;

namespace HomeOS.Hub.Platform.Contracts
{
    public interface IModuleState : IContract
    {
        int GetSimpleState();
        DateTime GetTimestamp();
        void Update(IModuleState s);
    }
}
