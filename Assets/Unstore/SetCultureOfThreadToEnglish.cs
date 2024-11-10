using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class SetCultureOfThreadToEnglish : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {

        CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
    }

  
}
