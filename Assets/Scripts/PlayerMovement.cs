using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public GameObject YoYo;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Physics2D.IgnoreCollision(YoYo.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }
}
