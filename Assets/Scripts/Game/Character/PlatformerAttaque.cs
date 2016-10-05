using UnityEngine;

public class PlatformerAttaque : MonoBehaviour {
    [SerializeField]
    private float _punshLenght;
    private float _punshCd = 0f;
    private bool _isPunsh = false;

    [SerializeField] private Collider2D _attaqueTrigger;
    [SerializeField] private Animator   _anim;

    private void Update() {
        if (_isPunsh) {
            if (_punshCd > 0f)
                _punshCd -= Time.deltaTime;
            else
                _isPunsh = false;
        }
    }

    private void FixedUpdate() {
        _anim.SetBool("isPunch", _isPunsh);
    }

    public void Punsh() {
        _isPunsh = true;
        _punshCd = _punshLenght;
    }
}
