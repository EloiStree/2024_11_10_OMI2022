
public class BoolHistoryDescription
{

    public static string GetDescriptionPastToNow(BoolHistory h, float timeWatch = 1f, float dotPerSecond = 4, string falseSym = "_", string trueSym = "-", string switchTrueSym = "1↓", string switchFalseSym = "0↑")
    {
        string result = "";
        if (dotPerSecond == 0)
            dotPerSecond = 1;
        BoolStatePeriode[] history;
        h.GetFromPastToNow(out history,false);

        for (int i = 0; i < history.Length; i++)
        {

            for (int j = 0; j < 1 + (int)(history[i].m_elapsedTime / (1f / dotPerSecond)); j++)
            {
                result += history[i].GetState() ? trueSym : falseSym;

            }
            result += history[i].GetState() ? switchTrueSym : switchFalseSym;
        }
        for (int j = 0; j < 1 + (int)(h.GetInProgressState().GetElpasedTime() / (1f / dotPerSecond)); j++)
        {
            result += h.GetState() ? trueSym : falseSym;

        }
        result += h.GetState() ? switchTrueSym : switchFalseSym;

        return result;
    }
    public static string GetDescriptionNowToPast(BoolHistory h, float timeWatch = 1f, float dotPerSecond = 4, string falseSym = "_", string trueSym = "-", string switchTrueSym = "1↓", string switchFalseSym = "0↑")
    {
        bool watcherUse = false;
        float timePast = 0;
        string result = "";
        if (dotPerSecond == 0)
            dotPerSecond = 1;
        BoolStatePeriode[] history;
        h.GetFromNowToPast(out history,false);
        result += h.GetState() ? switchTrueSym : switchFalseSym;
        for (int j = 0; j < 1 + (int)(h.GetInProgressState().GetElpasedTime() / (1f / dotPerSecond)); j++)
        {
            timePast += (1f / (float)dotPerSecond);
            if (!watcherUse && timePast > timeWatch)
            {
                watcherUse = true;
                result += "|";
            }
            result += h.GetState() ? trueSym : falseSym;
        }

        for (int i = 0; i < history.Length; i++)
        {

            result += history[i].GetState() ? switchTrueSym : switchFalseSym;
            for (int j = 0; j < 1 + (int)(history[i].m_elapsedTime / (1f / dotPerSecond)); j++)
            {
                timePast += (1f / (float)dotPerSecond);
                if (!watcherUse && timePast > timeWatch)
                {
                    watcherUse = true;
                    result += "|";
                }
                result += history[i].GetState() ? trueSym : falseSym;

            }
        }


        return result;
    }
    public static string GetNumericDescriptionNowToPast(BoolHistory h, float timeWatch = 1f, string switchTrueSym = "↓", string switchFalseSym = "↑")
    {
        bool watcherUse = false;
        float timePast = 0;
        string result = "";
        BoolStatePeriode[] history;
        h.GetFromNowToPast(out history,false);

        result += string.Format("{0:0.00}{1}", h.GetInProgressState().GetElpasedTime(), (h.GetState() ? switchTrueSym : switchFalseSym));


        for (int i = 0; i < history.Length; i++)
        {

            result += string.Format("{0:0.00}{1}", history[i].GetElpasedTime(), (history[i].GetState() ? switchTrueSym : switchFalseSym));
            timePast += history[i].GetElpasedTime();

            if (!watcherUse && timePast > timeWatch)
            {
                watcherUse = true;
                result += "|";
            }
        }


        return result;
    }

}

