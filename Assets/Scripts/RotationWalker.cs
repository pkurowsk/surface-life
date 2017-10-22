using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationWalker : MonoBehaviour {
    /// <summary>
    /// The circle that we're rotating around
    /// </summary>
    public CelestialBody circularBody;

    /// <summary>
    /// The sprite renderer to rotate while moving
    /// </summary>
    public SpriteRenderer sprite;

    /// <summary>
    /// Theta speed
    /// </summary>
    public float speed = 1.0f;

    /// <summary>
    /// The jump speed
    /// </summary>
    public float jumpSpeed = 10.0f;

    /// <summary>
    /// The circle's radius
    /// </summary>
    private float radius = 0f;

    /// <summary>
    /// Current degree position on the circle
    /// </summary>
    private float theta = 0f;

    /// <summary>
    /// The current speed of gravity acting on this
    /// </summary>
    private float curGravity = 0.0f;

    /// <summary>
    /// The max delta that the sprite angle should rotate when updating
    /// </summary>
    private const float MAX_SPRITE_ROTATION_DELTA = 200f;

    /// <summary>
    /// The update method that updates the body's motion
    /// </summary>
    System.Action positionUpdate;

	// Use this for initialization
	void Start () {
        if (circularBody)
        {
            SetCelestialBody(circularBody, true);
        }
        else
        {
            enabled = false;
        }
        
	}
	
	// Update is called once per frame
	void Update () {
        positionUpdate();

        transform.position = new Vector2(circularBody.transform.position.x + radius * Mathf.Sin(theta), circularBody.transform.position.y + radius * Mathf.Cos(theta));
        sprite.transform.eulerAngles = new Vector3(0, 0,
            Mathf.MoveTowardsAngle(sprite.transform.eulerAngles.z, -theta * (180f / Mathf.PI), MAX_SPRITE_ROTATION_DELTA * Time.deltaTime));
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        positionUpdate = updateTheta;
        if (radius < circularBody.radius || radius < 0)
        {
            radius = circularBody.radius;
            curGravity = circularBody.gravity;
        }

        jump();
    }

    public void SetCelestialBody(CelestialBody cb, bool initializeAnyways = false)
    {
        if (cb.Equals(circularBody) && !initializeAnyways)
        {
            return;
        }

        circularBody = cb;

        // Calculate new coordinates
        Vector2 dist = new Vector2(transform.position.x - circularBody.transform.position.x, transform.position.y - circularBody.transform.position.y);
        theta = Mathf.Atan2(dist.x, dist.y);
        radius = dist.magnitude;

        curGravity = -circularBody.gravity;

        positionUpdate = updateAll;

        enabled = true;
    }

    /// <summary>
    /// Sets the current gravity to the jump speed and starts updating motion and gravity
    /// </summary>
    private void jump()
    {
        if (jumpSpeed == 0)
        {
            return;
        }

        curGravity = jumpSpeed;

        positionUpdate = updateAll;
    }

    /// <summary>
    /// Updates only the surface motion, no gravity
    /// </summary>
    private void updateTheta()
    {
        theta += speed * Time.deltaTime;
    }

    /// <summary>
    /// Updates surface motion and gravity
    /// </summary>
    private void updateAll() 
    {
        updateTheta();

        curGravity -= circularBody.gravity;
        if (curGravity < -CelestialBody.TERMINAL_VELOCITY)
        {
            Debug.Log("Reached terminal velocity: " + curGravity + " at " + radius);
            curGravity = -CelestialBody.TERMINAL_VELOCITY;
        }

        radius = Mathf.Abs(radius) + curGravity * Time.deltaTime;
    }
}
