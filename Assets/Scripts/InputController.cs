using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

    public float speed = 100.0f;

    public LinkedSegment trailingSegment;

    private Rigidbody2D rgd2d;

    private Vector2 direction = Vector2.zero;

    private static InputController _instance;

    public static InputController getInstance()
    {
        if (!_instance)    {
            _instance = GameObject.FindObjectOfType<InputController>();
        }

        return _instance;
    }

	// Use this for initialization
	void Start () {
        rgd2d = GetComponent<Rigidbody2D>();
        direction = transform.up;
	}
	
	// Update is called once per frame
    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (!Vector2.zero.Equals(input))
        {
            direction = input.normalized;
        }

        rgd2d.velocity = direction * speed * Time.deltaTime;
        trailingSegment.Follow(transform);
		
	}
}
