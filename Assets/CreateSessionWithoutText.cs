using System;
using TMPro;
using Unity.Services.CloudCode;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CreateSessionWithoutText : MonoBehaviour
{
    public int charSize = 5;
    public TMP_InputField m_InputField;
    public Button createButton;

    public String GetRandomName(int size)
    {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var stringChars = new char[size];

        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[Random.Range(0, chars.Length)];
        }

        return new String(stringChars);
    }

    protected void Start()
    {
        m_InputField.text = GetRandomName(charSize);
        createButton.interactable = true;
    }
}