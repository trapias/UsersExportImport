#region Copyright

// 
// Copyright (c) _YEAR_
// by _OWNER_
// 

#endregion

#region Using Statements

using System;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Instrumentation;

#endregion

namespace forDNN.Modules.UsersExportImport
{

    public partial class Settings : ModuleSettingsBase
    {
        protected global::System.Web.UI.WebControls.TextBox txtSeparator;

        #region Base Method Implementations

        public override void LoadSettings()
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    //LoggerSource.Instance.GetLogger(this.GetType()).Debug("SEP1 ModuleSettings =" + (string)ModuleSettings["Separator"]);
                    //LoggerSource.Instance.GetLogger(this.GetType()).Debug("SEP2 ModuleSettings =" + (string)Settings["Separator"]);

                    if ((string)ModuleSettings["Separator"] != null && (string)ModuleSettings["Separator"] != "")
                        txtSeparator.Text = (string)ModuleSettings["Separator"];
                    else
                        txtSeparator.Text = ","; // default separator
                }
            }
            catch (Exception exc) // Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        public override void UpdateSettings()
        {
            try
            {
                //LoggerSource.Instance.GetLogger(this.GetType()).Debug("SET SEP = " + txtSeparator.Text);
                ModuleController mc = new ModuleController();
                mc.UpdateModuleSetting(ModuleId, "Separator", txtSeparator.Text);
                ModuleController.SynchronizeModule(ModuleId);
            }
            catch (Exception exc) // Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        #endregion

    }

}


