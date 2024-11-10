using System.Collections.Generic;

public class FileTileUtility
{


    public static void GetTile(string text, out List<TileLine> tileLines, bool useCommentary = true)
    {
        tileLines = new List<TileLine>();
        string[] lines = text.Split('\n');
        TileLine tokens;
        for (int i = 0; i < lines.Length; i++)
        {
            if (useCommentary)
            {
                char[] l = lines[i].Trim().ToCharArray();
                if ((l.Length > 0 && (l[0] == '#')) || (l.Length > 1 && (l[0] == '/' && l[1] == '/')))
                    continue;
            }
            tokens = new TileLine(lines[i]);
            tileLines.Add(tokens);
        }
    }
}