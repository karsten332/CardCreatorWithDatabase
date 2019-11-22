using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardCreatorDatabase.Domain;

namespace CardCreatorFin.Model
{
   public class DataModel
    {
        public DataModel()
        {
            NameText = "Hello";
            CreateTypeNameText = "Enter Type Name"; 
        }

        // Create type 
        public string CreateTypeNameText { get; set; }
        
        public string NameText { get; set; }

        public int AttackText { get; set; }
        public int HpText { get; set; }
        public int ManaCostText { get; set; }
        public int PowerLevelText { get; set; }



    }
}
