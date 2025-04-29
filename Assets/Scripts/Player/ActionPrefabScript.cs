using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPrefabScript : MonoBehaviour
{
    public float slashForce;
    public float dashForce;
    // [precast] --> [action(collider exists)]
    public float precastDuration;
    public float actionDuration;
    public GameObject colliderPrefab;
}
