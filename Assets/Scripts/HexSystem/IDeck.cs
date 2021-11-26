using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAE.HexSystem
{
    public interface IDeck<TCard>
    {
        int DeckSize { get; }
        public List<TCard> CardList { get; }
        public Stack<TCard> CurrentDeckList {get;}
        public Stack<TCard> StartingDecklist { get; }

        public void GenerateDeck();
        public List<TCard> ShuffleDeck();
        public List<TCard> ReShuffleDeck();

    }
}
