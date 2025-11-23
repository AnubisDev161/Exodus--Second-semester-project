using System.Collections;
using UnityEditor;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class WeaponAnimatonController : MonoBehaviour
{
    private Animator weaponAnimator;
    private string currentAnimation = "";

    // float shotLength;
    // int shots = 0;
    // Transform magazine;
    private void Awake()
    {
        weaponAnimator =  GetComponent<Animator>();
        
    }

    public void ChangeAnimation(string animation, float crossfade = 0.2f, float delay = 0f)
    {
        // if (currentAnimation != animation)
        // {
        //     currentAnimation = animation;
        //     weaponAnimator.CrossFade(animation, crossfade);
        // }
        
        if (delay > 0f)
        {
            StartCoroutine(Wait());
        }
        else
        {
            Validate();
        }
        
        void Validate()
        {
            weaponAnimator.CrossFade(animation, crossfade);
        }
        
        IEnumerator Wait()
        {
            yield return new WaitForSeconds(delay);
            Validate();
        }
    }

    public void ForcePlay(string animation, float delay = 0f)
    {
        if (delay > 0f)
        {
            StartCoroutine(Wait());
        }
        else
        {
            Validate();
        }
        
        void Validate()
        {
            weaponAnimator.Play(animation, 0, 0f);
        }
        
        IEnumerator Wait()
        {
            yield return new WaitForSeconds(delay);
            Validate();
        }
    }

    public bool IsPlaying()
    {
        if (weaponAnimator == null) return false;
        
        return true;
    }
    
    
    
    
    
    
    
    
    
    
    // public void OnGunShoot()
    // {
    //     weaponAnimator.SetTrigger("Shoot");
    //     // weaponAnimator.SetBool("")
    // }
    //
    //
    //
    //
    //
    // public void OnGunStoppedShooting()
    // {
    //     
    // }
    //
    // public void OnGunReload()
    // {
    //
    // }

    // IEnumerator StopGunAnimation()
    // {
    //     yield return new WaitForSeconds(shotLength);
    //     weaponAnimator.ResetTrigger("IsShooting");
    //   
    //
    //
    // }

}
