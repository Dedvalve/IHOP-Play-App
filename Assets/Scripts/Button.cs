using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour
{
	public string function;
	public string param;
	public int paramInt;
	public string param2;
	
	public bool video;
	public string videoButtonName;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	void OnMouseDown()
	{
		Debug.Log("ButtonPressed");
		GameObject.Find("GameController").GetComponent<ViewModel>().callFunction(function,param,paramInt);
		//transform.FindChild("Sprite").GetComponent<tk2dSprite>().SetSprite(videoButtonName);
	}
}

