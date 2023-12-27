using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMakeManager : MonoBehaviour
{
    const int BOARD_WIDTH = 30;
    const int BOARD_HEIGHT = 20;

    [Space]
    [Header("")]
    [SerializeField] GameObject Deta;

    private MapDeta D_Map;







    public enum MapObject
    {
        EndWall = 0,
        Spase = 1,
        Water = 2,
        Wall = 3,
        Floor = 4,
        Road = 5,
    }
    MapObject[,] _board = new MapObject[BOARD_HEIGHT, BOARD_WIDTH];

    [Header("Map_Object")]
    [Space]
    [SerializeField] GameObject endWall;
    [SerializeField] GameObject wall;
    [SerializeField] GameObject water;
    [SerializeField] GameObject spase;
    [SerializeField] GameObject floor;
    [SerializeField] GameObject road;

    
    private void MapMake()
    {
        int randam = Random.Range(0, 10);
       
        for (int y = 0; y < BOARD_HEIGHT; y++)
        {
            for (int x = 0; x < BOARD_WIDTH; x++)
            {
                switch(D_Map.MakeMapDeta(randam, y, x))
                {
                    case 0:
                        _board[y, x] = MapObject.EndWall;
                        break;
                    case 1:
                        _board[y, x] = MapObject.Spase;
                        break;
                    case 2:
                        _board[y, x] = MapObject.Water;
                        break;
                    case 3:
                        _board[y, x] = MapObject.Wall;
                        break;
                    case 4:
                        _board[y, x] = MapObject.Floor;
                        break;
                    case 5:
                        _board[y, x] = MapObject.Road;
                        break;

                }
                    
                if (_board[y, x] == MapObject.Wall && Random.Range(0, 10) < 2)
                    _board[y, x] = MapObject.Water;

                else if(_board[y, x] == MapObject.Wall && Random.Range(0, 10) == 9)
                    _board[y, x] = MapObject.Spase;

                SetObject(new Vector2Int(x, y));
            }
        }
    }

    public void SetObject(Vector2Int pos)
    {
        Vector3 Map_Object_position = transform.position + new Vector3(pos.x * 2.0f + 1.0f, 0.0f, pos.y * 2.0f + 1.0f);

        if(_board[pos.y, pos.x] == MapObject.Wall)
                 Instantiate(wall, Map_Object_position, Quaternion.identity, transform);

        else if(_board[pos.y, pos.x] == MapObject.EndWall)
                 Instantiate(endWall, Map_Object_position, Quaternion.identity, transform);

        else if (_board[pos.y, pos.x] == MapObject.Water)
        {
            //world_position.y -= 0.5f;
            Instantiate(water, Map_Object_position, Quaternion.identity, transform);
        }

        else if (_board[pos.y, pos.x] == MapObject.Spase)
        {
            //world_position.y -= 0.5f;
            Instantiate(spase, Map_Object_position, Quaternion.identity, transform);
        }

        else if (_board[pos.y, pos.x] == MapObject.Floor)
        {
            //world_position.y -= 0.5f;
            Instantiate(floor, Map_Object_position, Quaternion.identity, transform);
        }

        else if (_board[pos.y, pos.x] == MapObject.Road)
        {
            //world_position.y -= 0.5f;
            Instantiate(road, Map_Object_position, Quaternion.identity, transform);
        }
    }

    void Start()
    {
        //Random.InitState(58);
        D_Map = Deta.GetComponent<MapDeta>();
        MapMake();







    }

   
    void Update()
    {
        
    }
}
