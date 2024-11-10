using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleKeyCodeStacker : MonoBehaviour
{
    public float m_longTime = 0.15f;
    public float m_existTime = 0.5f;

    public void OnEnable()
    {
        for (int i = 0; i < m_keysToListen.Count; i++)
        { 
            m_trackers.Add(new KeyCodeTracker(m_keysToListen[i].m_nameId, m_keysToListen[i].m_keycode));
        }
    }
    private void OnDisable()
    {
        m_trackers.Clear();
    }

    public void Reset()
    {
        AddListener("Left", KeyCode.Q);
        AddListener("Left", KeyCode.LeftArrow);
        AddListener("Up", KeyCode.Z);
        AddListener("Up", KeyCode.UpArrow);
        AddListener("Down", KeyCode.S);
        AddListener("Down", KeyCode.DownArrow);
        AddListener("Right", KeyCode.D);
        AddListener("Right", KeyCode.RightArrow);
    }

    public void AddListener(KeyCode keycode) { 
        AddListener(keycode.ToString(), keycode); }
    public void AddListener(string name,KeyCode keycode) {
        m_keysToListen.Add(new KeycodeToListen(name, keycode));
    }
    public List<KeycodeToListen> m_keysToListen;
    [Header("Debug")]
    public List<KeyCodeTracker> m_trackers = new List<KeyCodeTracker>();

    [System.Serializable]
    public class KeycodeToListen {
       public  KeycodeToListen(string name, KeyCode code) { m_nameId = name;m_keycode = code; }
        public string m_nameId;
        public KeyCode m_keycode;
    }
    public class KeyCodeTracker {
        public string m_stackName;
        public KeyCode m_keycode;
        public float m_pressingTime;

        public KeyCodeTracker(string name, KeyCode keycode)
        {
            this.m_stackName = name;
            this.m_keycode = keycode;
        }
    }

    void Update()
    {
        for (int i = 0; i < m_trackers.Count; i++)
        {

            if (Input.GetKeyDown(m_trackers[i].m_keycode))
            {
                    m_trackers[i].m_pressingTime = 0;
            }
            else if (Input.GetKey(m_trackers[i].m_keycode)) {

                    m_trackers[i].m_pressingTime += Time.deltaTime ;
            }
            else if (Input.GetKeyUp(m_trackers[i].m_keycode))
            {
                MorseStacker.StackOn(m_trackers[i].m_stackName, m_trackers[i].m_pressingTime < m_longTime ? MorseKey.Short : MorseKey.Long, m_existTime);
            }
      
        }


    }
}
