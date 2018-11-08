using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBlade : MonoBehaviour {

    [SerializeField] float spinSpeed = 10f;

    void Update()
    {
        transform.Rotate(0, 0, Time.deltaTime * -spinSpeed, Space.Self);
    }
}
