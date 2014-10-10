using UnityEngine;
using System.Collections;

public class LemonadeGame : MonoBehaviour {


	public GameObject selectScreen;
	public GameObject game;
	public GameObject inGameUI;
	public GameObject introScreen;
	public GameObject Glass;
	public GameObject looseScreen;
	public GameObject win1;
	public GameObject winGlass;
	public GameObject win2;
	public GameObject splashCherry;
	public GameObject splashLemon;
	public GameObject splashMango;
	public GameObject screenFillCherry;
	public GameObject screenFillLemon;
	public GameObject screenFillMango;
	public GameObject screenFillCherryFruits;
	public GameObject screenFillLemonFruits;
	public GameObject screenFillMangoFruits;
	public GameObject replayButton;
	public GameObject cameraButton;

	public AudioSource CorrectFruit;
	public AudioSource IncorrectFruit;
	public AudioSource BackgroundMusic;
	public AudioSource WinningMusic;
	public AudioSource WaterExplosionSound;
	public AudioSource GameOverSound;
	public AudioSource flushSound;

	public FruitGenerator fruitGenerator;
	public GameObject pause;
	FruitType selectedFruit;
	int failures;
	int maxFailures = 5;
	int winScore = 10;
	int score;
	public UILabel scoreLabel;
	Transform redCrosses;

	public GameObject mainMenu;
	// Use this for initialization
	void Start () 
	{
		bool gyoBool = SystemInfo.supportsGyroscope;
		
		if( gyoBool ) {
			Gyroscope gyo1=Input.gyro;
			gyo1.enabled = true;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void Quit ()
	{
		for(int i = 1; i <= maxFailures; i++)
			redCrosses.FindChild(i.ToString()).gameObject.SetActive(false);
		Glass.transform.FindChild("Interior").GetComponent<GlassInterior>().Restart();
		score = 0;
		scoreLabel.text = "0";
		game.SetActive(false);
		pause.SetActive(false);
		looseScreen.SetActive(false);
		inGameUI.SetActive(false);
		win2.SetActive(false);
		Time.timeScale = 1;
		mainMenu.SetActive(true);
	}

	public void Restart()
	{
		//BackgroundMusic.Play();
		inGameUI.SetActive(false);
		Glass.SetActive(false);
		pause.SetActive(false);
		Time.timeScale = 1;
		fruitGenerator.finished = true;
		for(int i = 0; i < fruitGenerator.transform.childCount;i++)
		{
			fruitGenerator.transform.GetChild(i).gameObject.GetComponent<Fruit>().Destroy(fruitGenerator.transform);
		}
		win2.SetActive(false);
		looseScreen.SetActive(false);
		for(int i = 1; i <= maxFailures; i++)
			redCrosses.FindChild(i.ToString()).gameObject.SetActive(false);
		score = 0;
		scoreLabel.text = "0";
		Glass.transform.FindChild("Interior").GetComponent<GlassInterior>().Restart();
		GameStart();
		//SplashClick();
	}

	public void GameStart()
	{
		selectScreen.SetActive(true);
		redCrosses = inGameUI.transform.FindChild("Top").FindChild("Crosses").FindChild("RedCrosses");



	}

	public void SetFruit(FruitType type)
	{
		selectedFruit = type;
		fruitGenerator.selectedFruit = type;
		selectScreen.SetActive(false);
		Debug.Log(type.ToString());
		introScreen.transform.FindChild("FruitLabel").gameObject.GetComponent<UILabel>().text = type.ToString()+"s";
		introScreen.transform.FindChild("Fruits").FindChild(type.ToString()).gameObject.SetActive(true);

		introScreen.SetActive(true);

	}

	public void SplashClick()
	{
		failures = 0;
		introScreen.SetActive(false);
		introScreen.transform.FindChild("Fruits").FindChild("Cherry").gameObject.SetActive(false);
		introScreen.transform.FindChild("Fruits").FindChild("Lemon").gameObject.SetActive(false);
		introScreen.transform.FindChild("Fruits").FindChild("Mango").gameObject.SetActive(false);
		inGameUI.SetActive(true);
		game.SetActive(true);
		Glass.SetActive(true);
		fruitGenerator.StartFruit();
		BackgroundMusic.Play();
	}

	public void FruitCatched (GameObject fruit)
	{
		if(fruit.GetComponent<Fruit>().fruitType != selectedFruit)
		{
			StartCoroutine(ShowOops(fruit));
			failures++;
			IncorrectFruit.Play();
			if(failures == maxFailures)
			{

				redCrosses.FindChild(failures.ToString()).gameObject.SetActive(true);
				looseScreen.SetActive(true);
				fruitGenerator.finished = true;
				GameOverSound.Play();
				BackgroundMusic.Stop();

			}else if(failures < maxFailures){
						redCrosses.FindChild(failures.ToString()).gameObject.SetActive(true);
				}

		}else{
			CorrectFruit.Play();
			score++;
			scoreLabel.text = score.ToString();
			fruitGenerator.InstantiateFruit(fruit.GetComponent<Fruit>());
			StartCoroutine(CorrectFruitCatched(fruit));
			if(score == winScore)
			{
				StartCoroutine(WinRoutine());
			}
		}
	}

	IEnumerator CorrectFruitCatched(GameObject fruit)
	{
		yield return new WaitForSeconds(5.0f);
		fruit.rigidbody2D.isKinematic = true;
	}

	IEnumerator WinRoutine()
	{
		win1.SetActive(true);
		BackgroundMusic.Stop();
		WinningMusic.Play();
		WaterExplosionSound.Play();
		if(selectedFruit == FruitType.Cherry)
		{
			splashCherry.SetActive(true);

		}else if(selectedFruit == FruitType.Lemon)
			  {
				splashLemon.SetActive(true);
				}else if(selectedFruit == FruitType.Mango)
				{
					splashMango.SetActive(true);
				}

		fruitGenerator.finished = true;
		yield return new WaitForSeconds(5.0f);

		splashLemon.SetActive(false);
		splashMango.SetActive(false);
		splashCherry.SetActive(false);
		win1.SetActive(false);
		if(selectedFruit == FruitType.Cherry)
		{
			screenFillCherry.GetComponent<ScreenFill>().Restart();
			screenFillCherry.SetActive(true);
			screenFillCherryFruits.SetActive(true);
			
		}else if(selectedFruit == FruitType.Lemon)
		{
			screenFillLemon.GetComponent<ScreenFill>().Restart();
			screenFillLemon.SetActive(true);
			screenFillLemonFruits.SetActive(true);
		}else if(selectedFruit == FruitType.Mango)
		{
			screenFillMango.GetComponent<ScreenFill>().Restart();
			screenFillMango.SetActive(true);
			screenFillMangoFruits.SetActive(true);
		}
		inGameUI.SetActive(false);
		Glass.SetActive(false);
		replayButton.SetActive(true);
		cameraButton.SetActive(true);
		//win2.SetActive(true);
	}

	IEnumerator ShowOops(GameObject fruit)
	{
		Glass.transform.FindChild("Oops").gameObject.SetActive(true);
		yield return new WaitForSeconds(2.0f);
		fruit.GetComponent<Fruit>().Destroy(fruitGenerator.transform);
		Glass.transform.FindChild("Oops").gameObject.SetActive(false);

	}

	public void Pause()
	{
		BackgroundMusic.Stop();
		pause.SetActive(true);
		Time.timeScale = 0;
	}

	public void Continue()
	{
		BackgroundMusic.Play();
		pause.SetActive(false);
		Time.timeScale = 1;
	}

	public void WinReplay ()
	{
		screenFillCherry.GetComponent<ScreenFill>().waterLoopSound.Stop();
		screenFillLemon.GetComponent<ScreenFill>().waterLoopSound.Stop();
		screenFillMango.GetComponent<ScreenFill>().waterLoopSound.Stop();
		//flushSound.Play();
		win2.SetActive(true);
		replayButton.SetActive(false);	
		cameraButton.SetActive(false);
		screenFillCherry.GetComponent<ScreenFill>().Flush(false);
		screenFillLemon.GetComponent<ScreenFill>().Flush(false);
		screenFillMango.GetComponent<ScreenFill>().Flush(false);
		                       
	}
}
