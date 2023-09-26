using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour
{
    public enum MapObject
    {
        EndWall = 0,
        Spase = 1,
        Water = 2,
        Wall = 3,
        Floor = 4,
        Road = 5,
    }

    public const int BOARD_WIDTH = 30;
    public const int BOARD_HEIGHT = 20;

    MapObject[,] _board = new MapObject[BOARD_HEIGHT,BOARD_WIDTH];

    [SerializeField] GameObject endWall;
    [SerializeField] GameObject wall;
    [SerializeField] GameObject water;
    [SerializeField] GameObject spase;
    [SerializeField] GameObject floor;
    [SerializeField] GameObject road;

    private Mappreset Mp;
    private void MapMake()
    {
        int randam = Random.Range(0, 10);
       
        for (int y = 0; y < BOARD_HEIGHT; y++)
        {
            for (int x = 0; x < BOARD_WIDTH; x++)
            {
                switch(Mp.Makepreset(randam, y, x))
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
        Vector3 world_position = transform.position + new Vector3(pos.x * 2.0f + 1.0f, 0.0f, pos.y * 2.0f + 1.0f);

        if(_board[pos.y, pos.x] == MapObject.Wall)
                 Instantiate(wall, world_position, Quaternion.identity, transform);

        else if(_board[pos.y, pos.x] == MapObject.EndWall)
                 Instantiate(endWall, world_position, Quaternion.identity, transform);

        else if (_board[pos.y, pos.x] == MapObject.Water)
        {
            //world_position.y -= 0.5f;
            Instantiate(water, world_position, Quaternion.identity, transform);
        }

        else if (_board[pos.y, pos.x] == MapObject.Spase)
        {
            //world_position.y -= 0.5f;
            Instantiate(spase, world_position, Quaternion.identity, transform);
        }

        else if (_board[pos.y, pos.x] == MapObject.Floor)
        {
            //world_position.y -= 0.5f;
            Instantiate(floor, world_position, Quaternion.identity, transform);
        }

        else if (_board[pos.y, pos.x] == MapObject.Road)
        {
            //world_position.y -= 0.5f;
            Instantiate(road, world_position, Quaternion.identity, transform);
        }
    }

    void Start()
    {
        //Random.InitState(58);
        Mp = GameObject.Find("Preset").GetComponent<Mappreset>();
        MapMake();







    }

   
    void Update()
    {
        
    }
}
