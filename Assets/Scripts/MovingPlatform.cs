using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 1.0f;
    public Vector3[] worldNode;
    private int worldNodeIndex = 0;

    private void Start()
    {
        transform.position = worldNode[0];
        worldNodeIndex++;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, worldNode[worldNodeIndex]) < 0.02f)
        {
            worldNodeIndex++;
            worldNodeIndex = worldNodeIndex % worldNode.Length;
        }

        transform.position = Vector2.MoveTowards(transform.position, worldNode[worldNodeIndex], speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        other.transform.SetParent(transform);
    }   
    private void OnCollisionExit2D(Collision2D other)
    {
        other.transform.SetParent(null);
    }
}
