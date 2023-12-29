using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RordTest : MonoBehaviour
{
    [SerializeField] ItemData itemData;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(itemData.items[0].name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
