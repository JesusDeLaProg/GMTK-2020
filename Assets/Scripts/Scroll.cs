using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    public GameObject up;
    public GameObject down;
    public GameObject left;
    public GameObject right;
    private Transform player;
    private float height;
    private float width;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Spaceship").transform;
        height = GetComponent<SpriteRenderer>().bounds.size.y;
        width = GetComponent<SpriteRenderer>().bounds.size.x;

    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(up.transform.position.y - player.position.y) < height / 2)
        {
            transform.position += Vector3.up * height;
        }
        if (Mathf.Abs(left.transform.position.x - player.position.x) < width / 2)
        {
            transform.position += Vector3.left * width;
        }
        if (Mathf.Abs(right.transform.position.x - player.position.x) < width / 2)
        {
            transform.position += Vector3.right * width;
        }
        if (Mathf.Abs(down.transform.position.y - player.position.y) < height / 2)
        {
            transform.position += Vector3.down* height;
        }
    }
}
