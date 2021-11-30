using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrajectory : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] bool rotate = true;
    [SerializeField] Vector3[] pathPoints;

    private int nextPosIndex;
    private Vector3 nextPos;

    // Start is called before the first frame update
    void Start()
    {
        nextPos = pathPoints[0];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (transform.localPosition == nextPos)
        {
            nextPosIndex++;

            if (nextPosIndex >= pathPoints.Length)
            {
                nextPosIndex = 0;
            }

            nextPos = pathPoints[nextPosIndex];

            if(rotate) {
                transform.LookAt(nextPos);
            }
        }
        else
        {
            GetComponent<Rigidbody>().MovePosition(Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime));
        }
    }

}
