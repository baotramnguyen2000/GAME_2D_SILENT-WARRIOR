using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemAnimation : MonoBehaviour
{
    public float speed = 0.04f;
    float pointY;
    float targetPoint;
    bool isFlip;
    // Start is called before the first frame update
    void Start()
    {
        //0.25
        targetPoint = transform.position.y;
        pointY = transform.position.y + 0.25f;
    }

    // Update is called once per frame
    void Update()
    {
        Fly();
    }
    void Fly()
    {
        if (!isFlip)
        {
            if (transform.position.y >= targetPoint)
            {
                //Debug.Log("> Left");
                transform.position += new Vector3(0, -speed * Time.fixedDeltaTime, 0);
                if (transform.position.y < targetPoint)
                {
                    isFlip = true;
                }
            }
        }

        else
        {
            if (transform.position.y <= pointY)
            {
                transform.position += new Vector3(0, speed * Time.fixedDeltaTime, 0);
                //Debug.Log(" < Right");
                if (transform.position.y > pointY)
                {
                    isFlip = false;
                }
            }
        }
    }
}
