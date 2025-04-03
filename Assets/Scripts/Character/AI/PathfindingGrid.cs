using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile {
    public Vector2Int gridPosition;
    public Vector3 worldPosition;
    public bool walkable;
    public GridTile parent;
    public LayerMask levelMask;

    public int gCost;
    public int hCost;
    public int fCost {
        get {
            return gCost + hCost;
        }
    }

    public GridTile(Vector2Int gridPosition, float tileSize){
        this.gridPosition = gridPosition;
        worldPosition = new Vector3((gridPosition.x * tileSize) + tileSize / 2, (gridPosition.y * tileSize) + tileSize / 2, 0);
        walkable = !Physics2D.OverlapCircle(worldPosition, tileSize / 8, ~(1 << levelMask));
    }

    public static int Distance(GridTile a, GridTile b){
        int dstX = Mathf.Abs(a.gridPosition.x - b.gridPosition.x);
        int dstY = Mathf.Abs(a.gridPosition.y - b.gridPosition.y);

        if(dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        else
            return 14 * dstX + 10 * (dstY - dstX);
    }

    public static float DiagonalDistance(Vector2 a, Vector2 b){
        float dx = b.x - a.x;
        float dy = b.y - a.y;
        return Mathf.Max(Mathf.Abs(dx), Mathf.Abs(dy));
    }
}

public class PathfindingGrid : MonoBehaviour
{
    public Vector2Int gridSize;
    public float tileSize;
    public GridTile[,] tiles;
    public LayerMask unwalkableMask;

    public static PathfindingGrid instance;

    void Awake(){
        if(instance == null) instance = this;
        else Destroy(this);
        InitializeGrid();
    }

    void Start(){
    }

    void InitializeGrid(){
        tiles = new GridTile[gridSize.x, gridSize.y];
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                tiles[x, y] = new GridTile(new Vector2Int(x, y), tileSize);
            }
        }
    }

    public static List<GridTile> GetAdjacentTiles(Vector2Int tileGridPos){
        GridTile tile = instance.tiles[tileGridPos.x, tileGridPos.y];
        List<GridTile> adjacencies = new List<GridTile>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if(x == 0 && y == 0) continue;
                if(x != 0 && y != 0) continue;

                int checkX = tile.gridPosition.x + x;
                int checkY = tile.gridPosition.y + y;

                if(checkX >= 0 && checkX < instance.gridSize.x && checkY >= 0 && checkY < instance.gridSize.y){
                    adjacencies.Add(instance.tiles[checkX, checkY]);
                }
            }
        }
        return adjacencies;
    }

    public static GridTile GetTileAtVector2Int(Vector2Int pos){
        return instance.tiles[pos.x, pos.y];
    }

    public static GridTile GetTileAtWorldPosition(Vector2 pos){
        float x = Mathf.Floor(pos.x);
        float y = Mathf.Floor(pos.y);
        if(x < 0 || x >= instance.gridSize.x) return null;
        if(y < 0 || y >= instance.gridSize.y) return null;
        return instance.tiles[(int)x, (int)y];
    }

    void OnDrawGizmos(){
        if(tiles != null){
            foreach(GridTile tile in tiles){
                Gizmos.color = tile.walkable ? new Color(0, 1, 0, 0.15f) : new Color(1, 0, 0, 0.15f);
                Gizmos.DrawCube(tile.worldPosition, new Vector3(tileSize / 4, tileSize / 4, 0.2f));
            }
        }
    }
}
