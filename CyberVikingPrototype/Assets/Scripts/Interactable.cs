using UnityEngine;

public class Interactable : MonoBehaviour
{
    public virtual void Interact()
    {
        //This method is supposed to be overwritten
        Debug.Log("Interacting with: " + transform.name);
    }
}
