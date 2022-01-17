using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Project4.Models.Errors
{
    public class Error
    {
        public Error()
        {
            ID = 0;
            Moment = DateTime.Now;
            Description = "";
            File = "";
            Method = "";
            Line = "";
        }

        public static object ConnectionStrings { get; private set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "ID")]
        public long ID { get; set; }

        public string Description { get; set; }

        public DateTime Moment { get; set; }

        public string File { get; set; }

        public string Method { get; set; }

        public string Line { get; set; }

        public static void Add(Exception e)
        {
            // Get stack trace for the exception with source file information
            var st = new StackTrace(e, true);
            // Get the stack frames

            string file = "";
            string fileTemp = "";
            string method = "";
            string lineNumber = "";

            foreach (StackFrame frame in st.GetFrames())
            {
                // Get the file name from the stack frame
                fileTemp = frame.GetFileName() ?? "";
                fileTemp = fileTemp.Replace('\\', '-').Split('-').Last().Trim();

                int line = frame.GetFileLineNumber();

                if (line > 0)
                {
                    file += "-> " + fileTemp + "\n";

                    // Get the method from the stack frame
                    method = "-> " + frame.GetMethod().ToString().Substring(frame.GetMethod().ToString().IndexOf(' '), frame.GetMethod().ToString().IndexOf('(') - frame.GetMethod().ToString().IndexOf(' ')) + "\n";

                    // Get the line number from the stack frame
                    lineNumber += "-> " + frame.GetFileLineNumber().ToString() + "\n";
                }
            }

            if (st.GetFrames().Count() == 0)
            {
                method = "no frames! TargetSite: " + e.TargetSite?.ToString() + " - Source: " + e.Source != null ? e.Source : "";
                lineNumber = "HelpLink: " + e.HelpLink != null ? e.HelpLink : "";
            }

            string details = "Exception type: " + e.GetType().Name + " - Message: " + e.Message;

            if (e.Data.Count > 0)
            {
                details += "\nData: ";
                foreach (DictionaryEntry de in e.Data)
                    details += "\n    Key: " + de.Key + "Value: " + de.Value;
            }

            if (e.InnerException != null)
            {
                var innerException = e;

                Exception realerror = e;
                while (realerror.InnerException != null)
                {
                    realerror = realerror.InnerException;
                    details += "\n" + realerror.Message;
                }
            }

            string QueryString = "INSERT INTO Error(Description,Moment,[File],Method,Line) VALUES (@description,@moment,@file,@method,@line)";

        }

        public static ModelStateDictionary AddErrorsToModelState(IdentityResult identityResult, ModelStateDictionary modelState)
        {
            foreach (var e in identityResult.Errors)
            {
                modelState.TryAddModelError(e.Code, e.Description);
            }

            return modelState;
        }

        public static ModelStateDictionary AddErrorToModelState(string code, string description, ModelStateDictionary modelState)
        {
            modelState.TryAddModelError(code, description);
            return modelState;
        }
    }
}
