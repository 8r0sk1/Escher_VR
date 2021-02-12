using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

enum state { play_1, play_2, play_3, pause };

public class ProgressState : MonoBehaviour {

    state currentState;

    // Start is called before the first frame update
    void Start()
    {
        currentState = state.play_1;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
