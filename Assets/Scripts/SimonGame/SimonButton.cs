using UnityEngine;
using System.Collections;

public class SimonButton : UIButton
{
	SimonGame simonGame;

	public int index;

	public GameObject Sign;

	// Use this for initialization
	void Start ()
	{
		simonGame = GameObject.FindGameObjectWithTag("SimonGame").GetComponent<SimonGame>();
	}


	public bool buttonPressed;
	public bool oneClick  = true;
	void Update()
	{
			
	}
	
	protected override void OnHover (bool isOver) { if (isEnabled) base.OnHover(isOver); }
	
	protected override void OnPress (bool isPressed) 
	{ 
		if (isEnabled) 
		{
			//Debug.Log("buttonPressed");
			//base.OnPress(isPressed);
			if(oneClick)
			{
				if(isPressed)
				{
					StartCoroutine(ShowSign());

					buttonPressed = isPressed;
				}
			}
		}
	}




	IEnumerator ShowSign()
	{
		//Sign.SetActive(true);
		transform.FindChild("Background").gameObject.SetActive(true);
		yield return new WaitForSeconds(0.5f);
		simonGame.ButtonPressed(index);
		//Sign.SetActive(false);
		transform.FindChild("Background").gameObject.SetActive(false);


	}
}

