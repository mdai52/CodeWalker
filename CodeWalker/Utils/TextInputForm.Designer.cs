﻿namespace CodeWalker.Utils
{
    partial class TextInputForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextInputForm));
            this.MainTextBox = new FastColoredTextBoxNS.FastColoredTextBox();
            this.OkButton = new System.Windows.Forms.Button();
            this.CancelThisButton = new System.Windows.Forms.Button();
            this.PromptLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.MainTextBox)).BeginInit();
            this.SuspendLayout();
            // 
            // MainTextBox
            // 
            this.MainTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainTextBox.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.MainTextBox.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.MainTextBox.BackBrush = null;
            this.MainTextBox.CharHeight = 14;
            this.MainTextBox.CharWidth = 8;
            this.MainTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.MainTextBox.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.MainTextBox.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.MainTextBox.IsReplaceMode = false;
            this.MainTextBox.Location = new System.Drawing.Point(1, 33);
            this.MainTextBox.Name = "MainTextBox";
            this.MainTextBox.Paddings = new System.Windows.Forms.Padding(0);
            this.MainTextBox.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.MainTextBox.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("MainTextBox.ServiceColors")));
            this.MainTextBox.Size = new System.Drawing.Size(658, 293);
            this.MainTextBox.TabIndex = 0;
            this.MainTextBox.Zoom = 100;
            this.MainTextBox.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.MainTextBox_TextChanged);
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.OkButton.Location = new System.Drawing.Point(573, 332);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 28);
            this.OkButton.TabIndex = 1;
            this.OkButton.Text = "完成";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // CancelThisButton
            // 
            this.CancelThisButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelThisButton.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CancelThisButton.Location = new System.Drawing.Point(492, 332);
            this.CancelThisButton.Name = "CancelThisButton";
            this.CancelThisButton.Size = new System.Drawing.Size(75, 28);
            this.CancelThisButton.TabIndex = 2;
            this.CancelThisButton.Text = "取消";
            this.CancelThisButton.UseVisualStyleBackColor = true;
            this.CancelThisButton.Click += new System.EventHandler(this.CancelThisButton_Click);
            // 
            // PromptLabel
            // 
            this.PromptLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PromptLabel.AutoEllipsis = true;
            this.PromptLabel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PromptLabel.Location = new System.Drawing.Point(12, 5);
            this.PromptLabel.Name = "PromptLabel";
            this.PromptLabel.Size = new System.Drawing.Size(636, 25);
            this.PromptLabel.TabIndex = 3;
            this.PromptLabel.Text = "请输入文本：";
            this.PromptLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TextInputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 366);
            this.Controls.Add(this.PromptLabel);
            this.Controls.Add(this.CancelThisButton);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.MainTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TextInputForm";
            this.Text = "输入文本 - CodeWalker by dexyfex";
            ((System.ComponentModel.ISupportInitialize)(this.MainTextBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FastColoredTextBoxNS.FastColoredTextBox MainTextBox;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button CancelThisButton;
        private System.Windows.Forms.Label PromptLabel;
    }
}