using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAE.HexSystem
{
    public interface ICard
    {
        string Name { get; }

        bool Played { get; }

        CardType CardType { get; }

        bool Click { get; }

        bool Drag { get; }



    }
}
