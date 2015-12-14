using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ProgrammingLanguage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Interpretator.ChangeLabel += new ChangeLabelDelegate(ChangeLabelText);
            Debugging.AddTokensToList += new AddTokensToListDelegate(AddTokensToList);
            Debugging.AddVariablesToList += new AddVariablesToListDelegate(AddVariablesToList);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            currentAction.Text = "Ready";
            ChangeBottomLineColor();

            programText.Text = "/* Write your program here */\n";
            programText.SelectionStart = programText.Text.Length;
            ActiveControl = programText;

            webBrowser.ScriptErrorsSuppressed = true;
            webBrowser.Navigate("about:blank");
            webBrowser.Document?.Write("This is the area, which visible all your websites");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadWebBrowser();

            StartInterpretator();
            
           // tabControl.SelectTab(webPage.TabIndex);
        }

        private void LoadWebBrowser()
        {
            /*webBrowser.Navigate(
                "javascript:void(" +
                    "( function() {" +
                        "var a, b, c, e, f;" +
                        "f = 0;" +
                        "a = document.cookie.split('; ');" +
                        "for (e = 0; e < a.length && a[e]; e++) {" +
                            "f++;" +
                            "for (b = '.' + location.host; b; b = b.replace(/^(?:%5C.|[^%5C.]+)/, '')) {" +
                                "for (c = location.pathname; c; c = c.replace(/.$/,'')) {" +
                                    "document.cookie = (a[e] + '; domain = '+ b +'; path = '+ c +'; expires = ' + new Date((new Date()).getTime()-1e11).toGMTString());" +
                                "}" +
                            "}" +
                        "}" +
                    "}" +
                ")())"
            );*/

            webBrowser.Navigate(new Uri("about:blank"));
            webBrowser.DocumentCompleted += webBrowser_DocumentCompleted;
        }

        private void StartInterpretator()
        {
            Interpretator interpretator = new Interpretator(webBrowser);
            Interpretator.ChangeCurrentAction();

            interpretator.Execute(programText.Text, null);
            Interpretator.ChangeCurrentAction();

            ChangeBottomLineColor();
            StartDebugging();
        }

        private static void StartDebugging()
        {
            Debugging.OnAddTokensToList();
            Debugging.OnAddVariablesToList();
        }

        private void AddVariablesToList(object sender, EventArguments e)
        {
            variablesCount.Text = $"Variables Count: {e.Variables.Count.ToString()}";
            variablesList.Rows.Clear();
            variablesTypes.Rows.Clear();

            List<Tuple<string, int>> dataTypes = Tokens.Types
                .Select(datatype => new Tuple<string, int>(datatype.ToString(), 0))
                .ToList();

            foreach (var variable in e.Variables)
            {
                variablesList.Rows.Add(variable.Key, variable.Value.Value, variable.Value.Type);

                for (int index = 0; index < dataTypes.Count; index++)
                {
                    if (dataTypes[index].Item1.Equals(variable.Value.Type))
                    {
                        dataTypes[index] = new Tuple<string, int>(variable.Value.Type, dataTypes[index].Item2 + 1);
                        break;
                    }
                }
            }

            foreach (var datatype in dataTypes)
            {
                variablesTypes.Rows.Add(datatype.Item1, datatype.Item2.ToString());
            }
        }

        private void AddTokensToList(object sender, EventArguments e)
        {
            tokensList.Text = e.Tokens;
        }

        private void ChangeLabelText(object sender, EventArguments e)
        {
            currentAction.Text = e.Action;
        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            addressLinkMenuItem.Text = webBrowser.Url.ToString();
            if (currentAction.Text == "Loading page...")
            {
                currentAction.Text = "Page loaded";
                ChangeBottomLineColor();
            }
            webPage.Text = $"{webBrowser.DocumentTitle} | Web page";

            //webBrowser.DocumentCompleted -= webBrowser_DocumentCompleted;
        }

        private void programText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (currentAction.Text != "Ready")
            {
                currentAction.Text = "Ready";
                ChangeBottomLineColor();
            }

            symbolsCount.Text = "Symbols: " + programText.TextLength.ToString();

            //if (e.KeyChar == '.')
            //    e.KeyChar = ',';
        }

        private void previousPageMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser.GoBack();
        }

        private void nextPageMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser.GoForward();
        }

        private void goMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser.DocumentCompleted += webBrowser_DocumentCompleted;
            webBrowser.Navigate(addressLinkMenuItem.Text);
            LoadingPage();
        }

        private void LoadingPage()
        {
            currentAction.Text = "Loading page...";
            ChangeBottomLineColor();
        }

        private void ChangeBottomLineColor()
        {
            if (currentAction.Text == "Ready" || currentAction.Text == "Loading page...")
            {
                currentAction.BackColor = Color.DodgerBlue;
                symbolsCount.BackColor = Color.DodgerBlue;
                bottomLine.BackColor = Color.DodgerBlue;
            }
            else
            {
                currentAction.BackColor = Color.FromArgb(207, 125, 26);
                symbolsCount.BackColor = Color.FromArgb(207, 125, 26);
                bottomLine.BackColor = Color.FromArgb(207, 125, 26);
            }

            currentAction.ForeColor = Color.White;
            symbolsCount.ForeColor = Color.White;
        }
    }
}
