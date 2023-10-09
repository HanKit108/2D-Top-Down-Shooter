using UnityEngine;
using UnityEngine.UI;

public class DeleteItemButton : MonoBehaviour
{
    [SerializeField] private Button deleteButton;

    public Button GetButton() {
        return deleteButton;
    }
}
