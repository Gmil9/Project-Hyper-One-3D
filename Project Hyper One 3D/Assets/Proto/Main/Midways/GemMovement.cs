using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemMovement : MonoBehaviour {

    [SerializeField] float spinSpeed = 10f;

    void Update()
    {
        transform.Rotate(0, Time.deltaTime * spinSpeed, 0, Space.Self);
    }

}
