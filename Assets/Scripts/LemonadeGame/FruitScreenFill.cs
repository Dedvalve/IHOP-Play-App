using UnityEngine;
using System.Collections;

public class FruitScreenFill: MonoBehaviour
{

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
		Debug.Log("MOUSE");
			StartCoroutine(Move());
			
		}

		IEnumerator Move()
		{

			gameObject.GetComponent<Animator>().speed = 10;
			yield return new WaitForSeconds(2);
			gameObject.GetComponent<Animator>().speed = 1;
		}
}

