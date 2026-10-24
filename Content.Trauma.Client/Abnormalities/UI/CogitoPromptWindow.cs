// SPDX-License-Identifier: AGPL-3.0-or-later

using System.Numerics;
using Robust.Client.UserInterface.Controls;
using Robust.Client.UserInterface.CustomControls;
using static Robust.Client.UserInterface.Controls.BoxContainer;

namespace Content.Trauma.Client.Abnormalities.UI;

public sealed class CogitoPromptWindow : DefaultWindow
{
    public readonly Button AcceptButton;
    public readonly Button DenyButton;

    public CogitoPromptWindow()
    {
        Title = Loc.GetString("cogito-prompt-title");

        ContentsContainer.AddChild(new BoxContainer
        {
            Orientation = LayoutOrientation.Vertical,
            Children =
            {
                new Label { Text = Loc.GetString("cogito-prompt-text") },
                new BoxContainer
                {
                    Orientation = LayoutOrientation.Horizontal,
                    Align = AlignMode.Center,
                    Children =
                    {
                        (AcceptButton = new Button { Text = Loc.GetString("cogito-prompt-accept") }),
                        new Control { MinSize = new Vector2(20, 0) },
                        (DenyButton = new Button { Text = Loc.GetString("cogito-prompt-deny") }),
                    }
                }
            }
        });
    }
}
