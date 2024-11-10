using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;

public class Experiment_RawTextureToWhiteSquare : MonoBehaviour
{
    public Color m_testColor;

    public class FindBiggestPiece {

        public FindBiggestPiece(int width) {
            m_pixelInfo = new CrossNeighbourg[width];
            for (int i = 0; i < m_pixelInfo.Length; i++)
            {
                m_pixelInfo[i] = new CrossNeighbourg();
            }
        }
        public CrossNeighbourg[] m_pixelInfo;

        [System.Serializable]
        public class CrossNeighbourg
        {
            public int m_verticalPixelCount;
            public int m_horizontalPixelCount;
            public int m_totalCount;
        }

        public void SetEmpty(in int i)
        {
            m_pixelInfo[i].m_verticalPixelCount =0;
            m_pixelInfo[i].m_horizontalPixelCount = 0;
            m_pixelInfo[i].m_totalCount = 0;
        }
        public void AddVertical(in int i)
        {
            m_pixelInfo[i].m_verticalPixelCount++;
        }
        public void AddHorizontal(in int i)
        {
            m_pixelInfo[i].m_horizontalPixelCount++;
        }
        public void ComputerTotal(in int i, out int count) {
            m_pixelInfo[i].m_totalCount = m_pixelInfo[i].m_verticalPixelCount + m_pixelInfo[i].m_horizontalPixelCount;
            count = m_pixelInfo[i].m_totalCount;
        }
    }


    public ColorRange[] m_banColors = new ColorRange[] {
        new ColorRange(){ m_threshold =0.1f, m_color=  new Color(242/255f,209/255f,151/255f) ,m_description="Skin"},
    };
    public ColorRange[] m_allowColors = new ColorRange[] {
        new ColorRange(){ m_threshold =0.3f, m_color=  new Color(99/255f,170/255f,231/255f) ,m_description="Blue Paper"},
        new ColorRange(){ m_threshold =0.1f, m_color=  new Color(1,1,1) ,m_description="Pur White"},
    };


    [System.Serializable]
    public class ColorRange {
        public string m_description = "";
        [Range(0,1f)]
        public float m_threshold;
        public Color m_color;
    }



    public Texture2D m_toApplyOn;
    public Texture2D m_hasNoColorTouch;
    public Texture2D m_whiteEnough;
    public Texture2D m_blackOrWhite;
    public Texture2D m_result;
    public Texture2D m_resultCropped;
    public Texture2D m_resultCroppedImage;
   
    public float m_angleToRotate;
    public Transform m_transformToRotate;
    public UIRawImageWithRatioMono m_givenTexture;
    public UIRawImageWithRatioMono m_noColorDisplay;
    public UIRawImageWithRatioMono m_whiteEnoughDisplay;
    public UIRawImageWithRatioMono m_blackOrWhiteDisplay;
    public UIRawImageWithRatioMono m_resultDisplay;
    public UIRawImageWithRatioMono m_resultDisplayCropped;
    public UIRawImageWithRatioMono m_resultDisplayCroppedImage;

    public float m_colorDifferenceToColor=0.1f;
    
    public float m_maxGrayAllow=0.2f;

    public bool m_hasColorTouch;
    public bool m_isInWhiteRange;
    public bool m_isBlack;

    public float m_leftMeasure=0.45f;
    public float m_rightMeasure=0.55f;


    public void PushNewTexture(Texture2D texture) {
        m_toApplyOn = texture;
        Apply();
    }

    [ContextMenu("Test")]
    public void Test() {

        m_hasColorTouch = HasColorTouch(m_testColor);
        m_isInWhiteRange = IsInWhiteToleratedRange(m_testColor);
    
    }

    public ColorRange m_black = new ColorRange() { m_description = "Black", m_color = Color.black, m_threshold = 0.05f };
    [ContextMenu("Apply")]
    public void Apply() {

        Color[] original = m_toApplyOn.GetPixels();
        Color [] t = m_toApplyOn.GetPixels();
        m_givenTexture.ApplyTexture(m_toApplyOn);

        m_realWidth = m_toApplyOn.width;
        m_realHeight = m_toApplyOn.height;
        FindBiggestPiece bigPiece = new FindBiggestPiece(original.Length);


        for (int i = 0; i < t.Length; i++)
        {
            if (IsStillValide(in t, in i)) { 
                if ((IsColorBanned(in t[i])))
                {
                    t[i] = GetBlackTransparent();
                }
            }
        }

        for (int i = 0; i < t.Length; i++)
        {
            if (IsStillValide(in t, in i)) { 
                if (!(IsColorTolerated(in t[i])) )
                {
                    t[i] = GetBlackTransparent();
                }
            }
        }
        m_whiteEnough = new Texture2D(m_toApplyOn.width, m_toApplyOn.height);
        m_whiteEnough.SetPixels(t);
        m_whiteEnough.Apply();
        m_whiteEnoughDisplay.ApplyTexture(m_whiteEnough);



        for (int i = 0; i < t.Length; i++)
        {
            if (IsStillValide(in t, in i) && HasColorTouch(in t[i]))
            {
               
                t[i] = GetBlackTransparent();
            }
        }
        m_hasNoColorTouch = new Texture2D(m_toApplyOn.width, m_toApplyOn.height);
        m_hasNoColorTouch.SetPixels(t);
        m_hasNoColorTouch.Apply();
        m_noColorDisplay.ApplyTexture(m_hasNoColorTouch);

        FindBiggestPieceAlgo(ref t, in m_realWidth, ref bigPiece, out PixelPointDL2TR bestPoint);
        SelectAllValideJoinTo(in  t, in m_realWidth, in bestPoint, out List<int> indexLinked);
        if (indexLinked.Count > 0) { 
            Color b = Color.black;
            Color w = Color.white;
            for (int i = 0; i < t.Length; i++)
            {
                t[i] = b;

            }
            for (int i = 0; i < indexLinked.Count; i++)
            {
                t[indexLinked[i]] = w;
            }
            Debug.Log("Test");
        }


        //for (int i = 0; i < 1; i++)
        //{
        //    RemoveSmallPiece(ref t, in m_realWidth);
        //}


        for (int i = 0; i < t.Length; i++)
        {
            if (IsStillValide(in t, in i)) { 
                if (!IsCloseEnoughOfColor(in t[i], m_black))
                {
                    t[i] = Color.white;
                }
            }
        }
        DrawCross(ref t, in m_realWidth, bestPoint, Color.green);

        m_blackOrWhite = new Texture2D(m_toApplyOn.width, m_toApplyOn.height);
        m_blackOrWhite.SetPixels(t);
        m_blackOrWhite.Apply();
        m_blackOrWhiteDisplay.ApplyTexture(m_blackOrWhite);


        m_result = new Texture2D(m_hasNoColorTouch.width, m_hasNoColorTouch.height,TextureFormat.ARGB32, true,true);
        m_result.filterMode = FilterMode.Point;
        m_result.anisoLevel = 0;
        m_result.SetPixels(t);
        m_result.Apply();
        m_resultDisplay.ApplyTexture(m_result);


        /// PUT BORDER

        GetCropBorder(ref t, in m_realWidth ,in m_realHeight, out padLeft, out padTop, out padRight, out padDown);


        int width = padRight - padLeft;
        int height = padDown - padTop;
        m_x = padLeft;
        m_y = padTop;
        m_widthCrop = width;
        m_heightCrop = height;






        Color[] cropColor = Crop(ref t,m_realWidth, m_x, m_y, m_widthCrop, m_heightCrop);


        PixelPointDL2TR[] pointStartEmpty = new PixelPointDL2TR[] {
            new PixelPointDL2TR(2, 2)
        , new PixelPointDL2TR(m_widthCrop-2, 2)
        , new PixelPointDL2TR(2, m_heightCrop-2)
        , new PixelPointDL2TR(m_widthCrop-2, m_heightCrop-2) };
        SelectAllEmptyJoinTo(in cropColor, in m_widthCrop, pointStartEmpty, out List<int> index);
        List<int> allIndex = new List<int>();
        for (int i = 0; i < cropColor.Length; i++)
        { allIndex.Add(i);        }
        List<int> invsere = allIndex.Except(index).ToList();

        for (int i = 0; i < invsere.Count; i++)
        {
            cropColor[invsere[i]] = Color.white;
        }
        //Debug.Log("Count:" + index.Count);



        m_resultCropped = new Texture2D(m_widthCrop, m_heightCrop, TextureFormat.ARGB32, true, true);
        m_resultCropped.filterMode = FilterMode.Point;
        m_resultCropped.anisoLevel = 0;
        m_resultCropped.SetPixels(cropColor);
        m_resultCropped.Apply();
        m_resultDisplayCropped.ApplyTexture(m_resultCropped);

        Color[] cropRenderColor = Crop(ref original, m_realWidth, m_x, m_y, m_widthCrop, m_heightCrop); ;

        Keep(ref cropRenderColor, in cropColor);

        GetCornerPoints(ref cropColor, in m_widthCrop, in m_heightCrop,
            out PixelPointDL2TR topLeft,
            out PixelPointDL2TR topRight,
            out PixelPointDL2TR downLeft,
            out PixelPointDL2TR downRight);

        //cropRenderColor[topLeft.Get1D(in m_widthCrop)] = Color.red;
        //DrawCross(ref cropRenderColor, topRight, Color.cyan);
        //DrawCross(ref cropRenderColor, downLeft, Color.green);
        //DrawCross(ref cropRenderColor, downRight, Color.yellow);
        //DrawCross(ref cropRenderColor, topLeft, Color.red);

        //// Horizonal direction
        //cropRenderColor[100] = Color.white;
        //cropRenderColor[101] = Color.white;
        ////Top direction
        //cropRenderColor[width * 50] = Color.black * 0.5f;
        //cropRenderColor[width * 51] = Color.black * 0.5f;

       
        GetTopLineMeasurePoint(ref cropColor, in m_widthCrop, in m_heightCrop, out PixelPointDL2TR left, out PixelPointDL2TR center, out PixelPointDL2TR right);

        DrawCross(ref cropRenderColor, in m_widthCrop, left, Color.red);
        DrawCross(ref cropRenderColor, in m_widthCrop, center, Color.red);
        DrawCross(ref cropRenderColor, in m_widthCrop, right, Color.red);



        m_resultCroppedImage = new Texture2D(m_widthCrop, m_heightCrop, TextureFormat.ARGB32, true, true);
        m_resultCroppedImage.filterMode = m_r_filter;
        m_resultCroppedImage.anisoLevel = m_r_antiAliasing;
        m_resultCroppedImage.wrapMode = m_r_wrapMode;
        m_resultCroppedImage.SetPixels(cropRenderColor);
        m_resultCroppedImage.Apply();
        m_resultDisplayCroppedImage.ApplyTexture(m_resultCroppedImage);
        GuessRotationFromPixel(out m_angleToRotate, in left, in right);
        Quaternion rotation =  Quaternion.identity * Quaternion.Euler(0, 0, m_angleToRotate);
        m_transformToRotate.localRotation = rotation;


        



    }

    public FilterMode m_r_filter = FilterMode.Point;
    public int  m_r_antiAliasing=2;
    public TextureWrapMode m_r_wrapMode = TextureWrapMode.Clamp;


    private void SelectAllValideJoinTo(in  Color[] t, in int width, in  PixelPointDL2TR point, out List<int> indexLinked)
    {
        bool[] alreadyIn = new bool[t.Length];
        indexLinked = new List<int>();
        Queue<int> toExplore = new Queue<int>();
        int index = point.Get1D(in width);
        toExplore.Enqueue(index);

        int antiLoop = t.Length * 2;
        while (toExplore.Count>0 ) {
            index = toExplore.Dequeue();

            if (IsStillValide(in t, in index) && !alreadyIn[index])
            {
                alreadyIn[index] = true;
                indexLinked.Add(index);
                GetNeibourghtCrossCreated(in index, in width, out PixelPointDL2TR[] nextIndex);
                for (int ij = 0; ij < nextIndex.Length; ij++)
                {
                    int a = nextIndex[ij].Get1D(in width);
                    if (!alreadyIn[a])
                        toExplore.Enqueue(a);                    
                }
            }
            antiLoop--;
            if (antiLoop < 0) {
                Debug.Log("Antiloop used");
                return;
            }

        }

    }


    private void SelectAllEmptyJoinTo(in Color[] t, in int width, PixelPointDL2TR [] points, out List<int> indexLinked)
    {
        bool[] alreadyIn = new bool[t.Length];
        indexLinked = new List<int>();
        Queue<int> toExplore = new Queue<int>();

        int index=0;
        for(int i = 0; i < points.Length; i++)
        {
            index = points[i].Get1D(in width);
            toExplore.Enqueue(index);
        }

        int antiLoop = t.Length * 2;
        int iteration = 0;
        while (toExplore.Count > 0)
        {
            index = toExplore.Dequeue();
            iteration++;
            if (IsRemoved(in t, in index) && !alreadyIn[index])
            {
                alreadyIn[index] = true;
                indexLinked.Add(index);
                GetNeibourghtCrossCreated(in index, in width, out PixelPointDL2TR[] nextIndex);
                for (int ij = 0; ij < nextIndex.Length; ij++)
                {
                    int a = nextIndex[ij].Get1D(in width);
                    if (a>=0 && a<t.Length &&  !alreadyIn[a])
                        toExplore.Enqueue(a);
                }
            }
            antiLoop--;
            if (antiLoop < 0)
            {
                Debug.Log("Antiloop used");
                return;
            }

        }
        Debug.Log("Iteration:"+ iteration);

    }


    private void FindBiggestPieceAlgo(ref Color[] texture, in int width, ref FindBiggestPiece bigPiece, out PixelPointDL2TR bestPoint)
    {
        bestPoint = new PixelPointDL2TR(0,0);
        PixelPointDL2TR startPoint = new PixelPointDL2TR(0, 0);
        PixelPointDL2TR cursor = new PixelPointDL2TR(0, 0);
        PixelPointDL2TR nextPoint = new PixelPointDL2TR(0, 0);
        int best = 0;
        for (int i = 0; i < texture.Length; i++)
        {
            if (IsStillValide(in texture, in i))
            {
                startPoint.Set1D(in i, in width);
                cursor.Set(in startPoint);
                int index = cursor.Get1D(in width); 
                while (IsStillValide(in texture, in index)) 
                {
                    bigPiece.AddHorizontal(in i);
                    PixelPointUtility.GetRightOf(in cursor, ref nextPoint);
                    cursor.Set(in nextPoint);
                    index = cursor.Get1D(in width);
                }

                bigPiece.ComputerTotal(in i, out int count);
                if (count > best) {
                    best = count;
                    bestPoint.Set(in startPoint);
                }
            }
            else { bigPiece.SetEmpty(in i); }

        }


    }

    private bool IsStillValide(in Color[] t, in int i)
    {
        return i > -1 && i < t.Length && t[i].a > 0f;
    }
    private bool IsRemoved(in Color[] t,in int i)
    {
        return i > -1 && i < t.Length && ( (t[i].r <= 0.01f && t[i].g <= 0.01 && t[i].b <= 0.01) || t[i].a <= 0.01);
    }

    private void RemoveSmallPiece(ref Color[] t, in int width)
    {
        List<int> indexToRemove = new List<int>();
        for (int i = 0; i < t.Length; i++)
        {
            if (IsStillValide(in t, i)) { 
                GetNeibourght(in  i, in width, out PixelPointDL2TR[] nearIndex);
                for (int j = 0; j < nearIndex.Length; j++)
                {
                    int index = nearIndex[j].Get1D(in width);

                    if ( index >=0 && index<t.Length && IsFullBlack(in t[index]))
                    {
                        indexToRemove.Add(i);
                        break;
                    }

                }
            }
        }
        for (int i = 0; i < indexToRemove.Count; i++)
        {
            t[indexToRemove[i]] = GetBlackTransparent();
        }

    }

    private void GetNeibourght(in int index1D, in int width, out PixelPointDL2TR[] nearIndex)
    {
        PixelPointDL2TR p = new PixelPointDL2TR(0, 0);
        p.Set1D(in index1D, in width);
        nearIndex = new PixelPointDL2TR[] {
            new PixelPointDL2TR(p.m_x-1, p.m_y-1),
            new PixelPointDL2TR(p.m_x , p.m_y - 1),
            new PixelPointDL2TR(p.m_x + 1, p.m_y - 1),

            new PixelPointDL2TR(p.m_x-1, p.m_y+1),
            new PixelPointDL2TR(p.m_x , p.m_y + 1),
            new PixelPointDL2TR(p.m_x + 1, p.m_y + 1),

            new PixelPointDL2TR(p.m_x-1, p.m_y),
            new PixelPointDL2TR(p.m_x + 1, p.m_y )};
    }

    PixelPointDL2TR p = new PixelPointDL2TR(0, 0);
    PixelPointDL2TR []p4 = new PixelPointDL2TR[] {
            new PixelPointDL2TR(0,0),
            new PixelPointDL2TR(0,0),
            new PixelPointDL2TR(0,0),
            new PixelPointDL2TR(0,0)};
    private void GetNeibourghtCross(in int index1D, in int width, ref PixelPointDL2TR[] nearIndex)
    {
        p.Set1D(in index1D, in width);
        p4[0].Set(p.m_x, p.m_y - 1);
        p4[1].Set(p.m_x, p.m_y + 1);
        p4[2].Set(p.m_x - 1, p.m_y);
        p4[3].Set(p.m_x + 1, p.m_y);
    }
    private void GetNeibourghtCrossCreated(in int index1D, in int width, out  PixelPointDL2TR[] nearIndex)
    {
        p.Set1D(in index1D, in width);
        nearIndex = new PixelPointDL2TR[] {
            new PixelPointDL2TR(p.m_x, p.m_y - 1),
            new PixelPointDL2TR(p.m_x, p.m_y + 1),
            new PixelPointDL2TR(p.m_x - 1, p.m_y),
            new PixelPointDL2TR(p.m_x + 1, p.m_y)};
    }




    private bool IsColorTolerated(in Color color)
    {
        for (int i = 0; i < m_allowColors.Length; i++)
        {
            if (IsCloseEnoughOfColor(in color, in m_allowColors[i]))
                return true;
        }
        return false;
    }
    private bool IsColorBanned(in Color color)
    {
        for (int i = 0; i < m_banColors.Length; i++)
        {
            if (IsCloseEnoughOfColor(in color, in m_banColors[i]))
                return true;
        }
        return false;
    }

    private void GuessRotationFromPixel(out float angle, in PixelPointDL2TR left,in  PixelPointDL2TR right)
    {
        Vector2 rightPosition = new Vector2(right.m_x - left.m_x, right.m_y - left.m_y);
        float adjacentDistance = rightPosition.x;
        float oppositedDistance = rightPosition.y;
        angle =  - Mathf.Asin( oppositedDistance / adjacentDistance)*Mathf.Rad2Deg;

    }

    private void GetTopLineMeasurePoint(ref Color[] cropColor, in int widthCrop,in  int heightCrop, out PixelPointDL2TR left, out PixelPointDL2TR center, out PixelPointDL2TR right)
    {
        left = new PixelPointDL2TR(0, 0);
        center = new PixelPointDL2TR(0, 0);
        right = new PixelPointDL2TR(0, 0);
        PixelPointDL2TR p = new PixelPointDL2TR((int)(widthCrop * m_leftMeasure), 0);
        for (int i = 0; i < heightCrop; i++)
        {
            p.m_y = i;
            if (IsFullWhite(in cropColor[p.Get1D(widthCrop)]))
            {
                left = new PixelPointDL2TR(p);
                break;
            }
        }
         p = new PixelPointDL2TR((int)(widthCrop * 0.5f), 0);
        for (int i = 0; i < heightCrop; i++)
        {
            p.m_y = i;
            if (IsFullWhite(in cropColor[p.Get1D(widthCrop)])) {
                center = new PixelPointDL2TR(p);
                break;
            }
        }
         p = new PixelPointDL2TR((int)(widthCrop * m_rightMeasure), 0);
        for (int i = 0; i < heightCrop; i++)
        {
            p.m_y = i;
            if (IsFullWhite(in cropColor[p.Get1D(widthCrop)]))
            {
                right = new PixelPointDL2TR(p);
                break;
            }

        }
     
    }

    private static Color GetBlackTransparent()
    {
        return new Color(0, 0, 0, 0);
    }

    private Color[] Crop(ref Color[] source, int originalWidth, int x, int y,  int widthCrop, int heightCrop)
    {
       

        Color[] result = new Color[widthCrop * heightCrop];
        for (int sx = 0; sx < widthCrop; sx++)
        {
            for (int sy = 0; sy < heightCrop; sy++)
            {
                int realIndex = originalWidth * (y+sy) +  (x + sx);
                int resultIndex = widthCrop* sy  + sx;
                result[resultIndex] = source[realIndex];
            }
        }

        return result;
    }

    public int sizeOfPoint=10;
    private void DrawCross(ref Color[] cropRenderColor, in int width, PixelPointDL2TR pointGiven, Color color)
    {
        int index;
        PixelPointDL2TR p = pointGiven;
        for (int i = -sizeOfPoint; i < sizeOfPoint; i++)
        {
            for (int j = -sizeOfPoint; j < sizeOfPoint; j++)
            {
                p = new PixelPointDL2TR(pointGiven.m_x+i, pointGiven.m_y+j);
                index = p.Get1D(in width);
                if(index>-1 && index <cropRenderColor.Length)
                    cropRenderColor[index] = color;
            }
        }
        p = pointGiven;
        index = p.Get1D(in width);
        if (index > -1 && index < cropRenderColor.Length)
            cropRenderColor[index] = color;

    }

    private void GetCornerPoints(ref Color[] cropRenderColor,in int widthCrop, in int heightCrop,
        out PixelPointDL2TR topLeft, out PixelPointDL2TR topRight, out PixelPointDL2TR downLeft, out PixelPointDL2TR downRight)
    {
        int borderPad = 1;
        //topLeft = new PixelPoint(pad, pad);
        //topRight = new PixelPoint(widthCrop - 1 - pad, pad);
        //downLeft = new PixelPoint(pad, heightCrop - 1 - pad);
        //downRight = new PixelPoint(widthCrop - 1 - pad, heightCrop - 1 - pad);
        topLeft = new PixelPointDL2TR(0, 0);
        topRight = new PixelPointDL2TR(0, 0);
        downLeft = new PixelPointDL2TR(0, 0);
        downRight = new PixelPointDL2TR(0, 0);
        PixelPointDL2TR from = new PixelPointDL2TR(0, 0);
        PixelPointDL2TR to = new PixelPointDL2TR(0, 0);
        int maxTestZone = widthCrop > heightCrop ? heightCrop : widthCrop;

        
        for (int i = 0; i < maxTestZone; i++)
        {
            from.Set(i, borderPad);
            if (IsFullWhite(in cropRenderColor[from.Get1D(widthCrop)]))
            {
                downLeft = new PixelPointDL2TR(from);
                break;
            }
            to.Set(borderPad, i);
            if (IsFullWhite(in cropRenderColor[to.Get1D(widthCrop)]))
            {
                downLeft = new PixelPointDL2TR(to);
                break;
            }
            if (CheckPointBetween(ref cropRenderColor,in widthCrop, in from, in to, out PixelPointDL2TR found))
            {
                downLeft = found;
                break;
            }
        }

        for (int i = 0; i < maxTestZone; i++)
        {
            from.Set(widthCrop - 1 - i, heightCrop - 1 - borderPad);
            if (IsFullWhite(in cropRenderColor[from.Get1D(widthCrop)]))
            {
                topRight = new PixelPointDL2TR(from);
                break;
            }
            to.Set(widthCrop - 1 - borderPad, heightCrop - 1 - i);
            if (IsFullWhite(in cropRenderColor[to.Get1D(widthCrop)]))
            {
                topRight = new PixelPointDL2TR(to);
                break;
            }
            if (CheckPointBetween(ref cropRenderColor, in widthCrop, in from, in to, out PixelPointDL2TR found))
            {
                topRight = found;
                break;
            }
        }

        for (int i = 0; i < maxTestZone; i++)
        {
            from.Set(i, heightCrop - 1 - borderPad);
            if (IsFullWhite(in cropRenderColor[from.Get1D(widthCrop)]))
            {
                topLeft = new PixelPointDL2TR(from);
                break;
            }
            to.Set(borderPad, heightCrop - 1 - i);
            if (IsFullWhite(in cropRenderColor[to.Get1D(widthCrop)]))
            {
                topLeft = new PixelPointDL2TR(to);
                break;
            }
            if (CheckPointBetween(ref cropRenderColor, in widthCrop, in from, in to, out PixelPointDL2TR found))
            {
                topLeft = found;
                break;
            }
        }

        for (int i = 0; i < maxTestZone; i++)
        {
            from.Set(widthCrop - 1 - i, borderPad);
            if (IsFullWhite(in cropRenderColor[from.Get1D(widthCrop)]))
            {
                downRight = new PixelPointDL2TR(from);
                break;
            }
            to.Set(widthCrop - 1 - borderPad,  i);
            if (IsFullWhite(in cropRenderColor[to.Get1D(widthCrop)]))
            {
                downRight = new PixelPointDL2TR(to);
                break;
            }
            if (CheckPointBetween(ref cropRenderColor, in widthCrop, in from, in to, out PixelPointDL2TR found)) {
                downRight = found;
                break;
            }

        }

    }

    

    private bool CheckPointBetween(ref Color[] cropRenderColor, in int width, in PixelPointDL2TR from, in  PixelPointDL2TR to, out PixelPointDL2TR found)
    {
        int horizontalDifference = Mathf.Abs(from.m_x - to.m_x);
        int verticalDifference = Mathf.Abs(from.m_y - to.m_y);
        PixelPointDL2TR p = new PixelPointDL2TR(0,0);
        Eloi.E_CodeTag.CouldBeProblemLater.Info("This code should be precise but I don't have the mode to code it correctly");
        for (int i = 1; i < 99; i++)
        {
            int x = (int) Mathf.Lerp(from.m_x, to.m_x, i / 100f);
            int y = (int) Mathf.Lerp(from.m_y, to.m_y, i / 100f);
            p.Set(x,y);
            if (IsFullWhite(in cropRenderColor[p.Get1D(width)]))
            {
                found = new PixelPointDL2TR(to);
                return true;
            }
        }
        found = p;
        return false;
    }

    private void CheckPointBetween()
    {
     //   throw new NotImplementedException();
    }

    public int padLeft;
    public int padTop; 
    public int padDown;
    public int padRight;


    private void GetCropBorder(ref Color[] t, in int width,in int height, out int padLeft, out int padTop, out int padRight, out int padDown)
    {
        padLeft = 10;
        padTop = 10;
        padDown = height-10;
        padRight = width - 10;
        //for (int i = 0; i < t.Length; i++)
        //{
        //    int x = i % width;
        //    int y = (int) (i / width);
        //    bool isPixelBlack = IsFullBlack(t[i]);
        //}
        for (int i = 0; i < height; i++)
        {
            if (IsHorizontalFullBlack(ref t, in width, in i))
            {
                padTop = i;
            }
            else  break; 
        }
        for (int i = height - 1; i > -1; i--)
        {
            if (IsHorizontalFullBlack(ref t, in width, in i))
            {
                padDown = i;
            }
            else break;
        }


        for (int i = 0; i < width; i++)
        {
            if (IsVerticalFullBlack(ref t, in width, in height, in i))
            {
                padLeft = i;
            }
            else break;
        }
        for (int i = width - 1; i > -1; i--)
        {
            if (IsVerticalFullBlack(ref t, in width, in height, in i))
            {
                padRight = i;
            }
            else break;
        }
    }

    public bool IsHorizontalFullBlack(ref Color[] t, in int width, in int line) {
        for (int i = 0; i < width ; i++)
        {
            int p = (width * line) + i; 
            if (!IsFullBlack(in t[p]))
                return false;
        }
        return true;
    }
    public bool IsVerticalFullBlack(ref Color[] t, in int width, in int height,  in int column) {
        for (int i = 0; i < height; i++)
        {
            int p = column + width * i;
            if (!IsFullBlack(in t[p]))
                return false;
        }
        return true;
    }


    private void Keep(ref Color[] cropRenderColor, in Color[] cropColor)
    {
        for (int i = 0; i < cropColor.Length; i++)
        {
            if (IsFullBlack(cropColor, i))
                cropRenderColor[i] = new Color(0, 0, 0, 0);
        }
    }
    private bool IsFullWhite(in Color cropColor)
    {
        return cropColor.r >= 0.99f && cropColor.g >= 0.99f && cropColor.b >= 0.99f;
    }
    private static bool IsFullBlack(in  Color cropColor)
    {
        return cropColor.r <=0.01f && cropColor.g <= 0.01f && cropColor.b <= 0.01f;
    }
    private static bool IsFullBlack(Color[] cropColor, int i)
    {
        return cropColor[i].r == 0 && cropColor[i].g == 0 && cropColor[i].b == 0;
    }

    public int m_x;
    public int m_y;
    public int m_widthCrop;
    public int m_heightCrop;
    public int m_realWidth;
    public int m_realHeight;

    public int m_cropDistance=50;


    private bool HasColorTouch(in Color color)
    {
        float g = Math.Abs(color.r - color.g);
        float b = Math.Abs(color.r - color.b);
        return !(g > m_colorDifferenceToColor || b > m_colorDifferenceToColor);
    }


    private bool IsInWhiteToleratedRange(in Color color)
    {
        return ((color.r  > (m_maxGrayAllow))
            && (color.g  > ( m_maxGrayAllow))
            && (color.b > ( m_maxGrayAllow)));
    }

    bool IsCloseEnoughOfColor(in Color color, in ColorRange range)
    {
        bool r = Mathf.Abs(color.r - range.m_color.r) < range.m_threshold;
        bool g = Mathf.Abs(color.g - range.m_color.g) < range.m_threshold;
        bool b = Mathf.Abs(color.b - range.m_color.b) < range.m_threshold;
        if (r && g && b)
            return true;
        else
            return false;
    }


    

}


[System.Serializable]
public class PixelPointDL2TR
{
    public int m_x;
    public int m_y;



    public PixelPointDL2TR(int x, int y)
    {
        m_x = x;
        m_y = y;
    }
    public PixelPointDL2TR(PixelPointDL2TR source)
    {
        m_x = source.m_x;
        m_y = source.m_y;
    }



    public int Get1D(in int arrayWidth)
    {
        return arrayWidth * m_y + m_x;
    }
    public void Set1D(in int index, in int width)
    {
        m_x = index % width;
        m_y = (int)(index / (float)width);
    }
    public void Set(int x, int y)
    {
        m_x = x;
        m_y = y;
    }

    public void Set(in PixelPointDL2TR point)
    {
        m_x = point.m_x;
        m_y = point.m_y;
    }
}
public class PixelPointUtility
{
    public static void GetRightOf(in PixelPointDL2TR cursor, ref PixelPointDL2TR nextPoint)
    {
        nextPoint.Set(in cursor);
        nextPoint.m_x += 1;
    }
    public static void GetLeftOf(in PixelPointDL2TR cursor, ref PixelPointDL2TR nextPoint)
    {
        nextPoint.Set(in cursor);
        nextPoint.m_x -= 1;
    }
    public static void GetDownOf(in PixelPointDL2TR cursor, ref PixelPointDL2TR nextPoint)
    {
        nextPoint.Set(in cursor);
        nextPoint.m_y -= 1;
    }
    public static void GetUpOf(in PixelPointDL2TR cursor, ref PixelPointDL2TR nextPoint)
    {
        nextPoint.Set(in cursor);
        nextPoint.m_y += 1;
    }
}