using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace DAE.HexSystem.Cards
{
    [CreateAssetMenu(menuName = "DAE/Card")]
    public class CardObject : ScriptableObject
    {
        public string Name;

        public Texture2D AbilityImage;

        public CardType cardType;
    }
}
