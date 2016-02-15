using UnityEngine;

public class Hearth : MonoBehaviour {
    [SerializeField] private Animator _animator;

    public bool IsUp {
        get { return _animator.GetBool("IsUp"); }
        set { _animator.SetBool("IsUp", value); }
    }
}
