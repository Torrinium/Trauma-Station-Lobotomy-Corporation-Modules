// SPDX-License-Identifier: AGPL-3.0-or-later

using Robust.Shared.Serialization;

namespace Content.Trauma.Shared.Abnormalities;

/// <summary>
/// Marks an entity as an Abnormality and holds its core definition data, including a list of possible difficulty classes.
/// </summary>
[RegisterComponent, NetworkedComponent]
public sealed partial class AbnormalityComponent : Component
{
    [DataField]
    public AbnormalityRisk Risk = AbnormalityRisk.Zayin;
}

[Serializable, NetSerializable]
public enum AbnormalityRisk : Byte
{
    Zayin,
    Teth,
    He,
    Waw,
    Aleph,
}
