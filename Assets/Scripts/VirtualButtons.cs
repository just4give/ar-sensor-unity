using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class VirtualButtons : MonoBehaviour, IVirtualButtonEventHandler
{
    VirtualButtonBehaviour[] virtualButtonBehaviours;
    string vbName;
    public GameObject canvas;

    public GameObject[] Buttons =new GameObject[3];
    
    void Start()
    {
        //Register with the virtual buttons TrackableBehaviour
        virtualButtonBehaviours = GetComponentsInChildren<VirtualButtonBehaviour>();

        for (int i = 0; i < virtualButtonBehaviours.Length; i++)
            virtualButtonBehaviours[i].RegisterEventHandler(this);
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        vbName = vb.VirtualButtonName;
        Debug.Log("OnButtonPressed: "+vbName);
        for (int i = 0; i < Buttons.Length; i++)
            Buttons[i].SetActive(false);

       
        
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        Debug.Log("OnButtonReleased: "+vbName);
        for (int i = 0; i < Buttons.Length; i++)
            Buttons[i].SetActive(true);
    }

    
}
