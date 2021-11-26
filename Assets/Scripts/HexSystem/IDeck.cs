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
        public Stack<ICard> CurrentDeckList {get;}
        public Stack<ICard> StartingDecklist { get; }

        public void GenerateDeck();
        public List<ICard> ShuffleDeck();
        public List<ICard> ReShuffleDeck();

    }
}
