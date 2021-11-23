using DAE.BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAE.HexSystem
{
    abstract class ActionBase : ICheckPosition
    {
        public bool CanExecute(Board<Position, ICard> board, Grid<Position> grid, ICard piece)
        {
            return true;
        }

        public void ExecuteMove(Board<Position, ICard> board, Grid<Position> grid, ICard piece, Position position)
        {
            if (board.TryGetPieceAt(position, out var toPiece))
                board.Take(toPiece);

            board.Move(piece, position);
        }

        public void ExecuteAttack(Board<Position, ICard> board, Grid<Position> grid, ICard piece, Position position)
        {
            if (board.TryGetPieceAt(position, out var toPiece))
                board.Take(toPiece);           
        }

        public abstract List<Position> Positions(Board<Position, ICard> board, Grid<Position> grid, ICard piece);

    }
}
