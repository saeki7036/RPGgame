using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public MapDataBase Map_Data;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("  =  " + Map_Data.GetLowerLeftPos(0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
