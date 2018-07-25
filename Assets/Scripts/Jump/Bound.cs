using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bound : MonoBehaviour {

    private Rigidbody2D _rb;    // 2D剛体

    [SerializeField]
    private Rigidbody2D _pairRb;

    // Use this for initialization
	void Start () {
        _rb = GetComponent<Rigidbody2D>();

        _rb.AddForce(new Vector2(75.0f,105.0f));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.name == "Trail")
        {
            collision.enabled = false;
            float xFall = Random.Range(-50.0f, 50.0f);
            _rb.AddForce(new Vector2(xFall, -30.0f));
            _pairRb.AddForce(new Vector2(-xFall, -30.0f));
        }
    }
}
