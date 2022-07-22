using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour
{

    [SerializeField] private float size = 1000; // длинна луча
    [SerializeField] private Transform laser; // родительский объект модели луча
    [SerializeField] private Transform gunPoint; // точка откуда должен вылетать луч
    [SerializeField] private LayerMask ignoreMask; // фильтр по слоям
    private float dist;

    void Create()
    {
        dist = size;
        Vector3 hit = gunPoint.position + (gunPoint.localPosition + gunPoint.forward * dist);
        Vector3 center = (gunPoint.position + hit) / 2;

        RaycastHit line;
        if (Physics.Linecast(gunPoint.position, hit, out line, ~ignoreMask))
        {
            if (!line.collider.isTrigger)
            {
                dist = Vector3.Distance(gunPoint.position, line.point);
                center = (gunPoint.position + line.point) / 2;
            }
        }

        laser.localScale = new Vector3(1, 1, dist / 2);
        laser.position = center;
        laser.localPosition = new Vector3(0, 0, laser.localPosition.z);
        laser.gameObject.SetActive(true);
    }

    void LateUpdate()
    {

        Create();
    }
       
    
}
