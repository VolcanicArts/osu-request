﻿using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;
using osuTK.Graphics;

namespace osu_request.Drawables
{
    public class RequestsListingTab : Container
    {
        private Container _borderContainer;
        
        [BackgroundDependencyLoader]
        private void Load()
        {
            InitSelf();
            InitChildren();
        }

        private void InitSelf()
        {
            Anchor = Anchor.CentreLeft;
            Origin = Anchor.CentreLeft;
            RelativeSizeAxes = Axes.Both;
            Size = new Vector2(0.5f, 1.0f);
            Padding = new MarginPadding(20.0f);
        }

        protected override void UpdateAfterAutoSize()
        {
            base.UpdateAfterAutoSize();
            _borderContainer.CornerRadius = DrawWidth / 10.0f;
        }

        private void InitChildren()
        {
            Child = _borderContainer = new Container
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
                BorderThickness = 6,
                BorderColour = Color4.Black,
                Masking = true,
                CornerRadius = 50,
                Children = new Drawable[]
                {
                    new Box
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both,
                        Colour = Color4.Gray
                    },
                    new BeatmapsetListContainer
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both,
                        Padding = new MarginPadding(6.0f)
                    }
                }
            };
        }
    }
}