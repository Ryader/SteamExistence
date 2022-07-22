using Photon.Pun;
using UnityEngine;

public class PlayerMovement : MonoBehaviourPunCallbacks
{

    private PhotonView photonView;
    private CharacterController localCC;
    public Animator anim;
    [SerializeField] Vector3 moveDir;
    public MouseLook cameraLook;

    [SerializeField] private float movementSpeed;
    public float rotationSpeed;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        localCC = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (photonView.IsMine && !GameSetup.gameSetup.GameIsPaused && !GameSetup.gameSetup.matchEnded)
        {
            BasicMoves();
            Rotation();
        }

    }

    void BasicMoves()
    {

        anim.SetFloat("X", Input.GetAxis("Horizontal"));
        anim.SetFloat("Y", Input.GetAxis("Vertical"));
        localCC.Move(moveDir * Time.deltaTime * movementSpeed);
        if (Input.GetKey(KeyCode.W))
        {
            localCC.Move(transform.forward * Time.deltaTime * movementSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            localCC.Move(transform.right * Time.deltaTime * (movementSpeed / 1.5f));
        }
        if (Input.GetKey(KeyCode.A))
        {
            localCC.Move(-transform.right * Time.deltaTime * (movementSpeed / 1.5f));
        }

    }

    void Rotation()
    {
        anim.SetFloat("mouseY", cameraLook.rotationY);
        float mouseXaxis = Input.GetAxis("Mouse X") * Time.deltaTime * rotationSpeed;
        transform.Rotate(new Vector3(0, mouseXaxis, 0));
    }
}
