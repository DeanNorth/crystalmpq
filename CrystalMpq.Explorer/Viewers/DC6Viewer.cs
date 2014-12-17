#region Copyright Notice
// This file is part of CrystalMPQ.
// 
// Copyright (C) 2007-2011 Fabien BARBIER
// 
// CrystalMPQ is licenced under the Microsoft Reciprocal License.
// You should find the licence included with the source of the program,
// or at this URL: http://www.microsoft.com/opensource/licenses.mspx#Ms-RL
#endregion

using System;
using System.Drawing;
using System.Linq;
using Stream = System.IO.Stream;
using CrystalMpq.DataFormats;
using CrystalMpq.Explorer.Extensibility;
using System.Windows.Forms;

namespace CrystalMpq.Explorer.Viewers
{
	internal sealed class DC6Viewer : FileViewer
	{
		//private Bitmap bitmap;

        public DC6Viewer(IHost host)
			: base(host)
		{
			DoubleBuffered = true;
			BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			ApplySettings();

            this.AutoScroll = true;
		}

		public override void ApplySettings()
		{
			BackColor = Host.ViewerBackColor;
		}

        //public Bitmap Bitmap
        //{
        //    get
        //    {
        //        return bitmap;
        //    }
        //    set
        //    {
        //        if (value != bitmap)
        //        {
        //            bitmap = value;
        //            this.BackgroundImage = bitmap;
        //        }
        //    }
        //}

		protected override void OnFileChanged()
		{
            foreach (var item in this.Controls.Cast<Control>().ToArray())
            {
                Controls.Remove(item);
            }

			if (File == null)
			{
				//Bitmap = null;
				return;
			}
			else
			{
				Stream stream;
                
				stream = File.Open();
				try
				{
                    int y = 0;
                    foreach (var item in new DC6(stream).Images())
                    {
                        var pb = new PictureBox();
                        this.Controls.Add(pb);

                        pb.Location = new Point(0, y);

                        pb.Image = item;
                        pb.Size = pb.Image.Size;
                        y += pb.Image.Height + 1;
                    }
				}
				finally
				{
					stream.Close();
				}
			}
		}
	}
}
