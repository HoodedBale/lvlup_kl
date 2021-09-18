using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScroll : MonoBehaviour
{
    public float speed = 10.0f;
    public float exitX = -1950;
    public float entranceX = 1150;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition += new Vector3(-1, 0, 0) * speed * Time.deltaTime;
        Vector3 pos = transform.localPosition;
        if (pos.x <= exitX)
        {
            pos.x = entranceX;
            transform.localPosition = pos;
        }
    }
}
