using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerScript : MonoBehaviour
{
    public TextAsset levelJSON;

    [System.Serializable]
    public class JsonTile
    {
        public int x;
        public int y;
        public string code;
    }

    [System.Serializable]
    public class TileList
    {
        public JsonTile[] tile;
    }

    public TileList tileList = new TileList();


    public GameObject rockContainer;
    public GameObject monsterContainer;
    public GameObject trapContainer;
    public GameObject UnpassableTileContainer;

    public GameObject TrapPrefab;
    public GameObject RockPrefab;
    public GameObject MonsterPrefab;
    public GameObject UnpassableTilePrefab;

    //one instance of earch
    public GameObject door_part1;
    public GameObject door_part2;
    public GameObject dwarfAltar;
    public GameObject humanAltar;
    public GameObject giantAltar;

    public float tileSize;
    public float tileOffset;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("1: \n" + levelJSON.text);
        //GameObject g2 = Instantiate(Obstacle, new Vector3(9, 8, 9), Quaternion.identity);
        tileList = JsonUtility.FromJson<TileList>(levelJSON.text);

        Debug.Log("TileList: \n" + tileList);

        GenerateLevel();

    }



    void GenerateLevel()
    {
        foreach(JsonTile t in tileList.tile)
        {
            GameObject go = null, container = null;
            Quaternion q = Quaternion.identity;
            switch (t.code)
            {
                case "T":
                    go = TrapPrefab;
                    container = trapContainer;
                    go.transform.localScale = new Vector3(2, 2, 2);
                    Instantiate(go, new Vector3(t.y * (tileSize + tileOffset), 0, t.x * (tileSize + tileOffset)), q, container.transform);
                    break;

                case "R":
                    go = RockPrefab;
                    go.transform.localScale = new Vector3(15, 15, 15);
                    container = rockContainer;
                    Instantiate(go, new Vector3(t.y * (tileSize + tileOffset), 0, t.x * (tileSize + tileOffset)), q, container.transform);
                    break;

                case "M":
                    go = MonsterPrefab;
                    go.transform.localScale = new Vector3(3, 3, 3);
                    q = new Quaternion(0, 250, 0, 1);
                    container = monsterContainer;
                    Instantiate(go, new Vector3(t.y * (tileSize + tileOffset), 0, t.x * (tileSize + tileOffset)), q, container.transform);
                    break;

                case "U":
                    go = UnpassableTilePrefab;
                    go.transform.localScale = new Vector3(3, 3, 3);
                    container = UnpassableTileContainer;
                    Instantiate(go, new Vector3(t.y * (tileSize + tileOffset), 0, t.x * (tileSize + tileOffset)), q, container.transform);
                    break;

                case "HA":
                    humanAltar.SetActive(true);
                    humanAltar.transform.position = new Vector3(t.y * (tileSize + tileOffset), 0, t.x * (tileSize + tileOffset));
                    break;

                case "GA":
                    giantAltar.SetActive(true);
                    giantAltar.transform.position = new Vector3(t.y * (tileSize + tileOffset), 0, t.x * (tileSize + tileOffset));
                    break;

                case "DA":
                    dwarfAltar.SetActive(true);
                    dwarfAltar.transform.position = new Vector3(t.y * (tileSize + tileOffset), 0, t.x * (tileSize + tileOffset));
                    break;

                case "G":
                    door_part1.SetActive(true);
                    door_part2.SetActive(true);
                    door_part1.transform.position = new Vector3(t.y * (tileSize + tileOffset), 0, t.x * (tileSize + tileOffset));
                    door_part2.transform.position = door_part1.transform.position + new Vector3(0, 0, 0.9f);
                    break;



            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
