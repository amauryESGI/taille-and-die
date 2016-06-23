using UnityEngine;
using System.Collections;
public class IAInterpretator : MonoBehaviour
{
    [SerializeField]
    GameObject Myself;
    [SerializeField]
    GameObject Player;
    [SerializeField]
    private Animator _anim;
    [SerializeField]
    private Transform _groundCheck;
    [SerializeField]
    private LayerMask _whatIsGround;
    private float _groundedRadius = .2f;
    bool didIt = false, _grounded = false;
    PlatformerAttaque attackEnemy;
    Platformer.Character.PlatformerCharacter platformerChar;
    Rigidbody2D _rigidbody, _playerRigibody;
    private bool _isKnockback = false;


    int jumpcount = 0;
    // Use this for initialization
    void Start () {
        _rigidbody = Myself.GetComponent<Rigidbody2D>();
        _playerRigibody = Player.GetComponent<Rigidbody2D>();
        Debug.Log(_playerRigibody);
        platformerChar = Player.GetComponent<Platformer.Character.PlatformerCharacter>();
	}

    void OnCollisionEnter(Collision collision)
    {
    }

    // Update is called once per frame
    void Update () {


        _grounded = Physics2D.OverlapCircle(_groundCheck.position, _groundedRadius, _whatIsGround);

        //_anim.SetBool("Ground", _grounded);
        //_anim.SetBool("isKnockback", _isKnockback);
        //_anim.SetFloat("vSpeed", _rigidbody.velocity.y);
        //if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        //    didIt = false;

        if (Vector3.Distance(Player.transform.position, transform.position) < 2)
        {

            if(!didIt)
            {
                didIt = true;
                estate? todo = IAStats.SECONDE_IA.CheckActionToDo(_playerRigibody, this, _anim);
                if (todo.HasValue)
                {
                    switch (todo.Value)
                    {
                        case estate.fight:
                            {
                                _anim.SetBool("isPunchRight", true);
                                break;
                            }
                        case estate.jump:
                            {
                                _anim.SetBool("Ground", false);
                                _rigidbody.AddForce(new Vector2(0f, 500));
                                break;
                            }
                        case estate.stand:
                            {
                                break;
                            }
                        case estate.walk:
                            {
                                break;
                            }
                        case estate.resist:
                            {
                                break;
                            }
                        default:break;

                    }
                }
            }
        }
        else { didIt = false; }
	}
}
