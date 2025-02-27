using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SamplePoint {
    public Vector2 pos;
    public float score;

    public SamplePoint(Vector2 pos){
        this.pos = pos;
    }
}


public class RingGenerator
{
    public static List<SamplePoint> GenerateSamplePoints(Vector2 origin, int ringCount, int samplePointsPerRing, float distanceOfEachRing)
    {
        List<SamplePoint> points = new();
        float step = 2 * Mathf.PI / samplePointsPerRing;
        for (int i = 1; i < ringCount + 1; i++)
        {
            for (int j = 0; j < samplePointsPerRing; j++)
            {
                Vector2 pointPos = new(Mathf.Sin(step * j), Mathf.Cos(step * j));
                pointPos *= i * distanceOfEachRing;
                pointPos += origin;
                points.Add(new SamplePoint(pointPos));
            }
        }
        return points;
    }

    public static List<SamplePoint> DiscardUnwalkablePoints(List<SamplePoint> points){
        List<SamplePoint> walkablePoints = new();
        Tilemap floorMap = GameManager.instance.floorTileMap;
        foreach (SamplePoint point in points)
        {
            TileBase tileMapTile = floorMap.GetTile(floorMap.WorldToCell(point.pos));
            GridTile gridTile = PathfindingGrid.GetTileAtWorldPosition(point.pos);
            if(tileMapTile == null || gridTile == null) continue;
            if(gridTile.walkable){
                walkablePoints.Add(point);
            }
        }
        return walkablePoints;
    }
}