using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TreasureBoxClickHandler : MonoBehaviour
{
    private TreasureBox treasureBox;

    private void Start()
    {
        treasureBox = GetComponent<TreasureBox>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        treasureBox.OpenBox();
    }
}
