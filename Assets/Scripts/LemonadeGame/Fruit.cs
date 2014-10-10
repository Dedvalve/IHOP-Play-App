using UnityEngine;
using System.Collections;

public enum FruitType
{
	Lemon, Cherry, Mango
}

public class Fruit : MonoBehaviour {

	float YLimit = -21.5f;
	public FruitType fruitType;
	public bool Catched;
	public bool Throwed;
	public int index;
	public string prefabName;
	// Use this for initialization
	void Start () {
		Catched = false;
		//

	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.y < YLimit)
			gameObject.GetComponent<PolygonCollider2D>().enabled = false;
	}

	public void Throw()
	{
		Throwed = true;
		gameObject.GetComponent<PolygonCollider2D>().enabled = true;
		rigidbody2D.isKinematic = false;
		rigidbody2D.AddTorque(Random.Range(0.5f,1)*10);
	}

	public void Destroy()
	{
		StartCoroutine(DestroyCor(null));
	}

	public void Destroy(Transform p)
	{
		StartCoroutine(DestroyCor(p));
	}

	public void DestroyNoCor(Transform p)
	{
		rigidbody2D.isKinematic = true;
		Catched = false;
		Throwed = false;
		if(p!=null)
			transform.parent = p;
		transform.localPosition = Vector3.zero;
		Throwed = false;
	
	}

	IEnumerator DestroyCor(Transform p)
	{
		yield return new WaitForSeconds(1);
		//	GameObject.Destroy(gameObject);
		rigidbody2D.isKinematic = true;
		Catched = false;
		Throwed = false;
		if(p!=null)
			transform.parent = p;
		transform.localPosition = Vector3.zero;
		Throwed = false;
	}

}
