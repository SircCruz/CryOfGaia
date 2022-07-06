using UnityEngine;
using TMPro;

public class GardenName : MonoBehaviour
{
    public TMP_InputField gardenName;

    private void Start()
    {
        gardenName.text = PlayerPrefs.GetString("Garden Name", "Name your garden!");
    }
    public void SetName()
    {
        PlayerPrefs.SetString("Garden Name", gardenName.text);
    }
}
