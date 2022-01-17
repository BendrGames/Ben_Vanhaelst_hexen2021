using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DAE.HexSystem;
using System.Linq;

using System;
using Random = UnityEngine.Random;

namespace DAE.Gamesystem
{
    

    public class Deck : MonoBehaviour, IDeck<CardData>
    {
        [SerializeField]
        private List<CardData> _currentDeckList;

        [SerializeField]
        private List<CardData> _startingDeckList;
        
        private List<CardData> _temporaryCardList;

        [SerializeField]
        private List<CardData> _playerhandList;

        [SerializeField]
        private List<CardData> _discardList;

        [SerializeField]
        private GameObject CardBase;

        public GameObject HandView;

        private List<CardData> templist = new List<CardData>();
        private List<CardData> tempDrawnlist = new List<CardData>();


        //shuffle shit, generate new deck etc
        //public int DeckSize => _decksize;
        public List<CardData> CurrentDeckList => _currentDeckList;
        public List<CardData> StartingDecklist => _startingDeckList;      

        public List<CardData> TemporaryCardsList => _temporaryCardList;

        public List<CardData> PlayerHandList => _playerhandList;

        public List<CardData> DiscardList => _discardList;



        public void DrawCard()
        {
            _playerhandList.Add(CurrentDeckList[0]);        
            CurrentDeckList.RemoveAt(0);            
        }

        public void EqualizeDecks()        
        {         
            CurrentDeckList.AddRange(StartingDecklist);
        }
        public List<CardData> ShuffleCurrentDeck()
        {
            return _currentDeckList.OrderBy(x => Random.value).ToList();
        }

        //for prototype im working on
        public List<CardData> ShuffleStartingDeck()
        {
            return _startingDeckList.OrderBy(x => Random.value).ToList();
        }

        public void ExecuteCard(Card cardo)
        {
            _playerhandList.Remove(cardo.CardData);
            _discardList.Add(cardo.CardData);
            _playerhandList.Add(CurrentDeckList[0]);            
            CurrentDeckList.RemoveAt(0);

            cardo.Used();
            ClearHandGO();
            InstantiateHandGOs();        

        }

        public void ClearHandGO()
        {
            int childs = HandView.transform.childCount;
            for (int i = childs -1 ; i >= 0; i--)
            {
                Destroy(HandView.transform.GetChild(i).gameObject);
            }
                    
        }

        public void InstantiateHandGOs()
        {
            foreach (var handCard in _playerhandList)
            {
                var cardobject = Instantiate(CardBase, HandView.transform);                
                cardobject.GetComponent<Card>().InitializeCard(handCard);
            }

        }

      

    }


}

