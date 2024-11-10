
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UI_KofiGauge : MonoBehaviour
{

    public Image m_gauge;
    public Text m_count;
    public Text m_countObjectifs;
    public string m_idPref="CoffeCount";
    public long m_value;

    public Objectif m_objectif = new Objectif();
    public List<Objectif> m_objectifs = new List<Objectif>();

    [System.Serializable]
    public class Objectif {
        public long m_coffeCount;
    }

    private void Awake()
    {
        SetValue(PlayerPrefs.GetInt(m_idPref));
    }
    private void OnDestroy()
    {
        PlayerPrefs.SetInt(m_idPref, (int)m_value);
    }


    public void SetValue(long value) {
        Objectif[] obs = m_objectifs.Where(k => k.m_coffeCount > value).OrderBy(k => k.m_coffeCount).ToArray();
        if (obs.Length > 0)
            m_objectif = obs[0];
        if (m_count != null)
            m_count.text = "" + value;
        if (m_countObjectifs != null)
            m_countObjectifs.text = "" + m_objectif.m_coffeCount;
        if (m_gauge != null)
            m_gauge.fillAmount =  (float) ((value / (double) m_objectif.m_coffeCount));
        m_value = value;
        PlayerPrefs.SetInt(m_idPref, (int)value);
    }

    private void OnValidate()
    {
        SetValue(m_value);
    }
}
