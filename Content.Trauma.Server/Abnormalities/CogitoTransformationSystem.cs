// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Server.Chat.Systems;
using Content.Server.Jittering;
using Content.Shared.Speech.Components;
using Content.Shared.Speech.EntitySystems;
using Content.Trauma.Shared.Abnormalities;


namespace Content.Trauma.Server.Abnormalities;

public sealed partial class CogitoTransformationSystem : EntitySystem

{
    private static readonly EntProtoId DebugAbnormality = "MobAbnormalityDebug";
    [Dependency] private JitteringSystem _jitter = default!;
    [Dependency] private StutteringSystem _stutter = default!;
    [Dependency] private ChatSystem _chat = default!;
    [Dependency] private EntityQuery<VocalComponent> _vocalQuery = default!;
    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<CogitoInjectorComponent, CogitoInjectedEvent>(OnCogitoInjected);
    }

    private void OnCogitoInjected(Entity<CogitoInjectorComponent> ent, ref CogitoInjectedEvent args)
    {
        var target = args.Target;
        var duration = TimeSpan.FromSeconds(5);

        _jitter.DoJitter(target, duration, true, 10f, 10f, true);
        _stutter.DoStutter(target, duration, refresh: true);

        if (_vocalQuery.TryComp(target, out var vocal))
            _chat.TryEmoteWithChat(target, vocal.ScreamId, forceEmote: true);

        Spawn(DebugAbnormality, Transform(args.Target).Coordinates);
    }
}
