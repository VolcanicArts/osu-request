using System;
using System.Diagnostics;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Platform;
using osuTK;

namespace osu_request.Drawables
{
    /// <summary>
    /// A container that has a single relative size axes that affects the other absolute axes
    /// </summary>
    public class RelativeSizedContainer : Container
    {
        private GameHost _host;
        protected internal Axes RelativeAxes { get; set; } = Axes.Y;
        protected internal float ScaleFactor { get; set; } = 1.0f;

        [BackgroundDependencyLoader]
        private void Load(GameHost host)
        {
            Debug.Assert(RelativeAxes != Axes.Both);
            Debug.Assert(RelativeAxes != Axes.None);

            _host = host;
            _host.Window.Resized += UpdateSizing;
            RelativeSizeAxes = (RelativeAxes == Axes.Y) ? Axes.X : Axes.Y;
        }

        private void UpdateSizing()
        {
            Size = RelativeAxes switch
            {
                Axes.Y => new Vector2(Size.X, MathF.Max(DrawWidth * ScaleFactor, 0)),
                Axes.X => new Vector2(MathF.Max(DrawHeight * ScaleFactor, 0), Size.Y),
                _ => Size
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            UpdateSizing();
        }

        protected override void Dispose(bool isDisposing)
        {
            _host.Window.Resized -= UpdateSizing;
            base.Dispose(isDisposing);
        }
    }
}