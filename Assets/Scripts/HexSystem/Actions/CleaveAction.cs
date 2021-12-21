﻿using DAE.BoardSystem;
using DAE.HexSystem;
using DAE.HexSystem.Actions;
using DAE.ReplaySystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAE.HexSystem.Actions
{

    class CleaveAction<TCard, TPiece> : ActionBase<TCard, TPiece> where TPiece : IPiece where TCard : ICard
    {

        public CleaveAction(ReplayManager replayManager) : base(replayManager)
        {
        }

        public bool DisplayFullSelection;

        public override bool CanExecute(Board<Position, TPiece> board, Grid<Position> grid, Position position, TPiece piece, CardType card)
        {
            if (ValidPositionsCalc(board, grid, position, piece, card).Contains(position))
            {
                DisplayFullSelection = true;
                return true;
            }
            else
            {
                DisplayFullSelection = false;
                return true;
            }
        }

        public override void ExecuteAction(Board<Position, TPiece> board, Grid<Position> grid, Position position, TPiece piece, CardType card)
        {
            foreach (var hex in ValidPositionsCalc(board, grid, position, piece, card))
            {
                if (board.TryGetPieceAt(hex, out var enemy))
                {
                                       
                    board.TryGetPositionOf(piece, out var positionPlayer);
                    grid.TryGetCoordinateOf(positionPlayer, out var coordinate);
                    var playerX = coordinate.x;
                    var playerY = coordinate.y;
                    board.TryGetPositionOf(enemy, out var positionenemy);
                    grid.TryGetCoordinateOf(positionenemy, out var coordinateenemy);
                    var enemyX = coordinateenemy.x;
                    var enemyY = coordinateenemy.y;

                    var directionX = enemyX - playerX;
                    var directionY = enemyY - playerY;

                    var nexPositionenemyX = directionX + enemyX;
                    var nexPositionenemyY = directionY + enemyY;

                    if(!grid.TryGetPositionAt(nexPositionenemyX, nexPositionenemyY, out var enemyNextPosition))
                    {
                        board.Take(enemy);
                    }
                    
                    if (!board.TryGetPieceAt(enemyNextPosition, out var pieceInTheWay))
                    {
                        board.Take(enemy);
                        board.Place(enemy, enemyNextPosition);                       
                    }
                    else
                    {
                        board.Take(enemy);
                        board.Place(enemy, position);
                    }
                   


                    //ActionHelper<TCard, TPiece> actionHelper = new ActionHelper<TCard, TPiece>(board, grid, position, piece, card);
                    //actionHelper.GetNextDirectionDown();

                  
                }
            }
        }

        public override List<Position> ValidPositionsCalc(Board<Position, TPiece> board, Grid<Position> grid, Position position, TPiece piece, CardType card)
        {
            ActionHelper<TCard, TPiece> actionHelper = new ActionHelper<TCard, TPiece>(board, grid, position, piece, card);
            actionHelper.Direction0(1)
                        .Direction1(1)
                        .Direction2(1)
                        .Direction3(1)
                        .Direction4(1)
                        .Direction5(1);

            ActionHelper<TCard, TPiece> actionHelperPartual = new ActionHelper<TCard, TPiece>(board, grid, position, piece, card);
            actionHelperPartual.TargetedPlusSides(1)
                        .TargetedPlusSides1(1)
                        .TargetedPlusSides2(1)
                        .TargetedPlusSides3(1)
                        .TargetedPlusSides4(1)
                        .TargetedPlusSides5(1);


            if (!DisplayFullSelection)
                return actionHelper.Collect();

            else
                return actionHelperPartual.Collect();

        }
    }
}