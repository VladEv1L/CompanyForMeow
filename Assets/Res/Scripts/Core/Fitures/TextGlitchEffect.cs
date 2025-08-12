using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextGlitchEffect : MonoBehaviour
{
    public TMP_Text textComponent;
    public string message = "";
    public float typeSpeed = 0.05f;
    public int scrambleCount = 10;

    private string currentText = "";
    private int currentIndex = 0;
    private bool isLastLetter = false;
    private int scramblesLeft = 0;

    void Start()
    {


        StartCoroutine(TypingWithScrambledLastLetter());
    }

    IEnumerator TypingWithScrambledLastLetter()
    {
        while (currentIndex < message.Length || scramblesLeft > 0)
        {
            if (!isLastLetter && currentIndex < message.Length)
            {

                currentText += message[currentIndex];
                textComponent.text = currentText;
                currentIndex++;
            }
            else if (isLastLetter && scramblesLeft > 0)
            {

                currentText = ReplaceAt(currentText, currentText.Length - 1, RandomLetter());
                textComponent.text = currentText;
                scramblesLeft--;
            }
            else if (currentIndex == message.Length)
            {

                isLastLetter = true;
                scramblesLeft = scrambleCount;
            }

            yield return new WaitForSeconds(typeSpeed);
        }
    }


    private string ReplaceAt(string str, int index, char value)
    {
        return str.Substring(0, index) + value + str.Substring(index + 1);
    }


    private char RandomLetter()
    {
        const string chars = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        return chars[Random.Range(0, chars.Length)];
    }
}