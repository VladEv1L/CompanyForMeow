using UnityEngine;

public class P_STATE : MonoBehaviour
{
   public Vector3 MoveInput, LookInput;
   [SerializeField] public bool isMoved;
   [SerializeField] public Vector3 moveDirection;
   [SerializeField] public float moveSpeed = 3.0f;
   [SerializeField] public float moveSprintSpeed = 3.0f;
   [SerializeField] public float InRoomMoveSpeed = 1.7f;
   [SerializeField] public float OutRoomMoveSpeed = 3.0f;
   [SerializeField] public bool isSprint;
   [SerializeField] public int HP = 100;




   [Header("Ссылки")]
   [SerializeField] public GameObject Camera;
   public Animator ModelAnimator;
   public GameObject FlashLight;

   public void Start()
   {

      // this.AddComponent<CharacterController>();
      //this.AddComponent<P_INPUT>();
      //this.AddComponent<P_LOOK>();
      // this.AddComponent<P_MOVE>();
      // this.AddComponent<P_ANIMATOR>();
      // this.AddComponent<P_FLASHLIGHT>();
   }



}
