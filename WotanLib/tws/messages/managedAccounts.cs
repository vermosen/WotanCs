using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wotan
{
    public class managedAccounts : message
    {
        public string bla { get; private set; }

        public managedAccounts(string bla) : base(messageType.managedAccounts)
        {
            this.bla = bla;
        }
    }
}
