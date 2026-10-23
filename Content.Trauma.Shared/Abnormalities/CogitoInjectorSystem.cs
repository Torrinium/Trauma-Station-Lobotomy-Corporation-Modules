// SPDX-License-Identifier: AGPL-3.0-or-later
using Robust.Shared.Audio.Systems;
using Content.Shared.DoAfter;
using Content.Shared.Interaction;
using Content.Shared.Popups;


namespace Content.Trauma.Shared.Abnormalities;

/// <summary>
/// This handles how injecting Cogito and the doafter operates.
/// </summary>

public sealed partial class CogitoInjectorSystem : EntitySystem
{
    [Dependency] private SharedAudioSystem _audio = default!;
    [Dependency] private SharedDoAfterSystem _doAfter = default!;
    [Dependency] private SharedPopupSystem _popup = default!;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<CogitoInjectorComponent, BeforeRangedInteractEvent>(OnRangedInteract);
        SubscribeLocalEvent<CogitoInjectorComponent, CogitoInjectDoAfterEvent>(OnInjectDoAfter);

    }

    private void OnRangedInteract(Entity<CogitoInjectorComponent> ent, ref BeforeRangedInteractEvent args)
    {
        if (args.Handled || !args.CanReach || args.Target is not { } target|| ent.Comp.UsedUp)
            return;

        args.Handled = true;
        StartCogitoInject(ent, target, args.User);
    }

    private void StartCogitoInject(Entity<CogitoInjectorComponent> ent, EntityUid target, EntityUid user)
    {
        var doAfterArgs = new DoAfterArgs(EntityManager,
            user,
            ent.Comp.InjectDelay,
            new CogitoInjectDoAfterEvent(),
            eventTarget: ent,
            target: target,
            used: ent)
        {
            BreakOnDamage = true,
            BreakOnHandChange = true,
            BreakOnMove = true,
            NeedHand = true,
        };

        if (!_doAfter.TryStartDoAfter(doAfterArgs))
            return;

        if (target != user)
        {
            _popup.PopupEntity(Loc.GetString("cogito-inject-start-target"), target, target, PopupType.LargeCaution);
            _popup.PopupEntity(Loc.GetString("cogito-inject-start-user"), user, user);
        }


}
    private void OnInjectDoAfter(Entity<CogitoInjectorComponent> ent, ref CogitoInjectDoAfterEvent args)
    {
        if (args.Cancelled || args.Target is not { } target || ent.Comp.UsedUp)
            return;

        ent.Comp.UsedUp = true;
        Dirty(ent);

        _audio.PlayPredicted(ent.Comp.InjectSound, target, args.User);
        _popup.PopupEntity("The injector empties, pouring black ichor into the target.", target, args.User);

        args.Handled = true;
        // 1. Mark the injector as used.
        // 2. Tell clients the value changed.
        // 3. Play the injector's sound at the target.
        // 4. Show a short placeholder popup to the user.

    }
}

[Serializable, NetSerializable]
public sealed partial class CogitoInjectDoAfterEvent : SimpleDoAfterEvent;

