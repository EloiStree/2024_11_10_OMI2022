using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;

//TODO: ADD BUTTON TO REGEX Sandbox https://regexr.com/
public class ListenToApplicationFocusFilter : MonoBehaviour
{
    [SerializeField] ListenToApplicationFocus.OnFocusChange m_onFocusChange;
    [SerializeField] OnNewFocus m_onFocusChangeWithDetails;
    public List<ApplicationRegex> m_registeredApplication = new List<ApplicationRegex>();


   

    [System.Serializable]
    public class ApplicationRegex {
        public ApplicationRegex(string applicationId, params string[] confirmingRegex) {
            m_applicationId = applicationId;
            m_confirmingRegex = confirmingRegex;
        }
        public string m_applicationId;
        public string[] m_confirmingRegex;

        public bool TryToIdentify(string focusName, out string nameFound)
        {
            for (int i = 0; i < m_confirmingRegex.Length; i++)
            {
                try
                {
                    if (Regex.IsMatch(focusName, m_confirmingRegex[i]))
                    {
                        nameFound = m_applicationId;
                        return true;
                    }
                }
                catch (Exception) { }

            }
            nameFound = "";
            return false;
        }
    }

    public void Translate(string previousFocus, string currentFocus) {

        m_onFocusChange.Invoke(TryToIdentify(previousFocus), TryToIdentify(currentFocus));
        m_onFocusChangeWithDetails.Invoke(new FocusSwitchInfo() {
            currentFocusApp = TryToIdentify(currentFocus),
            previousFocusApp = TryToIdentify(previousFocus),
            currentFocusTitle =  currentFocus,
            previousFocusTitle =  previousFocus});
   
    }

    private string TryToIdentify(string focusName)
    {
        string nameFound="";
        for (int i = 0; i < m_registeredApplication.Count; i++)
        {
            if (m_registeredApplication[i].TryToIdentify(focusName, out nameFound))
                return nameFound;
        }
        return nameFound;
    }

    private void Reset()
    {
        m_registeredApplication.Add(new ApplicationRegex("Visual Studio", "\\s-\\sMicrosoft\\sVisual\\sStudio"));
        m_registeredApplication.Add(new ApplicationRegex("Unity", "\\s-\\sUnity\\s"));
        m_registeredApplication.Add(new ApplicationRegex("YouTube", "\\s-\\sYouTube\\s-\\sGoogle\\sChrome", "-\\sYouTube.*-\\sMicrosoft\\sEdge")) ;
        m_registeredApplication.Add(new ApplicationRegex("Google Chrome", "\\s-\\sGoogle\\sChrome"));
        m_registeredApplication.Add(new ApplicationRegex("Steam", "Steam"));
        m_registeredApplication.Add(new ApplicationRegex("Blender", "Blender"));
        m_registeredApplication.Add(new ApplicationRegex("Internet Explorer", "\\s-\\sMicrosoft\\sEdge"));
        m_registeredApplication.Add(new ApplicationRegex("Photoshop", "\\.psd\\s@"));
        m_registeredApplication.Add(new ApplicationRegex("Discord", "-\\sDiscord"));
        
    }
}
[System.Serializable]
public class OnNewFocus : UnityEvent<FocusSwitchInfo> { }
[System.Serializable]
public class FocusSwitchInfo
{
    public string previousFocusApp, currentFocusApp;
    public string previousFocusTitle, currentFocusTitle;
}
