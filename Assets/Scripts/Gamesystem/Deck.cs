using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DAE.HexSystem;

namespace DAE.Gamesystem
{
    public class Deck : MonoBehaviour, IDeck
    {


        //shuffle shit, generate new deck etc
        public int DeckSize => throw new System.NotImplementedException();

        public List<ICard> DeckList => throw new System.NotImplementedException();

        public List<ICard> CardList => throw new System.NotImplementedException();

        public List<ICard> GenerateDeck()
        {
            throw new System.NotImplementedException();
        }

        public List<ICard> ReShuffleDeck()
        {
            return null;
        }

        public List<ICard> ShuffleDeck()
        {
            throw new System.NotImplementedException();
        }
    }
}

