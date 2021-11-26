using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAE.HexSystem
{
    public interface IPiece
    {
        int PlayerID { get; }

        //string Name { get; }

        bool Moved { get; }

        player PieceType { get; }
        //object transform { get; }
    }
}
