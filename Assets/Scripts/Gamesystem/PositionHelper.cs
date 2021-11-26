using DAE.BoardSystem;
using DAE.HexSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DAE.Gamesystem
{
    [CreateAssetMenu(menuName = "DAE/Position Helper")]

    public class PositionHelper : ScriptableObject
    {
        private void OnValidate()
        {
            if (TileRadius <= 0)
                _tileradius = 1;
        }

        [SerializeField]
        private float _tileradius = 1;
        public float TileRadius => _tileradius;

        public ( int x, int y) ToGridPosition(Grid<Position> grid, Transform parent, Vector3 worldPosition)
        {

            var q = ((2f / 3f) * worldPosition.x) / TileRadius;
            //var r = ((-2f / 3f) * worldPosition.x) + ((Mathf.Sqrt(3f) / 3f) * worldPosition.z) / TileRadius;
            var r2 = ((-1f / 3f) * worldPosition.x) + (Mathf.Sqrt(3f) / 3f * worldPosition.z) / TileRadius;

            var x = (int)Mathf.Round(q);
            var y = (int)Mathf.Round(r2);

            return (x, y);
        }

        public Vector3 ToWorldPosition(Grid<Position> grid, Transform parent, int x, int y)
        {

            var q = TileRadius * ((3f / 2f) * x);
            var r = TileRadius * (Mathf.Sqrt(3f) / 2f * x + Mathf.Sqrt(3f) * y);

            var tileposition = new Vector3(q, 0, r);



            return tileposition;
        }

    }

}

            //var relativePosition = worldPosition - parent.position;

            //var scaledRelativePosition = relativePosition / _tileDimension;

            //var scaledBoardOffset = new Vector3(grid.rows / 2.0f, 0, grid.columns / 2.0f);
            //scaledRelativePosition += scaledBoardOffset;

            //var scaledHalfTileOffset = new Vector3(0.5f / 2.0f, 0, 0.5f);
            //scaledRelativePosition -= scaledHalfTileOffset; 

            //var x = (int)Mathf.Round(scaledRelativePosition.x);
            //var y = (int)Mathf.Round(scaledRelativePosition.z);

            //return (x, y);
            

//var scaledRelativePosition = new Vector3(x, 0, y);

            //var scaledHalfTileOffset = new Vector3(0.5f / 2.0f, 0, 0.5f);
            //scaledRelativePosition += scaledHalfTileOffset;

            //var scaledBoardOffset = new Vector3(grid.rows / 2.0f, 0, grid.columns / 2.0f);
            //scaledRelativePosition -= scaledBoardOffset;

            //var relativePosition = scaledRelativePosition * _tileDimension;

            //var worldPosition = relativePosition + parent.position;

            //return worldPosition;