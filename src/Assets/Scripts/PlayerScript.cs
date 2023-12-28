using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerScript : MonoBehaviour
{
    [SerializeField] float Speed = 0.04f;

    public Vector2Int Player_pos;
    private Vector2 _Direction;
    private Vector2Int Move_Direction;
    bool InputLag;

    [SerializeField] GameObject MMM_Obj;
    MapMakeManager MMM;

    enum PlayerAction
    {
        None = 0,
        Move = 1,
    }
    PlayerAction _playerAction;
    // Start is called before the first frame update
    void Start()
    {
        _playerAction = PlayerAction.None;
        InputLag = true;
        MMM = MMM_Obj.GetComponent<MapMakeManager>();
        //Player_pos.x = (int)transform.position.x / 2;
        //Player_pos.y = (int)transform.position.z / 2;
        Move_Direction = new Vector2Int(0,0);
    }

    public void SetPos_P(Vector2Int pos)
    {
        Player_pos = pos;
    }

    public void OnDirection(InputAction.CallbackContext context)
    {
        // MoveActionÇÃì¸óÕílÇéÊìæ
        var axis = context.ReadValue<Vector2>();

        // à⁄ìÆë¨ìxÇï€éù
        _Direction = new Vector2(axis.x, axis.y);
       
    }

    private void MoveAction()
    {
        
        if (Move_Direction == new Vector2Int(0, 0))
        {
            if (_Direction.x < 0)
                Move_Direction.x--;
            else if (_Direction.x > 0)
                Move_Direction.x++;

            if (_Direction.y < 0)
                Move_Direction.y--;
            else if (_Direction.y > 0)
                Move_Direction.y++;
        }

        Vector3 Target_pos = new Vector3(((float)Player_pos.x) * 2f + 1f, 1.0f, ((float)Player_pos.y) * 2f + 1f);

        if ((transform.position - Target_pos).magnitude > 0.01f) 
        {
            transform.position += new Vector3(Move_Direction.x, 0.0f, Move_Direction.y) * Speed;
            Debug.Log((transform.position - Target_pos).magnitude);
        }

        else
        {
            transform.position = Target_pos;
            InputLag = true;
            _playerAction = PlayerAction.None;
            Move_Direction = new Vector2Int(0, 0);
        }
    }

    private void ActionChange()
    {
        if (_Direction != Vector2.zero && InputLag)
        {
            if (MMM.NextCheck_P(Player_pos, _Direction))
            {
                InputLag = false;
                _playerAction = PlayerAction.Move;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(_Direction);
        ActionChange();
    
        if(_playerAction == PlayerAction.Move)
        {
            MoveAction();
        }
    }
}
