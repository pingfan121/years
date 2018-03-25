using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLib.Util
{
    public class DialogControl
    {
        public string controlType { get; set; }
        public string controlId { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string guideId { get; set; }
        

        public DialogControl(string id)
        {
            controlId = id;
        }
    }

    public class DialogControlLabel : DialogControl
    {
        public string text { get; set; }
        public string toolTip { get; set; }
        public int color { get; set; }
        public int size { get; set; }
        public int bold { get; set; }
        public string align { get; set; }
        public int underline { get; set; }

        public DialogControlLabel(string id)
            : base(id)
        {
            controlType = "Label";
        }
    }

    public class DialogControlButton : DialogControl
    {
        public string text { get; set; }
        public string toolTip { get; set; }
        public string skin { get; set; }
        public string sizeGrid { get; set; }
        public string labelMargin { get; set; }
        public string labelColors { get; set; }
        public string alert { get; set; }
        public int clickClose { get; set; }
        public DialogControlButton(string id)
            : base(id)
        {
            controlType = "Button";
            clickClose = 0;
        }
    }

    public class DialogControlLinkButton : DialogControl
    {
        public string text { get; set; }
        public string toolTip { get; set; }
        public string labelColors { get; set; }
        public int labelSize { get; set; }
        public string alert { get; set; }
        public int clickClose { get; set; }
        public DialogControlLinkButton(string id)
            : base(id)
        {
            controlType = "LinkButton";
            clickClose = 0;
        }
    }

    public class DialogControlImage : DialogControl
    {
        public string url { get; set; }
        public string toolTip { get; set; }
        public string sizeGrid { get; set; }
        public DialogControlImage(string id)
            : base(id)
        {
            controlType = "Image";
        }
    }

    public class DialogControlItem : DialogControl
    {
        public int itemId { get; set; }
        public DialogControlItem(string id)
            : base(id)
        {
            controlType = "Item";
        }
    }

    public class DialogControlTimer : DialogControl
    {
        public int time { get; set; }
        public DialogControlTimer(string id)
            : base(id)
        {
            controlType = "Timer";
        }
    }

    public class DialogPannel
    {
        public string dialog { get; set; }
        public Dictionary<string, DialogControl> controls { get; set; }

        public DialogPannel()
        {
            controls = new Dictionary<string, DialogControl>();
        }

        public bool addControl(DialogControl ctrl)
        {
            if (controls.ContainsKey(ctrl.controlId) == true)
                return false;

            controls.Add(ctrl.controlId, ctrl);

            return true;
        }

        public void delControl(string id)
        {
            controls.Remove(id);
        }

        public bool setControl(DialogControl ctrl)
        {
            if (controls.ContainsKey(ctrl.controlId) == false)
                return false;

            controls[ctrl.controlId] = ctrl;
            return true;
        }

        public DialogControl getControl(string id)
        {
            if (controls.ContainsKey(id) == false)
                return null;

            return controls[id];
        }

        public string toJson()
        {
            string json = string.Empty;
            if (controls.Count > 0)
                json = string.Format(@"{{""dialog"":""{0}"", ""controls"":{1}}}", dialog, JSON.Encode(controls.Values));
            else
                json = string.Format(@"{{""dialog"":""{0}""}}", dialog);
            return json;
        }
    }
}
