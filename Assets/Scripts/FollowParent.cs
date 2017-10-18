using UnityEngine;
using System.Collections;

public class FollowParent : MonoBehaviour {

    public Transform toFollow;

    private Vector3 offset;

    void Start() {
        offset = transform.position - toFollow.position;
    }

	void Update () {
        transform.position = toFollow.position + offset;
        transform.rotation = toFollow.rotation;
	}
}
