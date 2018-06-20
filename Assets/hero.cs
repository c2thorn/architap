using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hero : MonoBehaviour {
    public Animator _animator = null;

    // Use this for initialization
    void Start () {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
		bool attack = false;

        if (Input.GetMouseButtonDown(0))
        {
                attack = true;
        }
        _animator.SetBool("attack", attack);
	}
}
