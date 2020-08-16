using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HP : MonoBehaviour
{
    private Text hpTaxt;

    private void Awake()
    {
        Player.Notify += Messange;
        hpTaxt = gameObject.GetComponent<Text>();
    }

    private void Messange(int s)
    {
        hpTaxt.text = s.ToString();
    }
}
