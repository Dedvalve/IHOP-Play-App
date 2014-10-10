using UnityEngine;
using System.Collections;

public class Floating : MonoBehaviour
{

	bool floatup;
	public float wait = 0.1f;
	public float wait2 = 1;
	public float inc = 0.1f ;
	public float diff = 10;
	public float initialY;
	void Start()
	{
		floatup = false;


	}

	bool goDown;

	public void BeginFloat()
	{
		//initialY = transform.localPosition.y;

	}

	void Update()
	{
		float currentY = transform.localPosition.y;

		if(currentY >= initialY)
		{
			goDown = true;
		}else if(currentY < (initialY - diff))
			  {
					goDown = false;
			  }
		Debug.Log(Time.deltaTime);
		if(goDown)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime*inc,transform.position.z);
		}else{
			transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime*inc,transform.position.z);
		}
		//if(floatup)
		//	StartCoroutine(floatingup());
		//else if(!floatup)
		//	StartCoroutine(floatingdown());
	}
	IEnumerator floatingup(){
		for(int i = 0;i < 10; i++)
		{
		transform.position = new Vector3(transform.position.x, transform.position.y + inc,transform.position.z);
		yield return new WaitForSeconds(wait);
		}
		yield return new WaitForSeconds(wait2);
		floatup = false;
	}
 	IEnumerator floatingdown(){
		for(int i = 0;i < 10; i++)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y - inc,transform.position.z);
			yield return new WaitForSeconds(wait);
		}
		yield return new WaitForSeconds(wait2);
		floatup = true;
	}

}

