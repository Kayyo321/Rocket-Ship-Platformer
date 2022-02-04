using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlSix : MonoBehaviour
{
    #region GameObject
    public GameObject a;
    public GameObject b;
    public GameObject main;
    public GameObject Text;
    #endregion

    public GameController gameController;

    #region Vector3
    public Vector3 newScale;
    public Vector3 newPos;
    #endregion

    public float wait = 0f;

    public IEnumerator ScaleTextDown()
    {
        yield return new WaitForSeconds(2f);

        // scale down

        Text.transform.localScale = newScale;

        // move to bottom left

        Text.transform.position = newPos;

        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.1f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ScaleTextDown());
    }

    // Update is called once per frame
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
