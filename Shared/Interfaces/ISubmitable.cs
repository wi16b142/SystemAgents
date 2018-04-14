using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Interfaces
{
    public interface ISubmitable
    {

        void Start();
        //void Start(string serviceName);
        void Submit(CoreMessage message);

    }
}
