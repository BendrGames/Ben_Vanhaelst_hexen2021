using DAE.HexSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAE.HexSystem
{
    public interface IHand
    {
        public int Handsize { get; }

        public List<ICard> PlayerHandCardList { get; }
        public void Drawcard();
        public List<ICard> DiscardCard();
        public void PlayCard();

    }
}
