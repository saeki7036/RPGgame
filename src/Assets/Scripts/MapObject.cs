using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObject : MonoBehaviour
{
    public Vector2Int Enemy_pos;
    // Start is called before the first frame update
    void Start()
    {
        Enemy_pos.x = (int)transform.position.x / 2;
        Enemy_pos.y = (int)transform.position.z / 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
