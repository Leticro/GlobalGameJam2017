using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    private  GameObject _player;

	public System.Action<GameObject> OnSetPlayer;

    private void Update()
    {
        transform.transform.position = _player.transform.position; // camera follow player
    }

	public void SetPlayer(GameObject player)
	{
		_player = player;

		OnSetPlayer(_player);
	}
}
