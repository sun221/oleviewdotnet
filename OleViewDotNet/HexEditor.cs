﻿//    This file is part of OleViewDotNet.
//    Copyright (C) James Forshaw 2014
//
//    OleViewDotNet is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    OleViewDotNet is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with OleViewDotNet.  If not, see <http://www.gnu.org/licenses/>.

using Be.Windows.Forms;
using System.IO;
using System.Windows.Forms;

namespace OleViewDotNet
{
    public partial class HexEditor : UserControl
    {
        DynamicByteProvider _bytes;

        public HexEditor()
        {
            InitializeComponent();
            Bytes = new byte[0];
        }

        public byte[] Bytes
        {
            get
            {
                return _bytes.Bytes.ToArray();
            }

            set
            {
                _bytes = new DynamicByteProvider(value);
                hexBox.ByteProvider = _bytes;
                hexBox.Invalidate();
            }
        }

        private void loadFromFileToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "All Files (*.*)|*.*";

                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        Bytes = File.ReadAllBytes(dlg.FileName);
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void saveToFileToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Filter = "All Files (*.*)|*.*";

                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        File.WriteAllBytes(dlg.FileName, Bytes);
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void copyToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (hexBox.CanCopy())
            {
                hexBox.Copy();
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (hexBox.CanPaste())
            {
                hexBox.Paste();
            }
        }

        private void pasteHexToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (hexBox.CanPasteHex())
            {
                hexBox.PasteHex();
            }
        }

        private void contextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            pasteToolStripMenuItem.Enabled = hexBox.CanPaste();
            pasteHexToolStripMenuItem.Enabled = hexBox.CanPasteHex();
            copyToolStripMenuItem.Enabled = hexBox.CanCopy();
            copyHexToolStripMenuItem.Enabled = hexBox.CanCopy();
            cutToolStripMenuItem.Enabled = hexBox.CanCut();
        }

        private void copyHexToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (hexBox.CanCopy())
            {
                hexBox.CopyHex();
            }
        }

        private void cutToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (hexBox.CanCut())
            {
                hexBox.Cut();
            }
        }
    }
}
