using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Tilemaps;

public class MS : MonoBehaviour
{
    public TileBase[] modules;
    public int[] tileWeights;

    public mapCell[][] map;

    public int seed;

    public int mapSize = 64;

    public static MS Instance;

    public Tilemap tilemap;

    private void Awake()
    {
        Random.InitState(seed);
    }

    // Start is called before the first frame update
    void Start()
    {
        //Setup
        Instance = this;

        map = new mapCell[mapSize][];

        for(int i = 0; i < map.Length; i++)
        {
            map[i] = new mapCell[map.Length];

            for(int j = 0; j < map[i].Length; j++) 
            {
                map[i][j] = new mapCell();
            }
        }

        
        while(!areAllCellsAssigned())
        {
            //Cell Selection
            List<mapCell> possibleCells = CellsWithMinimumChooseThingy();

            mapCell cell = possibleCells[Mathf.FloorToInt(Random.value * possibleCells.Count)];

            //Collapse Cell
            int[] weigthedArray = getWeigthedArray(cell.possibleCells);

            cell.assignedTile = weigthedArray[Mathf.FloorToInt(Random.value * weigthedArray.Length)];

            //Propagation
        }

        //Apply array to Tilemap
        for (int i = -(map.Length/2); i < map.Length/2; i++)
        {
            for (int j = -(map[i+(map.Length / 2)].Length / 2); j < map[i+ (map.Length / 2)].Length / 2; j++)
            {
                tilemap.SetTile(new Vector3Int(i, j, 0), modules[map[i + (map.Length / 2)][j + (map[i + (map.Length / 2)].Length / 2)].assignedTile]);
            }
        }
    }

    public List<mapCell> CellsWithMinimumChooseThingy()
    {
        List<mapCell> result = new List<mapCell>();

        mapCell cell = map[Mathf.FloorToInt(Random.value * map.Length)][Mathf.FloorToInt(Random.value * map.Length)];

        while (cell.assignedTile >= 0)
        {
            cell = map[Mathf.FloorToInt(Random.value * map.Length)][Mathf.FloorToInt(Random.value * map.Length)];
        }

        result.Add(cell);

        return result;
    }

    public bool areAllCellsAssigned()
    {
        for (int i = 0; i < map.Length; i++)
        {
            for (int j = 0; j < map[i].Length; j++)
            {
                if (map[i][j].assignedTile < 0)
                    return false;
            }
        }

        return true;
    }

    public int[] getWeigthedArray(List<int[]> ew)
    {
        int[] result;

        int totalWeight = 0;
        for(int i = 0; i < ew.Count; i++)
        {
            totalWeight += ew[i][1];
        }
        result = new int[totalWeight];

        List<int> llist = new List<int>();
        for (int i = 0; i < ew.Count; i++)
        {
            for(int j = 0; j < ew[i][1]; j++)
            {
                llist.Add(ew[i][0]);
            }
        }
        result = llist.ToArray();

        return result;
    }


    public class mapCell
    {
        public int assignedTile = -1;
        public List<int[]> possibleCells = new List<int[]>();

        public mapCell()
        {
            possibleCells.Add(new int[] { 0, MS.Instance.tileWeights[0] });
            possibleCells.Add(new int[] { 1, MS.Instance.tileWeights[1] });
            possibleCells.Add(new int[] { 2, MS.Instance.tileWeights[2] });
            possibleCells.Add(new int[] { 3, MS.Instance.tileWeights[3] });
            possibleCells.Add(new int[] { 4, MS.Instance.tileWeights[4] });
        }

        public class weightedTile
        {
            public int tileID, weight;
        }
    }
}
