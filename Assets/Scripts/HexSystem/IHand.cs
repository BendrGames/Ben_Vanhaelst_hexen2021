using DAE.HexSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.HexSystem
{
    public interface IHand
    {
        public int Handsize { get; }
        public List<ICard> PlayerHand { get; }
        public List<ICard> Drawcard();
        public void PlayCard();

    }
}
