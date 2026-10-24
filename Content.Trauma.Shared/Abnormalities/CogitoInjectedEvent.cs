// SPDX-License-Identifier: AGPL-3.0-or-later

namespace Content.Trauma.Shared.Abnormalities;

/// <summary>
/// Raised on a Cogito injector after an injection completes. Server systems listen for this.
/// </summary>
[ByRefEvent]
public record struct CogitoInjectedEvent(EntityUid User, EntityUid Target);
