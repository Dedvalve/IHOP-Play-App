//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2013 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Similar to UIButtonColor, but adds a 'disabled' state based on whether the collider is enabled or not.
/// </summary>

public class UIButtonSplashScreen : UIButtonColor
{
	/// <summary>
	/// Color that will be applied when the button is disabled.
	/// </summary>
	
	//public Color disabledColor = Color.grey;
	public GameObject Glow;
	/// <summary>
	/// If the collider is disabled, assume the disabled color.
	/// </summary>
	
	protected override void OnEnable ()
	{
		if (isEnabled) base.OnEnable();
		else UpdateColor(false, true);
	}
	
	protected override void OnHover (bool isOver) { if (isEnabled) base.OnHover(isOver); }
	protected override void OnPress (bool isPressed) 
	{ 
		StartCoroutine(ShowGlow());
		if (isEnabled) base.OnPress(isPressed); 
	}
	
	/// <summary>
	/// Whether the button should be enabled.
	/// </summary>
	
	public bool isEnabled
	{
		get
		{
			Collider col = collider;
			return col && col.enabled;
		}
		set
		{
			Collider col = collider;
			if (!col) return;
			
			if (col.enabled != value)
			{
				col.enabled = value;
				UpdateColor(value, false);
			}
		}
	}
	
	/// <summary>
	/// Update the button's color to either enabled or disabled state.
	/// </summary>
	
	public void UpdateColor (bool shouldBeEnabled, bool immediate)
	{
		if (tweenTarget)
		{
			//if (!mStarted)
			//{
			//	mStarted = true;
			//	Init();
			//}
			
			Color c = shouldBeEnabled ? defaultColor : disabledColor;
			TweenColor tc = TweenColor.Begin(tweenTarget, 0.15f, c);
			
			if (immediate)
			{
				tc.value = c;
				tc.enabled = false;
			}
		}
	}

	IEnumerator ShowGlow()
	{
		Glow.SetActive(true);
		yield return new WaitForSeconds(0.75f);
		Glow.SetActive(false);
	}


}
