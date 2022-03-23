using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class holds code that makes the teleporters teleport the rocket around.
/// </summary>
public class Teleporter : MonoBehaviour
{
    [SerializeField] GameObject pointB;

    /// <summary>
    /// Checks to see if the opposite teleporter is in the scene.
    /// </summary>
    private void Start()
    {
        Debug.Assert(pointB != null);
    }

    /// <summary>
    /// Gets the position of the opposite teleporter.
    /// </summary>
    /// <returns>Other teleporter position.</returns>
    public Vector3 GetTeleportDestination()
    {
        return pointB.transform.position;
    }
}
