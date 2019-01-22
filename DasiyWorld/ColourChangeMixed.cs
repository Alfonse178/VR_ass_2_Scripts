using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColourChangeMixed : MonoBehaviour {

    public Transform player;
    bool hasPlayer = false;
    public GameObject[] flowersB;
    public GameObject[] flowersW;
    public Text temperature;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        float dist = Vector3.Distance(gameObject.transform.position, player.position);
        if (dist <= 0.75)
        {
            hasPlayer = true;
        }
        else
        {
            hasPlayer = false;
        }
        if (hasPlayer && Input.GetMouseButtonDown(0))
        {
            flowersB = GameObject.FindGameObjectsWithTag("BlackFlower");
            flowersW = GameObject.FindGameObjectsWithTag("WhiteFlower");

            for (int i = 0; i < flowersB.Length; i++)
            {
                Material[] mats = flowersB[i].GetComponent<Renderer>().materials;
                mats[1].color = Color.black;
            }

            for (int i = 0; i < flowersW.Length; i++)
            {
                Material[] mats = flowersW[i].GetComponent<Renderer>().materials;
                mats[1].color = Color.white;
            }
            temperature.text = "Temperature: 22.5°C";
        }
    }
}
