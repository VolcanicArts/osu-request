using System;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Events;
using osuTK;
using osuTK.Graphics;

namespace osu_request.Drawables
{
    public class ToolbarItem : Container
    {
        private static readonly ColourInfo HoverColour = ColourInfo.GradientVertical(OsuRequestColour.Gray7.Opacity(0.5f), OsuRequestColour.Invisible);
        private static readonly ColourInfo HoverLostColour = OsuRequestColour.Invisible;
        private static readonly ColourInfo SelectedColour = ColourInfo.GradientVertical(OsuRequestColour.Gray7, OsuRequestColour.Invisible);

        private Box Background;
        private bool Selected;

        public Action<int> OnSelected;

        protected internal int ID { get; init; }

        [BackgroundDependencyLoader]
        private void Load()
        {
            TextFlowContainer _text;

            Child = new Container
            {
                Anchor = Anchor.TopCentre,
                Origin = Anchor.TopCentre,
                RelativeSizeAxes = Axes.Y,
                Size = new Vector2(200, 1.0f),
                Children = new Drawable[]
                {
                    Background = new Box
                    {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        RelativeSizeAxes = Axes.Both,
                        Colour = OsuRequestColour.Invisible,
                        Size = new Vector2(1.0f, 0.9f)
                    },
                    _text = new TextFlowContainer
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        TextAnchor = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both
                    }
                }
            };

            _text.AddText(Name, t =>
            {
                t.Font = OsuRequestFonts.Regular.With(size: 30);
                t.Shadow = true;
                t.ShadowColour = Color4.Black.Opacity(0.5f);
                t.ShadowOffset = new Vector2(0.0f, 0.025f);
            });
        }

        protected override bool OnHover(HoverEvent e)
        {
            if (!Selected) Background.FadeColour(HoverColour, 300, Easing.OutCubic);
            return true;
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            if (!Selected) Background.FadeColour(HoverLostColour, 300, Easing.InCubic);
            base.OnHoverLost(e);
        }

        protected override bool OnClick(ClickEvent e)
        {
            OnSelected?.Invoke(ID);
            return true;
        }

        protected override bool OnDoubleClick(DoubleClickEvent e)
        {
            return true;
        }

        public void Select()
        {
            Selected = true;
            Background.FadeColour(SelectedColour, 200, Easing.OutCubic);
        }

        public void Deselect()
        {
            Selected = false;
            Background.FadeColour(OsuRequestColour.Invisible, 200, Easing.InCubic);
        }
    }
}