using UnityEngine;
using System.Collections;

public class VideoButton : MonoBehaviour
{
	public bool highlighted;
	public string videoButtonName;
	public GameObject titleTitle;
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
		//Put all deactive sprites
		for(int i = 0; i < transform.parent.childCount; i++)
		{
			VideoButton vb = transform.parent.GetChild(i).GetComponent<VideoButton>();
			vb.highlighted = false;
			//transform.parent.GetChild(i).FindChild("Sprite").GetComponent<tk2dSprite>().SetSprite(vb.videoButtonName);
			
		}
		//Set active sprite for current one
		string buttonName = videoButtonName;
		highlighted = true;
		buttonName = videoButtonName + "-high";
		//transform.FindChild("Sprite").GetComponent<tk2dSprite>().SetSprite(buttonName);
		
		//Put all deactive sprites for titles
		Transform titles = GameObject.Find("Titles").transform;
		for(int i = 0; i < titles.childCount; i++)
		{
			GameObject title = titles.GetChild(i).gameObject;
			title.SetActive(false);
		}
		//Set active sprite title for current one
		titleTitle.SetActive(true);
		highlighted = true;
		
		
	}
}

