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
        [SerializeField]
        private List<Card> _currentDeckList;
        [SerializeField]
        private List<Card> _startingDeckList;
        [SerializeField]
        private List<Card> _cardList;
      


        //shuffle shit, generate new deck etc
        public int DeckSize => _decksize;
        public List<Card> CurrentDeckList => _currentDeckList;
        public List<Card> StartingDecklist => _startingDeckList;
        public List<Card> CardList => _cardList;

     
        public void GenerateDeck()        {           

            List<Card> tempdeck = new List<Card>();

            for (int i = 0; i < DeckSize; i++)
            {
                var randomnum = Random.Range(0, _cardList.Count);
                tempdeck.Add(_cardList[randomnum]);
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

