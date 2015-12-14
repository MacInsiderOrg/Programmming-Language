namespace ProgrammingLanguage
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.programText = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.bottomLine = new System.Windows.Forms.PictureBox();
            this.currentAction = new System.Windows.Forms.Label();
            this.symbolsCount = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.codePage = new System.Windows.Forms.TabPage();
            this.tokensPage = new System.Windows.Forms.TabPage();
            this.tokensList = new System.Windows.Forms.RichTextBox();
            this.buildResultsPage = new System.Windows.Forms.TabPage();
            this.variablesTypes = new System.Windows.Forms.DataGridView();
            this.dataType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.variableCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.variablesCount = new System.Windows.Forms.Label();
            this.variablesList = new System.Windows.Forms.DataGridView();
            this.Variable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.webPage = new System.Windows.Forms.TabPage();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.previousPageMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nextPageMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addressLinkMenuItem = new System.Windows.Forms.ToolStripTextBox();
            this.goMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.bottomLine)).BeginInit();
            this.tabControl.SuspendLayout();
            this.codePage.SuspendLayout();
            this.tokensPage.SuspendLayout();
            this.buildResultsPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.variablesTypes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.variablesList)).BeginInit();
            this.webPage.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // programText
            // 
            this.programText.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.programText.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.programText.Location = new System.Drawing.Point(6, 6);
            this.programText.Name = "programText";
            this.programText.Size = new System.Drawing.Size(614, 467);
            this.programText.TabIndex = 0;
            this.programText.Text = "/* Write your Application */";
            this.programText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.programText_KeyPress);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(657, 23);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 33);
            this.button1.TabIndex = 1;
            this.button1.Text = "Run application";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // bottomLine
            // 
            this.bottomLine.Location = new System.Drawing.Point(0, 514);
            this.bottomLine.Name = "bottomLine";
            this.bottomLine.Size = new System.Drawing.Size(840, 24);
            this.bottomLine.TabIndex = 2;
            this.bottomLine.TabStop = false;
            // 
            // currentAction
            // 
            this.currentAction.AutoSize = true;
            this.currentAction.BackColor = System.Drawing.Color.Transparent;
            this.currentAction.Location = new System.Drawing.Point(6, 518);
            this.currentAction.Name = "currentAction";
            this.currentAction.Size = new System.Drawing.Size(38, 13);
            this.currentAction.TabIndex = 3;
            this.currentAction.Text = "Ready";
            // 
            // symbolsCount
            // 
            this.symbolsCount.AutoSize = true;
            this.symbolsCount.BackColor = System.Drawing.Color.Transparent;
            this.symbolsCount.Location = new System.Drawing.Point(737, 518);
            this.symbolsCount.Name = "symbolsCount";
            this.symbolsCount.Size = new System.Drawing.Size(58, 13);
            this.symbolsCount.TabIndex = 7;
            this.symbolsCount.Text = "Symbols: 0";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.codePage);
            this.tabControl.Controls.Add(this.tokensPage);
            this.tabControl.Controls.Add(this.buildResultsPage);
            this.tabControl.Controls.Add(this.webPage);
            this.tabControl.Location = new System.Drawing.Point(6, 3);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(827, 505);
            this.tabControl.TabIndex = 8;
            // 
            // codePage
            // 
            this.codePage.Controls.Add(this.programText);
            this.codePage.Controls.Add(this.button1);
            this.codePage.Location = new System.Drawing.Point(4, 22);
            this.codePage.Name = "codePage";
            this.codePage.Padding = new System.Windows.Forms.Padding(3);
            this.codePage.Size = new System.Drawing.Size(819, 479);
            this.codePage.TabIndex = 0;
            this.codePage.Text = "Code";
            this.codePage.UseVisualStyleBackColor = true;
            // 
            // tokensPage
            // 
            this.tokensPage.Controls.Add(this.tokensList);
            this.tokensPage.Location = new System.Drawing.Point(4, 22);
            this.tokensPage.Name = "tokensPage";
            this.tokensPage.Size = new System.Drawing.Size(819, 479);
            this.tokensPage.TabIndex = 1;
            this.tokensPage.Text = "Tokens";
            this.tokensPage.UseVisualStyleBackColor = true;
            // 
            // tokensList
            // 
            this.tokensList.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tokensList.Location = new System.Drawing.Point(6, 6);
            this.tokensList.Name = "tokensList";
            this.tokensList.ReadOnly = true;
            this.tokensList.Size = new System.Drawing.Size(805, 467);
            this.tokensList.TabIndex = 0;
            this.tokensList.Text = "";
            // 
            // buildResultsPage
            // 
            this.buildResultsPage.Controls.Add(this.variablesTypes);
            this.buildResultsPage.Controls.Add(this.label1);
            this.buildResultsPage.Controls.Add(this.variablesCount);
            this.buildResultsPage.Controls.Add(this.variablesList);
            this.buildResultsPage.Location = new System.Drawing.Point(4, 22);
            this.buildResultsPage.Name = "buildResultsPage";
            this.buildResultsPage.Size = new System.Drawing.Size(819, 479);
            this.buildResultsPage.TabIndex = 2;
            this.buildResultsPage.Text = "Build results";
            this.buildResultsPage.UseVisualStyleBackColor = true;
            // 
            // variablesTypes
            // 
            this.variablesTypes.AllowUserToAddRows = false;
            this.variablesTypes.AllowUserToDeleteRows = false;
            this.variablesTypes.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.variablesTypes.ColumnHeadersHeight = 28;
            this.variablesTypes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataType,
            this.variableCount});
            this.variablesTypes.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.variablesTypes.Location = new System.Drawing.Point(567, 77);
            this.variablesTypes.Name = "variablesTypes";
            this.variablesTypes.ReadOnly = true;
            this.variablesTypes.RowHeadersWidth = 20;
            this.variablesTypes.RowTemplate.Height = 26;
            this.variablesTypes.Size = new System.Drawing.Size(237, 162);
            this.variablesTypes.TabIndex = 3;
            // 
            // dataType
            // 
            this.dataType.HeaderText = "Type";
            this.dataType.Name = "dataType";
            this.dataType.ReadOnly = true;
            this.dataType.Width = 140;
            // 
            // variableCount
            // 
            this.variableCount.HeaderText = "Count";
            this.variableCount.Name = "variableCount";
            this.variableCount.ReadOnly = true;
            this.variableCount.Width = 75;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(567, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Count variables in types:";
            // 
            // variablesCount
            // 
            this.variablesCount.AutoSize = true;
            this.variablesCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.variablesCount.Location = new System.Drawing.Point(567, 11);
            this.variablesCount.Name = "variablesCount";
            this.variablesCount.Size = new System.Drawing.Size(116, 16);
            this.variablesCount.TabIndex = 1;
            this.variablesCount.Text = "Variables Count: 0";
            // 
            // variablesList
            // 
            this.variablesList.AllowUserToAddRows = false;
            this.variablesList.AllowUserToDeleteRows = false;
            this.variablesList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.variablesList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Variable,
            this.Value,
            this.Type});
            this.variablesList.Location = new System.Drawing.Point(6, 6);
            this.variablesList.Name = "variablesList";
            this.variablesList.ReadOnly = true;
            this.variablesList.Size = new System.Drawing.Size(546, 467);
            this.variablesList.TabIndex = 0;
            // 
            // Variable
            // 
            this.Variable.HeaderText = "Variable";
            this.Variable.Name = "Variable";
            this.Variable.ReadOnly = true;
            this.Variable.Width = 200;
            // 
            // Value
            // 
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            this.Value.ReadOnly = true;
            this.Value.Width = 200;
            // 
            // Type
            // 
            this.Type.HeaderText = "Type";
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            // 
            // webPage
            // 
            this.webPage.Controls.Add(this.webBrowser);
            this.webPage.Controls.Add(this.menuStrip1);
            this.webPage.Location = new System.Drawing.Point(4, 22);
            this.webPage.Name = "webPage";
            this.webPage.Size = new System.Drawing.Size(819, 479);
            this.webPage.TabIndex = 3;
            this.webPage.Text = "Web page";
            this.webPage.UseVisualStyleBackColor = true;
            // 
            // webBrowser
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.Location = new System.Drawing.Point(0, 31);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(819, 448);
            this.webBrowser.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Silver;
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.previousPageMenuItem,
            this.nextPageMenuItem,
            this.addressToolStripMenuItem,
            this.addressLinkMenuItem,
            this.goMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4);
            this.menuStrip1.Size = new System.Drawing.Size(819, 31);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // previousPageMenuItem
            // 
            this.previousPageMenuItem.Name = "previousPageMenuItem";
            this.previousPageMenuItem.Padding = new System.Windows.Forms.Padding(4, 0, 8, 0);
            this.previousPageMenuItem.Size = new System.Drawing.Size(79, 23);
            this.previousPageMenuItem.Text = "< Previous";
            this.previousPageMenuItem.Click += new System.EventHandler(this.previousPageMenuItem_Click);
            // 
            // nextPageMenuItem
            // 
            this.nextPageMenuItem.Name = "nextPageMenuItem";
            this.nextPageMenuItem.Padding = new System.Windows.Forms.Padding(4, 0, 8, 0);
            this.nextPageMenuItem.Size = new System.Drawing.Size(58, 23);
            this.nextPageMenuItem.Text = "Next >";
            this.nextPageMenuItem.Click += new System.EventHandler(this.nextPageMenuItem_Click);
            // 
            // addressToolStripMenuItem
            // 
            this.addressToolStripMenuItem.Name = "addressToolStripMenuItem";
            this.addressToolStripMenuItem.Padding = new System.Windows.Forms.Padding(4, 0, 6, 0);
            this.addressToolStripMenuItem.Size = new System.Drawing.Size(63, 23);
            this.addressToolStripMenuItem.Text = "Address";
            // 
            // addressLinkMenuItem
            // 
            this.addressLinkMenuItem.Margin = new System.Windows.Forms.Padding(1, 0, 8, 0);
            this.addressLinkMenuItem.Name = "addressLinkMenuItem";
            this.addressLinkMenuItem.Size = new System.Drawing.Size(400, 23);
            // 
            // goMenuItem
            // 
            this.goMenuItem.Name = "goMenuItem";
            this.goMenuItem.Size = new System.Drawing.Size(55, 23);
            this.goMenuItem.Text = "Go -->";
            this.goMenuItem.Click += new System.EventHandler(this.goMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 536);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.symbolsCount);
            this.Controls.Add(this.currentAction);
            this.Controls.Add(this.bottomLine);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(854, 575);
            this.MinimumSize = new System.Drawing.Size(854, 575);
            this.Name = "Form1";
            this.Text = "Programming Language";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bottomLine)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.codePage.ResumeLayout(false);
            this.tokensPage.ResumeLayout(false);
            this.buildResultsPage.ResumeLayout(false);
            this.buildResultsPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.variablesTypes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.variablesList)).EndInit();
            this.webPage.ResumeLayout(false);
            this.webPage.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox programText;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox bottomLine;
        public System.Windows.Forms.Label currentAction;
        private System.Windows.Forms.Label symbolsCount;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage codePage;
        private System.Windows.Forms.TabPage tokensPage;
        private System.Windows.Forms.TabPage buildResultsPage;
        private System.Windows.Forms.TabPage webPage;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem previousPageMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nextPageMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addressToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox addressLinkMenuItem;
        private System.Windows.Forms.ToolStripMenuItem goMenuItem;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.RichTextBox tokensList;
        private System.Windows.Forms.DataGridView variablesList;
        private System.Windows.Forms.DataGridViewTextBoxColumn Variable;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.Label variablesCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataType;
        private System.Windows.Forms.DataGridViewTextBoxColumn variableCount;
        private System.Windows.Forms.DataGridView variablesTypes;
    }
}

