using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManScript : MonoBehaviour
{
    public float camSpeed;
    public Transform camTarget;
    private Vector2 camTargetPos;
    private void Update()
    {
        camTargetPos = camTarget.position;
        transform.position = Vector2.Lerp(transform.position, camTargetPos, camSpeed*Time.deltaTime);
    }
}
