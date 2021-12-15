using DAE.BoardSystem;
using DAE.HexSystem;
using DAE.HexSystem.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAE.HexSystem.Actions
{
    class TeleportAction<TCard, TPiece> : ActionBase<TCard, TPiece> where TPiece : IPiece where TCard : ICard
    {
        public override bool CanExecute(Board<Position, TPiece> board, Grid<Position> grid, Position position, TPiece piece, CardType card)
        {
            return base.CanExecute(board, grid, position, piece, card);
        }

        public override void ExecuteAction(Board<Position, TPiece> board, Grid<Position> grid, Position position, TPiece piece, CardType card)
        {          
            board.Move(piece, position);
        }

        public override List<Position> Positions(Board<Position, TPiece> board, Grid<Position> grid, Position position, TPiece piece, CardType card)
        {
            ActionHelper<TCard, TPiece> actionHelper = new ActionHelper<TCard, TPiece>(board, grid, position, piece, card);
            actionHelper.Direction0(10)
                        .Direction1(10)
                        .Direction2(10)
                        .Direction3(10)
                        .Direction4(10)
                        .Direction5(10);


            return actionHelper.Collect();
        }
    }
}