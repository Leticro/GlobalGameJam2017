using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    public GameObject _player;
    [SerializeField]
    private float _zoomOutCap;
    [SerializeField]
    private float _zoomInCap;

    private Vector3 camDistance = new Vector3(-8, 20, -15);
    private Vector3 camSense = new Vector3(-8, 20, -15)/10;

    void Update()
    {
        mouseScroll(); // enable mouse scrolling

        _camera.transform.position = _player.transform.position + camDistance; // camera follow player
    }

    void mouseScroll()
    {
        var scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f && _camera.transform.position.y > _zoomInCap)
        {
            camDistance -= camSense;
        }
        else if (scroll < 0f && _camera.transform.position.y < _zoomOutCap)
        {
            camDistance += camSense;
        }
    }
}