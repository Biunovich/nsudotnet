using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARP1
{
    class entry
    {
        public entry next;
        public object data;
        public entry (entry next, object data)
        {
            this.next = next;
            this.data = data;
        }
    }
    class stack
    {
        entry top;
        public void push (object data)
        {
            top = new entry(top, data);
        }
        public object pop ()
        {
            if (top == null)
                throw new InvalidOperationException();
            object rezult = top.data;
            top = top.next;
            return rezult;
        }
    }
}
