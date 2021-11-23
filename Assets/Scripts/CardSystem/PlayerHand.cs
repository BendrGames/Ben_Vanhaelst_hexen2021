using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DAE.CardSystem
{
    public class PlayerHand<TDeck>
    {        
        public List<TDeck> PlayerDeck;

        public PlayerHand(List<TDeck> playerDeck)
        {
            PlayerDeck = playerDeck;
        }
    }
}