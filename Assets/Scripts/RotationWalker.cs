using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationWalker : MonoBehaviour {
    /// <summary>
    /// The circle that we're rotating around
    /// </summary>
    public RectTransform circularBody;

    /// <summary>
    /// The sprite renderer to rotate while moving
    /// </summary>
    public SpriteRenderer sprite;

    /// <summary>
    /// Move speed
    /// </summary>
    public float speed = 1.0f;

    /// <summary>
    /// The circle's radius
    /// </summary>
    private float radius = 0f;

    /// <summary>
    /// Current degree position on the circle
    /// </summary>
    private float theta = 0f;

	// Use this for initialization
	void Start () {
        radius = circularBody.sizeDelta.x * circularBody.localScale.x / 2f;
	}
	
	// Update is called once per frame
	void Update () {
        theta += speed * Time.deltaTime;

        transform.position = new Vector2(circularBody.position.x + radius * Mathf.Sin(theta), circularBody.position.y + radius * Mathf.Cos(theta));
        sprite.transform.eulerAngles = new Vector3(0, 0, -theta * (180 / Mathf.PI));
	}
}
