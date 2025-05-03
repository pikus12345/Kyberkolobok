using StarterAssets;
using UnityEngine;

public class BounceButton : MonoBehaviour
{
    [Header("��������� �������������")]
    public float bounceForce = 10f; // ���� �������������
    //public ForceMode forceMode = ForceMode.Impulse; // ����� ���������� ����

    [Header("�������")]
    public ParticleSystem bounceEffect; // ������ ��� ���������
    public AudioClip bounceSound; // ���� ��� ���������

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("��������");
        // ���������, ��� ���������� �����
        if (collision.gameObject.CompareTag("Player"))
        {
            ThirdPersonController th = collision.gameObject.GetComponent<ThirdPersonController>();

            if (th != null)
            {
                // ��������� ���� �����
                var tempHeight = th.JumpHeight;
                th.JumpHeight = bounceForce;
                th.JumpAndGravity(true);
                th.JumpHeight = tempHeight;

                // ����������� �������
                if (bounceEffect != null)
                    bounceEffect.Play();

                if (bounceSound != null)
                    AudioSource.PlayClipAtPoint(bounceSound, transform.position);
            }
        }
    }
}