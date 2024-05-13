using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class MapMakeManager : MonoBehaviour
{
    const int BOARD_WIDTH = 30;
    const int BOARD_HEIGHT = 20;

    [Space]
    [Header("Important_Object")]
    [SerializeField] GameObject Deta;
    [SerializeField] GameObject Player;
    private PlayerScript P_Scr;
    //private MapDeta D_Map;
    public MapDataBase Map_Data;
    public EnemyDataBase Enemy_Data;
    private class Floor_Deta
    {
        int _Sta_Point_X;
        int _Sta_Point_Y;
        int _Ran_Point_X;
        int _Ran_Point_Y;

        public Floor_Deta()//(int sta_x, int sta_y, int ran_x, int ran_y)
        {
            //_Sta_Point_X = sta_x;
            //_Sta_point_Y = sta_y;
            //_Ran_point_X = ran_x;
            //_Ran_point_Y = ran_y;
        }
      
        public int Get_sta_x() { return _Sta_Point_X; }
        public int Get_sta_y() { return _Sta_Point_Y; }
        public int Get_ran_x() { return _Ran_Point_X; }
        public int Get_ran_y() { return _Ran_Point_Y; }

        public void Set_sta_x(int sta_x) { _Sta_Point_X = sta_x; }
        public void Set_sta_y(int sta_y) { _Sta_Point_Y = sta_y; }
        public void Set_ran_x(int ran_x) { _Ran_Point_X = ran_x; }
        public void Set_ran_y(int ran_y) { _Ran_Point_Y = ran_y; }
    }
    private Floor_Deta[] floor_Deta;
    private int Floor_Menber;

    enum MapObject
    {
        EndWall = 0,
        Spase = 1,
        Water = 2,
        Wall = 3,
        Floor = 4,
        Road = 5,
    }
    Map_Object[,] _board_Map = new Map_Object[BOARD_HEIGHT, BOARD_WIDTH];

    enum ItemObject
    {
        None = 0,
        Exist = 1,
        Gate = 2,
        Trap = 3,
    }
    ItemObject[,] _board_Item = new ItemObject[BOARD_HEIGHT, BOARD_WIDTH];

    enum CharaObject
    {
        None = 0,
        Player = 1,
        Enemy = 2,
    }
    CharaObject[,] _board_Chara = new CharaObject[BOARD_HEIGHT, BOARD_WIDTH];

    public class Enemy_stetas
    {
        public Vector2Int pos;
        public int ID;
        public int HP;
        public GameObject Object;
    }
    Enemy_stetas[]enemy_Stetas = new Enemy_stetas[30];
    int EnemyValue = 0;



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
                _board_Item[y, x] = ItemObject.None;
                _board_Chara[y, x] = CharaObject.None;               
                _board_Map[y, x] = Map_Data.GetMapData(0, y, x);
 
                SetObject_Map(new Vector2Int(x, y));
            }
        }
    }

    public void SetObject_Map(Vector2Int pos)
    {
        Vector3 Map_Object_position = transform.position + new Vector3(pos.x * 2.0f + 1.0f, 0.0f, pos.y * 2.0f + 1.0f);
        GameObject MapSetObj = endWall;
        switch (_board_Map[pos.y, pos.x])//Œã‚Å•Ï‚¦‚é
        {
            case Map_Object.EndWall:
                MapSetObj = endWall;
                break;
            case Map_Object.Spase:
                MapSetObj = spase;
                break;
            case Map_Object.Water:
                MapSetObj = water;
                break;
            case Map_Object.Wall:
                MapSetObj = wall;
                break;
            case Map_Object.Floor:
                MapSetObj = floor;
                break;
            case Map_Object.Road:
                MapSetObj = road;
                break;
        }

        Instantiate(MapSetObj, Map_Object_position, Quaternion.identity, transform);
        /*
        if (_board_Map[pos.y, pos.x] == Map_Object.Wall)
        {
            Instantiate(wall, Map_Object_position, Quaternion.identity, transform);
        }  
        else if(_board_Map[pos.y, pos.x] == Map_Object.EndWall)
        {
            Instantiate(endWall, Map_Object_position, Quaternion.identity, transform);
        }
        else if (_board_Map[pos.y, pos.x] == Map_Object.Water)
        {
            //world_position.y -= 0.5f;
            Instantiate(water, Map_Object_position, Quaternion.identity, transform);
        }
        else if (_board_Map[pos.y, pos.x] == Map_Object.Spase)
        {
            //world_position.y -= 0.5f;
            Instantiate(spase, Map_Object_position, Quaternion.identity, transform);
        }
        else if (_board_Map[pos.y, pos.x] == Map_Object.Floor)
        {
            //world_position.y -= 0.5f;
            Instantiate(floor, Map_Object_position, Quaternion.identity, transform);
        }
        else if (_board_Map[pos.y, pos.x] == Map_Object.Road)
        {
            //world_position.y -= 0.5f;
            Instantiate(road, Map_Object_position, Quaternion.identity, transform);
        }
        */
    }

    public void SetObject_Item(Vector2Int pos)
    {
        Vector3 Map_Item_position = transform.position + new Vector3(pos.x * 2.0f + 1.0f, 0.0f, pos.y * 2.0f + 1.0f);

        if (_board_Item[pos.y, pos.x] == ItemObject.Gate)
            Instantiate(wall, Map_Item_position, Quaternion.identity, transform);

        else if (_board_Item[pos.y, pos.x] == ItemObject.Exist)
        {
            Instantiate(endWall, Map_Item_position, Quaternion.identity, transform);
        }
            
        else if (_board_Item[pos.y, pos.x] == ItemObject.Trap)
        {
            Instantiate(water, Map_Item_position, Quaternion.identity, transform);
        }
    }

    public void SetObject_Chara(GameObject Set_Chara,Vector2Int pos)
    {
        Set_Chara.transform.position = new Vector3(pos.x * 2.0f + 1.0f, 1.0f, pos.y * 2.0f + 1.0f);
    }

    private Vector2Int RandamFloorSetting()
    {
        int randam_floor = Random.Range(0, Floor_Menber);

        int x_pos_min = floor_Deta[randam_floor].Get_sta_x();
        int y_pos_min = floor_Deta[randam_floor].Get_sta_y();

        int x_pos_max = floor_Deta[randam_floor].Get_ran_x();
        int y_pos_max = floor_Deta[randam_floor].Get_ran_y();

        return new Vector2Int(Random.Range(x_pos_min, x_pos_max), Random.Range(y_pos_min, y_pos_max));
    }

    private int SearchFloor()
    {
        int Number = Map_Data.preset[0].Point.Length;
        floor_Deta = new Floor_Deta[Number];
        Debug.Log("  =  " + Map_Data.GetLowerLeftPos(0, 0));
        for (int i = 0; i < Number; i++)
        {
            floor_Deta[i] = new Floor_Deta();
            floor_Deta[i].Set_sta_x(Map_Data.GetLowerLeftPos(0, i).x);
            floor_Deta[i].Set_sta_y(Map_Data.GetLowerLeftPos(0, i).y);
            floor_Deta[i].Set_ran_x(Map_Data.GetUpperRightPos(0, i).x);
            floor_Deta[i].Set_ran_y(Map_Data.GetUpperRightPos(0, i).y);
        }
        
        return Number;
    }

    private void SetEnemy(int SetValue)
    {
        for(int i = 0; i< SetValue;i++)
        {
            enemy_Stetas[i] = new Enemy_stetas();
            enemy_Stetas[i].ID = Random.Range(0, Enemy_Data.ID.Length);
            enemy_Stetas[i].HP = Enemy_Data.ID[enemy_Stetas[i].ID].max_hp;
            RandamEnemySetting(i);

            Debug.Log(Enemy_Data.ID[enemy_Stetas[i].ID].Object);

            GameObject SetObj = Instantiate(Enemy_Data.ID[enemy_Stetas[i].ID].Object, transform.position + 
                new Vector3(enemy_Stetas[i].pos.x * 2.0f + 1.0f, 0.0f, enemy_Stetas[i].pos.y * 2.0f + 1.0f),
                Quaternion.identity, transform);
            enemy_Stetas[i].Object = SetObj;

            EnemyValue++;
        }
    }
private void RandamEnemySetting(int I)
{
    Vector2Int Pos = RandamFloorSetting();
        if (_board_Chara[Pos.y, Pos.x] == CharaObject.None)
        {
            enemy_Stetas[I].pos = Pos;
            _board_Chara[Pos.y, Pos.x] = CharaObject.Enemy;
        }
        else RandamEnemySetting(I);
    }













    public bool NextCheck_P(Vector2Int pos,Vector2 Dier)
    {
        if(Dier == Vector2.zero)
                return false;

        int dier_x = 0,dier_y = 0;

        if (Dier.x < 0)
            dier_x--;
        else if(Dier.x > 0)
            dier_x++;

        if (Dier.y < 0)
            dier_y--;
        else if (Dier.y > 0)
            dier_y++;

        dier_x += pos.x;
        dier_y += pos.y;

        if (dier_x != pos.x && dier_y != pos.y)
        {
            if ((int)_board_Map[dier_y, pos.x] == 3 || (int)_board_Map[dier_y, pos.x] == 0)
                return false;
            if ((int)_board_Map[pos.y, dier_x] == 3 || (int)_board_Map[pos.y, dier_x] == 0)
                return false;           
        }

        if ((int)_board_Chara[dier_y, dier_x] == 2)
            return false;



        if ((int)_board_Map[dier_y, dier_x] >= 4)
        {
            _board_Chara[pos.y, pos.x] = CharaObject.None;
            _board_Chara[dier_y, dier_x] = CharaObject.Player;
            P_Scr.SetPos_P(new Vector2Int(dier_x, dier_y));
        }

        return true;
    }
    



    void Start()
    {
        P_Scr = Player.GetComponent<PlayerScript>();
        //Random.InitState(58);

        //Map_Data = GetComponent<MapDataBase>();
        MapMake();
        Floor_Menber = SearchFloor();
        Vector2Int PlayerPoint = RandamFloorSetting();

        SetEnemy(Random.Range(8, 15));

        SetObject_Chara(Player, PlayerPoint);
        P_Scr.SetPos_P(PlayerPoint);


    }
}
