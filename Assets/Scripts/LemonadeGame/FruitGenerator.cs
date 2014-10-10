using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FruitGenerator : MonoBehaviour {


	public GameObject [] lemons;
	public GameObject [] cherrys;
	public GameObject [] mangos;

	int qFruits = 3;
	public FruitType selectedFruit;


	public float minX;
	public float maxX;

	// Use this for initialization
	void Start () {
		finished = false;
		StartFruit();	
		for(int j = 0; j < cherrys.Length; j++)
		{
			cherrys[j].GetComponent<Fruit>().index = j;
			lemons[j].GetComponent<Fruit>().index = j;
			mangos[j].GetComponent<Fruit>().index = j;
		}
	}
	
	// Update is called once per frame
	void Update () {

	
	}

	public bool finished;

	public void StartFruit()
	{
		finished = false;
		StartCoroutine(ThrowFruit());
	}

	IEnumerator ThrowFruit()
	{
		yield return new WaitForSeconds(2.0f);
		CreateSelectedFruit(Vector3.zero);
		float c = 2;
		float inc = 0.3f;
		float max = 8;
		yield return new WaitForSeconds(2.0f);
		while(!finished)
		{
			for(int i = 0; i < c && i < maxX; i++)
			{
				CreateRandomFruit(GetRandomPosition());
				yield return new WaitForSeconds(Random.Range(0.5f,1));

			}
			c+=inc;
			yield return new WaitForSeconds(2.0f);
		}
		yield return null;
	}

	public void InstantiateFruit(Fruit fruit)
	{
		switch(fruit.GetComponent<Fruit>().fruitType)
		{
		case FruitType.Cherry:
			cherrys[fruit.index] = GameObject.Instantiate(Resources.Load(fruit.GetComponent<Fruit>().prefabName)) as GameObject;
			cherrys[fruit.index].transform.parent = gameObject.transform;
			cherrys[fruit.index].GetComponent<Fruit>().index = fruit.index;
			break;
		case FruitType.Lemon:
			lemons[fruit.index] = GameObject.Instantiate(Resources.Load(fruit.GetComponent<Fruit>().prefabName)) as GameObject;
			lemons[fruit.index].transform.parent = gameObject.transform;
			lemons[fruit.index].GetComponent<Fruit>().index = fruit.index;
			break;
		case FruitType.Mango:
			mangos[fruit.index] = GameObject.Instantiate(Resources.Load(fruit.GetComponent<Fruit>().prefabName)) as GameObject;
			mangos[fruit.index].transform.parent = gameObject.transform;
			mangos[fruit.index].GetComponent<Fruit>().index = fruit.index;
			break;
		}
	}

	void CreateSelectedFruit(Vector3 position)
	{
		GameObject f = GetFruit(selectedFruit);
		//f.transform.parent = gameObject.transform;
		f.transform.localPosition = position;
	}

	void CreateRandomFruit(Vector3 position)
	{
		GameObject f = GetFruit(RandomType());
		if(f!=null)
		{
			f.GetComponent<Fruit>().Throw();
			//f.transform.parent = gameObject.transform;
			f.transform.localPosition = position;
		}
	}

	GameObject GetFruit(FruitType type)
	{

		switch(type)
		{
			case  FruitType.Cherry:
				for(int j = 0; j < cherrys.Length; j++)
				{
					//i = (int)Random.Range(0,cherrys.Length);
					if((!cherrys[j].GetComponent<Fruit>().Throwed)&&(!cherrys[j].GetComponent<Fruit>().Catched))
						return cherrys[j];
				}	
			break;
		case FruitType.Lemon:
				for(int j = 0; j < lemons.Length; j++)
				{
					if((!lemons[j].GetComponent<Fruit>().Throwed)&&(!lemons[j].GetComponent<Fruit>().Catched))
						return lemons[j];
				}		

				
			break;
		case FruitType.Mango:
			for(int j = 0; j < mangos.Length; j++)
			{
				if((!mangos[j].GetComponent<Fruit>().Throwed)&&(!mangos[j].GetComponent<Fruit>().Catched))
					return mangos[j];
			}

			break;
		
		}
		return null;

	}

	GameObject GetFruitFromArray(GameObject [] array)
	{
		int i = (int)Random.Range(0,array.Length);
		return array[i];
	}



	FruitType RandomType()
	{
		float i = Random.Range(0,qFruits);
		if(i <= 0)
		{
			return FruitType.Cherry;
		}else if(i <= 1)
			   {
					return FruitType.Lemon;	
			   }else if(i <= 2)
					 {
						return FruitType.Mango;
					 }
		return 0;
	}

	Vector3 GetRandomPosition()
	{
		float x = Random.Range(minX,maxX);
		return new Vector3(x,0,0);

	}


}
