using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GroundTileBased : MonoBehaviour {

    [SerializeField] private float tileArea = 0.05f;
    [SerializeField] private float terrainRangeWidth = 10.0f;
    [SerializeField] private float terrainRangeHeight = 2.0f;
    [SerializeField] private bool drawGizmos = true;
    private float tilesNeededWidth = 0;
    private float tilesNeededHeight = 0;

    GroundTile[,] tileGrid;

	// Use this for initialization
	void Start () {
        tilesNeededWidth = terrainRangeWidth / tileArea;
        tilesNeededHeight = terrainRangeHeight / tileArea;

        tileGrid = new GroundTile[(int)tilesNeededWidth, (int)tilesNeededHeight];
        for (int y = 0; y < tilesNeededHeight; y++)
        {
            for (int x = 0; x < tilesNeededWidth; x++)
            {
                GroundTile tile = new GroundTile(true, new Rect((float)x * tileArea - (terrainRangeWidth / 2), (float)y * tileArea, tileArea, tileArea));
                Debug.Log(tile.area.position);
                tileGrid[x, y] = tile;
            }
        }

        Debug.Log(tileGrid.GetLength(0));
        Debug.Log(tileGrid.GetLength(1));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDrawGizmos()
    {
        if (!drawGizmos) return;

        if (!Application.isPlaying) return;

        for (int y = 0; y < tilesNeededHeight; y++)
        {
            for (int x = 0; x < tilesNeededWidth; x++)
            {
                if(tileGrid[x,y] != null)
                Gizmos.DrawSphere(tileGrid[x, y].area.position, tileArea / 2);
            }
        }
    }
}

public class GroundTile
{
    public bool isActive = true;
    public Rect area = new Rect(0.0f, 0.0f, 0.05f, 0.05f);

    public GroundTile (bool isActive, Rect area)
    {
        this.isActive = isActive;
        this.area = area;
    }
}
