using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_AbstractMidi_DebugLine_PitchChange : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hide(bool hide)
    {
        this.gameObject.SetActive(hide);
    }

    internal void PushIn(IMidiPitchChangeEventGet value)
    {
    }
}
