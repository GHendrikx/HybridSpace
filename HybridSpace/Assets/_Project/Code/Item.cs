using UnityEngine;
using UnityEngine.UI;
using Vuforia;

[RequireComponent(typeof(ImageTargetBehaviour))]
public class Item : MonoBehaviour
{
    [SerializeField]
    private string itemName;
    [SerializeField]
    private string itemDescription;
    [SerializeField]
    private Text itemText;
    [SerializeField]
    private Text description;

    private ImageTargetBehaviour target;

    private void Start()
    {
        target = GetComponent<ImageTargetBehaviour>();
        Debug.LogWarning($"{target.TrackableName} - {target.ImageTarget.Name} - {target.ImageTarget.ID}");
    }

    private void SetUI()
    {
        itemText.text = itemName;
        description.text = itemDescription;
    }
}
