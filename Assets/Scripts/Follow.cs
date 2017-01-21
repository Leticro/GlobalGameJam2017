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
    private float sensativity = 1 / 10;

    void Update()
    {
        var scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f && _camera.transform.position.y > 6) //zoom in
        {
            camDistance -= new Vector3((float)-0.8, 2, (float)-1.5);
        }
        else if (scroll < 0f)
        {
            camDistance += new Vector3((float)-0.8, 2, (float)-1.5); //zoom out
        }

        _camera.transform.position = _player.transform.position + camDistance;

        //Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
    }
}