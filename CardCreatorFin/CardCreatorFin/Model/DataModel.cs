using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardCreatorFin.Model
{
   public class DataModel
    {
        public DataModel()
        {
            NameText = "Hello";
        }
        public string NameText { get; set; }

        public string TypeText { get; set; }
        public int AttackText { get; set; }
        public int HpText { get; set; }
        public int ManaCostText { get; set; }
        public int PowerLevelText { get; set; }



    }
}
