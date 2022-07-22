using UnityEngine;

public class TestScene : MonoBehaviour
{

    void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            DialogueTrigger tr = transform.GetComponent<DialogueTrigger>();
            if (tr != null && tr.fileName != string.Empty)
            {
                DialogueManager.Internal.DialogueStart(tr.fileName);
            }
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            GameObject obj = GameObject.Find("Camera holder");
            CameraHandler scriptCamera = obj.GetComponent<CameraHandler>();
            scriptCamera.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.E) && !DialogueManager.isActive)
        {
            transform.gameObject.SetActive(false);
        }
    }
}
