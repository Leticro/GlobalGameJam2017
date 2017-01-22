using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ClearPlayerPrefs : Editor 
{
	[MenuItem("Tools/Clear Player Prefs")]
	private static void Clear()
	{
		PlayerPrefs.DeleteAll();
	}
}
