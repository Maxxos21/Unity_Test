using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DynamicScrollView : MonoBehaviour
{
    [SerializeField] private Transform scrollViewContent;
    [SerializeField] private GameObject prefab;
    [SerializeField] private int numberOfItems, numberOfActiveItems;
    [SerializeField] private TMP_Text fpsText, errorText, quoteText;

    private const float ERROR_TEXT_DISPLAY_TIME = 2f;

    private void Update()
    {
        fpsText.text = $"FPS: {(1f / Time.deltaTime):0}";
    }

    public void AddItems(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject item = Instantiate(prefab, scrollViewContent);
            item.GetComponent<Image>().color = Random.ColorHSV();
            item.GetComponentInChildren<TMP_Text>().text = scrollViewContent.childCount.ToString();
        }

        numberOfActiveItems = scrollViewContent.childCount;

        ResetQuote();
    }

    public void DeleteItems(int count)
    {
        if (count > scrollViewContent.childCount)
        {
            StartCoroutine(DisplayErrorMessage($"Cannot delete <color=red>{count}</color> items. There are only <color=red>{scrollViewContent.childCount}</color> items in the list."));
            return;
        }

        for (int i = scrollViewContent.childCount - count; i < scrollViewContent.childCount; i++)
        {
            Destroy(scrollViewContent.GetChild(i).gameObject);
        }

        numberOfActiveItems = scrollViewContent.childCount;

        ResetQuote();
    }

    private IEnumerator DisplayErrorMessage(string message)
    {
        if (scrollViewContent.childCount > 0)
        {
            errorText.text = $"Error: {message}";
            yield return new WaitForSeconds(ERROR_TEXT_DISPLAY_TIME);
            errorText.text = "";
        }
        else
        {
            errorText.text = $"Error: {message}. There are no items in the list.";
            yield return new WaitForSeconds(ERROR_TEXT_DISPLAY_TIME);
            errorText.text = "";
        }
    }

    public void ClearList()
    {
        foreach (Transform child in scrollViewContent)
        {
            Destroy(child.gameObject);
        }

        ResetQuote();
    }

    private void ResetQuote()
    {
        quoteText.text = "";
    }
}