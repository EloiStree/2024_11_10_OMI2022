using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDD_SendMouseBoardCastBasedOnPourcent : MonoBehaviour
{

    public int m_processTarget;

    [Range(0f,1f)]
    public float m_xLeftToRight;

    [Range(0f, 1f)]
    public float m_yBotToTop;

    public FetchWindowOfProcessBasedOnNameMono m_accessLoaded;
    public DeductedInfoOfWindowSizeWithSource m_deductedInfoTarget;

    public int m_xLeftToRightRelative;
    public int m_yTopToBottomRelative;

    public bool m_use;

    public bool m_useForground;
    public bool m_usePost;

    public IEnumerator Start()
    {
        if (m_use)
        {
            yield return new WaitForSeconds(3);
            TryToSimulateClick();
        }

    }

    [ContextMenu("Try to simulate click")]
    public void TryToSimulateClick()
    {
        StartCoroutine(CoroutineTryToSimulateClick());

    }

    public IEnumerator CoroutineTryToSimulateClick()
    {

        m_accessLoaded.Fetch(in m_processTarget, out bool found, out m_deductedInfoTarget);
        m_deductedInfoTarget.m_frameSize.GetBottomToTopToRelative(m_yBotToTop, out int y);
        m_deductedInfoTarget.m_frameSize.GetLeftToRightToRelative(m_xLeftToRight, out int x);

        m_xLeftToRightRelative = x;
        m_yTopToBottomRelative = y;

        yield return PostMouseUtility.SendMouseLeftDown(m_processTarget,
                 x, y);
        yield return PostMouseUtility.SendMouseLeftUp(m_processTarget,
                x, y);


    }

}

