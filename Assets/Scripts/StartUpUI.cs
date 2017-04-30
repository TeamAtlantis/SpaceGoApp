using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VR;

public class StartUpUI : MonoBehaviour
{
    public AudioClip introductionClip;
    public AudioClip instructionsClip;

    private AudioSource audioSource;

    private GameObject flyThroughSpaceButton;
    private GameObject sateliteRideButton;

    private GameObject aquaGameObject;
    private GameObject viewRoomGameObject;

    private GameObject startUpUIGameObject;
    private GameObject countDownGameObject;

    private GameObject startupUICamera;

    private int countDownStartIndex;

    // Use this for initialization
    void Start () {
        flyThroughSpaceButton = GameObject.Find("StartupUI/Canvas/FlyThroughSpace");
        sateliteRideButton = GameObject.Find("StartupUI/Canvas/SateliteRide");

        aquaGameObject = GameObject.Find("SolarSystem/Satelites/Aqua/Aqua");
        viewRoomGameObject = GameObject.Find("SolarSystem/Satelites/Aqua/ViewRoom");

        startUpUIGameObject = GameObject.Find("StartupUI");
        countDownGameObject = GameObject.Find("StartupUI/Canvas/Countdown");
        startupUICamera = GameObject.Find("StartupUI/Camera");

        audioSource = GameObject.Find("SolarSystem/Satelites").GetComponent<AudioSource>();

        VRSettings.enabled = false;

        countDownStartIndex = 10;
        Runtime.Instance.CountDownIndex = countDownStartIndex + 1;

        countDownGameObject.SetActive(false);

        Button flyThroughSpacebtn = flyThroughSpaceButton.GetComponent<Button>();
        flyThroughSpacebtn.onClick.AddListener(onFlyThroughSpaceClick);
        Button sateliteRidebtn = sateliteRideButton.GetComponent<Button>();
        sateliteRidebtn.onClick.AddListener(onSateliteRideClick);

        audioSource.clip = introductionClip;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update () {
        if (Runtime.Instance.CountDownIndex <= 0)
        {
            startDemo();
        }
		else if (Runtime.Instance.CountDownIndex <= countDownStartIndex)
        {
            countDownGameObject.GetComponent<Text>().text = Convert.ToString(Math.Ceiling(Runtime.Instance.CountDownIndex));
            Runtime.Instance.CountDownIndex -= Time.deltaTime;
        }
	}

    public void onFlyThroughSpaceClick()
    {
        Runtime.Instance.IsFloating = true;

        aquaGameObject.SetActive(false);
        viewRoomGameObject.SetActive(false);

        startCountDown();
    }

    public void onSateliteRideClick()
    {
        Runtime.Instance.IsFloating = false;

        startCountDown();
    }

    private void startCountDown()
    {
        audioSource.clip = instructionsClip;
        audioSource.Play();

        countDownGameObject.SetActive(true);

        flyThroughSpaceButton.SetActive(false);
        sateliteRideButton.SetActive(false);

        Runtime.Instance.CountDownIndex = countDownStartIndex;
    }
    
    private void startDemo()
    {
        Runtime.Instance.CurrentSateliteAngle = 0;
        Runtime.Instance.CurrentSateliteIndex = -1;

        startupUICamera.SetActive(false);

        VRSettings.enabled = true;
        UnityEngine.Object gearVR = Instantiate(Resources.Load("GoogleVR\\Prefabs\\GvrViewerMain"));

        startUpUIGameObject.SetActive(false);
    }
}
