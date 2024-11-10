using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YoutubeLinkToClipboard : MonoBehaviour
{
    public enum ImageType { SdDefault, Default, HqDefault,MqDefault, MaxResDefault }
    public ImageType m_typeOfYoutubeImageWanted;


    public void CopyConvertPastDefaultMarkdown() {
        string url = ClipboardUtility.Get();
        string id = GetIdFrom(url);
        if (string.IsNullOrEmpty(id))
            throw new System.ArgumentException("Not a youtube link: " + url);
        string mkFormat = "![{1}]({0})  \n{1}";
        ClipboardUtility.Set(string.Format(mkFormat, GetLinkFromId(id, m_typeOfYoutubeImageWanted), url));

    
    }

    public string GetIdFrom(string videourl) {
        string id="";

        //https://youtu.be/gYMBf_0HqS4
        if (videourl.ToLower().IndexOf("youtu") > 0) { id = videourl.Substring(videourl.LastIndexOf("/")+1); }

        //https://www.youtube.com/watch?v=gYMBf_0HqS4
        if (videourl.ToLower().IndexOf("/watch?") > 0) { id = videourl.Substring(videourl.LastIndexOf("v=")+2); }
        return id;
    }



    //Source: https://stackoverflow.com/questions/2068344/how-do-i-get-a-youtube-video-thumbnail-from-the-youtube-api
    public string GetLinkFromId(string id, ImageType image) {
        string format = "";
        switch (image)
        {
            case ImageType.SdDefault:
                format = "https://img.youtube.com/vi/{0}/sddefault.jpg";
                break;
            case ImageType.HqDefault:
                format = "https://img.youtube.com/vi/{0}/hqdefault.jpg";
                break;
            case ImageType.MqDefault:
                format = "https://img.youtube.com/vi/{0}/mqdefault.jpg";
                break;
            case ImageType.MaxResDefault:
                format = "https://img.youtube.com/vi/{0}/maxresdefault.jpg";
                break;
            default:
                format = "https://img.youtube.com/vi/{0}/default.jpg";
                break;
        }
        return string.Format(format, id);
    }
   
}
