﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon.models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Tag> ConnectedTags { get; set; } = new();
    }

}
