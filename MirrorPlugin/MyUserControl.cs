using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oop.Tasks.Paint.Interface;

namespace MirrorPlugin
{
    public partial class MyUserControl : UserControl
    {
        private IPaintApplicationContext applicationContext = null;
        private System.Windows.Forms.NumericUpDown sizeNumericUpDown;

        public MyUserControl(IPaintApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
            InitializeComponent();
            this.sizeNumericUpDown=numericUpDown1;
        }

        internal int PencilSize
        {
            get
            {
                return (int)sizeNumericUpDown.Value;
            }
        }

        internal Pen GetPen()
        {
            if (applicationContext == null)
                return null;

            Pen result = new Pen(applicationContext.ForegroundColor, 1);
            return result;
        }


        internal Brush GetBrush()
        {
            if (applicationContext == null)
                return null;

            Brush result = new SolidBrush(applicationContext.ForegroundColor);
            return result;
        }

    }
}
