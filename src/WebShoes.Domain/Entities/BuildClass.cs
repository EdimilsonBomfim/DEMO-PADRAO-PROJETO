using System;
using System.Collections.Generic;
using System.Text;

namespace WebShoes.Domain.Entities
{
    public class BuildClass
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public bool PrimaryKey { get; set; }
        //public bool ForeignKey { get; set; }
        public int? MinLength { get; set; }
        public int? MaxLength { get; set; }
        public bool NotNull { get; set; } = false;
    }
}
