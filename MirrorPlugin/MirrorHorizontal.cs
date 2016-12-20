using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oop.Tasks.Paint.Interface;
using System.Drawing;
using System.Windows.Forms;


namespace MirrorPlugin
{
    [PluginForLoad(true)]
    public class Pencil : IToolPaintPlugin
    {
        private bool canDraw { get; set; }

        private IPaintPluginContext pluginContext = null;

        private MyUserControl optionsControl = null;

        private Cursor cursor = null;

        private Image icon = null;

        private IPaintApplicationContext ApplicationContext
        {
            get
            {
                if (pluginContext == null)
                    return null;
                else
                    return pluginContext.ApplicationContext;
            }
        }

        public void AfterCreate(IPaintPluginContext pluginContext)
        {
            this.pluginContext = pluginContext;

            optionsControl = new MyUserControl(ApplicationContext);

            string imageDir = pluginContext.PluginDir;

            canDraw = false;

            if (imageDir != null)
            {
                imageDir += @"\Images\";

                try
                {
                    icon = Image.FromFile(imageDir + "Icon.bmp");
                }
                catch { }

                try
                {
                    cursor = new Cursor(imageDir + "Cursor.cur");
                }
                catch { }
            }
        }

        public void BeforeDestroy()
        {
            optionsControl.Dispose();
            optionsControl = null;
            if (cursor != null)
                cursor.Dispose();
            if (icon != null)
                icon.Dispose();
        }

        public void Select(bool selection)
        {
            if (selection)
            {
                ApplicationContext.OptionsControl = optionsControl;
                ApplicationContext.Cursor = cursor;
            }
            else
            {
                ApplicationContext.OptionsControl = null;
                ApplicationContext.Cursor = null;
            }
        }

        public string Name
        {
            get
            {
                return this.GetType().Name;
            }
        }

        public string CommandName
        {
            get
            {
                return "Карандаш";
            }
        }

        public void MouseDown(MouseEventArgs me, Keys modifierKeys)
        {
            canDraw = true;
        }

        public void MouseUp(MouseEventArgs me, Keys modifierKeys)
        {
            canDraw = false;
        }

        public void MouseMove(MouseEventArgs me, Keys modifierKeys)
        {
            if (canDraw)
            {
                Graphics graphics = Graphics.FromImage(ApplicationContext.Image);

                Pen pen = optionsControl.GetPen();
                Brush brush = optionsControl.GetBrush();
                int size = optionsControl.PencilSize;

                int x = me.X - size / 2;
                int y = me.Y - size / 2;
                size--;

                graphics.FillEllipse(brush, x, y, size, size);
                graphics.DrawEllipse(pen, x, y, size, size);

                pen.Dispose();
                brush.Dispose();

                ApplicationContext.Invalidate();

            }

        }

        public void Escape()
        {
        }

        public void Enter()
        {
        }

        public void ColorChange()
        {
        }

        public void ImageChange()
        {
        }

        public string ToolName
        {
            get
            {
                return "Карандаш";
            }
        }

        public Image Icon
        {
            get
            {
                return icon;
            }
        }

    }
}
