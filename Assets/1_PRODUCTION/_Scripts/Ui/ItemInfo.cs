using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemInfo : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] TextMeshProUGUI price;

    private void Start()
    {
        canvas.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canvas.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canvas.enabled = false;
        }
    }

    public void SetPriceText(int value)
    {
        price.SetText(value + " Hell Coins");
    }

    public void SetDescriptionText(string description)
    {
        this.description.SetText(description + ".");
    }

    public void SetIcon(Sprite icon)
    { this.icon.sprite = icon; }
}
