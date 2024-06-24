using Photon.Pun;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(PhotonTransformView))]
public abstract class ItemPickUp : MonoBehaviourPunCallbacks
{
    public Item Item { get; set; }
    public event Action OnPickUp;

    private void Awake()
    {
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;

        PhotonTransformView photonTransformView = gameObject.GetComponent<PhotonTransformView>();
        photonTransformView.m_SynchronizePosition = true;
        photonTransformView.m_SynchronizeRotation = false;
        photonTransformView.m_SynchronizeScale = false;
        photonTransformView.m_UseLocal = false;
    }
    private void Start()
    {
        Item = DataManager.Instance.GetData(name.Split('(')[0]);
    }

    public override void OnDisable()
    {
        base.OnDisable();
        OnPickUp = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            OnPickUp?.Invoke();
            if (Item.Type == ItemType.Manual)
            {
                collision.GetComponent<Player>().PickedUpItems.Add(Item);
            }
            else // ItemType.Auto
            { 
                PickUp(collision);
            }
            photonView.RPC("OnPickedUp", RpcTarget.AllBuffered);
            //ObjectPoolManager.Instance.obj = this.gameObject;
            //ObjectPoolManager.Instance.photonView.RPC("ReleaseObject", RpcTarget.All, Item.Rcode);
        }
    }

    [PunRPC]
    public void OnPickedUp()
    {
        ObjectPoolManager.Instance.ReleaseObject(Item.Rcode, gameObject);
    }

    public abstract void PickUp(Collider2D other);
}