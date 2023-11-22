using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wirstADO.net
{
    public class FAV
    {
        public string Name { get;set; }
        public string Type { get;set; }
        public string Color { get;set; }
        public int Callories { get; set; }
        public FAV(string name,string type,string color,int callories) 
        {
            Name = name;
            Type = type;
            Color = color;
            Callories = callories;
        }
    }
}
