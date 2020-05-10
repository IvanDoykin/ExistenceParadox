using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Speedometer : MonoBehaviour, ITick
{
    [SerializeField] private MoveControllerV3 controllerV3;
    TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void Tick()
    {
        text.text = controllerV3.GetSpeed().ToString();
    }
}
