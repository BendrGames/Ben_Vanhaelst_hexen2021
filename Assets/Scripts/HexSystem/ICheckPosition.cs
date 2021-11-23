using DAE.BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAE.HexSystem
{
    interface ICheckPosition
    {
        bool CanExecute(Board<Position, ICard> board, Grid<Position> grid, ICard piece);

        void ExecuteMove(Board<Position, ICard> board, Grid<Position> grid, ICard piece, Position position);

        void ExecuteAttack(Board<Position, ICard> board, Grid<Position> grid, ICard piece, Position position);

        //SpawnObject
        List<Position> Positions(Board<Position, ICard> board, Grid<Position> grid, ICard piece);
    }
}
