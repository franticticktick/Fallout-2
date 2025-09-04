using TMPro;
using UnityEngine;

public class Logger : MonoBehaviour
{
    [SerializeField]
    private TMP_Text logs;

    private int logsCount = 0;

    public void AddLog(string log)
    {
        if (logs != null)
        {
            logsCount++;
            if (logsCount % 5 == 0)
            {
                logs.text = RemoveTopLine(logs.text);
                logsCount--;
            }
            logs.text += log;
        }
    }

    private string RemoveTopLine(string input)
    {
        int endIndex = input.IndexOf("<br>") + 4;
        return input.Remove(0, endIndex);
    }
}
