using System;
using System.Collections.Generic;
using System.Text;

namespace Collections
{
    class ReadOnlyException: Exception
    {
        public ReadOnlyException(): base("Collection is Read only")
        { 
        }
    }
}
