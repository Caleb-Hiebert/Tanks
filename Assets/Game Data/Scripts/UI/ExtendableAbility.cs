using UnityEngine;
using System.Collections;

public abstract class ExtendableAbility : Bolt.EntityEventListener<IPlayer> {

    private SkinAssets skinAssets;

    //This should only ever get called on the server
    abstract public void OnAbility(int code);

    abstract public void OnEntityAbility(int code);

    void Awake()
    {
        skinAssets = GetComponent<SkinAssets>(); 
    }

    public void SendAbility(int code)
    {
        var ability = AbilityEvent.Create(Bolt.GlobalTargets.OnlyServer, Bolt.ReliabilityModes.ReliableOrdered);
        ability.AbilityCode = code;
        ability.Send();

        OnAbilitySent(code);
    }

    public void SentEntityEvent(int code)
    {
        var eEvent = EntityEvent.Create(entity, Bolt.EntityTargets.OnlyControllerAndOwner);
        eEvent.Code = code;
        eEvent.Send();
    }

    public virtual void OnAbilitySent(int code) { }

    public SkinAssets SkinAssets
    {
        get { return skinAssets; }
    }
}
