using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerScript : MonoBehaviour
{
    [SerializeField] float Speed = 0.1f;
    private Vector2 _velocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        // MoveAction�̓��͒l���擾
        var axis = context.ReadValue<Vector2>();

        // �ړ����x��ێ�
        _velocity = new Vector2(axis.x, axis.y);

        Debug.Log(_velocity);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += _velocity * Speed;
        //Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        //transform.Translate(move * Time.deltaTime * Speed);
    }
}
