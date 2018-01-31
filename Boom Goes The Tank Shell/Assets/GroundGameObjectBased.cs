using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGameObjectBased : MonoBehaviour {

    [SerializeField] private GameObject prefab;
    [SerializeField] private float localSize = 0.25f;
    [SerializeField] private float terrainRangeWidth = 10.0f;
    [SerializeField] private float terrainRangeHeight = 2.0f;
    [SerializeField] private bool drawGizmos = true;
    private float tilesNeededWidth = 0;
    private float tilesNeededHeight = 0;

    GameObject[,] tileGrid;

    // Use this for initialization
    void Start () {
        tilesNeededWidth = terrainRangeWidth / localSize;
        tilesNeededHeight = terrainRangeHeight / localSize;

        tileGrid = new GameObject[(int)tilesNeededWidth, (int)tilesNeededHeight];
        StartCoroutine(Spawn());
        //for (int y = 0; y < tilesNeededHeight; y++)
        //{
        //    for (int x = 0; x < tilesNeededWidth; x++)
        //    {
        //        GameObject go = Instantiate(prefab);
        //        prefab.transform.localScale = new Vector3(localSize, localSize, localSize);
        //        prefab.transform.position = new Vector3(x * localSize - (terrainRangeWidth / 2), (y * -1) * localSize, 0);
        //        go.transform.SetParent(transform);                
        //        tileGrid[x, y] = go;
        //    }
        //}
    }

    private IEnumerator Spawn()
    {
        int y = 0;
        int x = 0;

        while(y < tilesNeededHeight)
        {
            while(x < tilesNeededWidth)
            {
                GameObject go = Instantiate(prefab);
                go.transform.localScale = new Vector3(localSize, localSize, localSize);
                go.transform.position = new Vector3(x * localSize - (terrainRangeWidth / 2), (y * -1) * localSize, 0);
                go.transform.SetParent(transform);
                tileGrid[x, y] = go;
                x++;
                yield return null;
            }
            y++;
            x = 0;
            yield return null;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
