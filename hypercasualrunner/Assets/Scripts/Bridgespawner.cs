using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridgespawner : MonoBehaviour
{
    public GameObject endref, startref;
    public BoxCollider hiddenPlatform;
    void Start()
    {
        Vector3 direction = endref.transform.position - startref.transform.position;
        float distance = direction.magnitude;
        direction = direction.normalized;
        hiddenPlatform.transform.forward = direction;
        hiddenPlatform.size = new Vector3(hiddenPlatform.size.x, hiddenPlatform.size.y,distance);
        hiddenPlatform.transform.position = startref.transform.position + (direction * distance / 2) + (new Vector3(0, -direction.z, direction.y) * hiddenPlatform.size.y/2);

    }


}
