﻿using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PJK.WPF.PRISM.PM2020.Module.Projects.Event
{
    public class SelectDetailEvent : PubSubEvent<SelectDetailEventArgs>
    {
    }

    public class SelectDetailEventArgs
    {
        public int Id { get; set; }
    }
}
