using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class Satelites : MonoBehaviour {
    public AudioClip terraClip;
    public AudioClip auraClip;
    public AudioClip issClip;
    public AudioClip aquariusClip;

    public AudioClip startClip;
    public AudioClip endingClip;

    public Vector3 terraAxis;
    public Vector3 auraAxis;
    public Vector3 issAxis;
    public Vector3 aquariusAxis;

    public float terraSpeed;
    public float auraSpeed;
    public float issSpeed;
    public float aquariusSpeed;

    AudioSource audioSource;

    private float viewRoomIntroductionIndex;

    public string[] sateliteNames =
    {
        "Terra", "Aura", "ISS", "Aquarius"
    };

    private GameObject[] sateliteGameObjects;
    private Vector3[] sateliteAxises;
    private float[] sateliteSpeeds;
    private AudioClip[] sateliteAudioClips;
    private GameObject[] sateliteCameras;

    private Texture renderTexture;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        renderTexture = GameObject.Find("SolarSystem/Satelites/Aqua/ViewRoom/Displays/Display_1/SateliteVideoFeed").GetComponent<Renderer>().material.mainTexture;

        sateliteAxises = new Vector3[]
        {
            terraAxis, auraAxis, issAxis, aquariusAxis
        };

        sateliteSpeeds = new float[]
        {
            terraSpeed, auraSpeed, issSpeed, aquariusSpeed
        };

        sateliteAudioClips = new AudioClip[]
        {
            terraClip, auraClip, issClip, aquariusClip
        };

        sateliteGameObjects = new GameObject[sateliteNames.Length];
		for (int i = 0; i < sateliteNames.Length; i++)
        {
            sateliteGameObjects[i] = GameObject.Find("SolarSystem/Satelites/" + sateliteNames[i]);
        }

        sateliteCameras = new GameObject[sateliteNames.Length];
        for (int i = 0; i < sateliteNames.Length; i++)
        {
            sateliteCameras[i] = GameObject.Find("SolarSystem/Satelites/" + sateliteNames[i] + "/Camera");
        }

        for (int i = 0; i < sateliteNames.Length; i++)
        {
            sateliteGameObjects[i].SetActive(false);
        }

        Runtime.Instance.CurrentSateliteIndex = -2;
        viewRoomIntroductionIndex = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (Runtime.Instance.CurrentSateliteIndex == -1)
        {
            if (!Runtime.Instance.IsFloating)
            {
                startupSequence();
            }
            Runtime.Instance.CurrentSateliteIndex = 0;
        }
        else
        {
            if (viewRoomIntroductionIndex > 70 || Runtime.Instance.IsFloating)
            {
                if (Runtime.Instance.CurrentSateliteIndex != -2 && Runtime.Instance.CurrentSateliteIndex < sateliteNames.Length)
                {
                    if (Runtime.Instance.CurrentSateliteAngle == 0 && Runtime.Instance.CurrentSateliteAngle == 0)
                    {
                        startSateliteDemo();
                    }

                    if (Runtime.Instance.CurrentSateliteAngle >= 180)
                    {
                        if (Runtime.Instance.CurrentSateliteIndex >= sateliteNames.Length)
                        {
                            endSequence();
                        }
                        else
                        {
                            sateliteGameObjects[Runtime.Instance.CurrentSateliteIndex].SetActive(false);

                            Runtime.Instance.CurrentSateliteIndex++;
                            Runtime.Instance.CurrentSateliteAngle = 0;

                            startSateliteDemo();
                        }
                    }
                    else
                    {
                        float speed = sateliteSpeeds[Runtime.Instance.CurrentSateliteIndex] * Time.deltaTime;
                        sateliteGameObjects[Runtime.Instance.CurrentSateliteIndex].transform.RotateAround(Vector3.zero, sateliteAxises[Runtime.Instance.CurrentSateliteIndex], speed);
                        Runtime.Instance.CurrentSateliteAngle += speed;
                    }
                }
            }
            else
            {
                viewRoomIntroductionIndex += Time.deltaTime;
            }
        }
	}

    private void startupSequence()
    {
        audioSource.clip = startClip;
        audioSource.Play();
    }

    private void startSateliteDemo()
    {
        audioSource.clip = sateliteAudioClips[Runtime.Instance.CurrentSateliteIndex];
        audioSource.Play();
        sateliteGameObjects[Runtime.Instance.CurrentSateliteIndex].SetActive(true);
        sateliteCameras[Runtime.Instance.CurrentSateliteIndex].GetComponent<Camera>().targetTexture = (RenderTexture) renderTexture;
    }

    private void endSequence()
    {
        audioSource.clip = endingClip;
        audioSource.Play();
    }
}
