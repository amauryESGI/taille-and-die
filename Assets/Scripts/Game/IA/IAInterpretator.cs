using UnityEngine;
using System.Collections;
public class IAInterpretator : MonoBehaviour
{
    [SerializeField]
    GameObject Myself;
    [SerializeField]
    GameObject Player;
    [SerializeField]
    TypeIA typeIA;
    private IAStats _actualIA;
    [SerializeField]
    private Animator _anim;
    [SerializeField]
    private Transform _groundCheck;
    [SerializeField]
    private LayerMask _whatIsGround;
    private float _groundedRadius = .2f;
    bool _isIn = false, _grounded = false;
    PlatformerAttaque attackEnemy;
    Platformer.Character.PlatformerCharacter platformerChar;
    Rigidbody2D _rigidbody, _playerRigibody;
    Collider2D _playerColl;
    SpriteRenderer _mySkin;
    estate _actualInterpretor;
    private bool _isKnockback = false;


    int jumpcount = 0;
    // Use this for initialization
    void Start ()
    {
        if (typeIA == TypeIA.ia1) _actualIA = IAStats.FIRST_IA;
        if (typeIA == TypeIA.ia2) _actualIA = IAStats.SECONDE_IA;
        _rigidbody = Myself.GetComponent<Rigidbody2D>();
        _playerRigibody = Player.GetComponent<Rigidbody2D>();
        Debug.Log(_playerRigibody);
        platformerChar = Player.GetComponent<Platformer.Character.PlatformerCharacter>();
        _playerColl = Player.GetComponent<Collider2D>();
        _mySkin = transform.GetComponentInParent<SpriteRenderer>();
	}

    void OnTriggerEnter2D(Collider2D triggered)
    {
        if (triggered == _playerColl)
        {
            if ((Player.transform.localScale.x > 0 && _mySkin.flipX) || (Player.transform.localScale.x < 0 && !_mySkin.flipX))
                _isIn = true;
            MakeAction();
        }
    }
    void OnTriggerExit2D(Collider2D triggered)
    {
        if (triggered == _playerColl && _isIn)
        {
            _isIn = false;
            AddStat(_actualInterpretor);
        }
    }

    void AddStat(estate resultEstate) {
        Debug.Log(resultEstate);
        _actualIA.AddOneIncToEstate(resultEstate);
        //estate? toChange = null;
        //if (Mathf.Abs(Player.velocity.x) > 3.5)
        //    toChange = estate.fight;
        //if (Mathf.Abs(Player.velocity.y) > 0)
        //    toChange = estate.jump;
        //if (playerAnimator.GetBool("isPunchLeft") || playerAnimator.GetBool("isPunchRight") || playerAnimator.GetBool("isKickLeft") || playerAnimator.GetBool("isKickRight"))
        //    toChange = estate.resist;
        //Debug.Log(toChange);
        ////if (toChange.HasValue)
        ////    AddOneIncToEstate(toChange.Value);

    }

    void MakeAction()
    {

        estate? todo = _actualIA.CheckActionToDo();
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
                        if (_grounded)
                        {
                            _anim.SetBool("Ground", false);
                            _rigidbody.AddForce(new Vector2(0f, 500));
                        }
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
                default: break;

            }
        }
    }

    // Update is called once per frame
    void Update () {


        _grounded = Physics2D.OverlapCircle(_groundCheck.position, _groundedRadius, _whatIsGround);

        if (_isIn)
        {
            estate? toChange = null;
            if (_playerRigibody.position.x <= transform.position.x)
            {
                if (Mathf.Abs(_playerRigibody.velocity.x) > 3.5)
                    toChange = estate.fight;
                if (Mathf.Abs(_playerRigibody.velocity.y) > 0)
                    toChange = estate.jump;
                if (_anim.GetBool("isPunchLeft") || _anim.GetBool("isPunchRight") || _anim.GetBool("isKickLeft") || _anim.GetBool("isKickRight"))
                    toChange = estate.resist;
                if (toChange.HasValue)
                    _actualInterpretor = toChange.Value;
            }
        }
	}
}
