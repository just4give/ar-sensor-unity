using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using System.Net;
using System;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using uPLibrary.Networking.M2Mqtt.Utility;
using uPLibrary.Networking.M2Mqtt.Exceptions;


public class SensorReader : MonoBehaviour
{

    private MqttClient client;
    private System.Random random = new System.Random();
    private bool isCoroutineExecuting = false;

    SensorData sensorData = new SensorData();

    // Start is called before the first frame update
    void Start()
    {
        client = new MqttClient("broker.hivemq.com",1883 , false , null ); 
		
		// register to message received 
		client.MqttMsgPublishReceived += client_MqttMsgPublishReceived; 
		
		string clientId = Guid.NewGuid().ToString(); 
		client.Connect(clientId); 
		
		// subscribe to the topic "/home/temperature" with QoS 2 
		client.Subscribe(new string[] { "/AHXPD/arduino" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE }); 

        //string strValue = Convert.ToString(value); 
 
        // publish a message on "/home/temperature" topic with QoS 2 
        //client.Publish("/home/temperature", Encoding.UTF8.GetBytes(strValue), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE); 
 
    }

    void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e) 
	{ 

        string msg = System.Text.Encoding.UTF8.GetString(e.Message);
        //Debug.Log ("Received message from " + e.Topic + " : " + msg);
        sensorData  = JsonUtility.FromJson<SensorData>(msg);
		//Debug.Log("Temperature:"+sensorData.temperature);
        
	} 


    // Update is called once per frame
    void Update()
    {
        
        StartCoroutine(UpdateWithDelay(5));
        //UpdateComponent();
      
    }

    IEnumerator UpdateWithDelay(float time) {
        if (isCoroutineExecuting)
         yield break;
        
        isCoroutineExecuting = true;
 
        yield return new WaitForSeconds(time);

        UpdateComponent();

        isCoroutineExecuting = false;
    }
    void UpdateComponent(){
        //gameObject.GetComponent<Dashboard> ().temperatureVal = random.Next(15, 30);
        gameObject.GetComponent<Dashboard> ().temperatureVal=sensorData.temperature;
        gameObject.GetComponent<Dashboard> ().humidityVal=sensorData.humidity;
        
    } 
}
