using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Clock
    {
        public event EventHandler ValueChanged;
        internal volatile bool Cancel;
        public Stopwatch watch = new Stopwatch();
        protected void OnValueChanged()
        {
            if (ValueChanged != null)
                ValueChanged(this, EventArgs.Empty);
        }

        public void AddEvent(EventHandler func)
        {
            this.ValueChanged = func;
        }

        public void EraseEvent()
        {
            this.ValueChanged = null;
        }

    }
}
