﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class DestroyedCarEventArgs : EventArgs
    {
        public string SpawnerName { get; set; }
    }
}
