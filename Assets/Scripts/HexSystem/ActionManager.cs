using DAE.BoardSystem;
using DAE.HexSystem.Moves;
using DAE.Commons;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DAE.HexSystem
{
    public class ActionManager
    {
        private MultiValueDictionary<CardType, ICheckPosition> _actions = new MultiValueDictionary<CardType, ICheckPosition>();

        private readonly Board<Position, ICard> _board;
        private readonly Grid<Position> _grid;

        public ActionManager(Board<Position, ICard> board, Grid<Position> grid/*deck?*/)
        {
            _board = board;
            _grid = grid;

            InitializeMoves();

        }

        private void InitializeMoves()
        {
            //_actions.Add(CardType.Beam, new ConfigurableAction((Board<Position, IPiece> b, Grid<Position> g, IPiece p) =>
            //new ActionHelper(b, g, p).Direction0(1).Collect()));

            _actions.Add(CardType.Beam, new ConfigurableAction((b, g, p)
            => new ActionHelper(b, g, p).Direction0()
                                        .Direction1()
                                        .Direction2()
                                        .Direction3()
                                        .Direction4()
                                        .Direction5()
                                        .Collect()));

            _actions.Add(CardType.Thunderclap, new ConfigurableAction((b, g, p)
           => new ActionHelper(b, g, p).Direction0(1)
                                       .Direction1(1)
                                       .Direction2(1)
                                       .Direction3(1)
                                       .Direction4(1)
                                       .Direction5(1)
                                       .Collect()));

            _actions.Add(CardType.Cleave, new ConfigurableAction((b, g, p)
           => new ActionHelper(b, g, p).Direction0(1)
                                       .Direction1(1)
                                       .Direction2(1)                                     
                                       .Collect()));

            _actions.Add(CardType.Teleport, new ConfigurableAction((b, g, p)
                => new ActionHelper(b, g, p).Collect()));

            //_actions.Add(PieceType.Enemy, new ConfigurableAction((b, g, p)
            //=> new ActionHelper(b, g, p).North(1).NorthEast(1)
            //                                       .South(1)
            //                                       .SouthEast(1)
            //                                       .East(1)
            //                                       .West(1)
            //                                       .SouthWest(1)
            //                                       .NorthWest(1)
            //                                       .Collect()));
        }


        public List<Position> ValidPisitionsFor(ICard card)
        {
            return _actions[card.CardType]
                .Where(m => m.CanExecute(_board, _grid, card))
                .SelectMany(m => m.Positions(_board, _grid, card))
                .ToList();
        }

        public void Move(ICard card, Position position)
        {
            _actions[card.CardType]
            .Where(m => m.CanExecute(_board, _grid, card))
            .First(m => m.Positions(_board, _grid, card).Contains(position))
            .ExecuteMove(_board, _grid, card, position);
        }

        public void Attack(ICard card, Position position)
        {
            _actions[card.CardType]
            .Where(m => m.CanExecute(_board, _grid, card))
            .First(m => m.Positions(_board, _grid, card).Contains(position))
            .ExecuteAttack(_board, _grid, card, position);
        }
    }

}