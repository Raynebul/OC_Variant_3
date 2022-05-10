using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace OC
{
    public class MLFQ
    {

    }
    public class Proccess
    {
        public int priority;
        public int burst_time;
        public int tt_time;
        public int total_time = 0;

        public Proccess()
        {

        }
    }

    public class Queues
    {
        int priority_start;
        int priority_end;
        int total_time = 0;
        int length = 0;
        public Proccess p { get; }
        bool executed = false;

        public Queues()
        {

        }
    }
}
