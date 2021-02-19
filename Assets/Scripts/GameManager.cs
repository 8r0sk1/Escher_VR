using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

enum state { play_1, play_2, start, finish };

public class GameManager : MonoBehaviour
{
   // public event EventHandler EventToFire;

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
    public GameObject eventLauncherGO_start;

    public Material bMat;

    private EventLauncher room1_completed;
    private EventLauncher room2_completed;
    private EventLauncher start_completed;

    private state currentState;

    // Start is called before the first frame update
    void Start()
    {
        currentState = state.start;

        room1_completed = eventLauncherGO_room1.GetComponent<EventLauncher>();
        room1_completed.EventToFire += OnEventReceived_room1; //mi sottoscrivo all'evento --> divento LISTENER

        room2_completed = eventLauncherGO_room2.GetComponent<EventLauncher>();
        room2_completed.EventToFire += OnEventReceived_room2; //mi sottoscrivo all'evento --> divento LISTENER

        //start_completed = eventLauncherGO_start.GetComponent<EventLauncher>();
        //start_completed.EventToFire += OnEventReceived_start; //mi sottoscrivo all'evento --> divento LISTENER

        Portal_in2_A.SetActive(false);

        Portal_out2_A.SetActive(false);
        Portal_out2_B.SetActive(false);

        Portal_out1_B.SetActive(false);

        Portal_out0_A.SetActive(false);
        Portal_out0_B.SetActive(false);

    }

    /*private void OnEventReceived_start(object sender, EventArgs args)
    {
        Debug.Log("start --> play_1\n");

        EventToFire(this, EventArgs.Empty);

        currentState = state.play_1;
    }*/

    private void OnEventReceived_room1(object sender, EventArgs args)
    {
        Debug.Log("play_1 --> play_2\n");
        Portal_in1_A.GetComponent<Renderer>().material = bMat;
        Portal_in1_B.GetComponent<Renderer>().material = bMat;
        Portal_out1_A.GetComponent<Renderer>().material = bMat;
        Portal_out1_B.GetComponent<Renderer>().material = bMat;

        Portal_in2_A.SetActive(true);
        Portal_out1_B.SetActive(true);

        currentState = state.play_2;
    }

    private void OnEventReceived_room2(object sender, EventArgs args)
    {
        Debug.Log("play_2 --> finish\n");
        Portal_in2_A.GetComponent<Renderer>().material = bMat;
        Portal_in2_B.GetComponent<Renderer>().material = bMat;

        Portal_out2_A.SetActive(true);
        Portal_out2_B.SetActive(true);
        Portal_out2_B.GetComponent<Renderer>().material = bMat;

        Portal_out0_A.SetActive(true);
        Portal_out0_B.SetActive(true);

        currentState = state.finish;
    }
}