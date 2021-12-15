using DAE.HexSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAE.HexSystem
{
    public interface IHand<TCard>
    {
        public int Handsize { get; }

        public List<TCard> PlayerHandCardList { get; }
        public TCard Drawcard();
        public List<TCard> DiscardCard();
        public void PlayCard();

    }
}
