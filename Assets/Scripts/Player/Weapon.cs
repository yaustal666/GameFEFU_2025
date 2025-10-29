using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Animator _anim;
    private List<Hitbox> _hitboxes = new List<Hitbox>();
    
    private int attackTrigger = Animator.StringToHash("attack");

    private int currentCombo = 0;
    private int maxCombo = 2;
    private float comboStartWindow = 0.5f;
    private Timer comboContinueTimer = new Timer(0.2f);
    [SerializeField] private AnimatorOverrideController _controller;

    public bool IsAttacking = false;

    private void Awake()
    {
        GetComponentsInChildren<Hitbox>(_hitboxes);
        Debug.Log(_hitboxes.Count);
    }

    private void Update()
    {
        AnimatorStateInfo state = _anim.GetCurrentAnimatorStateInfo(0);

        if (state.IsTag("Attack"))
        {
            IsAttacking = true;
        } else
        {
            IsAttacking = false;
        }

        if (state.IsTag("Attack") && !state.IsName("Attack" + maxCombo))
        {
            if (state.normalizedTime > comboStartWindow)
            {
                comboContinueTimer.Start();
            }
        }
    }

    public void Attack()
    {

        AnimatorStateInfo state = _anim.GetCurrentAnimatorStateInfo(0);

        if (state.IsTag("Attack") && state.normalizedTime < comboStartWindow)
        {
            return;
        }

        if (state.IsName("Attack" + (currentCombo - 1)))
        {
            return;
        }

        if (state.IsName("Attack"  + currentCombo) && comboContinueTimer.IsRunning())
        {
            comboContinueTimer.Stop();
            currentCombo = currentCombo == maxCombo ? 0 : currentCombo + 1;
        }
        else
        {
            currentCombo = 0;
        }

        _anim.SetInteger("comboNumber", currentCombo);

        if (!state.IsTag("Attack"))
        {
            _anim.SetTrigger(attackTrigger);
        }
    }

    public void Initialize(Animator anim, IAttacker owner)
    {
        Debug.Log("Initialize weapon");
        _anim = anim;

        foreach (Hitbox hitbox in _hitboxes)
        {
            Debug.Log("Try set owner");
            hitbox.SetOwner(owner);
        }
    }

}