using UnityEngine;
using System.Collections;

public class GlassInterior : MonoBehaviour {

	public LemonadeGame lemonadeGame;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{

		other.transform.parent = transform;
		//other.GetComponent<PolygonCollider2D>().enabled = false;
		if((other.tag == "Fruit")&&(!other.gameObject.GetComponent<Fruit>().Catched))
		{

			other.gameObject.GetComponent<Fruit>().Catched = true;
			lemonadeGame.FruitCatched(other.gameObject);
		}
	}

	public void Restart()
	{
		for(int i = 0; i < transform.childCount ; i++)
		{
			if(transform.GetChild(i).gameObject.GetComponent<Fruit>().fruitType ==  lemonadeGame.fruitGenerator.selectedFruit)
			{
				GameObject.Destroy(transform.GetChild(i).gameObject);
			}
		}
	}
}
