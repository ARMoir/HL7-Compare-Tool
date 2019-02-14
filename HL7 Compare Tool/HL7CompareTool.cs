using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Microsoft.XmlDiffPatch;
using HL7toXML;

namespace HL7_Compare_Tool
{
    public partial class HL7CompareTool : Form
    {
      
        public string filename1 = string.Empty;
        public string filename2 = string.Empty;
        string diffFile = null;

        //The main class which is used to compare two files.
        XmlDiff diff = new XmlDiff();
        XmlDiffOptions diffOptions = new XmlDiffOptions();
        bool compareFragments = false;

        public System.Windows.Forms.MenuItem icoOpt;
     
        public HL7CompareTool()
        {
            InitializeComponent();

            UserTXT.Text = new System.Security.Principal.WindowsPrincipal(System.Security.Principal.WindowsIdentity.GetCurrent()).Identity.Name;

        }

        private void LoadOriginal_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenOriginal = new OpenFileDialog();
            OpenOriginal.Filter = "txt files (*.txt)|*.txt|HL7 files (*.hl7)|*.hl7";
            OpenOriginal.ShowDialog();
            OriginalPath.Text = OpenOriginal.FileName;
        }

        private void LoadCompare_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenCompare = new OpenFileDialog();
            OpenCompare.Filter = "txt files (*.txt)|*.txt|HL7 files (*.hl7)|*.hl7";
            OpenCompare.ShowDialog();
            ComparePath.Text = OpenCompare.FileName;
        }

        private void Report_Click(object sender, EventArgs e)
        {
            try
            {
                // Set Progress Bar
                MainProBar.Visible = true;
                MainProBar.Minimum = 1;
                MainProBar.Maximum = 18;
                MainProBar.Value = 1;
                MainProBar.Step = 1;
                MainProBar.PerformStep();

                //Check if file 1 is safe and valid.
                if (OriginalPath.Text == null || OriginalPath.Text == string.Empty)
                {
                    MessageBox.Show("File 1 not selected, please select");
                    MainProBar.Value = 1;
                    return;
                }
                MainProBar.PerformStep();

                if (!File.Exists(OriginalPath.Text))
                {
                    MessageBox.Show("File 1 doesnt exist, please select another file");
                    MainProBar.Value = 1;
                    return;
                }
                MainProBar.PerformStep();

                //Check if file 2 is safe and valid.
                if (ComparePath.Text == null || ComparePath.Text == string.Empty)
                {
                    MessageBox.Show("File 2 not selected, please select");
                    MainProBar.Value = 1;
                    return;
                }
                MainProBar.PerformStep();

                if (!File.Exists(ComparePath.Text))
                {
                    MessageBox.Show("File 2 doesnt exist, please select another file");
                    MainProBar.Value = 1;
                    return;
                }
                MainProBar.PerformStep();

                //Call DoCompare which will compare two files.
                MainProBar.PerformStep();
                DoCompare(OriginalPath.Text, ComparePath.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                MainProBar.Value = 1;
            }
        }
        public void DoCompare(string file1, string file2)
        {
            

            try
            {
                MainProBar.Visible = true;
                MainProBar.Minimum = 1;
                MainProBar.Maximum = 18;
                MainProBar.Value = 6;
                MainProBar.Step = 1;
                MainProBar.PerformStep();

                //Convert HL7 to XML
                System.IO.StreamReader InputFile = new System.IO.StreamReader(file1);
                string InputHL7 = InputFile.ReadToEnd();
                InputFile.Close();
                InputHL7 = InputHL7.Replace((Char)127,(Char)13);

                String InputHL7asXml = HL7ToXmlConverter.ConvertToXml(InputHL7, "");
                string myTempFile1 = Path.Combine(Path.GetTempPath(), "Input.xml");
                System.IO.StreamWriter fileI = new System.IO.StreamWriter(myTempFile1);
                fileI.WriteLine(InputHL7asXml);
                fileI.Close();
                file1 = myTempFile1;

                MainProBar.PerformStep();

                System.IO.StreamReader OutputFile = new System.IO.StreamReader(file2);
                string OutputHL7 = OutputFile.ReadToEnd();
                OutputFile.Close();
                OutputHL7 = OutputHL7.Replace((Char)127, (Char)13);

                String OutputHL7asXml = HL7ToXmlConverter.ConvertToXml(OutputHL7, "");
                string myTempFile2 = Path.Combine(Path.GetTempPath(), "Output.xml");
                System.IO.StreamWriter fileO = new System.IO.StreamWriter(myTempFile2);
                fileO.WriteLine(OutputHL7asXml);
                fileO.Close();
                file2 = myTempFile2;

                MainProBar.PerformStep();

                string startupPath = Application.StartupPath;
                //output diff file.
                
                diffFile = startupPath + Path.DirectorySeparatorChar + "vxd.out";
                XmlTextWriter tw = new XmlTextWriter(new StreamWriter(diffFile));
                tw.Formatting = Formatting.Indented;

                MainProBar.PerformStep();

                //This method sets the diff.Options property.
                SetDiffOptions();

                bool isEqual = false;

                //Now compare the two files.
                try
                {
                    isEqual = diff.Compare(file1, file2, compareFragments, tw);
                }
                catch (XmlException xe)
                {
                    xe.StackTrace.ToString();
                    //MessageBox.Show("An exception occured while comparing\n" + xe.StackTrace);
                }
                finally
                {
                    tw.Close();
                }

                MainProBar.PerformStep();

                if (isEqual)
                {
                    //This means the files were identical for given options.
                    MessageBox.Show("Files Identical for the given options");
                    MainProBar.Value = 1;
                    return; //dont need to show the differences.
                }

                MainProBar.PerformStep();
                //Files were not equal, so construct XmlDiffView.
                XmlDiffView dv = new XmlDiffView();

                //Load the original file again and the diff file.
                XmlTextReader orig = new XmlTextReader(file1);
                XmlTextReader diffGram = new XmlTextReader(diffFile);
                dv.Load(orig,
                diffGram);

                MainProBar.PerformStep();

                //Wrap the HTML file with necessary html and 
                //body tags and prepare it before passing it to the GetHtml method.

                string tempFile = startupPath + Path.DirectorySeparatorChar + "Compare_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".htm";
                StreamWriter sw1 = new StreamWriter(tempFile);

                MainProBar.PerformStep();

                sw1.Write("<html><body><table width='100%'>");
                //Write Legend.
                sw1.Write("<tr><td colspan='2' align='center'><b>Legend:</b> <font style='background-color: yellow'" +
                " color='black'>added</font>&nbsp;&nbsp;<font style='background-color: red'" +
                " color='black'>removed</font>&nbsp;&nbsp;<font style='background-color: " +
                "lightgreen' color='black'>changed</font>&nbsp;&nbsp;" +
                "<font style='background-color: red' color='blue'>moved from</font>" +
                "&nbsp;&nbsp;<font style='background-color: yellow' color='blue'>moved to" +
                "</font>&nbsp;&nbsp;<font style='background-color: white' color='#AAAAAA'>" +
                "ignored</font></td></tr>");


                sw1.Write("<tr><td><b> File Name : ");
                sw1.Write(OriginalPath.Text);
                sw1.Write("</b></td><td><b> File Name : ");
                sw1.Write(ComparePath.Text);
                sw1.Write("</b></td></tr>");

                MainProBar.PerformStep();
                //This gets the differences but just has the 
                //rows and columns of an HTML table
                dv.GetHtml(sw1);

                MainProBar.PerformStep();
                //Finish wrapping up the generated HTML and complete the file.
                sw1.Write("</table></body></html>");

                //Open the IE Control window and pass it the HTML file we created.
                ReportWindow.Navigate(new Uri(tempFile));
                MainProBar.PerformStep();

                //HouseKeeping...close everything we dont want to lock.
                dv = null;
                file1 = null;
                file2 = null;
                sw1.Close();
                orig.Close();
                diffGram.Close();
                orig.Dispose();
                diffGram.Dispose();
                tw.Dispose();
                sw1.Dispose();
                fileI.Dispose();
                fileO.Dispose();
                File.Delete(diffFile);
                GC.Collect();
                MainProBar.PerformStep();
                MainProBar.PerformStep();
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message.ToString());
                MainProBar.Value = 1;
            }
        }

        private void SetDiffOptions()
        {
            //Reset to None and refresh the options from the menuoptions
            //else eventually all options may get set and the menu changes will
            // not be reflected.
            diffOptions = XmlDiffOptions.None;


            //Read the options settings and OR the XmlDiffOptions enumeration.
           
                diffOptions = diffOptions | XmlDiffOptions.IgnorePI;
                diffOptions = diffOptions | XmlDiffOptions.IgnoreChildOrder;
                diffOptions = diffOptions | XmlDiffOptions.IgnoreComments;
                diffOptions = diffOptions | XmlDiffOptions.IgnoreDtd;
                diffOptions = diffOptions | XmlDiffOptions.IgnoreNamespaces;
                diffOptions = diffOptions | XmlDiffOptions.IgnorePrefixes;
                diffOptions = diffOptions | XmlDiffOptions.IgnoreWhitespace;
                diffOptions = diffOptions | XmlDiffOptions.IgnoreXmlDecl;

                compareFragments = true;

            //Default algorithm is Auto.
            diff.Algorithm = XmlDiffAlgorithm.Auto;

           // if (algFast.Checked)
               // diff.Algorithm = XmlDiffAlgorithm.Fast;

            //if (algPrecise.Checked)
               // diff.Algorithm = XmlDiffAlgorithm.Precise;

            diff.Options = diffOptions;
        }

        private void Export_Click(object sender, EventArgs e)
        {
            try
            {
                MainProBar.Visible = true;
                MainProBar.Minimum = 1;
                MainProBar.Maximum = 8;
                MainProBar.Value = 1;
                MainProBar.Step = 1;
                MainProBar.PerformStep();

                SaveFileDialog sfd = new SaveFileDialog();
                MainProBar.PerformStep();

                sfd.Filter = "HTML files (*.htm)|*.htm";
                MainProBar.PerformStep();

                sfd.FileName = "Compare_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".htm";
                MainProBar.PerformStep();

                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    MainProBar.PerformStep();
                    // If they've selected a save location...
                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(sfd.FileName, false))
                    {
                        // Write the stringbuilder text to the the file.
                        sw.WriteLine(ReportWindow.DocumentText.ToString());
                        MainProBar.PerformStep();
                    }
                    MainProBar.PerformStep();
                    MainProBar.PerformStep();
                }
                else
                {
                    MainProBar.Value = 1;
                } 
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
                MainProBar.Value = 1;
            }
        }

    }
}
