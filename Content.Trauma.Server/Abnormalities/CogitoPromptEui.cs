// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Server.EUI;
using Content.Shared.Eui;
using Content.Trauma.Shared.Abnormalities;

namespace Content.Trauma.Server.Abnormalities;

public sealed class CogitoPromptEui : BaseEui
{
    private readonly CogitoTransformationSystem _system;

    public readonly EntityUid MindId;
    public readonly EntityUid Abnormality;
    public readonly TimeSpan Deadline;
    public bool Resolved;

    public CogitoPromptEui(CogitoTransformationSystem system, EntityUid mindId, EntityUid abnormality, TimeSpan deadline)
    {
        _system = system;
        MindId = mindId;
        Abnormality = abnormality;
        Deadline = deadline;
    }

    public override void HandleMessage(EuiMessageBase msg)
    {
        base.HandleMessage(msg);

        if (msg is not CogitoPromptChoiceMessage choice)
            return;

        _system.ResolvePrompt(this, choice.Accepted);
        Close();
    }

    public override void Closed()
    {
        base.Closed();

        // Covers disconnects and any other way the window can disappear.
        _system.ResolvePrompt(this, false);
    }
}
