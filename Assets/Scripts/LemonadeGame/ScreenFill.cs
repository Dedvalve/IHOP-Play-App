using UnityEngine;
using System.Collections;

public class ScreenFill : MonoBehaviour
{

	public float maxY = 1.0f;
	public float YInc = 0.1f;
	public Vector3 initialPosition = new Vector3(0,-19,0);
	public AudioSource waterLoopSound;
	public UILabel debugLabel;
	public Transform fruits;

	private const float lowPassFilterFactor = 0.2f;

	private readonly Quaternion baseIdentity =  Quaternion.Euler(90, 0, 0);
	private Quaternion cameraBase =  Quaternion.identity;
	private Quaternion calibration =  Quaternion.identity;
	private Quaternion baseOrientation =  Quaternion.Euler(90, 0, 0);
	private Quaternion baseOrientationRotationFix =  Quaternion.identity;

	public AudioSource flushSound;

	private Quaternion referanceRotation = Quaternion.identity;


	// Use this for initialization
	void Start ()
	{
		AttachGyro();
	
	}

	/// <summary>
	/// Attaches gyro controller to the transform.
	/// </summary>
	private void AttachGyro()
	{

		ResetBaseOrientation();
		UpdateCalibration(true);
		UpdateCameraBaseRotation(true);
		RecalculateReferenceRotation();
	}


	
	public void Restart()
	{
		transform.localPosition = initialPosition;
		fruits.localPosition = initialPosition;
		fruits.rotation = new Quaternion(0,0,0,0);
		transform.rotation = new Quaternion(0,0,0,0);
		soundStarted = false;
		flush = false;
		drainTime = 0;
	}

	bool soundStarted;

	bool flush;

	float drainTime;

	float rotz;

	float xInc = 0;

	float zLimit = 0.45f;
	void Update ()
	{
		if(!flush)
		{
			if(transform.localPosition.y < maxY)
			{
				transform.position = new Vector3(transform.position.x, transform.position.y + YInc, transform.position.z);
				//fruits.position = new Vector3(fruits.position.x, fruits.position.y + YInc, fruits.position.z);

					
			}else {
				if((!soundStarted)&&(transform.localPosition.y >= maxY))
				  {
						soundStarted = true;
						waterLoopSound.Play();
						fruits.GetComponent<Floating>().BeginFloat();
				}
			
				/*
				Quaternion r = ConvertRotation(Input.gyro.attitude);
			
				float rz = r.w;

				transform.localRotation = new Quaternion(transform.localRotation.x, transform.localRotation.y, rz, transform.localRotation.w);
				fruits.localRotation = new Quaternion(fruits.localRotation.x, fruits.localRotation.y, rz, fruits.localRotation.w);

				float posX = 0.15f*rz;
				float posy = -(0.15f)*Mathf.Abs(rz) + (maxY);

				transform.localPosition = new Vector3(posX,posy,0);
				//fruits.localPosition = new Vector3(posX,posy,0);

				*/

				Quaternion rot = Quaternion.Slerp(transform.rotation,
				                                      cameraBase * ( ConvertRotation(referanceRotation * Input.gyro.attitude) * GetRotFix()), lowPassFilterFactor);
				rotz = rot.z;

				if(rotz > zLimit)
				{
					
					drainTime+=Time.deltaTime;
					rotz = zLimit;
				}else if(rotz < -zLimit)
				{
					rotz = -zLimit;
					drainTime+=Time.deltaTime;

				}else{
					drainTime = 0;
				}

				transform.rotation = new Quaternion(transform.rotation.x,transform.rotation.y, rotz, rot.w);
				fruits.localRotation = new Quaternion(fruits.localRotation.x, fruits.localRotation.y, rotz, rot.w);
				float posX = 0.15f*rotz;
				float posy = -(0.15f)*Mathf.Abs(rotz) + (maxY);
				
				transform.localPosition = new Vector3(posX,posy,0);

				if(drainTime >= 1.5f)
				{
					Flush(true);
				}
			}
		}else{

			if(transform.localPosition.y > initialPosition.y)

			{
				if(rotz >= zLimit)
				{
					xInc = 0.17f;	
				}else if(rotz <= -zLimit)
					{
						xInc = -0.17f;
					}else{
						xInc = 0;
					}

				transform.position = new Vector3(transform.position.x + xInc, transform.position.y - YInc, transform.position.z);
				fruits.position = new Vector3(fruits.position.x + xInc, fruits.position.y - YInc, fruits.position.z);

			}else{
					flush = false;
					gameObject.SetActive(false);
					fruits.gameObject.SetActive(false);
				}


		}
	}



	private static Quaternion ConvertRotation(Quaternion q)
	{
		return new Quaternion(q.x, q.y, -q.z, -q.w);
	}

	public void Flush(bool callReplay)
	{
		waterLoopSound.Stop();
		if(!flush)
		{
			flushSound.Play();
			flush = true;
		}

		if(callReplay)
		{
			StartCoroutine(Replay());
		}

	}

	IEnumerator Replay()
	{
		yield return new WaitForSeconds(2);
		GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().LemonadeWinReplay();
	}
	/// <summary>
	/// Recalculates reference system.
	/// </summary>
	private void ResetBaseOrientation()
	{
		baseOrientationRotationFix = GetRotFix();
		baseOrientation = baseOrientationRotationFix * baseIdentity;
	}
	
	/// <summary>
	/// Recalculates reference rotation.
	/// </summary>
	private void RecalculateReferenceRotation()
	{
		referanceRotation = Quaternion.Inverse(baseOrientation)*Quaternion.Inverse(calibration);
	}

	private Quaternion GetRotFix()
	{
		#if UNITY_3_5
		if (Screen.orientation == ScreenOrientation.Portrait)
			return Quaternion.identity;
		
		if (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.Landscape)
			return landscapeLeft;
		
		if (Screen.orientation == ScreenOrientation.LandscapeRight)
			return landscapeRight;
		
		if (Screen.orientation == ScreenOrientation.PortraitUpsideDown)
			return upsideDown;
		return Quaternion.identity;
		#else
		return Quaternion.identity;
		#endif
	}

	/// <summary>
	/// Update the gyro calibration.
	/// </summary>
	private void UpdateCalibration(bool onlyHorizontal)
	{
		if (onlyHorizontal)
		{
			var fw = (Input.gyro.attitude) * (-Vector3.forward);
			fw.z = 0;
			if (fw == Vector3.zero)
			{
				calibration = Quaternion.identity;
			}
			else
			{
				calibration = (Quaternion.FromToRotation(baseOrientationRotationFix * Vector3.up, fw));
			}
		}
		else
		{
			calibration = Input.gyro.attitude;
		}
	}
	
	/// <summary>
	/// Update the camera base rotation.
	/// </summary>
	/// <param name='onlyHorizontal'>
	/// Only y rotation.
	/// </param>
	private void UpdateCameraBaseRotation(bool onlyHorizontal)
	{
		if (onlyHorizontal)
		{
			var fw = transform.forward;
			fw.y = 0;
			if (fw == Vector3.zero)
			{
				cameraBase = Quaternion.identity;
			}
			else
			{
				cameraBase = Quaternion.FromToRotation(Vector3.forward, fw);
			}
		}
		else
		{
			cameraBase = transform.rotation;
		}
	}


}

