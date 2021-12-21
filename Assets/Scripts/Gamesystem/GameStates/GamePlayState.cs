using DAE.BoardSystem;
using DAE.Gamesystem;
using DAE.HexSystem;
using DAE.SelectionSystem;
using DAE.StateSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAE.GameSystem.GameStates
{
    class GamePlayState : GameStateBase
    {
        private SelectionManager<Piece> _selectionManager;
        private ActionManager<Card,Piece> _actionManager;
        private Board<Position, Piece> _board;
        private PlayerHand _playerHand;
        private Card _currentCard;
        private Deck _deck;
      

        public GamePlayState(StateMachine<GameStateBase> stateMachine, Board<Position, Piece> board, ActionManager<Card,Piece> moveManager, PlayerHand playerhand, Deck deck) : base(stateMachine)
        {
            _playerHand = playerhand;
            _deck = deck;
            _actionManager = moveManager;
            _board = board;

            _playerHand.InitializePlayerHand(_deck, 5);
            DrawCard();
            DrawCard();
            DrawCard();
            DrawCard();
            DrawCard();

        }

        private void DrawCard()
        {
            var card = _playerHand.Drawcard();
            card.BeginDrag += (s, e) =>
            {
                _currentCard = e.Card;                
            };
        }

        public override void OnEnter()
        {
           
        }

        public override void OnExit()
        {
           
        }

        internal override void Backward()
        {
            StateMachine.MoveToState(GameState.ReplayState);
        }

        //internal override void BeginDrag(Card card)
        //{
        //    base.BeginDrag(card);
        //}

        internal override void HighLightNew(Piece piece, Position position)
        {
            var positions = _actionManager.ValidPisitionsFor(piece, position, _currentCard._cardType);
            foreach (var pos in positions)
            {
                pos.Activate();
            }
        }

        internal override void OnDrop(Piece player, Position position)
        {
            var validpositions = _actionManager.ValidPisitionsFor(player, position, _currentCard._cardType);

            if (validpositions.Contains(position))
            {
                _actionManager.Action(player, position, _currentCard._cardType);                
                DrawCard();
                _currentCard.Used();
            }

            foreach (var pos in validpositions)
            {
                pos.Deactivate();
            }

            
        }

        internal override void UnHighlightOld(Piece piece, Position position)
        {
            var positions = _actionManager.ValidPisitionsFor(piece, position, _currentCard._cardType);
            foreach (var pos in positions)
            {
                pos.Deactivate();
            }
        }
      

    }
}
