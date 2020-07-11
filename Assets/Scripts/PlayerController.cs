using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 force = new Vector2(0, 0);
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            force += new Vector2(-1, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            force += new Vector2(1, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            force += new Vector2(0, 1);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            force += new Vector2(0, -1);
        }
        rigidbody.AddForce(force);
    }
}
