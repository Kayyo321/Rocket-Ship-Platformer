using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeInputLvl7 : MonoBehaviour
{
    [SerializeField] Camera     camMove;
    [SerializeField] GameObject context;

    private float fov;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        context.SetActive(true);

        fov = 90;
        speed = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= 10 && context.activeInHierarchy)
        {
            context.SetActive(false);
        }

        if (Input.GetKey(KeyCode.UpArrow) && !(fov >= 90))
        {
            fov++;

            camMove.transform.Rotate(-speed, 0f, 0f, Space.Self);
        }   
        else if (Input.GetKey(KeyCode.DownArrow) && !(fov <= -90))
        {
            fov--;

            camMove.transform.Rotate(speed, 0f, 0f, Space.Self);
        }
    }
}
