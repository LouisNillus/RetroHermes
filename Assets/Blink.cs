using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour
{
    public Image image;

    public Color col1;
    public Color col2;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(PingPong), 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PingPong()
    {
        if (image.color == col2)
            image.color = col1;
        else image.color = col2;
    }
}
