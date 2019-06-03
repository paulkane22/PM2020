using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PJK.WPF.PRISM.PM2020.Module.Projects.Event
{
    public class AfterProjectSavedEvent:PubSubEvent<AfterProjectSavedEventArgs>
    {
    }

    public class AfterProjectSavedEventArgs
    {
        public int Id { get; set; }
        public string DisplayMember { get; set; }
    }
}
