namespace dormitory_management_system
{
    partial class theForm
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
            this.menu = new dormitory_management_system.Menu();
            this.add = new dormitory_management_system.Add();
            this.search = new dormitory_management_system.search();
            this.home = new dormitory_management_system.Home();
            this.bills1 = new dormitory_management_system.Bills();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.menu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.menu.ForeColor = System.Drawing.Color.White;
            this.menu.Location = new System.Drawing.Point(0, -2);
            this.menu.Margin = new System.Windows.Forms.Padding(0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(827, 37);
            this.menu.TabIndex = 0;
            // 
            // add
            // 
            this.add.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.add.AutoScroll = true;
            this.add.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.add.Location = new System.Drawing.Point(9, 44);
            this.add.Margin = new System.Windows.Forms.Padding(0, 9, 0, 0);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(808, 520);
            this.add.TabIndex = 3;
            // 
            // search
            // 
            this.search.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.search.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.search.Location = new System.Drawing.Point(9, 44);
            this.search.Margin = new System.Windows.Forms.Padding(0, 9, 0, 0);
            this.search.Name = "search";
            this.search.Size = new System.Drawing.Size(808, 45);
            this.search.TabIndex = 1;
            // 
            // home
            // 
            this.home.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.home.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.home.Location = new System.Drawing.Point(9, 98);
            this.home.Margin = new System.Windows.Forms.Padding(0, 9, 0, 9);
            this.home.Name = "home";
            this.home.Size = new System.Drawing.Size(808, 466);
            this.home.TabIndex = 2;
            // 
            // bills1
            // 
            this.bills1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bills1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.bills1.Location = new System.Drawing.Point(9, 44);
            this.bills1.Margin = new System.Windows.Forms.Padding(0, 9, 0, 0);
            this.bills1.Name = "bills1";
            this.bills1.Size = new System.Drawing.Size(808, 520);
            this.bills1.TabIndex = 4;
            // 
            // theForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(35)))), ((int)(((byte)(39)))));
            this.ClientSize = new System.Drawing.Size(826, 573);
            this.Controls.Add(this.menu);
            this.Controls.Add(this.add);
            this.Controls.Add(this.search);
            this.Controls.Add(this.home);
            this.Controls.Add(this.bills1);
            this.Name = "theForm";
            this.Text = "theForm";
            this.Load += new System.EventHandler(this.theForm_Load);
            this.ResumeLayout(false);

        }
        #endregion

        private Menu menu;
        private Add add;
        private Home home;
        private search search;
        private Bills bills1;
    }
}