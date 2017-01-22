using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
	#region Private
	[SerializeField]
	private string _name;
	#endregion

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			// TODO: Level complete screen.
			var hud = FindObjectOfType<HUD>();
			hud.SetCountDownText("Finished!");
			hud.ShowButtons(_name);
			
			var waveMaker = FindObjectOfType<WaveMaker>();
			waveMaker.EndGame();

			Time.timeScale = 0;
		}
	}
}
