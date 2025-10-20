using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;   // 아이템 이름
    public Sprite itemIcon;   // 아이템 아이콘 이미지
    public int maxStack = 99; // 최대 겹침 개수

    public bool isUsable = false;    //사용 가능한 아이템 인지 설정
    public int healAmount = 0;       //회복량
}
