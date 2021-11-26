using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DAE.HexSystem;
using System.Linq;


namespace DAE.Gamesystem
{
    public class Deck : MonoBehaviour, IDeck
    {
        [SerializeField]
        private int _decksize;
        private Stack<ICard> _currentDeckList;
        private Stack<ICard> _startingDeckList;
        [SerializeField]
        [SerializeReference]
        private List<ICard> _cardList;
        [SerializeField]
        private List<Card> _cardListtest;


        //shuffle shit, generate new deck etc
        public int DeckSize => _decksize;
        public Stack<ICard> CurrentDeckList => _currentDeckList;
        public Stack<ICard> StartingDecklist => _startingDeckList;
        public List<ICard> CardList => _cardList;

        public Deck(int decksize, List<ICard> cardList)
        {
            _decksize = decksize;
            _cardList = cardList;
        }

        public void GenerateDeck()
        {
            Stack<ICard> tempdeck = new Stack<ICard>();

            for (int i = 0; i < DeckSize -1; i++)
            {
                var randomnum = Random.Range(0, _cardList.Count);
                tempdeck.Push(_cardList[randomnum]);
            }

            _currentDeckList = tempdeck;
            _startingDeckList = tempdeck;           
        }

        public List<ICard> ReShuffleDeck()
        {
            return _startingDeckList.OrderBy(x => Random.value).ToList();
        }

        public List<ICard> ShuffleDeck()
        {
            return _currentDeckList.OrderBy(x => Random.value).ToList();
        }
    }
}

