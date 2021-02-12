using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

enum state { play_1, play_2, play_3, pause };

public class GameManager : MonoBehaviour
{
    public GameObject Portal_in1_A;
    public GameObject Portal_in1_B;

    public GameObject eventLauncherGO_room1; //gameobject da cui proviene l'evento
    public GameObject eventLauncherGO_room2;
    public GameObject eventLauncherGO_room3;

    private EventLauncher room1_completed;
    private EventLauncher room2_completed;
    private EventLauncher room3_completed;

    private state currentState;

    // Start is called before the first frame update
    void Start()
    {
        currentState = state.play_1;

        room1_completed = eventLauncherGO_room1.GetComponent<EventLauncher>();
        room1_completed.EventToFire += OnEventReceived_room1; //mi sottoscrivo all'evento --> divento LISTENER

        room2_completed = eventLauncherGO_room2.GetComponent<EventLauncher>();
        room2_completed.EventToFire += OnEventReceived_room2; //mi sottoscrivo all'evento --> divento LISTENER

        room3_completed = eventLauncherGO_room1.GetComponent<EventLauncher>();
        room3_completed.EventToFire += OnEventReceived_room3; //mi sottoscrivo all'evento --> divento LISTENER
    }

    private void OnEventReceived_room1(object sender, EventArgs args)
    {
        Debug.Log("play_1 --> play_2\n");
        Portal_in1_A.GetComponent<Renderer>().material.color = Color.blue;
        Portal_in1_B.GetComponent<Renderer>().material.color = Color.blue;
        //DO SOMETHING
    }

    private void OnEventReceived_room2(object sender, EventArgs args)
    {
        //DO SOMETHING
    }

    private void OnEventReceived_room3(object sender, EventArgs args)
    {
        //DO SOMETHING
    }
}