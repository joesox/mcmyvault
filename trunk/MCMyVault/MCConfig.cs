using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.IO;

namespace MCMyVault
{
    class MCConfig
    {
        public List<List<String>> Sections
        {
            get { return _Sections; }
            set { _Sections = value; }
        }
        List<List<String>> _Sections = new List<List<string>>();

        public FileInfo Fileinfo = null;
        public MCConfig(string fullname)
        {
            Fileinfo = new FileInfo(fullname);
            List<List<String>> Sections = new List<List<string>>();
            this.Load(Fileinfo.FullName);
        }

        /// <summary>
        /// Create the sections from the .cfg
        /// </summary>
        /// <param name="fullname"></param>
        private void Load(string fullname)
        {
            //make sure only .cfg
            if (fullname.EndsWith(".cfg"))
            {
                //read each line one-by-one
                    //skip if it is a comment or blank line
                int line_number = 0;
                int section_line_number_closing = -1;
                List<String> SECTION = new List<string>();
                List<String> GLOBALSECTION = new List<string>();
                foreach (string line in File.ReadAllLines(fullname))
                {
                    line_number = line_number + 1;//we are examining this line number right now
                    if (line.Trim().StartsWith("#") || line.Trim().StartsWith("//") || String.IsNullOrWhiteSpace(line))
                    {
                    }
                    else
                    {
                        //examine more
                        //look for opening '{'
                        if (line.Contains("{"))
                        {
                            //this is probably the start of a section; 
                            section_line_number_closing = 0;//We are in a section; note it!
                            SECTION.Add(line);
                        }
                        else
                        {
                            //well, we are not sure what this line is but take note...
                            
                            //if we are in a section, keep reading the 
                            if (section_line_number_closing == 0)
                            {
                                //let's see if it is a closing
                                if (line.Contains("}"))
                                {
                                    section_line_number_closing = -1;
                                    //Now to add this section to 
                                    SECTION.Add(line);
                                    Sections.Add(new List<String>(SECTION.ToArray()));
                                    SECTION.Clear();
                                }
                                else
                                {
                                    //we are still in the section, so add it
                                    SECTION.Add(line);
                                }
                            }
                            else
                                GLOBALSECTION.Add(line);//global I guess
                        }
                    }
                }
                Sections.Add(new List<String>(GLOBALSECTION.ToArray()));
            }
            else
                Console.WriteLine("MCConfig: not a .cfg file: " + fullname);
        }

        public static string PPrint(List<String> section)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string line in section)
            {
                sb.AppendLine(line);
            }
            return sb.ToString();
        }
    }
}
