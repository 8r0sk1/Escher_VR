using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

enum state { play_1, play_2, play_3, finish, pause };

public class GameManager : MonoBehaviour
{
    public GameObject Portal_in0_A;
    public GameObject Portal_in0_B;
    public GameObject Portal_out0_A;
    public GameObject Portal_out0_B;

    public GameObject Portal_in1_A;
    public GameObject Portal_in1_B;
    public GameObject Portal_out1_A;
    public GameObject Portal_out1_B;

    public GameObject Portal_in2_A;
    public GameObject Portal_in2_B;
    public GameObject Portal_out2_A;
    public GameObject Portal_out2_B;

    //public GameObject Portal_in3_A;
    //public GameObject Portal_in3_B;

    public GameObject eventLauncherGO_room1; //gameobject da cui proviene l'evento
    public GameObject eventLauncherGO_room2;
    public GameObject eventLauncherGO_finish;

    private EventLauncher room1_completed;
    private EventLauncher room2_completed;
    private EventLauncher finish;

    private state currentState;

    // Start is called before the first frame update
    void Start()
    {
        currentState = state.play_1;

        room1_completed = eventLauncherGO_room1.GetComponent<EventLauncher>();
        room1_completed.EventToFire += OnEventReceived_room1; //mi sottoscrivo all'evento --> divento LISTENER

        room2_completed = eventLauncherGO_room2.GetComponent<EventLauncher>();
        room2_completed.EventToFire += OnEventReceived_room2; //mi sottoscrivo all'evento --> divento LISTENER

        //room3_completed = eventLauncherGO_room3.GetComponent<EventLauncher>();
        //room3_completed.EventToFire += OnEventReceived_room3; //mi sottoscrivo all'evento --> divento LISTENER

        Portal_in2_A.SetActive(false);

        Portal_out2_A.SetActive(false);
        Portal_out2_B.SetActive(false);

        Portal_out1_B.SetActive(false);

        Portal_out0_A.SetActive(false);
        Portal_out0_B.SetActive(false);

    }

    private void OnEventReceived_room1(object sender, EventArgs args)
    {
        Debug.Log("play_1 --> play_2\n");
        Portal_in1_A.GetComponent<Renderer>().material.color = Color.blue;
        Portal_in1_B.GetComponent<Renderer>().material.color = Color.blue;
        Portal_out1_A.GetComponent<Renderer>().material.color = Color.blue;
        Portal_out1_B.GetComponent<Renderer>().material.color = Color.blue;

        Portal_in2_A.SetActive(true);
        Portal_out1_B.SetActive(true);

        currentState = state.play_2;
    }

    private void OnEventReceived_room2(object sender, EventArgs args)
    {
        Debug.Log("play_2 --> play_3\n");
        Portal_in2_A.GetComponent<Renderer>().material.color = Color.blue;
        Portal_in2_B.GetComponent<Renderer>().material.color = Color.blue;

        Portal_out2_A.SetActive(true);
        Portal_out2_B.SetActive(true);
        Portal_out2_B.GetComponent<Renderer>().material.color = Color.blue;

        Portal_out0_A.SetActive(true);
        Portal_out0_B.SetActive(true);

        currentState = state.play_3;
    }

    private void OnEventReceived_room3(object sender, EventArgs args)
    {
        Debug.Log("play_3 --> finish\n");

        

        currentState = state.finish;
        //DO SOMETHING
    }
}