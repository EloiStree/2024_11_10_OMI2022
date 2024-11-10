using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportButtonSwitchMono : MonoBehaviour
{

    public GameObjectLoopList m_allPanels;

    [ContextMenu("Next")]
    public void Next()
    {
        foreach (var item in m_allPanels.GetAll())
        {
            item.SetActive(false);
        }
        m_allPanels.Next();
        m_allPanels.GetCurrent().SetActive(true);
    }
    [ContextMenu("Previous")]
    public void Previous()
    {
        foreach (var item in m_allPanels.GetAll())
        {
            item.SetActive(false);
        }
        m_allPanels.Previous();
        m_allPanels.GetCurrent().SetActive(true);
    }
}

[System.Serializable]
public class GameObjectLoopList : LoopList<GameObject> { 

}


[System.Serializable]
public class LoopList<T>
{
    public int m_index = 0;
    public List<T> m_elements = new List<T>();

    public List<T> GetAll() { return m_elements; }
    public void Next()
    {
        m_index++;
        if (m_index >= m_elements.Count)
            m_index = 0;
    }
    public void Previous()
    {
        m_index--;
        if (m_index <0)
            m_index = m_elements.Count-1;
    }
    public T GetCurrent()
    {
        return m_elements[m_index];
    }
    public void GetCurrent(T currentElement)
    {
        currentElement = m_elements[m_index];
    }
}
[System.Serializable]
public class ClampList<T>
{
    public int m_index = 0;
    public List<T> m_elements = new List<T>();

    public void Next()
    {
        m_index++;
        if (m_index >= m_elements.Count)
            m_index = m_elements.Count-1;
    }
    public void Previous()
    {
        m_index--;
        if (m_index < 0)
            m_index = 0;
    }
    public T GetCurrent()
    {
        return m_elements[m_index];
    }
    public void GetCurrent(T currentElement)
    {
        currentElement = m_elements[m_index];
    }
}