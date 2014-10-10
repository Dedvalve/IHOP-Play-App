using UnityEngine;
using System.Collections;
using Prime31;

public class GameController : MonoBehaviour {

	public LemonadeGame lemonadeGame;
	public SimonGame simonGame;
	GameObject moviePlane;
	metaioMovieTexture movieTexture;
	bool started;
	// Use this for initialization
	void Start () {
		//StartCoroutine(ShowMovie());
		started = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	


	IEnumerator ShowMovie() 
	{
		started=true;
		yield return new WaitForSeconds(3);
		moviePlane = GameObject.Find("moviePlane");
		moviePlane.SetActive(true);
		movieTexture = moviePlane.GetComponent<metaioMovieTexture>();
		movieTexture.play(true);
		

	}


	public void StartLemonadeGame()
	{
		if(!started)
		{
			started = true;
			lemonadeGame.GameStart();
		}
	}

	public void SetFruitLemonade(FruitType type)
	{
		lemonadeGame.SetFruit(type);
	}

	public void LemonadeSplashClick()
	{
		lemonadeGame.SplashClick();
	}

	public void LemonadeQuit()
	{
		started = false;
		lemonadeGame.Quit();
	}

	public void LemonadeRestart()
	{
		lemonadeGame.Restart();
	}

	public void LemonadePause()
	{
		lemonadeGame.Pause();
	}

	public void LemonadeContinue()
	{
		lemonadeGame.Continue();
	}

	public void LemonadeWinReplay()
	{
		lemonadeGame.WinReplay();
	}
	//Simon Game

	public void StartSimonGame()
	{
		if(!started)
		{
			started = true;
			simonGame.StartGame();
		}
	}

	public void SimonBeginClick()
	{
		simonGame.BeginTutorial();
	}

	public void SimonRestart()
	{
		simonGame.Restart();
	}

	public void SimonQuit()
	{
		started = false;
		simonGame.Quit();
	}

	public void SimonHelp()
	{
		simonGame.Help();
	}

	public void SimonCloseHelp()
	{
		simonGame.CloseHelp();
	}

	public void SimonPause()
	{
		simonGame.Pause();
	}

	public void SimonContinue()
	{
		simonGame.Continue();
	}

	public void LemonadeCamera()
	{
		#if UNITY_ANDROID
		StartCoroutine( saveScreenshotToSDCard(path =>
		                                       {
			var didSave = EtceteraAndroid.saveImageToGallery( path, "My image from Unity" );
			Debug.Log( "did save to gallery: " + didSave );
		} ) );
		//bool saved = EtceteraAndroid.saveImageToGallery(path,ihopUi.screenshotFilename);//ﬁ + currentScreenshot.ToString() +".png" );
		
		#endif
		#if UNITY_IPHONE
		StartCoroutine( saveScreenshotToSDCard(path =>
		                                       {
			EtceteraBinding.saveImageToPhotoAlbum(path);
			Debug.Log( "save to gallery");
		} ) );

		#endif
	}

	int screenShot = 0;
	
	// Saves a screenshot to the SD card and then calls completionHandler with the path to the image
	IEnumerator saveScreenshotToSDCard( System.Action<string> completionHandler )
	{
		yield return new WaitForEndOfFrame();
		
		var tex = new Texture2D( Screen.width, Screen.height, TextureFormat.RGB24, false );
		tex.ReadPixels( new Rect( 0, 0, Screen.width, Screen.height ), 0, 0, false );
		
		var bytes = tex.EncodeToPNG();
		Destroy( tex );
		screenShot++;
		var path = System.IO.Path.Combine( Application.persistentDataPath, "IHOPPlay"+screenShot.ToString()+".png" );
		System.IO.File.WriteAllBytes( path, bytes );
		
		completionHandler( path );
	}
}
