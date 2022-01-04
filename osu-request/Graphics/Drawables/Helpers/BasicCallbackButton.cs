using System;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;

namespace osu_request.Drawables
{
    public class BasicCallbackButton : BasicButton
    {
        public Action<ClickEvent> OnButtonClick;

        protected override bool OnClick(ClickEvent e)
        {
            OnButtonClick.Invoke(e);
            return base.OnClick(e);
        }
    }
}