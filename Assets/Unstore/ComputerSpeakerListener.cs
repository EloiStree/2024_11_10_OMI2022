using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NAudio.Wave;
using NAudio.CoreAudioApi;

public class ComputerSpeakerListener : MonoBehaviour
{
    public NAudio.Wave.WasapiOut capture;
    // Start is called before the first frame update
    void Start()
    {
        //MMDevice device = new MMDevice();
        //capture = new WasapiOut(device, NAudio.CoreAudioApi.AudioClientShareMode.Shared,true,0);

        //capture.

        //NAudio.CoreAudioApi.MMDeviceEnumerator MMDE = new NAudio.CoreAudioApi.MMDeviceEnumerator();
        ////Get all the devices, no matter what condition or status
        //NAudio.CoreAudioApi.MMDeviceCollection DevCol = MMDE.EnumerateAudioEndPoints(NAudio.CoreAudioApi.DataFlow.All, NAudio.CoreAudioApi.DeviceState.Active);
        ////Loop through all devices
        //foreach (NAudio.CoreAudioApi.MMDevice dev in DevCol)
        //{
        //    try
        //    {
        //        if (dev.FriendlyName.Contains("Headphone") || dev.FriendlyName.Contains("Speakers"))
        //        {
        //            //Get its audio volume
        //            System.Diagnostics.Debug.Print("Volume of " + dev.FriendlyName + " is " + dev.AudioEndpointVolume.MasterVolumeLevel.ToString());
        //            audioDev = dev;
        //            return;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //Do something with exception when an audio endpoint could not be muted
        //        System.Diagnostics.Debug.Print(dev.FriendlyName + " could not be muted" + ex.Message);
        //    }
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
