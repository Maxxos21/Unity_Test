using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetRandomQuote : MonoBehaviour
{
    private string lastQuote = "";
    private TMP_Text quoteText;

    void Start()
    {
        quoteText = GameObject.Find("Quote Text").GetComponent<TMP_Text>();
    }

    public void GetQuote()
    {
        if (!string.IsNullOrEmpty(lastQuote))
        {
            quoteText.text = lastQuote;
        }
        else
        {
            StartCoroutine(LoadQuote());
        }
    }
    
    IEnumerator LoadQuote()
    {
        string url = "https://api.quotable.io/random";
        using (WWW www = new WWW(url))
        {
            yield return www;
            if (www.error != null)
            {
                Debug.Log("Error loading quote: " + www.error);
            }
            else
            {
                QuoteData quoteData = JsonUtility.FromJson<QuoteData>(www.text);
                string quote = $"{quoteData.content} - {quoteData.author}";
                if (quote != lastQuote)
                {
                    lastQuote = quote;
                }
                quoteText.text = lastQuote;
            }
        }
    }
}

[System.Serializable]
public class QuoteData
{
    public string content;
    public string author;
}