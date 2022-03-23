using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles code that disables some features in level 6, that are in the rest of the game
/// </summary>
public class LvlSix : MonoBehaviour
{
    #region GameObject
    public GameObject a;
    public GameObject b;
    public GameObject main;
    public GameObject Text;
    #endregion

    #region Other Scripts
    public GameController gameController;
    #endregion

    #region Vector3
    public Vector3 newScale;
    public Vector3 newPos;
    #endregion

    #region Float
    public float wait = 0f;
    #endregion

    /// <summary>
    /// Makes sure that the teleporters are in the level, rather than being disabled
    /// </summary>
    void Update()
    {
        if (wait < 2f)
        {
            if (!a.activeSelf && wait == 0f) 
            {
                a.SetActive(true); 
                wait = 1f; 
            }
            
            if (!b.activeSelf && wait == 1f) 
            {
                b.SetActive(true); 
                wait = 3f; 
            }
        }
    }
}
