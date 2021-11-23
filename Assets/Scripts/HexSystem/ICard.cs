using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAE.HexSystem
{
    public interface ICard
    {
        int player { get; }

        string Name { get; }

        bool Moved { get; }


        //prob dont need this anymore, for now
        //PieceType PieceType { get; }
        CardType CardType { get; }
        
        //object transform { get; }
    }
}
