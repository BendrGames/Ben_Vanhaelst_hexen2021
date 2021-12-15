using DAE.BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAE.HexSystem
{
    abstract class ActionBase<TCard, TPiece> : ICheckPosition<TCard, TPiece> where TPiece : IPiece where TCard : ICard
    {
        public virtual bool CanExecute(Board<Position, TPiece> board, Grid<Position> grid, Position position, TPiece piece, CardType card)
        {
            return true;
        }      

        public virtual void ExecuteAction(Board<Position, TPiece> board, Grid<Position> grid, Position position, TPiece piece, CardType card)
        {
            if (board.TryGetPieceAt(position, out var toPiece))
                board.Take(toPiece);

            board.Move(piece, position);
        }

        public abstract List<Position> Positions(Board<Position, TPiece> board, Grid<Position> grid, Position position, TPiece piece, CardType card);

     

    }
}
