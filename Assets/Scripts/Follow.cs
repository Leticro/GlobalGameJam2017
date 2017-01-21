using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private GameObject _player;

    private Vector3 camDistance = new Vector3(-8, 20, -15);

    void Update()
    {
        var scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
        {
            camDistance -= new Vector3(0, 2, 0);
        }
        else if (scroll < 0f)
        {
            camDistance += new Vector3(0, 2, 0);
        }

        _camera.transform.position = _player.transform.position + camDistance;

        //Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
    }
}