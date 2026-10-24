// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.Examine;

namespace Content.Trauma.Shared.Abnormalities;

/// <summary>
/// Provides the difficulty class of an Abnormality on examination.
/// </summary>
public sealed class AbnormalitySystem : EntitySystem
{
    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<AbnormalityComponent, ExaminedEvent>(OnExamined);


    }


    private void OnExamined(Entity<AbnormalityComponent> ent, ref ExaminedEvent args)
    {
        args.PushMarkup(Loc.GetString("abnormality-examine-risk",
            ("risk", ent.Comp.Risk.ToString().ToUpperInvariant())));
    }


}
