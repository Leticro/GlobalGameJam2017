using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
	#region Private
	[SerializeField]
	private string _name;

    AudioSource _audio;
	#endregion

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			// TODO: Level complete screen.
			var hud = FindObjectOfType<HUD>();
			hud.SetCountDownText("");
			hud.ShowButtons(_name);
			
			var waveMaker = FindObjectOfType<WaveMaker>();
			waveMaker.EndGame();

			Time.timeScale = 0;
            if (_audio)
            {
                _audio.Play();
            }
        }
	}
}
