using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HL7toXML
{
    /// This class takes an HL7 message
    /// and transforms it into an XML representation.
    public static class HL7ToXmlConverter
    {
        // This is the XML document we'll be creating
        private static XmlDocument _xmlDoc;

        //This is where we will store the encoding characters and field separator
        private static char _fieldSeparator;
        private static char _componentSeparator;
        private static char _subComponentSeparator;
        private static char _repetitionSeparator;
        private static char _escapeCharacter = '\\';

        /// <summary>
        /// Converts an HL7 message into an XML representation of the same message.
        /// </summary>
        ///
        /// <param name="sHL7">The HL7 to convert</param>
        /// <returns></returns>
        public static string ConvertToXml(string sHL7, string validationType)
        {
            try
            {
                // Go and create the base XML
                _xmlDoc = CreateXmlDoc();

            // HL7 message segments are terminated by carriage returns,
            // so to get an array of the message segments, split on carriage return
            char[] delimiter = { '\r', '\n' };
            string[] sHL7Lines = sHL7.Split(delimiter);

            // Now we want to replace any other unprintable control
            // characters with whitespace otherwise they'll break the XML
            int j = -1;
            for (int i = 0; i < sHL7Lines.Length; i++)
            {
                //sHL7Lines[i] = Regex.Replace(sHL7Lines[i], @"[^-~]", " ");
                j = sHL7Lines[i].IndexOf(@"<");
            }

            //Retrieve the field separator
            _fieldSeparator = sHL7Lines[0][3];

            //Retrieve the encoding characters
            _componentSeparator = sHL7Lines[0][4];
            _subComponentSeparator = sHL7Lines[0][7];
            _repetitionSeparator = '~';
            string encodingCharacters = GetEncodingCharacters
        (_componentSeparator, _repetitionSeparator, _escapeCharacter, _subComponentSeparator);

            /// Go through each segment in the message
            /// and first get the fields, separated by pipe (|),
            /// then for each of those, check for repetition (~)
            /// then get the field components,separated by carat (^)
            /// and also check each component for subcomponents.

            for (int i = 0; i < sHL7Lines.Length; i++)
            {
                // Don't care about empty lines
                if (sHL7Lines[i] != string.Empty)
                {
                    // Get the line and get the line's segments
                    string sHL7Line = sHL7Lines[i];

                    string[] sFields;

                    // For the first segment(MSG), handle it so that MSG-1 is a pipe(|)
                    if (i == 0)
                    {
                        sFields = HL7ToXmlConverter.GetMessageFieldsForMSH(sHL7Line);
                    }
                    else
                    {
                        sFields = HL7ToXmlConverter.GetMessgeFields(sHL7Line);
                    }

                    if ((sFields[0] == "OBX" && validationType == "Syndromic Surveillance")
                       || (sFields[0] == "OBX" && validationType == "Immunization"))
                    {
                        sFields[0] = sFields[0] + "-" + GetComponents(sFields[3])[0];
                    }

                    if (sFields[0] == "OBX" && validationType == "Lab")
                    {
                        // intentionally left blank
                    }

                    // Create a new element in the XML for the line
                    XmlElement el = _xmlDoc.CreateElement(sFields[0]);
                    _xmlDoc.DocumentElement.AppendChild(el);

                    // For each field in the line of HL7
                    for (int a = 0; a < sFields.Length; a++)
                    {
                        // Create a new element

                        /// Part of the HL7 specification is that part
                        /// of the message header defines which characters
                        /// are going to be used to delimit the message
                        /// and since we want to capture the field that
                        /// contains those characters we need
                        /// to just capture them and stick them in an element.intrane
                        if (sFields[a] != encodingCharacters)
                        {
                            string[] fieldRepetitions = GetRepetitions(sFields[a]);
                            if (fieldRepetitions.Length > 1)
                            {
                                for (int d = 0; d < fieldRepetitions.Length; d++)
                                {
                                    if (d == 0)
                                    {
                                        XmlElement fieldEl = _xmlDoc.CreateElement(sFields[0] +
                                "-" + a.ToString());
                                        string[] sComponents = HL7ToXmlConverter.GetComponents
                            (fieldRepetitions[d]);
                                        if (sComponents.Length > 1)
                                        {
                                            for (int b = 0; b < sComponents.Length; b++)
                                            {
                                                XmlElement componentEl = _xmlDoc.CreateElement
                        (sFields[0] + "-" + a.ToString() +
                                               "." + (b + 1).ToString());
                                                string[] subComponents =
                        GetSubComponents(sComponents[b]);
                                                if (subComponents.Length > 1)
                                                // There were subcomponents
                                                {
                                                    for (int c = 0; c < subComponents.Length; c++)
                                                    {
                                                        XmlElement subComponentEl =
                                                      _xmlDoc.CreateElement(sFields[0] + "-" +
                            a.ToString() + "." +
                                                      (b + 1).ToString() + "." + (c + 1).ToString());
                                                        subComponentEl.InnerText = subComponents[c];
                                                        componentEl.AppendChild(subComponentEl);
                                                    }
                                                    fieldEl.AppendChild(componentEl);
                                                }
                                                else
                                                {
                                                    componentEl.InnerText = sComponents[b];
                                                    fieldEl.AppendChild(componentEl);
                                                    el.AppendChild(fieldEl);
                                                }
                                            }
                                            el.AppendChild(fieldEl);

                                        }
                                        else
                                        {
                                            fieldEl.InnerText = fieldRepetitions[d];
                                            el.AppendChild(fieldEl);
                                        }
                                    }
                                    else
                                    {
                                        XmlElement fieldEl = _xmlDoc.CreateElement(sFields[0] +
                    "-" + a.ToString() + "_" + (d + 1).ToString());
                                        string[] sComponents = HL7ToXmlConverter.GetComponents
                        (fieldRepetitions[d]);
                                        if (sComponents.Length > 1)
                                        {
                                            for (int b = 0; b < sComponents.Length; b++)
                                            {
                                                XmlElement componentEl = _xmlDoc.CreateElement
                        (fieldEl.Name + "." + (b + 1).ToString());
                                                string[] subComponents = GetSubComponents
                                (sComponents[b]);
                                                if (subComponents.Length > 1)
                                                {
                                                    for (int c = 0; c < subComponents.Length; c++)
                                                    {
                                                        XmlElement subComponentEl =
                            _xmlDoc.CreateElement(componentEl.Name +
                            "." + (c + 1).ToString());
                                                        subComponentEl.InnerText = subComponents[c];
                                                        componentEl.AppendChild(subComponentEl);
                                                    }
                                                    fieldEl.AppendChild(componentEl);
                                                }
                                                else
                                                {
                                                    componentEl.InnerText = sComponents[b];
                                                    fieldEl.AppendChild(componentEl);
                                                    el.AppendChild(fieldEl);
                                                }
                                            }
                                            el.AppendChild(fieldEl);
                                        }
                                        else
                                        {
                                            fieldEl.InnerText = fieldRepetitions[d];
                                            el.AppendChild(fieldEl);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                XmlElement fieldEl = _xmlDoc.CreateElement(sFields[0] + "-" +
                                a.ToString());
                                string[] sComponents = HL7ToXmlConverter.GetComponents(sFields[a]);
                                if (sComponents.Length > 1)
                                {
                                    for (int b = 0; b < sComponents.Length; b++)
                                    {
                                        XmlElement componentEl = _xmlDoc.CreateElement(sFields[0] +
                                "-" + a.ToString() +
                                       "." + (b + 1).ToString());
                                        string[] subComponents = GetSubComponents(sComponents[b]);
                                        if (subComponents.Length > 1)
                                        // There were subcomponents
                                        {
                                            for (int c = 0; c < subComponents.Length; c++)
                                            {
                                                XmlElement subComponentEl =
                                              _xmlDoc.CreateElement(sFields[0] + "-" +
                                a.ToString() + "." +
                                              (b + 1).ToString() + "." + (c + 1).ToString());
                                                subComponentEl.InnerText = subComponents[c];
                                                componentEl.AppendChild(subComponentEl);
                                            }
                                            fieldEl.AppendChild(componentEl);
                                        }
                                        else
                                        {
                                            componentEl.InnerText = sComponents[b];
                                            fieldEl.AppendChild(componentEl);
                                            el.AppendChild(fieldEl);
                                        }
                                    }
                                    el.AppendChild(fieldEl);
                                }
                                else
                                {
                                    fieldEl.InnerText = sFields[a];
                                    el.AppendChild(fieldEl);
                                }
                            }
                        }
                        else
                        {
                            XmlElement fieldEl = _xmlDoc.CreateElement(sFields[0] + "-" + a.ToString());
                            fieldEl.InnerText = sFields[a];
                            el.AppendChild(fieldEl);
                        }
                    }
                }
            }

            return _xmlDoc.OuterXml;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return ex.Message.ToString();
            }
        }

        /// <summary>
        /// Gets the message fields with proper field number for MSH.
        /// </summary>
        /// <param name="MSH Segment">MSH segment string</param>
        /// <returns>Split MSH Segment</returns>
        private static string[] GetMessageFieldsForMSH(string s)
        {
            string[] sFieldsPlaceHolder = HL7ToXmlConverter.GetMessgeFields(s);
            string[] sFieldsForMSG = new string[sFieldsPlaceHolder.Length + 1];

            sFieldsForMSG[0] = sFieldsPlaceHolder[0];
            sFieldsForMSG[1] = "|";

            for (int x = 2; x < sFieldsForMSG.Length; x++)
            {
                sFieldsForMSG[x] = sFieldsPlaceHolder[x - 1];
            }

            return sFieldsForMSG;
        }
        /// <summary>
        /// Gets the encoding characters.
        /// </summary>
        /// <param name="componentSeparator">The component separator.</param>
        /// <param name="repetitionSeparator">The repetition separator.</param>
        /// <param name="escapeCharacter">The escape character.</param>
        /// <param name="subComponentSeparator">The sub component separator.</param>
        /// <returns>Encoding String</returns>
        public static string GetEncodingCharacters(char componentSeparator,
        char repetitionSeparator, char escapeCharacter, char subComponentSeparator)
        {
            // Combine chars into array
            char[] arr = new char[4];
            arr[0] = componentSeparator;
            arr[1] = repetitionSeparator;
            arr[2] = escapeCharacter;
            arr[3] = subComponentSeparator;
            // Return new string key
            return new string(arr);
        }

        /// <summary>
        /// Split a line into its component parts based on pipe.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static string[] GetMessgeFields(string s)
        {
            return s.Split(_fieldSeparator);
        }

        /// <summary>
        /// Get the components of a string by splitting based on carat.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static string[] GetComponents(string s)
        {
            return s.Split(_componentSeparator);
        }

        /// <summary>
        /// Get the subcomponents of a string by splitting on ampersand.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static string[] GetSubComponents(string s)
        {
            return s.Split(_subComponentSeparator);
        }

        /// <summary>
        /// Get the repetitions within a string based on tilde.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static string[] GetRepetitions(string s)
        {
            return s.Split(_repetitionSeparator);
        }

        /// <summary>
        /// Create the basic XML document that represents the HL7 message
        /// </summary>
        /// <returns></returns>
        private static XmlDocument CreateXmlDoc()
        {
            XmlDocument output = new XmlDocument();
            XmlElement rootNode = output.CreateElement("HL7Message");
            output.AppendChild(rootNode);
            return output;
        }

    }
}