using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomLister : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] RoomTab tab;

    List<RoomTab> tabs;

    private void OnEnable()
    {
    }
}
