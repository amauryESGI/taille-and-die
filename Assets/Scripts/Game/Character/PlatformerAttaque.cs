using UnityEngine;

public class PlatformerAttaque : MonoBehaviour {
    #region PunshLeft

    [SerializeField]
    private float _punshLeftLenght;
    private float _punshLeftCd = 0f;
    private bool _isPunshLeft = false;

    #endregion PunshLeft
    #region PunshRight

    [SerializeField]
    private float _punshRightLenght;
    private float _punshRightCd = 0f;
    private bool _isPunshRight = false;

    #endregion PunshRight
    #region KickLeft

    [SerializeField]
    private float _kickLeftLenght;
    private float _kickLeftCd = 0f;
    private bool _isKickLeft = false;

    #endregion KickLeft
    #region KickRight

    [SerializeField]
    private float _kickRightLenght;
    private float _kickRightCd = 0f;
    private bool _isKickRight = false;

    #endregion KickRight

    [SerializeField] private Collider2D _attaqueTrigger;
    [SerializeField] private Animator   _anim;

    private void Update() {
        if (_isPunshLeft) {
            if (_punshLeftCd > 0f)
                _punshLeftCd -= Time.deltaTime;
            else
                _isPunshLeft = false;
        }

        if (_isPunshRight) {
            if (_punshRightCd > 0f)
                _punshRightCd -= Time.deltaTime;
            else
                _isPunshRight = false;
        }

        if (_isKickLeft) {
            if (_kickLeftCd > 0f)
                _kickLeftCd -= Time.deltaTime;
            else
                _isKickLeft = false;
        }

        if (_isKickRight) {
            if (_kickRightCd > 0f)
                _kickRightCd -= Time.deltaTime;
            else
                _isKickRight = false;
        }
    }

    private void FixedUpdate() {
        _anim.SetBool("isPunchLeft", _isPunshLeft);
        _anim.SetBool("isPunchRight", _isPunshRight);
        _anim.SetBool("isKickLeft", _isKickLeft);
        _anim.SetBool("isKickRight", _isKickRight);
    }

    public void PunshLeft() {
        _isPunshLeft = true;
        _punshLeftCd = _punshLeftLenght;
    }

    public void PunshRight() {
        _isPunshRight = true;
        _punshRightCd = _punshRightLenght;
    }

    public void KickLeft() {
        _isKickLeft = true;
        _kickLeftCd = _kickLeftLenght;
    }

    public void KickRight() {
        _isKickRight = true;
        _kickRightCd = _kickRightLenght;
    }
}
