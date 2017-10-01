using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBudgetMvvmLight.PlatformServices
{
    public interface IFileService
    {
        string DbPath { get; }
    }
}
