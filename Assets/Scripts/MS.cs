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

        int x = Mathf.FloorToInt(Random.value * map.Length);
        int y = Mathf.FloorToInt(Random.value * map.Length);

        map[x][y].assignedTile = 0;
        map[Mathf.Abs(x + 1) % map.Length][y].possibleCells.Add(new int[] { 0, MS.Instance.tileWeights[0] });
        map[Mathf.Abs(x - 1) % map.Length][y].possibleCells.Add(new int[] { 0, MS.Instance.tileWeights[0] });
        map[x][Mathf.Abs(y + 1) % map.Length].possibleCells.Add(new int[] { 0, MS.Instance.tileWeights[0] });
        map[x][Mathf.Abs(y - 1) % map.Length].possibleCells.Add(new int[] { 0, MS.Instance.tileWeights[0] });



        while (!areAllCellsAssigned())
        {
            //Cell Selection
            List<mapCell> cellsWithMinimumThingy = CellsWithMinimumChooseThingy();

            mapCell cell = cellsWithMinimumThingy[Mathf.FloorToInt(Random.value * cellsWithMinimumThingy.Count)];

            bool hasChanged = false;

            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    if(map[i][j] == cell)
                    {
                        x = i;
                        y = j;

                        hasChanged = true;

                        break;
                    }
                }

                if(hasChanged)
                    break;
            }

            //Collapse Cell
            int[] weigthedArray = getWeigthedArray(cell.possibleCells);

            cell.assignedTile = weigthedArray[Mathf.FloorToInt(Random.value * weigthedArray.Length)];

            //Propagation
            if(cell.assignedTile == 0)
            {
                map[Mathf.Abs(x + 1) % map.Length][y].possibleCells.Add(new int[] { 0, MS.Instance.tileWeights[0] });
                map[Mathf.Abs(x - 1) % map.Length][y].possibleCells.Add(new int[] { 0, MS.Instance.tileWeights[0] });
                map[x][Mathf.Abs(y + 1) % map.Length].possibleCells.Add(new int[] { 0, MS.Instance.tileWeights[0] });
                map[x][Mathf.Abs(y - 1) % map.Length].possibleCells.Add(new int[] { 0, MS.Instance.tileWeights[0] });
            }
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

        for (int i = 0; i < map.Length; i++)
        {
            //Debug.Log(i);
            for (int j = 0; j < map[i].Length; j++)
            {
                if (map[i][j].assignedTile < 0)
                {
                    Debug.Log(i + " | " + j + " | " + calculateEntropy(map[i][j].possibleCells));
                    if (calculateEntropy(map[i][j].possibleCells) == calculateEntropy(result[0].possibleCells) && map[i][j] != result[0])
                    {
                        result.Add(map[i][j]);
                        Debug.Log(i + " | " + j + " | " + calculateEntropy(map[i][j].possibleCells));
                    }
                    else if (calculateEntropy(map[i][j].possibleCells) < calculateEntropy(result[0].possibleCells) && map[i][j] != result[0])
                    {
                        result.Clear();

                        result.Add(map[i][j]);

                        //i = 0;

                        Debug.Log("FINAL: " + i + " | " + j + " | " + calculateEntropy(map[i][j].possibleCells));

                        break;
                    }
                }
            }
        }

        return result;
    }

    public float calculateEntropy(List<int[]> possCells)
    {
        int totalWeight = 0;
        float result = 0;

        for (int i = 0; i < possCells.Count; i++)
        {
            totalWeight += possCells[i][1];
        }

        for (int i = 0; i < possCells.Count; i++)
        {
            float probability = (possCells[i][1] / totalWeight);
            result += probability * Mathf.Log(probability);
        }

        return -result;
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
