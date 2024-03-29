﻿namespace Client
{
    partial class ClientForm
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
            this.SubmitButton = new System.Windows.Forms.Button();
            this.InputField = new System.Windows.Forms.TextBox();
            this.MessageWindow = new System.Windows.Forms.TextBox();
            this.ChangeNicknameButton = new System.Windows.Forms.Button();
            this.NicknameInputField = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // SubmitButton
            // 
            this.SubmitButton.Location = new System.Drawing.Point(666, 389);
            this.SubmitButton.Name = "SubmitButton";
            this.SubmitButton.Size = new System.Drawing.Size(122, 49);
            this.SubmitButton.TabIndex = 0;
            this.SubmitButton.Text = "Submit";
            this.SubmitButton.UseVisualStyleBackColor = true;
            this.SubmitButton.Click += new System.EventHandler(this.SubmitButton_Click);
            // 
            // InputField
            // 
            this.InputField.Location = new System.Drawing.Point(12, 404);
            this.InputField.Name = "InputField";
            this.InputField.Size = new System.Drawing.Size(648, 20);
            this.InputField.TabIndex = 1;
            // 
            // MessageWindow
            // 
            this.MessageWindow.Location = new System.Drawing.Point(12, 61);
            this.MessageWindow.Multiline = true;
            this.MessageWindow.Name = "MessageWindow";
            this.MessageWindow.Size = new System.Drawing.Size(776, 322);
            this.MessageWindow.TabIndex = 2;
            // 
            // ChangeNicknameButton
            // 
            this.ChangeNicknameButton.Location = new System.Drawing.Point(247, 12);
            this.ChangeNicknameButton.Name = "ChangeNicknameButton";
            this.ChangeNicknameButton.Size = new System.Drawing.Size(108, 33);
            this.ChangeNicknameButton.TabIndex = 3;
            this.ChangeNicknameButton.Text = "Change Nickname";
            this.ChangeNicknameButton.UseVisualStyleBackColor = true;
            this.ChangeNicknameButton.Click += new System.EventHandler(this.ChangeNicknameButton_Click_1);
            // 
            // NicknameInputField
            // 
            this.NicknameInputField.Location = new System.Drawing.Point(13, 20);
            this.NicknameInputField.Name = "NicknameInputField";
            this.NicknameInputField.Size = new System.Drawing.Size(228, 20);
            this.NicknameInputField.TabIndex = 4;
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.NicknameInputField);
            this.Controls.Add(this.ChangeNicknameButton);
            this.Controls.Add(this.MessageWindow);
            this.Controls.Add(this.InputField);
            this.Controls.Add(this.SubmitButton);
            this.Name = "ClientForm";
            this.Text = "ClientForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SubmitButton;
        private System.Windows.Forms.TextBox InputField;
        private System.Windows.Forms.TextBox MessageWindow;
        private System.Windows.Forms.Button ChangeNicknameButton;
        private System.Windows.Forms.TextBox NicknameInputField;
    }
}