using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] GameObject pointB;

    private void Start()
    {
        Debug.Assert(pointB != null);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    public Vector3 GetTeleportDestination()
    {
        return pointB.transform.position;
    }
}
