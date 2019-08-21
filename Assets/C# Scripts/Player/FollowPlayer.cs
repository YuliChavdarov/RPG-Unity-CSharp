using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    [SerializeField] GameObject player;
    [SerializeField] Vector3 cameraOffset;

	void Start () {

	}

	void Update () {
        gameObject.transform.position = player.transform.position + cameraOffset;
	}
}
