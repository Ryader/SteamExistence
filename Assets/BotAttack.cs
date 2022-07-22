using UnityEngine;

public class BotAttack : MonoBehaviour
{
    private float timer;
    public GameObject Player;
    public ParticleSystem muzzleFlash;

    [SerializeField] float speed = 10f;
    [SerializeField] float waitTime = 3f;
    public float damage;
    private bool OnTrigger { get; set; }

    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Maneken")
        {
            OnTrigger = true;
            Player = col.gameObject;
            gameObject.GetComponent<Animator>().SetBool("Fire", true);
            transform.LookAt(col.transform.position);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            OnFire();
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Maneken")
        {
            OnTrigger = false;
            gameObject.GetComponent<Animator>().SetBool("Fire", false);
        }
    }

    void OnFire()
    {
        timer += 1 * Time.deltaTime;
        if (timer >= 1.4f)
        {
            muzzleFlash.Play();
            Player.GetComponent<EnemyHealth>().AddDamage(damage);
            Player.GetComponent<EnemyHealth>().HP -= 20;
            timer = 0;
        }
    }
}
