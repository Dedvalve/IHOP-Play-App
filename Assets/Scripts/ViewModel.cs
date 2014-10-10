using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using Prime31;
using System.IO;

public class ihopUi : EZData.Context
{
	
	#if UNITY_IPHONE
	private string _userId;
	public bool _canUserUseFacebookComposer = false;
	public bool _hasPublishPermission = false;
	public bool _hasPublishActions = false;
	
	public bool canUseTweetSheet = false; // requires iOS 5 and a Twitter account setup in Settings.app

	#endif
	public static string screenshotFilename = "fb_thumbnail.png";
	//TODO: add your dependency properties and collections here
	public GameObject panelOne;
	public GameObject panelTwo;
	public GameObject fullScreenGO;
	public GameObject socialFullScreen;
	public metaioMovieTexture moviePlaneFS;
	public GameObject videoPlane;
	public GameObject loading;
	public GameController gameController;
	public GameObject menu;

	public void MenuContinue()
	{
		menu.SetActive(false);
	}

	public void SimonBeginClick()
	{
		gameController.SimonBeginClick();
	}

	public void SimonRestart()
	{
		gameController.SimonRestart();
	}

	public void SimonQuit()
	{
		gameController.SimonQuit();
	}

	public void SimonHelp()
	{
		gameController.SimonHelp();
	}

	public void SimonCloseHelp()
	{
		gameController.SimonCloseHelp();
	}

	public void SimonPause()
	{
		gameController.SimonPause();
	}

	public void SimonContinue()
	{
		gameController.SimonContinue();
	}

	public void LemonadeWinReplay()
	{
		gameController.LemonadeWinReplay();
	}

	public void LemonadeSplashClick()
	{
		gameController.LemonadeSplashClick();
	}
	public void LemonadeGameSelectLemon()
	{
		gameController.SetFruitLemonade(FruitType.Lemon);
	}

	public void LemonadeQuit()
	{
		gameController.LemonadeQuit();
	}

	public void LemonadeRestart()
	{
		gameController.LemonadeRestart();
	}

	public void LemonadePause()
	{
		gameController.LemonadePause();
	}

	public void LemonadeContinue()
	{
		gameController.LemonadeContinue();
	}

	public void LemonadeGameSelectCherry()
	{
		gameController.SetFruitLemonade(FruitType.Cherry);
	}

	
	public void LemonadeGameSelectMango()
	{
		gameController.SetFruitLemonade(FruitType.Mango);
	}

	int currentScreenshot = 0;

	public void LemonadeCamera()
	{
		currentScreenshot++;
		gameController.LemonadeCamera();
		//var path = PaLemonadeCamera()th.Combine( Application.persistentDataPath, ihopUi.screenshotFilename );
		//var path = System.IO.Path.Combine( Application.streamingAssetsPath,ihopUi.screenshotFilename); //+ currentScreenshot.ToString() +".png" );
		//Application.CaptureScreenshot(path );

	}




	public void GoPanelTwo()
	{
		panelOne.SetActive(false);
		panelTwo.SetActive(true);
	}

	
	
	public void LoadMainGameScene()
	{
		#if UNITY_IPHONE
		Mixpanel.SendEvent("iOS Start Button");
		#endif
		#if UNITY_ANDROID
		Mixpanel.SendEvent("Android Start Button");
		#endif
		
		panelOne.transform.FindChild("Panel").gameObject.SetActive(false);
		loading.SetActive(true);
		
		Application.LoadLevel("MainGames");
	}

	public void LoadMainScene()
	{
		#if UNITY_IPHONE
		Mixpanel.SendEvent("iOS Start Button");
		#endif
		#if UNITY_ANDROID
		Mixpanel.SendEvent("Android Start Button");
		#endif
		
		panelOne.transform.FindChild("Panel").gameObject.SetActive(false);
		loading.SetActive(true);
		
		Application.LoadLevel("Main");
	}
	
	public void BackToMenu()
	{
		Application.LoadLevel("Menu");	
	}
	

	
	public void PostMessageFacebook()
	{
		
#if UNITY_IPHONE
		Mixpanel.SendEvent("iOS Facebook Share");
		Debug.Log("FB1");
		if( _canUserUseFacebookComposer )
		{
			Debug.Log("FB2");
			// ensure the image exists before attempting to add it!
			var pathToImage = System.IO.Path.Combine(Application.streamingAssetsPath,screenshotFilename);
			if( !System.IO.File.Exists( pathToImage ) )
				pathToImage = null;
			FacebookBinding.showFacebookComposer( "I just finished this great game at IHOP®! Download IHOP PLAY and come in to experience it for yourself right at the table", pathToImage, "http://bit.ly/HUTkhc" );
		}else{
			Debug.Log("FB3");
			// parameters are optional. See Facebook's documentation for all the dialogs and paramters that they support
			var parameters = new Dictionary<string,string>
			{
				{ "link", "http://bit.ly/HUTkhc" },
				{ "name", "I just finished this great game at IHOP®! Download IHOP PLAY and come in to experience it for yourself right at the table." },
				{ "picture", "" },
				{ "caption", "" }
			};
			FacebookBinding.showDialog( "stream.publish", parameters );
		}
#endif
		
#if UNITY_ANDROID
		Mixpanel.SendEvent("Android Facebook Share");	

		var parameters = new Dictionary<string,object>
		{
			{ "link", "http://bit.ly/HUTkhc" },
			{ "name", "I just finished this great game at IHOP®! Download IHOP PLAY and come in to experience it for yourself right at the table." },
			{ "picture", "http://jokatu.com.ar/Images/fb_thumbnail.png" },
			{ "caption", "" }
		};
		FacebookAndroid.showFacebookShareDialog( parameters );
		
		
#endif
	
	}
	
	public void PostMessageTwitter()
	{
#if UNITY_IPHONE
		Mixpanel.SendEvent("iOS Twitter Share");
		if( canUseTweetSheet )
		{
			{
				//var pathToImage = System.IO.Path.Combine(Application.streamingAssetsPath ,screenshotFilename);
				TwitterBinding.showTweetComposer("I just finished this great game at IHOP®! Download IHOP PLAY and come in to experience it. http://bit.ly/HUTkhc");
			}
		}
#endif
	
#if UNITY_ANDROID
	Mixpanel.SendEvent("Android Twitter Share");
		
	const string Address = "http://twitter.com/intent/tweet";

		string text = "I just finished this great game at IHOP®! Download IHOP PLAY and come in to experience it. http://bit.ly/HUTkhc";

	
	Application.OpenURL(Address +
	        "?text=" + WWW.EscapeURL(text));
	
//	TwitterAndroid.postStatusUpdate("Join the Breakfast Bunch in their wintery wonderland this holiday — download IHOP's new Play app! http://bit.ly/HUTkhc");

#endif
	}



	public void PostMessageFacebookFood()
	{
		
		#if UNITY_IPHONE
		Mixpanel.SendEvent("iOS Facebook Share");
		Debug.Log("FB1");
		if( _canUserUseFacebookComposer )
		{
			Debug.Log("FB2");
			// ensure the image exists before attempting to add it!
			var pathToImage = System.IO.Path.Combine(Application.streamingAssetsPath,screenshotFilename);
			if( !System.IO.File.Exists( pathToImage ) )
				pathToImage = null;
			FacebookBinding.showFacebookComposer( "You’ve got to get into IHOP® today and play this game. Download IHOP PLAY, then come in and experience this game right at the table.", pathToImage, "http://bit.ly/HUTkhc" );
		}else{
			Debug.Log("FB3");
			// parameters are optional. See Facebook's documentation for all the dialogs and paramters that they support
			var parameters = new Dictionary<string,string>
			{
				{ "link", "http://bit.ly/HUTkhc" },
				{ "name", "You’ve got to get into IHOP® today and play this game. Download IHOP PLAY, then come in and experience this game right at the table." },
				{ "picture", "" },
				{ "caption", "" }
			};
			FacebookBinding.showDialog( "stream.publish", parameters );
		}
		#endif
		
		#if UNITY_ANDROID
		Mixpanel.SendEvent("Android Facebook Share");	
		
		var parameters = new Dictionary<string,object>
		{
			{ "link", "http://bit.ly/HUTkhc" },
			{ "name", "You’ve got to get into IHOP® today and play this game. Download IHOP PLAY, then come in and experience this game right at the table." },
			{ "picture", "http://jokatu.com.ar/Images/fb_thumbnail.png" },
			{ "caption", "" }
		};
		FacebookAndroid.showFacebookShareDialog( parameters );
		
		
		#endif
		
	}
	
	public void PostMessageTwitterFood()
	{
		#if UNITY_IPHONE
		Mixpanel.SendEvent("iOS Twitter Share");
		if( canUseTweetSheet )
		{
			{
				//var pathToImage = System.IO.Path.Combine(Application.streamingAssetsPath ,screenshotFilename);
				TwitterBinding.showTweetComposer("You’ve got to get into IHOP® today and play this game. Download IHOP PLAY, then come in and experience this game. http://bit.ly/HUTkhc");
			}
		}
		#endif
		
		#if UNITY_ANDROID
		Mixpanel.SendEvent("Android Twitter Share");
		
		const string Address = "http://twitter.com/intent/tweet";
		
		string text = "You’ve got to get into IHOP® today and play this game. Download IHOP PLAY, then come in and experience this game. http://bit.ly/HUTkhc";
		
		
		Application.OpenURL(Address +
		                    "?text=" + WWW.EscapeURL(text));
		
		//	TwitterAndroid.postStatusUpdate("Join the Breakfast Bunch in their wintery wonderland this holiday — download IHOP's new Play app! http://bit.ly/HUTkhc");
		
		#endif
	}
	
	// common event handler used for all graph requests that logs the data to the console
	void completionHandler( string error, object result )
	{
		if( error != null )
			Debug.LogError( error );
		else
			Prime31.Utils.logObject( result );
	}
	
	public void CloseFullScreen()
	{
		gameController.GetComponent<ViewModel>().isFullScreen = false;
		metaioMovieTexture movieFullScreen = moviePlaneFS.GetComponent<metaioMovieTexture>();
		movieFullScreen.stop();
		fullScreenGO.SetActive(false);
		socialFullScreen.SetActive(false);
	}
	
	public void Terms()
	{
		#if UNITY_IPHONE
		Mixpanel.SendEvent("iOS Terms and Conditions Button");
		#endif
		#if UNITY_ANDROID
		Mixpanel.SendEvent("Android Terms and Conditions Button");
		#endif
		Application.OpenURL("http://www.ihop.com/Legal/Social%20Terms%20and%20Conditions");
	}
	
	public void LinkIhop()
	{
		#if UNITY_IPHONE
		Mixpanel.SendEvent("iOS Link to IHOP Site");
		#endif
		#if UNITY_ANDROID
		Mixpanel.SendEvent("Android Link to IHOP Site");
		#endif
		
		Application.OpenURL("http://www.ihop.com/");
	}
	
	public void Privacy()
	{
		#if UNITY_IPHONE
		Mixpanel.SendEvent("iOS Privacy Button");
		#endif
		#if UNITY_ANDROID
		Mixpanel.SendEvent("Android Privacy Button");
		#endif
		
		Application.OpenURL("http://www.ihop.com/legal/privacy-policy");
	}
	
	public void ShareFullScreen()
	{
		metaioMovieTexture movieFullScreen = moviePlaneFS.GetComponent<metaioMovieTexture>();
		movieFullScreen.stop();
		fullScreenGO.SetActive(false);
		socialFullScreen.SetActive(true);	
	}
	
	public void WatchAgainFullScreen()
	{
		FullScreen();
	}
	
	public void FullScreen()
	{
		#if UNITY_IPHONE
		Mixpanel.SendEvent("iOS Expand Video Button");
		#endif
		#if UNITY_ANDROID
		Mixpanel.SendEvent("Android Expand Video Button");
		#endif
		
		gameController.GetComponent<ViewModel>().isFullScreen = true;
		//Set off normal video
		metaioMovieTexture movie = videoPlane.GetComponent<metaioMovieTexture>();
		TimerUtility timer = videoPlane.GetComponent<TimerUtility>();
		movie.stop();
		timer.PauseTimer();
		//Set on fullscreen video
		//#if UNITY_IPHONE
		//iPhoneSettings.screenOrientation = iPhoneScreenOrientation.LandscapeRight;
		//#endif
		fullScreenGO.SetActive(true);
		socialFullScreen.SetActive(false);
		metaioMovieTexture movieFullScreen = moviePlaneFS.GetComponent<metaioMovieTexture>();
		
		
		TimerUtility timerFullScreen = moviePlaneFS.GetComponent<TimerUtility>();
		if(!String.IsNullOrEmpty(movie.movieFile))
				movieFullScreen.movieFile = movie.movieFile;
		
		movieFullScreen.stop();
		//movieFullScreen.Initialize();
		
		
		timerFullScreen.setDuration(timer.m_Duration,4);
		timerFullScreen.AddCallback(ShowShareFullScreen);
		timerFullScreen.AddCycleCallback(movieCycle);
		movieFullScreen.play(false);
		timerFullScreen.RestartTimer();
		
		/*#if UNITY_IPHONE
		
			Handheld.PlayFullScreenMovie (videoPlane.GetComponent<metaioMovieTexture>().movieFile, Color.black, FullScreenMovieControlMode.CancelOnInput);
			
		#endif*/
	}
	
	void ShowShareFullScreen()
	{
		metaioMovieTexture movieFullScreen = moviePlaneFS.GetComponent<metaioMovieTexture>();
		movieFullScreen.stop();
		socialFullScreen.SetActive(true);
	}
	
	void movieCycle(object sender, CycleEventArgs args)
	{
		int p = args.Cycle*25;
		#if UNITY_IPHONE
		Mixpanel.SendEvent("iOS Video "+p+"% Watched");
		#endif
		#if UNITY_ANDROID
		Mixpanel.SendEvent("Android Video "+p+"% Watched");
		#endif
	}
}

public class ViewModel : MonoBehaviour
{
	public metaioCallback mCallback;
	public NguiRootContext View;
	public ihopUi Context;
	public GameObject panelOne;
	public GameObject panelTwo;
	public GameObject socialFullScreen;
	public metaioMovieTexture moviePlaneFS;
	public GameObject fullScreenGO;
	public GameObject fullScreenButton;
	public GameObject loading;
	public GameObject videoPlane;
	GameObject socialSharing;
	public GameObject playButton;
	public Vector3 socialPosition;
	public Vector3 titlePosition;
	public GameObject title;
	public GameObject social;
	public int firstVideoLenght;
	float seconds;
	public bool firstTime;
	TimerUtility movieTimer; 
	public bool isFullScreen;
	public GameObject menu;
	GameController gameController;
	
	void Awake()
	{
		gameController = GetComponent<GameController>();
		firstTime = true;
		seconds = 0;
		Context = new ihopUi();
		View.SetContext(Context);
		Context.panelOne = panelOne;
		Context.panelTwo = panelTwo;
		Context.fullScreenGO = fullScreenGO;
		Context.socialFullScreen = socialFullScreen;
		Context.moviePlaneFS = moviePlaneFS;
		Context.videoPlane = videoPlane;
		Context.loading = loading;
		Context.gameController = gameController;
		Context.menu = menu;
		isFullScreen = false;
		#if UNITY_IPHONE
		FacebookBinding.init();
		// Replace these with your own CONSUMER_KEY and CONSUMER_SECRET!
		TwitterBinding.init( "DZVuvzBxi7soDZ5ZDeYbXw", "2DCPRBjPXho4KeV0xF9aT3exDM9F1dVE8Y2wG2lnmE" );
		// when the session opens or a reauth occurs we check the permissions to see if we can publish
		FacebookManager.sessionOpenedEvent += () =>
		{
			Context._hasPublishPermission = FacebookBinding.getSessionPermissions().Contains( "publish_stream" );
			Context._hasPublishActions = FacebookBinding.getSessionPermissions().Contains( "publish_actions" );
		};

		FacebookManager.reauthorizationSucceededEvent += () =>
		{
			Context._hasPublishPermission = FacebookBinding.getSessionPermissions().Contains( "publish_stream" );
			Context._hasPublishActions = FacebookBinding.getSessionPermissions().Contains( "publish_actions" );
		};
		// grab a screenshot for later use
		//Application.CaptureScreenshot( ihopUi.screenshotFilename );

		// this is iOS 6 only!
		Context._canUserUseFacebookComposer = FacebookBinding.canUserUseFacebookComposer();
		Context.canUseTweetSheet = TwitterBinding.isTweetSheetSupported() && TwitterBinding.canUserTweet();
		
		#endif
		
		#if UNITY_ANDROID
		
		FacebookAndroid.init();
		TwitterAndroid.init( "DZVuvzBxi7soDZ5ZDeYbXw", "2DCPRBjPXho4KeV0xF9aT3exDM9F1dVE8Y2wG2lnmE" );
		
		#endif

		#if UNITY_IPHONE
			Mixpanel.SendEvent("iOS Start");
		#endif
		#if UNITY_ANDROID
			Mixpanel.SendEvent("Android Start");
		#endif
	}
	bool first;
    void Start()
	{
		first = true;
		Mixpanel.Token = "6e7530b0620fd29bf90f7cb8ae5d4fcf";
	}
	void Update()
	{
		seconds = seconds + Time.deltaTime;
	}
	
	void OnApplicationQuit()
	{
		int t = (int) seconds;
		#if UNITY_IPHONE
		Mixpanel.SendEvent("iOS Application Quit",new Dictionary<string, object> {
		    			{"Time Spent",t}
		});
		#endif
		#if UNITY_ANDROID
		Mixpanel.SendEvent("Android Application Quit",new Dictionary<string, object> {
		    			{"Time Spent",t}
		});
		#endif
		
	}
	
	public void callFunction(string name, string param, int paramInt)
	{
		if(name == "PostMessageFacebook")
			PostMessageFacebook();
		else if(name == "PostMessageTwitter")
				PostMessageTwitter();
		else if(name == "ChangeVideo")
				ChangeVideo(param,paramInt);
		else if(name == "FullScreen")
				FullScreen();
		else if(name == "BackToMenu")
				BackToMenu();
		else if(name == "WatchAgain")
				WatchAgain();
		else if(name == "PlayMovie")
				PlayMovie(true);
	
		
	}
	
	public void PostMessageFacebook()
	{
		Context.PostMessageFacebook();
	}
	
	public void PostMessageTwitter()
	{
		Context.PostMessageTwitter();
	}
	
	#region FullScreen
	
	public void FullScreen()
	{
		#if UNITY_IPHONE
		Mixpanel.SendEvent("iOS Expand Video Button");
		#endif
		#if UNITY_ANDROID
		Mixpanel.SendEvent("Android Expand Video Button");
		#endif
		
		isFullScreen = true;
		//Set off normal video
		metaioMovieTexture movie = videoPlane.GetComponent<metaioMovieTexture>();
		TimerUtility timer = videoPlane.GetComponent<TimerUtility>();
		movie.stop();
		timer.RestartAndPause();
		//Set on fullscreen video
		//#if UNITY_IPHONE
		//iPhoneSettings.screenOrientation = iPhoneScreenOrientation.LandscapeRight;
		//#endif
		fullScreenGO.SetActive(true);
		socialFullScreen.SetActive(false);
		metaioMovieTexture movieFullScreen = moviePlaneFS.GetComponent<metaioMovieTexture>();
		
		
		TimerUtility timerFullScreen = moviePlaneFS.GetComponent<TimerUtility>();
		if(!String.IsNullOrEmpty(movie.movieFile))
				movieFullScreen.movieFile = movie.movieFile;
		if(!first)
		{
			movieFullScreen.stop();
			//movieFullScreen.Initialize();
		}
		else
		{
			first = false;
		}
		
		timerFullScreen.setDuration(timer.m_Duration,4);
		timerFullScreen.AddCallback(ShowShareFullScreen);
		timerFullScreen.AddCycleCallback(movieCycle);
		movieFullScreen.play(false);
		timerFullScreen.RestartTimer();
		
		/*#if UNITY_IPHONE
		
			Handheld.PlayFullScreenMovie (videoPlane.GetComponent<metaioMovieTexture>().movieFile, Color.black, FullScreenMovieControlMode.CancelOnInput);
			
		#endif*/
	}
	
	public void CloseFullScreen()
	{
		TimerUtility timerFullScreen = moviePlaneFS.GetComponent<TimerUtility>();
		timerFullScreen.PauseTimer();
		metaioMovieTexture movieFullScreen = moviePlaneFS.GetComponent<metaioMovieTexture>();
		movieFullScreen.stop();
		socialFullScreen.SetActive(false);
		fullScreenGO.SetActive(false);
	}
	
	void ShowShareFullScreen()
	{
        #if UNITY_IPHONE
		Mixpanel.SendEvent("iOS Video 100% Watched");
		#endif
		#if UNITY_ANDROID
		Mixpanel.SendEvent("Android Video 100% Watched");
		#endif
		
		metaioMovieTexture movieFullScreen = moviePlaneFS.GetComponent<metaioMovieTexture>();
		movieFullScreen.stop();
		fullScreenGO.SetActive(false);
		socialFullScreen.SetActive(true);
	}
	
	#endregion
	
	
	public void ChangeVideo(string videoName, int videoLenght)
	{
		if(firstTime)
			PlayMovie(false);
		// Hide Social Sharing
		socialSharing = GameObject.FindGameObjectWithTag("Social");
		if(socialSharing != null)
			socialSharing.SetActive(false);
		
		title.transform.position = titlePosition;
		playButton.transform.position = new Vector3(10000,10000,10000);
		TimerUtility timer = videoPlane.GetComponent<TimerUtility>();
		timer.PauseTimer();
		timer.setDuration(videoLenght,4);
		//timer.AddCallback(movieFinished);
		//timer.AddCycleCallback(movieCycle);
		//Stop Current Movie, change to new one and plays it
		metaioMovieTexture movie = videoPlane.GetComponent<metaioMovieTexture>();
		movie.stop();
		movie.movieFile = videoName;
		movie.play(false);
		timer.RestartTimer();
		#if UNITY_IPHONE
		Mixpanel.SendEvent("iOS Play Video",new Dictionary<string, object> {
		    			{"Video Name", videoPlane.GetComponent<metaioMovieTexture>().movieFile}
		});
		#endif
		#if UNITY_ANDROID
		Mixpanel.SendEvent("Android Play Video",new Dictionary<string, object> {
		    			{"Video Name", videoPlane.GetComponent<metaioMovieTexture>().movieFile}
		});
		#endif
	
	}
	
	public void BackToMenu()
	{
		Application.LoadLevel("Menu");
		
	}
	
	public void WatchAgain()
	{
		#if UNITY_IPHONE
		Mixpanel.SendEvent("iOS Watch Again Video",new Dictionary<string, object> {
		    			{"Video Name", videoPlane.GetComponent<metaioMovieTexture>().movieFile}
		});
		#endif
		#if UNITY_ANDROID
		Mixpanel.SendEvent("Android Watch Again Video",new Dictionary<string, object> {
		    			{"Video Name", videoPlane.GetComponent<metaioMovieTexture>().movieFile}
		});
		#endif
		
		ChangeVideo(videoPlane.GetComponent<metaioMovieTexture>().movieFile,int.Parse(videoPlane.GetComponent<TimerUtility>().m_Duration.ToString()));
	}
	
	public void PlayMovie(bool play)
	{
		if(firstTime)
		{
			firstTime = false;
			//GameObject snowSprite = GameObject.Find("SnowSprite");
			//snowSprite.GetComponent<tk2dSpriteAnimator>().Play("IntroOut");
			playButton.transform.position = new Vector3(10000,10000,10000);
			StartCoroutine(ShowVideo());
		}
		//if(play)
		//	ChangeVideo(videoPlane.GetComponent<metaioMovieTexture>().movieFile,int.Parse(videoPlane.GetComponent<TimerUtility>().m_Duration.ToString()));
	}
	
	
	IEnumerator ShowVideo()
	{
		yield return new WaitForSeconds(3);
	    GameObject snowSprite = GameObject.Find("SnowSprite");
		snowSprite.SetActive(false);
		Debug.Log("Show Video");
		videoPlane.SetActive(true);
		fullScreenButton.transform.position = socialPosition;
		title.transform.position = titlePosition;
		metaioMovieTexture movieTexture = videoPlane.GetComponent<metaioMovieTexture>();
		movieTexture.play(false);
		
		//StartCoroutine(GetScreenshot());
		if(movieTimer == null)
		{
			movieTimer = videoPlane.GetComponent<TimerUtility>();
			movieTimer.setDuration(firstVideoLenght,4);
			movieTimer.AddCallback(movieFinished);
			movieTimer.AddCycleCallback(movieCycle);
			movieTimer.StartTimer();
		}else{
				movieTimer.ResumeTimer();
			}
	}
	
	void movieFinished()
	{
		#if UNITY_IPHONE
			Mixpanel.SendEvent("iOS Video 100% Watched");
		#endif
		#if UNITY_ANDROID
			Mixpanel.SendEvent("Android Video 100% Watched");
		#endif
		
		movieTimer.PauseTimer();
		videoPlane.GetComponent<metaioMovieTexture>().stop();
		title.transform.position = new Vector3(10000,10000,10000);
		social.SetActive(true);
	}
	
	
	void movieCycle(object sender, CycleEventArgs args)
	{
		int p = args.Cycle*25;
		#if UNITY_IPHONE
			Mixpanel.SendEvent("iOS Video "+p+"% Watched");
		#endif
		#if UNITY_ANDROID
			Mixpanel.SendEvent("Android Video "+p+"% Watched");
		#endif
		
	}

	//GAMES

	public void StartLemonadeGame()
	{
		gameController.StartLemonadeGame();
	}

	public void StartSimonGame()
	{
		gameController.StartSimonGame();
	}
	

	
}
