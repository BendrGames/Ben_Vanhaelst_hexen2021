using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAE.HexSystem
{
    public interface IDeck
    {
        int DeckSize { get; }
        public List<ICard> CardList { get; }
        public List<ICard> DeckList {get;}

        public List<ICard> GenerateDeck();
        public List<ICard> ShuffleDeck();
        public List<ICard> ReShuffleDeck();

    }
}
