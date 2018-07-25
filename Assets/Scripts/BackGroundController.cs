using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGroundController : MonoBehaviour {

    [SerializeField]
    private GameObject _Player;

	// Use this for initialization
	void Start () {
		
	}

    void Update()
    {
        float scroll = _Player.transform.position.x / 10;
        Rect offset = GetComponent<RawImage>().uvRect;
        offset.x = +scroll;
        GetComponent<RawImage>().uvRect = offset;
    }
}
