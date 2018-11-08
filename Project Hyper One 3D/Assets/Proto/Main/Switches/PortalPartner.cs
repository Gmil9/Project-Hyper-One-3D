using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalPartner : MonoBehaviour {

    [SerializeField] int lane;

    public int GetPartnerLane(){
        return lane;
    }
}
