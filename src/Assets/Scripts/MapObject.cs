using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObject : MonoBehaviour
{
    public int map_ob_x, map_ob_y;
    // Start is called before the first frame update
    void Start()
    {
        map_ob_x = (int)transform.position.x / 2;
        map_ob_y = (int)transform.position.y / 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
