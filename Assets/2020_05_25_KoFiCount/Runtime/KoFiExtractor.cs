using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using UnityEngine;

public class KoFiExtractor : MonoBehaviour
{

    public delegate void FoundProfile(ExtractedInfoFromPublicProfile profile);
    
    public static IEnumerator ExtractInfoFrom(string userName, FoundProfile foundProfile)
    {
        ExtractedInfoFromPublicProfile  info = new ExtractedInfoFromPublicProfile();
        //WebClient client = new WebClient();
        WWW www = new WWW("https://ko-fi.com/" + userName);
        yield return www;
        if(www.error!=null &&  www.error.Length>0)
            {
            Debug.LogWarning("Error:"+www.error);
            yield break;
            }
        string downloadString = www.text;
        info.SetUserName(userName);

        ExtractAboutInformation(downloadString, out info.m_aboutProfile);
        ExtractAboutIntroduction(downloadString, out info.m_aboutIntroduction);
        ExtractProfileImage(downloadString, out info.m_profilePictureUrl);  
        ExtractSocialLinks(downloadString, out info.m_socialLinksUrl);
        ExtractTags(downloadString, out info.m_tag);

        bool found;
        int coffee;
        KoFiWidget.LookForCoffeeInfoInHtml(downloadString, out found, out coffee);
        info.SetCoffeAsHidden(!found);
        info.SetCoffee(coffee);

        // Not coded
        ExtractCommissions(downloadString, out info.m_commissions);
        //Broken
        ExtractLastDonations(downloadString, out info.m_donations);
        ExtractPosts(downloadString, out info.m_latestPosts);
        /*
        //*/

        foundProfile(info);


    }

    private static void ExtractSocialLinks(string downloadString, out List<string> socialUrlFound)
    {  //<div class="social-profile-link> ... <a target="_blank" class="buy-header-link" href="link"> 
        int indexCursor=0;
        socialUrlFound = new List<string>();

        while (indexCursor > -1) { 
            indexCursor = downloadString.IndexOf("class=\"social-profile-link");
            if (indexCursor < 0) continue;
            downloadString = downloadString.Substring(indexCursor);
            indexCursor = downloadString.IndexOf("href=\"");
            if (indexCursor < 0) continue;
            downloadString = downloadString.Substring(indexCursor+6);
            indexCursor = downloadString.IndexOf("\"");
            if (indexCursor < 0) continue;
            socialUrlFound.Add(downloadString.Substring(0, indexCursor));
            socialUrlFound = socialUrlFound.Distinct().ToList();
            downloadString = downloadString.Substring(indexCursor);
            indexCursor = 0;
        }
    }

    private static void ExtractProfileImage(string downloadString, out string profileUrl)
    {
        profileUrl = "";
        int indexCursor;
         indexCursor = downloadString.IndexOf("alt=\"Profile picture\"");
        if (indexCursor < 0) return ;
        downloadString= downloadString.Substring(indexCursor);


        indexCursor = downloadString.IndexOf("src=");
        if (indexCursor < 0) return;
        downloadString = downloadString.Substring(indexCursor+4 + 1); 
        indexCursor = downloadString.IndexOf("\"");
        downloadString = downloadString.Substring(0, indexCursor);
        profileUrl = downloadString;
    }

    private static void ExtractAboutInformation(string downloadString, out string about)
    {  //<p class="kfds-c-para-control line-breaks break-long-words">Fuck the Rules ! VR &amp; AR, Unity 3D, New-tech, R&amp;D. We want to know if it is possible. Grab some ☕ and 🍺 and let's try to code it.</p>
        about = "";
        int indexCursor;
        indexCursor = downloadString.IndexOf("class=\"kfds-c-para-control");
        if (indexCursor < 0) return;
        downloadString = downloadString.Substring(indexCursor);
        indexCursor = downloadString.IndexOf(">");
        if (indexCursor < 0) return;
        downloadString = downloadString.Substring(indexCursor + 1);
        indexCursor = downloadString.IndexOf("<");
        downloadString = downloadString.Substring(0, indexCursor);
        about = downloadString;
    }

    private static void ExtractAboutIntroduction(string downloadString, out string about)
    {  //<span class="line-breaks break-long-words">... Long description of who the guy is</span>
        about = "";
        int indexCursor;
        indexCursor = downloadString.IndexOf("class=\"line-breaks break-long-words\"");
        if (indexCursor < 0) return;
        downloadString = downloadString.Substring(indexCursor);
        indexCursor = downloadString.IndexOf(">");
        if (indexCursor < 0) return;
        downloadString = downloadString.Substring(indexCursor + 1);
        indexCursor = downloadString.IndexOf("<");
        downloadString = downloadString.Substring(0, indexCursor);
        about = downloadString;

        //  throw new NotImplementedException();
    }

    private static void ExtractTags(string downloadString, out List<string> tags)
    {
        //<span class="label-tag">... One Tag</span>
        int indexCursor = 0;
        tags = new List<string>();

        while (indexCursor > -1)
        {
            indexCursor = downloadString.IndexOf("class=\"label-tag");
            if (indexCursor < 0) continue;
            downloadString = downloadString.Substring(indexCursor);
            indexCursor = downloadString.IndexOf(">");
            if (indexCursor < 0) continue;
            downloadString = downloadString.Substring(indexCursor +1);
            indexCursor = downloadString.IndexOf("<");
            if (indexCursor < 0) continue;
            tags.Add(downloadString.Substring(0, indexCursor));
            tags = tags.Distinct().ToList();
            downloadString = downloadString.Substring(indexCursor);
            indexCursor = 0;
        }
    }

    private static void ExtractCommissions(string downloadString, out List<ExtractedInfoCommission> commissions)
    {
        // < div class="comm-option-name">JOMI Custom Interface</div>
        // <a class="btn btn-primary custom-color-solid-bg" onclick="$('#rad-a96b7be9-6daa-488a-ae05-38b40dfd5863').click(); $('#commsNextBtn').click();">Buy(€5,00)</a>
        int indexCursor = 0;
        commissions = new List<ExtractedInfoCommission>();

        while (indexCursor > -1)
        {
            ExtractedInfoCommission post = new ExtractedInfoCommission();
            indexCursor = downloadString.IndexOf("comm-option-name");
            if (indexCursor < 0) continue;
            downloadString = downloadString.Substring(indexCursor);
            indexCursor = downloadString.IndexOf(">");
            if (indexCursor < 0) continue;
            downloadString = downloadString.Substring(indexCursor + 1);
            indexCursor = downloadString.IndexOf("<");
            if (indexCursor < 0) continue;
            post.SetName(downloadString.Substring(0, indexCursor));
            commissions.Add(post);
            commissions = commissions.Distinct().ToList();
            downloadString = downloadString.Substring(indexCursor);
            indexCursor = 0;
        }

    }

    private static void ExtractPosts(string downloadString, out List<ExtractedInfoLastPost> latestPosts)
    {    //<div class="update-bubble top kfds-c-shadow-buble">.Lot's of info of last recent posts</div>
         //<div class="comm-panel-label">

        int indexCursor = 0;
        latestPosts = new List<ExtractedInfoLastPost>();

        while (indexCursor > -1)
        {
            ExtractedInfoLastPost post = new ExtractedInfoLastPost();
            indexCursor = downloadString.IndexOf("class=\"update-bubble");
          //  indexCursor = downloadString.IndexOf("update-bubble");
            if (indexCursor < 0) continue;
            downloadString = downloadString.Substring(indexCursor);
            indexCursor = downloadString.IndexOf(">");
            if (indexCursor < 0) continue;
            downloadString = downloadString.Substring(indexCursor + 1);
            indexCursor = downloadString.IndexOf("<");
            if (indexCursor < 0) continue;
            post.SetName(downloadString.Substring(0, indexCursor));
            downloadString = downloadString.Substring(indexCursor);
            latestPosts.Add(post);
            indexCursor = 0;
        }

    }

    public  static void ExtractLastDonations(string html , out List<ExtractedInfoDonation> donations) {

        //< div class="kfds-right-mrgn-16 ">
        //                            <a href = "/G2G61QH5M" >
        //                                < img src="https://storage.ko-fi.com/cdn/useruploads/1ec0c729-d9bb-47b5-bce2-6ecaefdcf258_tiny.png" class="feeditem-profile-img">
        //                            </a>
        //                    </div>
        // < name class="feeditem-poster-name kfds-font-clr-dark kfds-c-text-ch-control" style="display: inline;">Somebody</name>
        // < div class="caption-pdg">  Thanks for all you do 🧡    </div> class="caption-pdg"


        int indexCursor = 0;
        donations = new List<ExtractedInfoDonation>();

        


        while (indexCursor > -1)
        {
            ExtractedInfoDonation donation = new ExtractedInfoDonation();
            //          indexCursor = downloadString.IndexOf("class=\"update-bubble");
            indexCursor = html.IndexOf("kfds-right-mrgn-16");

            //Debug.Log(">>>A?" + html);
            if (indexCursor < 0) continue;

            donations.Add(donation);
            html = html.Substring(indexCursor);
            indexCursor = html.IndexOf("src=\"");
            if (indexCursor < 0) continue;
            html = html.Substring(indexCursor + 4);
            indexCursor = html.IndexOf("\"");
            if (indexCursor < 0) continue;

            donation.SetProfilePicture(html.Substring(0, indexCursor));
            html = html.Substring(indexCursor);

            //Debug.Log(">>>B?" + html);
            indexCursor = html.IndexOf("< name");
            if (indexCursor < 0) continue;
            html = html.Substring(indexCursor);
            indexCursor = html.IndexOf(">");
            if (indexCursor < 0) continue;
            html = html.Substring(indexCursor + 1);
            indexCursor = html.IndexOf("<");
            if (indexCursor < 0) continue;
            donation.SetName(html.Substring(0, indexCursor));
            html = html.Substring(indexCursor);

            //Debug.Log(">>>C?" + html);
            indexCursor = html.IndexOf("caption-pdg");
            if (indexCursor < 0) continue;
            html = html.Substring(indexCursor);
            indexCursor = html.IndexOf(">");
            if (indexCursor < 0) continue;
            html = html.Substring(indexCursor + 1);
            indexCursor = html.IndexOf("</div>");
            if (indexCursor < 0) continue;
            donation.SetMessage(html.Substring(0, indexCursor));    

            html = html.Substring(indexCursor);
            indexCursor = 0;
        }

    }
}

[Serializable]
public class ExtractedInfoDonation
{
    public string m_userName;
    public string m_message;
    public string m_urlProfile;

    public void SetMessage(string value)
    {
        m_message = value;
    }

    public void SetName(string value)
    {
        m_userName = value;
    }

    public void SetProfilePicture(string value)
    {
        m_urlProfile = value;
    }
}

    [Serializable]
public class ExtractedInfoFromPublicProfile
{
    public string m_userName = "";
    public string m_profilePictureUrl = "";
    public string m_aboutProfile = "";
    public string m_aboutIntroduction = "";
    public bool m_coffeeHidden = false;
    public long m_coffee = 0;
    public List<string> m_tag = new List<string>();
    public List<string> m_socialLinksUrl = new List<string>();
    public List<ExtractedInfoCommission> m_commissions= new List<ExtractedInfoCommission>();
    public List<ExtractedInfoLastPost> m_latestPosts = new List<ExtractedInfoLastPost>();
    public List<ExtractedInfoDonation> m_donations = new List<ExtractedInfoDonation>();

    public void SetUserName(string userName)
    {
        m_userName = userName;
    }

    public void SetCoffeAsHidden(bool found)
    {
        m_coffeeHidden = found;
    }

    public void SetCoffee(int coffee)
    {
        m_coffee = coffee;
    }
}
[Serializable]
public class ExtractedInfoCommission
{
    public string m_commissionName;

    public void SetName(string name)
    {
        m_commissionName = name;
    }
}
[Serializable]
public class ExtractedInfoLastPost
{
    public string m_postName;

    public void SetName(string name)
    {
        m_postName = name;
    }

}