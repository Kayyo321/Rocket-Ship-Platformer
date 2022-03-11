using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonWall : MonoBehaviour
{
    [SerializeField] GameObject Wall;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(Wall != null);
    }
    public void Pressed()
    {
        Destroy(Wall);
        Destroy(gameObject);
    }
}