using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardCreatorDatabase.Domain
{
    public class Card
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageURL { get; set; }

        public int TypeId { get; set; }

        public int ManaCost { get; set; }

        public int AttackPower { get; set; }

        public int Hp { get; set; }

        // PowerLevel = -1;//PowerLevel = (AttackPower + Hp) % ManaCost;
        public int PowerLevel { get; set; }


        // BattleCry, spesial effect, DeathRattle, Image
    }
}
