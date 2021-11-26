using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DAE.HexSystem
{
    public interface ICard
    {
        string Name { get; }     
        
        string Description { get; }

        Color Color { get; }
        bool Played { get; }
        CardType CardType { get; }
        Texture2D CardTexture { get; }



        //bool Click { get; }

        //bool Drag { get; }



    }
}
