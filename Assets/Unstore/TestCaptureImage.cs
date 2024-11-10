using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCaptureImage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Texture2D tex = new Texture2D(200, 300, TextureFormat.RGB24, false);
        //Rectangle screenSize = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
        //Bitmap target = new Bitmap(screenSize.Width, screenSize.Height);
        //using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(target))
        //{
        //    g.CopyFromScreen(0, 0, 0, 0, new Size(screenSize.Width, screenSize.Height));
        //}
        //MemoryStream ms = new MemoryStream();
        //target.Save(ms, ImageFormat.Png);
        //ms.Seek(0, SeekOrigin.Begin);

        //tex.LoadImage(ms.ToArray());

        //renderer.material.mainTexture = tex;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
