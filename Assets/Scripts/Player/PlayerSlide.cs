using System.Collections;
using UnityEngine;

public class PlayerSlide : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public PolygonCollider2D regColl;
    public PolygonCollider2D slideColl;
    public float slideSpeed;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            PerformSlide();
        }
    }

    private void PerformSlide()
    {
        anim.SetBool("isSliding", true);
        regColl.enabled = false;
        slideColl.enabled = true;

        Vector2 slideDirection = Vector2.right;

        if (anim.GetBool("isFacingLeft"))
        {
            slideDirection = Vector2.left;
        }

        rb.AddForce(slideDirection * slideSpeed);

        StartCoroutine(StopSlide());
    }

    IEnumerator StopSlide()
    {
        yield return new WaitForSeconds(0.5f);
        anim.Play("Player Run");
        anim.SetBool("isSliding", false);
        regColl.enabled = true;
        slideColl.enabled = false;
    }
}
