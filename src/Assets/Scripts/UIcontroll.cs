using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIcontroll : MonoBehaviour
{
    private Vector2 _Direction;
    private bool CloseMenu;

    [SerializeField] GameObject Carsol_Obj, Menu_Obj,Stetas_Obj;

    PlayerScript playerScript;
    private bool[][] MenuSelect = new bool[3][];
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            MenuSelect[i] = new bool[2];

            if(i == 0)
                MenuSelect[i][0] = true;
            else 
                MenuSelect[i][0] = false;

            MenuSelect[i][1] = false;

        }
        playerScript = this.gameObject.GetComponent<PlayerScript>();
    }


    public void OnDirection(InputAction.CallbackContext context)
    {
        // MoveAction‚Ì“ü—Í’l‚ğæ“¾
        var axis = context.ReadValue<Vector2>();

        // ˆÚ“®‘¬“x‚ğ•Û
        _Direction = new Vector2(axis.x, axis.y);

    }
    public void OnCloseMenu(InputAction.CallbackContext context)
    {
        var isTrigger = context.ReadValueAsButton();
        //if (!context.performed) return;
        CloseMenu = isTrigger;
    }



    // Update is called once per frame
    void Update()
    {
        //Debug.Log(CloseMenu);
        if (Menu_Obj.activeSelf)
        {
            if (CloseMenu)
            {
                Menu_Obj.SetActive(false);
                Carsol_Obj.SetActive(false);
                Stetas_Obj.SetActive(false);
                playerScript.InputLag = true;
                playerScript.playerInput.currentActionMap = playerScript.playerInput.actions.actionMaps[0];
            }
               
            if(_Direction.y < 0)
            {

            }
            if (_Direction.y > 0)
            {

            }
        }



    }
}
