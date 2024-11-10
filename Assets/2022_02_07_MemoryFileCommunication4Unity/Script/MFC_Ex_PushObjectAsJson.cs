using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MFC_Ex_PushObjectAsJson : MonoBehaviour
{
    public MemoryFileConnectionMono m_connection;
    [System.Serializable]
    public class LevelName {
        public string m_levelName;
        public int m_levelIndex;
        public float m_difficultyPercent;
    }

    [System.Serializable]
    public class Donjon {
        public LevelName[] m_levelInDonjon;
    }
    public Donjon m_donjonToPush;
    public Donjon m_donjonRecovered;

    [ContextMenu("Push and recover")]
    public void PushAndRecoverDonjon() {
        m_connection.GetConnection().SetAsOjectInJsonFromat<Donjon>(m_donjonToPush);
        m_connection.GetConnection().GetInObjectFromJsonFormat<Donjon>(out m_donjonRecovered);
    }

}
