using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
[AddComponentMenu("NGUI/NData/OnClick Binding")]
public class NguiOnClickBindingRetarded : NguiCommandBinding
{
	public int seconds = 2;
	void OnClick()
	{
		if (_command == null)
		{
			return;
		}

		StartCoroutine(ClickRoutine());

	}

	IEnumerator ClickRoutine()
	{
		yield return new WaitForSeconds(seconds);
		_command.DynamicInvoke();
	}
}

