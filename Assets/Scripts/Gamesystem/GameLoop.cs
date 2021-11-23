using DAE.BoardSystem;
using DAE.SelectionSystem;
using DAE.CardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
//using DAE.HexSystem.Cards;
//using DAE.HexSystem;

namespace DAE.Gamesystem
{

    class GameLoop : MonoBehaviour
    {
        [SerializeField]
        private PositionHelper _positionHelper;

        [SerializeField]
        private Transform _boardParent;

        private SelectionManager<Piece> _selectionmanagerPiece;
        private SelectionManager<Tile> _selectionmanagerTile;
        private Grid<Tile> _grid;
        private Board<Tile, Piece> _board;

        private PlayerHand<Deck> _playerhand;

        //[SerializeField]
        //private DeckObject _mydeckObject;

        public void Start()
        {
            //_playerhand = new PlayerHand<DeckObject>(_mydeckObject);

            _grid = new Grid<Tile>(3, 3);
            ConnectGrid(_grid);
            _board = new Board<Tile, Piece>();

            _selectionmanagerPiece = new SelectionManager<Piece>();
            ConnectPiece(_selectionmanagerPiece, _grid, _board);
            InitializePieceSelection();

            _selectionmanagerTile = new SelectionManager<Tile>();
            ConnectTile(_selectionmanagerTile, _grid, _board);
            InitializeTileSelection();
        }

        private void InitializeTileSelection()
        {
            _selectionmanagerTile.Selected += (s, e) =>
            {
                //if (_board.TryGetPieceAt(e.SelectableItem, out var piece))
                //{
                //    Debug.Log($"Piece {e.SelectableItem} on tile {piece.gameObject.name}");
                //}
                Debug.Log($"tile {e.SelectableItem.name}");

                e.SelectableItem.Highlight = true;
            };

            _selectionmanagerTile.DeSelected += (s, e) =>
            {
                // dehighlight
                e.SelectableItem.Highlight = false;
            };
        }

        private void InitializePieceSelection()
        {
            _selectionmanagerPiece.Selected += (s, e) =>
            {
                if (_board.TryGetPositionOf(e.SelectableItem, out var tile))
                {
                    Debug.Log($"Piece {e.SelectableItem} on tile {tile.gameObject.name}");
                }
                e.SelectableItem.Highlight = true;
            };

            _selectionmanagerPiece.DeSelected += (s, e) =>
            {
                // dehighlight
                e.SelectableItem.Highlight = false;
            };
        }

        public void DeselectAll()
        {
            _selectionmanagerPiece.DeselectAll();
            _selectionmanagerTile.DeselectAll();
        }

        private void ConnectGrid(Grid<Tile> grid)
        {
            var tiles = FindObjectsOfType<Tile>();

            foreach (var tile in tiles)
            {
                var (x, y) = _positionHelper.ToGridPosition(_grid, _boardParent, tile.transform.position);

                Debug.Log($"value of tile { tile.name} is X: {x} and y: {y}");


                grid.Register(x, y, tile);

                tile.gameObject.name = $"tile {x},{y}";
            }
        }

        private void ConnectPiece(SelectionManager<Piece> selectionmanager, Grid<Tile> grid, Board<Tile,Piece> board)
        {
            var pieces = FindObjectsOfType<Piece>();
            foreach (var piece in pieces)
            {                
                var (x, y) = _positionHelper.ToGridPosition(_grid, _boardParent, piece.transform.position);
                if (grid.TryGetPositionAt(x,y, out var tile))
                {                    
                    piece.Clicked += (s, e) => selectionmanager.Toggle(s as Piece);
                    board.Place(piece, tile);
                } 
            }
        }

        private void ConnectTile(SelectionManager<Tile> selectionmanager, Grid<Tile> grid, Board<Tile, Piece> board)
        {
            var Tiles = FindObjectsOfType<Tile>();
            foreach (var found in Tiles)
            {
                Debug.Log("found");

                var (x, y) = _positionHelper.ToGridPosition(_grid, _boardParent, found.transform.position);
                if (grid.TryGetPositionAt(x, y, out var tile))
                {
                    Debug.Log("registered");
                    found.Clicked += (s, e) => selectionmanager.Toggle(s as Tile);
                }


                //if (grid.TryGetPositionAt(x, y, out var tile))
                //{
                //    tile.Clicked += (s, e) => selectionmanager.Toggle(s as Tile);
                //    //board.Place(tile, tile);
                //}
            }
        }

    }
}

