using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class findPlayer : MonoBehaviour
{

    private CinemachineVirtualCamera virtualCamera;
    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }
    // Update is called once per frame
    void Update()
    {
        virtualCamera.Follow = GameObject.FindGameObjectWithTag("Player").transform;
        virtualCamera.LookAt = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
