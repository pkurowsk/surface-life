using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedSegment : MonoBehaviour {
    public LinkedSegment trailingSegment;

    private const float MAX_DIST = 0.5f;
    private const float SPEED_MOD = 50.0f;

    public void Follow(Transform followingSegment)
    {
        if (Vector2.Distance(transform.position, followingSegment.position) >= MAX_DIST)   {
            transform.position = Vector2.MoveTowards(transform.position, followingSegment.position, InputController.getInstance().speed / SPEED_MOD * Time.deltaTime);

            if (trailingSegment)
            {
                trailingSegment.Follow(transform);
            }
        }
	}
}
