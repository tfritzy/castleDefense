using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseUIOnClickAway : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            Vector2 touchPos = Vector2.zero;
            if (Input.touchCount > 0)
                touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            else
                touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 diffVect = touchPos - (Vector2)this.transform.position;
            if (diffVect.magnitude > 4f)
                Destroy(this.gameObject);

        }
    }

}
