using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypingEffect : MonoBehaviour
{
    [TextArea(5, 10)]
    public string textToType;
    public float typingSpeed;
    private TMP_Text textComponent;
    private bool isTyping = true;
    private Coroutine typingCoroutine;

    void Start()
    {
        textComponent = GetComponent<TMP_Text>();
        typingCoroutine = StartCoroutine(AnimateText());
    }

    void Update()
    {
        if (isTyping && Input.anyKeyDown)
        {
            StopCoroutine(typingCoroutine);
            textComponent.text = textToType;
            isTyping = false;
        }
    }

    IEnumerator AnimateText()
    {
        for (int i = 0; i <= textToType.Length; i++)
        {
            textComponent.text = textToType.Substring(0, i);
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }
}
