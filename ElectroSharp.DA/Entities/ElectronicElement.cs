using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroSharp.DA.Entities
{
    public class ElectronicElement
    {
        public int Id { get; set; }  
        public string Type { get; set; }
        public int ElementValue { get; set; }

        [NotMapped]
        public string ConnectedType { get; set; }                 
        [NotMapped]
        public int X{ get; set; }           
        [NotMapped]
        public int Y{ get; set; }       
        [NotMapped]
        public string ImagePath{ get; set; }
        


    }
}
