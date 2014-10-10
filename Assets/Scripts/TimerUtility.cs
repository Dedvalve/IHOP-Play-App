using UnityEngine;
using System;

public class CycleEventArgs : EventArgs
{
    private readonly int cycle;

    public CycleEventArgs(int cycle)
    {
        this.cycle = cycle;
    }

    public int Cycle
    {
        get { return this.cycle; }
    }
}


public class TimerUtility : MonoBehaviour
{
	//----------------------------------------------------------------------
	#region Class members
	
	public delegate void Callback();
	public delegate void CycleCallback(object sender, CycleEventArgs args);
	public event Callback m_CallbackEvents;
	public double m_Duration = 0.0;
	public bool m_WriteToLog = false;
	protected Callback m_Callback;
	protected bool m_Finished = false;
	protected bool m_Running = false;
	protected bool m_Started = false;
	public DateTime m_StartTime;
	protected DateTime m_PauseTime;
	protected DateTime m_DesiredTime;
	public event CycleCallback m_CycleCallbackEvents;
	public int cycles;
	private int currentCycle;
	public double cycleDuration = 0.0;
	
	#endregion
	
	
	//----------------------------------------------------------------------
	#region Public duration member accessor
	
	public double Duration
	{
		get
		{
			// Return current member value
			return this.m_Duration;
		}
	}
	
	#endregion
	
	
	//----------------------------------------------------------------------
	#region Public running member accessor
	
	public bool Running
	{
		get
		{
			// Return current member value
			return this.m_Running;
		}
	}
	
	#endregion
	
	
	//----------------------------------------------------------------------
	#region Public finished member accessor
	
	public bool Finished
	{
		get
		{
			// Return current member value
			return this.m_Finished;
		}
	}
	
	#endregion
	public void setDuration(double Duration, int Cycles)
	{ 
		this.m_Duration=Duration;
		this.cycles=Cycles;
		this.cycleDuration=Duration/Cycles;
	}
	
	
	//----------------------------------------------------------------------
	#region Public get elapsed time method
	
	public double GetElapsedTime()
	{
		// Create local variable
		TimeSpan interval;
		
		// Check if timer is running
		if(this.m_Running == true)
		{
			// Set interval value
			interval = DateTime.Now - this.m_StartTime;
		}
		else
		{
			// Check if timer is finished
			if(this.m_Finished == true)
			{
				interval = this.m_DesiredTime - this.m_DesiredTime;
			}
			else
			{
				// Set interval value
				interval = this.m_PauseTime - this.m_StartTime;
			}
		}
		
		// Check if running a debug build
		if(Debug.isDebugBuild == true && this.m_WriteToLog == true)
		{
			// Output to debug log
			Debug.Log("Elapsed time on timer is : " + interval.TotalSeconds.ToString());
		}
		
		// Return value
		return interval.TotalSeconds;
	}
	
	#endregion
	
	
	//----------------------------------------------------------------------
	#region Public get remaining time method
	
	public double GetRemainingTime()
	{
		// Create local variable
		TimeSpan interval;
		
		// Check if timer is running
		if(this.m_Running == true)
		{
			// Set interval value
			interval = this.m_DesiredTime - DateTime.Now;
		}
		else
		{
			// Check if timer is finished
			if(this.m_Finished == true)
			{
				interval = this.m_DesiredTime - this.m_DesiredTime;
			}
			else
			{
				// Set interval value
				interval = this.m_DesiredTime - this.m_PauseTime;
			}
		}
		
		// Check if running a debug build
		if(Debug.isDebugBuild == true && this.m_WriteToLog == true)
		{
			// Output to debug log
			Debug.Log("Remaining time on timer is : " + interval.TotalSeconds.ToString());
		}
		
		// Return value
		return interval.TotalSeconds;
	}
	
	#endregion
	
	
	//----------------------------------------------------------------------
	#region Public add callback method
	
	public virtual void AddCallback(Callback handler)
	{
		// Add callback to callback events
		this.m_CallbackEvents += new TimerUtility.Callback(handler);
	}
	
	protected virtual void SendCallEvents(){
		if(this.m_CallbackEvents != null)
				{
					// Run callbacks from callback events
					this.m_CallbackEvents();
				}
	}
	
	public void AddCycleCallback(CycleCallback handler)
	{
		this.m_CycleCallbackEvents += new TimerUtility.CycleCallback(handler);
	}
	#endregion
	
	
	//----------------------------------------------------------------------
	#region Public start timer method
	
	public virtual void StartTimer()
	{
		// Check to see if timer has already been started
		if(this.m_Started == false)
		{
			// Set member values
			this.m_StartTime = DateTime.Now;
			this.m_DesiredTime = this.m_StartTime.AddSeconds(this.m_Duration);
			this.m_Running = true;
			this.m_Started = true;
			this.currentCycle = 1;
		}
	}
	
	public void RestartTimer(){
		this.m_Started = false;
		this.m_Finished=false;
		StartTimer();
	}
	
	public void StartTimer(DateTime startTime)
	{
		// Check to see if timer has already been started
		if(this.m_Started == false)
		{
			
			// Set member values
			this.m_StartTime = startTime;
			this.m_DesiredTime = this.m_StartTime.AddSeconds(this.m_Duration);
			Debug.Log("Start timer from: "+this.m_StartTime+" to: "+this.m_DesiredTime);
			this.m_Running = true;
			this.m_Started = true;
		}
	}
	
	
	#endregion
	
	public void RestartAndPause()
	{
		this.m_Started = false;
		this.m_Finished=false;
		StartTimer();
		// Check to see if timer has already been started
		if(this.m_Started == true && this.m_Running == true)
		{
			// Set member values
			this.m_PauseTime = DateTime.Now;
			this.m_Running = false;
		}
	}
	//----------------------------------------------------------------------
	#region Public pause timer method
	
	public void PauseTimer()
	{
		// Check to see if timer has already been started
		if(this.m_Started == true && this.m_Running == true)
		{
			// Set member values
			this.m_PauseTime = DateTime.Now;
			this.m_Running = false;
		}
	}
	
	#endregion
	
	
	//----------------------------------------------------------------------
	#region Public resume timer method
	
	public void ResumeTimer()
	{
		// Check to see if timer has already been started
		if(this.m_Started == true && this.m_Running == false)
		{
			// Set member values
			this.m_StartTime = DateTime.Now;
			this.m_DesiredTime = this.m_StartTime.AddSeconds(this.GetRemainingTime());
			this.m_Running = true;
		}
	}
	
	#endregion
	
	
	//----------------------------------------------------------------------
	#region Private update method
	
	protected virtual void Update()
	{
		// Check to see if timer is running
		if(this.m_Running == true)
		{
			// Check to see if there is any time remaining
			double remainingTime=this.GetRemainingTime();
			if(remainingTime <= 0.0)
			{
				Debug.Log("Timer Finished");
				// Set member values
				this.m_Finished = true;
				this.m_Running = false;
				
				// Check to see if callback events is empty
				
				SendCallEvents();
			}else if(( (m_Duration-remainingTime) >= (currentCycle*cycleDuration))&&(currentCycle < cycles))
				  {
					Debug.Log("Cycle Finished "+currentCycle);
					//Debug.Log(m_Duration +"-"+remainingTime+"-"+currentCycle+"-"+cycleDuration);
				   	   // Run cycle callbacks from callback events 
					   if (this.m_CycleCallbackEvents!=null)
							this.m_CycleCallbackEvents(this, new CycleEventArgs(currentCycle));
					   currentCycle++;
				  }
		}
	}
	
	#endregion
	
	
	//----------------------------------------------------------------------
	#region Private on draw gizmos method
	
	private void OnDrawGizmos()
	{
		// Draw editor gizmos icon
		Gizmos.DrawIcon(this.transform.position, "timer.png");
	}
	
	public string outReadableTime() 
	{ //format the floating point seconds to M:S 
   	 	int i_minutes; 
   		int i_seconds; 
   		int i_time; 
		int i_days;
   		int i_hours;
		string s_timetext; 
   		i_time = (int)GetRemainingTime(); 
		i_days = i_time / 86400;
		//s_timetext = i_days + " Dias ";
		i_hours = (i_time / 3600) - (i_days * 24);
		/*if (i_hours > 9)
			s_timetext = i_hours.ToString()+":";
		else
			s_timetext += "0" + i_hours.ToString()+":";*/
		
		i_minutes = (i_time / 60) - (i_days * 1440) - (i_hours * 60);
		//if (i_minutes > 9)
			s_timetext= i_minutes.ToString()+":";
		/*else
			s_timetext += "0" + i_minutes.ToString()+":";*/
		
		i_seconds = i_time % 60;

		if (i_seconds > 9)
			s_timetext += i_seconds.ToString();
		else
			s_timetext += "0" + i_seconds.ToString();
	

		
		return s_timetext;

	}
	#endregion
}