﻿using System.Management.Automation;
using PowerForensics.Artifacts.Persistence;

namespace PowerForensics.Cmdlets
{
    #region GetForensicRunKeyCommand

    /// <summary> 
    /// This class implements the Get-ForensicRunKey cmdlet. 
    /// </summary> 
    [Cmdlet(VerbsCommon.Get, "ForensicRunKey")]
    public class GetForensicRunKeyCommand : PSCmdlet
    {
        #region Parameters

        /// <summary> 
        /// 
        /// </summary> 
        [Parameter(Position = 0, ParameterSetName = "ByVolume")]
        public string VolumeName
        {
            get { return volume; }
            set { volume = value; }
        }
        private string volume;

        /// <summary> 
        /// This parameter provides the the path of the Registry Hive to parse.
        /// </summary> 
        [Alias("Path")]
        [Parameter(Mandatory = true, ParameterSetName = "ByPath")]
        public string HivePath
        {
            get { return hivePath; }
            set { hivePath = value; }
        }
        private string hivePath;

        #endregion Parameters

        #region Cmdlet Overrides

        /// <summary> 
        /// 
        /// </summary> 
        protected override void BeginProcessing()
        {
            Util.checkAdmin();

            if (ParameterSetName == "ByVolume")
            {
                Util.getVolumeName(ref volume);
            }
        }

        /// <summary> 
        /// 
        /// </summary>  
        protected override void ProcessRecord()
        {
            switch (ParameterSetName)
            {
                case "ByVolume":
                    WriteObject(RunKey.GetInstances(volume), true);
                    break;
                case "ByPath":
                    WriteObject(RunKey.Get(hivePath), true);
                    break;
            }
        }

        #endregion Cmdlet Overrides
    }

    #endregion GetForensicRunKeyCommand
}
