using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardCreatorFin.Model
{
    public class TypeDataModel
    {
        public TypeDataModel()
        {
            CreateTypeNameText = "Enter Type Name";
        }

        public string CreateTypeNameText { get; set; }

        public int TypeMinStatText { get; set; }

        public int TypeMaxStatText { get; set; }
    }
}
