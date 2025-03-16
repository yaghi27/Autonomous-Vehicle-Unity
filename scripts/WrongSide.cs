using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrongSide : MonoBehaviour
{
    // Start is called before the first frame update


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.matrix = transform.localToWorldMatrix;
        BoxCollider box = GetComponent<BoxCollider>();
        if (box)
        {
            Gizmos.DrawWireCube(box.center, box.size);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
