using System.Collections;
using UnityEngine;
using Vuforia;

public class ARManager : MonoBehaviour
{
    [SerializeField]
    private Items[] items;
}

[System.Serializable]
public struct Items
{
    [SerializeField]
    private ARItems itemType;
    [SerializeField]
    private Item item;
}

public enum ARItems
{
    Vinyl_record,
    paperweight,
    TV,
    Music_box,
    Bible,
    Book,
    UV_lamp,
    Lamp,
    Vinyl_player,
    Water_glass,
    Family_photo,
    Cake_mold,
    Knife,
    Moving_box,
    Polaroid_camera,
}