using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform = null;
    [SerializeField] private float Z_DistanceFromPlayer = 0.0f;

    #region Rotation Degrees Limits
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveWithPlayer();
    }

    private void MoveWithPlayer()
    {
        transform.position = new Vector3( transform.position.x , transform.position.y , playerTransform.position.z + Z_DistanceFromPlayer );
    }

    
}
