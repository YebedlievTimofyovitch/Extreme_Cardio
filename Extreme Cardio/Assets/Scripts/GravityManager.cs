using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour
{
    [SerializeField] Vector3[] gravityDirection = new Vector3[2] { Vector3.zero, Vector3.zero };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SwitchGravity();   
    }

    void SwitchGravity()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            Physics.gravity = gravityDirection[0];
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            Physics.gravity = gravityDirection[1];
        }
    }
}
