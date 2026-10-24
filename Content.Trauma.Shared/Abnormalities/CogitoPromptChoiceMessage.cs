// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.Eui;
using Robust.Shared.Serialization;

namespace Content.Trauma.Shared.Abnormalities;

[Serializable, NetSerializable]
public sealed class CogitoPromptChoiceMessage : EuiMessageBase
{
    public readonly bool Accepted;

    public CogitoPromptChoiceMessage(bool accepted)
    {
        Accepted = accepted;
    }
}
