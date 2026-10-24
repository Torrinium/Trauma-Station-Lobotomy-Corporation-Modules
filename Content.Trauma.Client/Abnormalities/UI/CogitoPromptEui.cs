// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Client.Eui;
using Content.Trauma.Shared.Abnormalities;
using JetBrains.Annotations;
using Robust.Client.Graphics;

namespace Content.Trauma.Client.Abnormalities.UI;

[UsedImplicitly]
public sealed class CogitoPromptEui : BaseEui
{
    private readonly CogitoPromptWindow _window;

    public CogitoPromptEui()
    {
        _window = new CogitoPromptWindow();

        _window.AcceptButton.OnPressed += _ =>
        {
            SendMessage(new CogitoPromptChoiceMessage(true));
            _window.Close();
        };

        // Pressing "refuse" just closes the window, and closing counts as a "no".
        _window.DenyButton.OnPressed += _ => _window.Close();
        _window.OnClose += () => SendMessage(new CogitoPromptChoiceMessage(false));
    }

    public override void Opened()
    {
        IoCManager.Resolve<IClyde>().RequestWindowAttention();
        _window.OpenCentered();
    }

    public override void Closed()
    {
        base.Closed();
        _window.Close();
    }
}
