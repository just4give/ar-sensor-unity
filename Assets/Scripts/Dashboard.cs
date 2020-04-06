using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class Dashboard : MonoBehaviour,ITrackableEventHandler
{
    public GameObject imageTarget;
    public GameObject canvas;
	public Text timeText;
    public Text temperatureText;
    public float temperatureVal;

    public Text humidityText;
    public float humidityVal;

    int index;
	public GameObject[] Buttons =new GameObject[3];
    
    private bool targetFound = false;
    private TrackableBehaviour mTrackableBehaviour;
    // Start is called before the first frame update
    void Start()
    {
        mTrackableBehaviour = imageTarget.GetComponent<TrackableBehaviour> ();
		if (mTrackableBehaviour) {
			mTrackableBehaviour.RegisterTrackableEventHandler (this);
		}
    }

    // Update is called once per frame
    void Update()
    {
        if(targetFound){
            canvas.SetActive (true);
            //Debug.Log("Target found");
            timeText.text = System.DateTime.Now.ToString ("h:mm:ss tt");
            temperatureText.text = temperatureVal.ToString();
            humidityText.text = humidityVal.ToString();

        }else{
            canvas.SetActive (false);
            
        }
         
    }

    public void OnTrackableStateChanged (TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
	{
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
			newStatus == TrackableBehaviour.Status.TRACKED ||
			newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED) {
			targetFound = true; //when target is found
		} else {
			targetFound = false; //when target is lost
		}
	}

    private void greenButtonColor (int i) { 
		//Debug.Log ("Green");
		Color greenColor = new Color (0.00f, 0.94f, 0.12f, 1.0f);
		Button b = Buttons[i].GetComponent<Button>(); 
		ColorBlock cb = b.colors;
		cb.normalColor = greenColor;
		b.colors = cb;
		//EffectColor.GetComponent<LineRenderer> ().material.SetColor ("_TintColor",greenColor);
	}
	private void orangeButtonColor (int i) {
		//Debug.Log ("Orange"); // yellow
		Color redColor = new Color (1.0f, 0.48f, 0.16f, 1.0f);
		Button b = Buttons[i].GetComponent<Button>(); 
		ColorBlock cb = b.colors;
		cb.normalColor = redColor;
		b.colors = cb;
	}

	private void blueButtonColor (int i) {
		//Debug.Log ("Blue");
		Color blueColor = new Color (0.27f, 0.43f, 1.0f, 1.0f);
		Button b = Buttons[i].GetComponent<Button> (); 
		ColorBlock cb = b.colors;
		cb.normalColor = blueColor;
		b.colors = cb;
	}

    private void changeButtonBgColor(int i, Color color){
        Button b = Buttons[i].GetComponent<Button> (); 
		ColorBlock cb = b.colors;
		cb.normalColor = color;
		b.colors = cb;
    }
}
