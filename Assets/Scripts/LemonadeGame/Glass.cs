using UnityEngine;
using System.Collections;

public class Glass : MonoBehaviour
{
	bool drag;
	public Camera gameCamera;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		foreach(Touch touch in Input.touches){

			Ray ray = gameCamera.ScreenPointToRay(touch.position);
			RaycastHit hit;
			if (Physics.Raycast (ray,out hit, 100.0f)) {
				if(touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved) {
					var cameraTransform = gameCamera.transform.InverseTransformPoint(0, 0, 0);
					transform.position = new Vector3(gameCamera.ScreenToWorldPoint(new Vector3 (touch.position.x, touch.position.y, cameraTransform.z - 0.5f)).x,transform.position.y, transform.position.z);
				}
			}
		}
	}
	
	
	void OnMouseDown()
	{

	}

}

