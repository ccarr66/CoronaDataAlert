using Microsoft.Win32;

namespace CoronaDataAlert
{
    partial class MainWindow
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
                SystemEvents.PowerModeChanged -= SystemEvents_PowerModeChanged;
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
            SystemEvents.PowerModeChanged += SystemEvents_PowerModeChanged;

            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_Minimize = new System.Windows.Forms.Button();
            this.btn_DragRegion = new System.Windows.Forms.Button();
            this.lbl_USCases = new System.Windows.Forms.Label();
            this.lbl_CACases = new System.Windows.Forms.Label();
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.SuspendLayout();
            // 
            // btn_Close
            // 
            this.btn_Close.FlatAppearance.BorderSize = 0;
            this.btn_Close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.btn_Close.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Close.Image = global::CoronaDataAlert.Properties.Resources.X_button;
            this.btn_Close.Location = new System.Drawing.Point(580, 0);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(20, 20);
            this.btn_Close.TabIndex = 0;
            this.btn_Close.TabStop = false;
            this.btn_Close.UseVisualStyleBackColor = false;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_Minimize
            // 
            this.btn_Minimize.FlatAppearance.BorderSize = 0;
            this.btn_Minimize.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.btn_Minimize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.btn_Minimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Minimize.Image = global::CoronaDataAlert.Properties.Resources.Minimize;
            this.btn_Minimize.Location = new System.Drawing.Point(560, 0);
            this.btn_Minimize.Name = "btn_Minimize";
            this.btn_Minimize.Size = new System.Drawing.Size(20, 20);
            this.btn_Minimize.TabIndex = 0;
            this.btn_Minimize.TabStop = false;
            this.btn_Minimize.UseVisualStyleBackColor = true;
            this.btn_Minimize.Click += new System.EventHandler(this.btn_Minimize_Click);
            // 
            // btn_DragRegion
            // 
            this.btn_DragRegion.FlatAppearance.BorderSize = 0;
            this.btn_DragRegion.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.btn_DragRegion.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.btn_DragRegion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_DragRegion.Location = new System.Drawing.Point(0, 0);
            this.btn_DragRegion.Name = "btn_DragRegion";
            this.btn_DragRegion.Size = new System.Drawing.Size(560, 20);
            this.btn_DragRegion.TabIndex = 0;
            this.btn_DragRegion.TabStop = false;
            this.btn_DragRegion.UseVisualStyleBackColor = true;
            this.btn_DragRegion.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_DragRegion_MouseDown);
            this.btn_DragRegion.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btn_DragRegion_MouseMove);
            this.btn_DragRegion.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_DragRegion_MouseUp);
            // 
            // lbl_USCases
            // 
            this.lbl_USCases.AutoSize = true;
            this.lbl_USCases.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_USCases.ForeColor = System.Drawing.Color.Gray;
            this.lbl_USCases.Location = new System.Drawing.Point(0, 35);
            this.lbl_USCases.Name = "lbl_USCases";
            this.lbl_USCases.Size = new System.Drawing.Size(0, 39);
            this.lbl_USCases.TabIndex = 0;
            // 
            // lbl_CACases
            // 
            this.lbl_CACases.AutoSize = true;
            this.lbl_CACases.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_CACases.ForeColor = System.Drawing.Color.Gray;
            this.lbl_CACases.Location = new System.Drawing.Point(0, 135);
            this.lbl_CACases.Name = "lbl_CACases";
            this.lbl_CACases.Size = new System.Drawing.Size(0, 39);
            this.lbl_CACases.TabIndex = 0;
            // 
            // trayIcon
            // 
            this.trayIcon.BalloonTipTitle = "COVID Alerts";
            this.trayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIcon.Icon")));
            this.trayIcon.Text = "COVID Alerts";
            this.trayIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.trayIcon_MouseDoubleClick);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(600, 200);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_Minimize);
            this.Controls.Add(this.btn_DragRegion);
            this.Controls.Add(this.lbl_USCases);
            this.Controls.Add(this.lbl_CACases);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainWindow";
            this.Text = "MainWindow";
            this.ResizeEnd += new System.EventHandler(this.MainWindow_ResizeEnd);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_Minimize;
        private System.Windows.Forms.Button btn_DragRegion;
        private System.Windows.Forms.Label lbl_USCases;
        private System.Windows.Forms.Label lbl_CACases;
        private System.Windows.Forms.NotifyIcon trayIcon;
    }
}