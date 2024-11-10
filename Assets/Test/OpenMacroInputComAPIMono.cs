using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenMacroInputComAPI;

public class OpenMacroInputComAPIMono : MonoBehaviour
{

    BooleanRawRegister defaultRegister;
    BooleanRawRegisterIPC defaultMemoryPointer;

    public string m_sharedMemoryFile = "DefaultBooleanState";

    [TextArea(0,10)]
    public string m_names;

    [TextArea(0, 10)]
    public string m_booleans;



    private void Awake()
    {
        defaultRegister = new BooleanRawRegister(BooleanRawRegister.Size._4x4);
        defaultMemoryPointer = new BooleanRawRegisterIPC(ref defaultRegister, m_sharedMemoryFile);


    }
    void Start()
    {

        InvokeRepeating("PushBoolean", 0, 2);
        InvokeRepeating("ImportBoolean", 1, 2);
        InvokeRepeating("RandomTest", 0, 0.5f);

    }

    public void RandomTest() {
        uint r = (uint) UnityEngine.Random.Range(0,40);
        defaultRegister.SetIndexName( (int) r, "Unity " + r);
        defaultRegister.SetIndexValue( (int) r, ! defaultRegister.GetState(r) );


    }
    public void PushBoolean() {


        defaultMemoryPointer.OverrideMemoryValueWithRegister();
        m_names = defaultMemoryPointer.m_linkedRegisterCompressor.GetIdsCompressed();
        m_booleans = defaultMemoryPointer.m_linkedRegisterCompressor.GetBooleansCompressed();

    }
    public void ImportBoolean()
    {

        defaultMemoryPointer.ImportRegisterStateFromMemoryValue();
        m_names = defaultMemoryPointer.m_linkedRegisterCompressor.GetIdsCompressed();
        m_booleans = defaultMemoryPointer.m_linkedRegisterCompressor.GetBooleansCompressed();

    }
    
}
