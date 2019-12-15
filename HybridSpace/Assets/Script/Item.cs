using UnityEngine;
using UnityEngine.UI;

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
    private bool scanned;
    
    // Start is called before the first frame update
    private void OnEnable()
    {
        itemText.text = itemName;
        description.text = itemDescription;
        scanned = true;
        Debug.Log("Enabled");
    }
}
