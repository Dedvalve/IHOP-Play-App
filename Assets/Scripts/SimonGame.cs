using UnityEngine;
using System.Collections;

public class SimonGame : MonoBehaviour
{

	int [] sequence;

	int squares = 3;

	int sequenceMax = 50;

	public GameObject [] buttons;

	public GameObject [] scoreBackgrounds;

	public AudioSource [] buttonSounds;

	public UILabel scoreLabel;

	public GameObject mainGame;

	public GameObject steam;

	public GameObject buttonsContainer;

	public GameObject repeat;

	public GameObject getReady;

	public GameObject loose;

	public GameObject help;

	public GameObject pause;

	public GameObject mainMenu;

	int currentSequenceIndex;

	int currentSequenceLength;

	int score;

	bool firstTime;

	public int scoreIncrement = 5;

	public AudioSource wrongAnswerSound;
	public AudioSource warningSound;

	// Use this for initialization
	void Start ()
	{

	}

	public void StartGame()
	{
		mainGame.SetActive(true);
		steam.SetActive(true);
		buttonsContainer.SetActive(false);

	}

	public void BeginTutorial()
	{
		sequence = new int[3];
		
		for(int i = 0; i < 3; i++)
		{
			sequence[i] = i;
		}
		currentSequenceLength = 3;
		
		steam.SetActive(false);
		firstTime = true;
		StartCoroutine(ShowSequenceTutorial());
		score = 0;
		scoreLabel.text = score.ToString();
		scoreLabel.gameObject.SetActive(false);
		for(int i = 0; i < squares; i++)
		{
			scoreBackgrounds[i].SetActive(false);
		}
	}


	IEnumerator ShowSequenceTutorial()
	{
		for(int i = 0; i < squares; i++)
		{
			buttons[i].collider.enabled = false;
		}
		buttonsContainer.SetActive(true);
	
		
		for(int i = 0; i < squares; i++)
		{
			buttons[i].transform.FindChild("Background").gameObject.SetActive(false);
		}
		
		for(int i = 0; i < currentSequenceLength; i++)
		{
			GlowSquare(i, true);
			yield return new WaitForSeconds(0.5f);
			DarkSquare(i);
			yield return new WaitForSeconds(0.25f);
		}
		
		repeat.SetActive(false);
		getReady.SetActive(true);

		GlowSquare(0, false);
		GlowSquare(1, false);
		GlowSquare(2, false);


		yield return new WaitForSeconds(1);

		DarkSquare(0);
		DarkSquare(1);
		DarkSquare(2);

		getReady.SetActive(false);

		currentSequenceIndex = 0;

		Begin ();
		//StartCoroutine(WarningSoundRoutine());
	}


	public void Begin()
	{
		sequence = new int[sequenceMax];
		
		for(int i = 0; i < sequenceMax; i++)
		{
			sequence[i] = (int)Random.Range(0,squares);
		}
		currentSequenceLength = 2;

		steam.SetActive(false);
		StartCoroutine(ShowSequence());
		score = 0;
		scoreLabel.text = score.ToString();
		scoreLabel.gameObject.SetActive(false);
		for(int i = 0; i < squares; i++)
		{
			scoreBackgrounds[i].SetActive(false);
		}
	}

	public void Restart()
	{
		Time.timeScale = 1;
		pause.SetActive(false);
		loose.SetActive(false);
		Begin();
	}

	public void Quit()
	{
		pause.SetActive(false);
		loose.SetActive(false);
		mainGame.SetActive(false);
		steam.SetActive(false);
		getReady.SetActive(false);	
		repeat.SetActive(false);
		Time.timeScale = 1;
		mainMenu.SetActive(true);
	}


	IEnumerator ShowSequence()
	{
		for(int i = 0; i < squares; i++)
		{
			buttons[i].collider.enabled = false;
		}
		buttonsContainer.SetActive(true);

		if(firstTime)
		{
			repeat.SetActive(true);
			yield return new WaitForSeconds(2);

		}else{
			yield return new WaitForSeconds(1);
			repeat.SetActive(true);
		}

		for(int i = 0; i < squares; i++)
		{
			buttons[i].transform.FindChild("Background").gameObject.SetActive(false);
		}
		
		for(int i = 0; i < currentSequenceLength; i++)
		{
			GlowSquare(i, true);
			yield return new WaitForSeconds(0.5f);
			DarkSquare(i);
			yield return new WaitForSeconds(0.25f);
		}

		repeat.SetActive(false);
		getReady.SetActive(true);

		if(firstTime)
		{
			yield return new WaitForSeconds(2);
			scoreBackgrounds[0].SetActive(true);
			scoreLabel.gameObject.SetActive(true);
			firstTime = false;
		}
		getReady.SetActive(false);
		for(int i = 0; i < squares; i++)
		{
			buttons[i].collider.enabled = true;
			buttons[i].transform.FindChild("BackgroundTransparent").gameObject.SetActive(true);
			buttons[i].transform.FindChild("Background").gameObject.SetActive(false);
			buttons[i].SetActive(true);
		}
		currentSequenceIndex = 0;
		//StartCoroutine(WarningSoundRoutine());
	}


	void GlowSquare(int i, bool sound)
	{
		int index = sequence[i];	
		if(sound)
			buttonSounds[index].Play();
		buttons[index].transform.FindChild("Sign").gameObject.SetActive(true);
		buttons[index].transform.FindChild("Background").gameObject.SetActive(true);
	}

	void DarkSquare(int i)
	{
		int index = sequence[i];	
		buttons[index].transform.FindChild("Background").gameObject.SetActive(false);
		buttons[index].transform.FindChild("Sign").gameObject.SetActive(false);

	}

	public void ButtonPressed (int index)
	{
		//StopCoroutine("WarningSoundRoutine");
		//buttonSounds[index].Play();
		Debug.Log("Pressed: "+index+"- Correct: "+sequence[currentSequenceIndex] +"- currentSequenceIndex: "+currentSequenceIndex +" -currentSequenceLength"+currentSequenceLength);
		if(sequence[currentSequenceIndex] == index)
		{
			//Correct
			for(int i = 0; i < squares; i++)
			{
				scoreBackgrounds[i].SetActive(false);
			}
			scoreBackgrounds[index].SetActive(true);
			scoreLabel.gameObject.SetActive(true);

			currentSequenceIndex++;
			if(currentSequenceIndex == currentSequenceLength)
			{
					currentSequenceLength++;
					score+=scoreIncrement;
					scoreLabel.text = score.ToString();
					StartCoroutine(ShowSequence());
		
			}//else{
				//StartCoroutine(WarningSoundRoutine());
			//}


		}else{
			//Incorrect
			wrongAnswerSound.Play();
			StartCoroutine(LooseRoutine());
		}
	}

	IEnumerator LooseRoutine()
	{
		for(int i = 0; i < squares; i++)
		{
			buttons[i].collider.enabled = false;
		}

		yield return new WaitForSeconds(0.5f);
		for(int i = 0; i < squares; i++)
		{
			buttons[i].transform.FindChild("Background").gameObject.SetActive(false);
		}
		buttons[sequence[currentSequenceIndex]].transform.FindChild("Background").gameObject.SetActive(true);
		buttons[sequence[currentSequenceIndex]].transform.FindChild("Sign").gameObject.SetActive(true);
		yield return new WaitForSeconds(1.5f);
		loose.SetActive(true);
		buttons[sequence[currentSequenceIndex]].transform.FindChild("Background").gameObject.SetActive(false);
		buttons[sequence[currentSequenceIndex]].transform.FindChild("Sign").gameObject.SetActive(false);

	}
	bool buttonPressed;

	IEnumerator WarningSoundRoutine()
	{
		yield return new WaitForSeconds(3.0f);
		if(!buttonPressed)
			warningSound.Play();
	}
	
	// Update is called once per frame
	void Update ()
	{

	}


	public void Help ()
	{
		help.SetActive(true);
		Time.timeScale = 0;
		for(int i = 0; i < squares; i++)
		{
			buttons[i].collider.enabled = false;
		}

	}

	public void CloseHelp()
	{
		help.SetActive(false);
		Time.timeScale = 1;
		for(int i = 0; i < squares; i++)
		{
			buttons[i].collider.enabled = true;
		}

	}

	public void Pause()
	{
		pause.SetActive(true);
		Time.timeScale = 0;
	}

	public void Continue()
	{
		pause.SetActive(false);
		Time.timeScale = 1;
	}


}

