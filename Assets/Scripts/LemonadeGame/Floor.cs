﻿using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if((coll.tag == "Fruit")&&(!coll.gameObject.GetComponent<Fruit>().Catched))
		{
			coll.enabled = false;
			coll.gameObject.SendMessage("Destroy");
		}
	}
}
