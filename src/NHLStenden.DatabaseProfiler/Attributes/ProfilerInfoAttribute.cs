using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHLStenden.DatabaseProfiler.Attributes
{
    public class ProfilerInfoAttribute : Attribute
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        public ProfilerInfoAttribute(string name, string description)
        {
            this.Description = description;
            this.Name = name;
        }
    }
}
