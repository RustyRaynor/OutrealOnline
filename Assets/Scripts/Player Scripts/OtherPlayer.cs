using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherPlayer : MonoBehaviour
{
    public int id;
    public TMPro.TextMeshProUGUI nameText;
    Canvas canvas;

    private void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
    }

    private void Update()
    {
        canvas.transform.LookAt(Camera.main.transform);
    }
}
