using DAE.BoardSystem;
using DAE.ReplaySystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAE.HexSystem
{
    abstract class ActionBase<TCard, TPiece> : ICheckPosition<TCard, TPiece> where TPiece : IPiece where TCard : ICard
    {
        protected ReplayManager ReplayManager;

        protected ActionBase(ReplayManager replayManager)
        {
            ReplayManager = replayManager;
        }

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

        public abstract List<Position> ValidPositionsCalc(Board<Position, TPiece> board, Grid<Position> grid, Position position, TPiece piece, CardType card);

     

    }
}
