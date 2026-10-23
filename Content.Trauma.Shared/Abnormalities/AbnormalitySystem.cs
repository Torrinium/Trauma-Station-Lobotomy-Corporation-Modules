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

        // TODO 1: subscribe to ExaminedEvent for entities with AbnormalityComponent
        // Complete
    }


    private void OnExamined(Entity<AbnormalityComponent> ent, ref ExaminedEvent args)
    {
        args.PushMarkup(Loc.GetString("abnormality-examine-risk",
            ("risk", ent.Comp.Risk.ToString().ToUpperInvariant())));
    }

    // TODO 2: write the handler that pushes a line of text onto the examine
    // Complete
}
