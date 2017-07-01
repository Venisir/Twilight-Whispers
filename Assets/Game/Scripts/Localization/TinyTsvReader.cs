using UnityEngine;
using System.Collections;

public class TinyTsvReader
{
    private string tsvString = "";
    private int idx = 0;
    private int headersCount = 0;
    private string[] missingContent = new string[0];
    public TinyTsvReader(string newTsvString)
    {
        tsvString = newTsvString;
    }

    public string[] headers = new string[0];
    public string[] content = new string[0];

    // properly looks for the next index of _c, without stopping at line endings, allowing tags to be break lines   
    int IndexOf(char _c, int _i)
    {
        int i = _i;
        while (i < tsvString.Length)
        {
            if (tsvString[i] == _c)
            {
                return i;
            }

            ++i;
        }

        return -1;
    }

    public bool ReadHeaders()
    {
        int oldIndex = idx;
        idx = 0;
        bool result = Read(true);
        headersCount = content.Length;
        headers = new string[headersCount];
        content.CopyTo(headers, 0);
        missingContent = new string[headersCount];
        for (int i = 0; i < headersCount; i++)
        {
            missingContent[i] = "Missing field";
        }
        // avoid reading the headers more than once the first time
        if (oldIndex > idx)
            idx = oldIndex;
        return result;
    }

    public bool Read(bool readingHeaders = false)
    {
        if (idx <= -1 || idx >= tsvString.Length)
        {
            return false;
        }

        // skip attributes, don't include them in the name!
        int endOfLine = IndexOf('\n', idx + 1);
        if ((endOfLine <= -1))
        {
            if (idx + 1 < tsvString.Length)
                endOfLine = tsvString.Length;
            else
                return false;
        }

        //get the content
#if UNITY_IOS
        string line = tsvString.Substring(idx, endOfLine - idx).Trim();
#else
        string line = tsvString.Substring(idx, endOfLine - 1 - idx).Trim();
#endif
        idx = endOfLine;
        content = line.Split('\t');
        if (!readingHeaders && content.Length != headersCount)
        {
            content = missingContent;
        }

        return true;
    }
}