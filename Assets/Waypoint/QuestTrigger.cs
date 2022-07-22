using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    public float WaypointDelaySeconds;

    private bool Entered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !Entered)
        {
            QuestPoint.Instance.UpdateObjective();
            Entered = true;
        }
    }
}
