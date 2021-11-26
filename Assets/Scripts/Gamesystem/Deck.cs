using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DAE.HexSystem;
using System.Linq;


namespace DAE.Gamesystem
{
    public class Deck : MonoBehaviour, IDeck<Card>
    {
        [SerializeField]
        private int _decksize;
        private Stack<Card> _currentDeckList;
        private Stack<Card> _startingDeckList;
        [SerializeField]
        private List<Card> _cardList;
      


        //shuffle shit, generate new deck etc
        public int DeckSize => _decksize;
        public Stack<Card> CurrentDeckList => _currentDeckList;
        public Stack<Card> StartingDecklist => _startingDeckList;
        public List<Card> CardList => _cardList;

        public Deck(int decksize, List<Card> cardList)
        {
            _decksize = decksize;
            _cardList = cardList;
        }

        public void GenerateDeck()
        {
            Stack<Card> tempdeck = new Stack<Card>();

            for (int i = 0; i < DeckSize -1; i++)
            {
                var randomnum = Random.Range(0, _cardList.Count);
                tempdeck.Push(_cardList[randomnum]);
            }

            _currentDeckList = tempdeck;
            _startingDeckList = tempdeck;           
        }

        public List<Card> ReShuffleDeck()
        {
            return _startingDeckList.OrderBy(x => Random.value).ToList();
        }

        public List<Card> ShuffleDeck()
        {
            return _currentDeckList.OrderBy(x => Random.value).ToList();
        }
    }
}

