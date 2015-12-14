using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ProgrammingLanguage
{
    internal class Functions
    {
        private static bool setOperationWithValue;
        private static bool canExecuteNextOperation;
        private static int countOfInterations;

        static Functions()
        {
            setOperationWithValue = false;
            canExecuteNextOperation = false;
            countOfInterations = 0;
        }

        /* Open new website by inside param */
        public static void OpenUrl(object[] param, WebBrowser webBrowser)
        {
            string url = ExecuteParam(param);

            if (!url.StartsWith("http"))
                url = url.Insert(0, "http://");

            webBrowser.DocumentCompleted += webBrowser_DocumentCompleted;
            webBrowser.Navigate(new Uri(url));
            WaitOnDocumentLoad();
        }

        /* Get element by class name */
        public static void GetElementByClassName(object[] param, WebBrowser webBrowser)
        {
            string searchElement = ExecuteParam(param);

            setOperationWithValue = false;
            FindElements(webBrowser, "className", searchElement, "");
        }

        /* Set element by class name */
        public static void SetElementByClassName(object[] param, WebBrowser webBrowser)
        {
            string searchElement, value;
            ExecuteParams(param, out searchElement, out value);

            setOperationWithValue = true;
            FindElements(webBrowser, "className", searchElement, value);
        }

        /* Get element by ID */
        public static void GetElementById(object[] param, WebBrowser webBrowser)
        {
            string searchElement = ExecuteParam(param);

            setOperationWithValue = false;
            FindElement(webBrowser, "id", searchElement, "");
        }

        /* Set element by ID */
        public static void SetElementById(object[] param, WebBrowser webBrowser)
        {
            string searchElement, value;
            ExecuteParams(param, out searchElement, out value);

            setOperationWithValue = true;
            FindElement(webBrowser, "id", searchElement, value);
        }

        /* Get element by name */
        public static void GetElementByName(object[] param, WebBrowser webBrowser)
        {
            string searchElement = ExecuteParam(param);

            setOperationWithValue = false;
            FindElement(webBrowser, "name", searchElement, "");
        }

        /* Set element by name */
        public static void SetElementByName(object[] param, WebBrowser webBrowser)
        {
            string searchElement, value;
            ExecuteParams(param, out searchElement, out value);

            setOperationWithValue = true;
            FindElement(webBrowser, "name", searchElement, value);
        }

        /* Click on button */
        public static void ClickOnButton(object[] param, WebBrowser webBrowser)
        {
            string searchElement, searchMethod;
            ExecuteParams(param, out searchElement, out searchMethod);

            setOperationWithValue = false;
            FindElements(webBrowser, searchMethod, searchElement, "click");
        }

        /* Start executing new operation, if the previous operation is completed */
        private static void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            canExecuteNextOperation = true;

            var webBrowser = sender as WebBrowser;
            webBrowser.DocumentCompleted -= webBrowser_DocumentCompleted;
        }

        /* Wait a few milliseconds to start executing new operation */
        private static void WaitOnDocumentLoad()
        {
            while (!canExecuteNextOperation)
            {
                Application.DoEvents();
                countOfInterations++;
                
                if (countOfInterations > 1000000)
                    break;
            }

            countOfInterations = 0;
            canExecuteNextOperation = false;
        }
        
        /* Execute all params from current method */
        private static void ExecuteParams(object[] param, out string searchElement, out string value)
        {
            searchElement = param[0].ToString().Trim('"');
            value = "";

            if (param.Length > 1)
                value = param[1].ToString().Trim('"');
        }

        /* Execute param from current get method */
        private static string ExecuteParam(object[] param)
        {
            return param[0].ToString().Trim('"');
        }

        /* Find elements using loops */
        private static void FindElements(WebBrowser webBrowser, string attribute, string searchElement, string value)
        {
            HtmlDocument htmlDocument = webBrowser.Document;

            try
            {
                if (htmlDocument != null)
                {
                    bool elementFinded = false;

                    foreach (HtmlElement element in htmlDocument.All)
                    {
                        if (element.GetAttribute(attribute) == searchElement)
                        {
                            elementFinded = true;
                            webBrowser.DocumentCompleted += webBrowser_DocumentCompleted;

                            if (!setOperationWithValue)
                            {
                                OutputElementData(element);
                                if (value == "click")
                                    element.InvokeMember("click");
                            }
                            else
                                SetElementValue(element, value);

                            WaitOnDocumentLoad();
                        }
                    }

                    if (!elementFinded)
                    {
                        MessageBox.Show($"Element with {attribute} {searchElement} can't finded");
                    }
                }
            }
            catch (Exception e)
            {
                OutputException(attribute, e);
            }
        }

        /* Find element by attribute (id, name) */
        public static void FindElement(WebBrowser webBrowser, string attribute, string searchElement, string value)
        {
            HtmlDocument htmlDocument = webBrowser.Document;

            try
            {
                if (htmlDocument != null)
                {
                    HtmlElement element;
                    switch (attribute)
                    {
                        case "id":
                            element = htmlDocument.GetElementById(searchElement);
                            break;

                        case "name":
                            element = htmlDocument.All.GetElementsByName(searchElement)[0];
                            break;

                        default:
                            element = htmlDocument.ActiveElement;
                            break;
                    }

                    if (element != null)
                    {
                        webBrowser.DocumentCompleted += webBrowser_DocumentCompleted;

                        if (!setOperationWithValue)
                            OutputElementData(element);
                        else
                            SetElementValue(element, value);

                        WaitOnDocumentLoad();
                    }
                    else
                    {
                        MessageBox.Show($"Element {searchElement} can't finded");
                    }
                }
            }
            catch (Exception e)
            {
                OutputException(attribute, e);
            }
        }

        /* Display exception */
        private static void OutputException(string attribute, Exception e)
        {
            Console.WriteLine($"Error -> Method in block with attribute name {attribute} with message {e.Message}");
        }
        
        /* Fill element by presented value */
        private static void SetElementValue(HtmlElement element, string value)
        {
            switch (element.TagName.ToLower())
            {
                case "input":
                    element.SetAttribute("value", value);
                    break;

                case "textarea":
                case "a":
                case "div":
                    element.InnerText = value;
                    break;

                default:
                    element.InnerText = value;
                    break;
            }
        }

        /* Output element data */
        private static void OutputElementData(HtmlElement element)
        {
            Dictionary<string, bool> attributes = new Dictionary<string, bool>() {
                { "id", false },
                { "class", false },
                { "name", false },
                { "type", false },
                { "value", false },
                { "href", false },
                { "innerText", false },
                { "innerHtml", false }
            };

            switch (element.TagName.ToLower())
            {
                case "input":
                    attributes["id"] = true;
                    attributes["class"] = true;
                    attributes["name"] = true;
                    attributes["type"] = true;
                    attributes["value"] = true;
                    break;

                case "textarea":
                    attributes["id"] = true;
                    attributes["class"] = true;
                    attributes["name"] = true;
                    attributes["type"] = true;
                    attributes["innerText"] = true;
                    break;

                case "a":
                    attributes["id"] = true;
                    attributes["class"] = true;
                    attributes["name"] = true;
                    attributes["type"] = true;
                    attributes["href"] = true;
                    attributes["innerText"] = true;
                    break;

                case "div":
                default:
                    attributes["id"] = true;
                    attributes["class"] = true;
                    attributes["innerHtml"] = true;
                    break;
            }

            string outputMessage = "Element data:\n";

            foreach (var attr in attributes)
            {
                if (attr.Value)
                {
                    switch (attr.Key)
                    {
                        case "id":
                            outputMessage += (element.GetAttribute("id") != "") ? $"- ID:\t{element.GetAttribute("id")}\n" : "";
                            break;

                        case "class":
                            outputMessage += (element.GetAttribute("className") != "") ? $"- Class:\t{element.GetAttribute("className")}\n" : "";
                            break;

                        case "name":
                            outputMessage += (element.Name != "") ? $"- Name:\t{element.Name}\n" : "";
                            break;

                        case "type":
                            outputMessage += (element.GetAttribute("type") != "") ? $"- Type:\t{element.GetAttribute("type")}\n" : "";
                            break;

                        case "value":
                            outputMessage += (element.GetAttribute("value") != "") ? $"- Value:\t{element.GetAttribute("value")}\n" : "";
                            break;

                        case "href":
                            outputMessage += (element.GetAttribute("href") != "") ? $"- HREF:\t{element.GetAttribute("href")}\n" : "";
                            break;

                        case "innerText":
                            outputMessage += (element.InnerText != "") ? $"- InnerText:\t{element.InnerText}\n" : "";
                            break;

                        case "innerHtml":
                            outputMessage += (element.InnerHtml != "") ? $"- InnerHTML:\t{element.InnerHtml}\n" : "";
                            break;
                    }
                }
            }

            MessageBox.Show(outputMessage, $"{element.TagName} Tag Data");
        }
    }
}
