using System;
using System.Collections.Generic;
using FubuCore.Util;
using FubuMVC.Core.Registration.Nodes;

namespace FubuMVC.Core.Diagnostics
{
    public interface IConfigurationObserver
    {
        bool IsRecording { get; }
        void RecordCallStatus(ActionCall call, string status);
        //void RecordStatus(string status);
        IEnumerable<string> GetLog(ActionCall call);
    }

    public class NulloConfigurationObserver : IConfigurationObserver
    {
        public bool IsRecording
        {
            get { return false; }
        }

        public void RecordCallStatus(ActionCall call, string status)
        {
            // no-op
        }

        public void RecordStatus(string status)
        {
            // no-op
        }

        public IEnumerable<string> GetLog(ActionCall call)
        {
            // no-op
            yield break;
        }
    }

    public class RecordingConfigurationObserver : IConfigurationObserver
    {
        private readonly Cache<ActionCall, IList<string>> _log = new Cache<ActionCall, IList<string>>(c=>new List<string>());

        public bool IsRecording
        {
            get { return true; }
        }

        public void RecordCallStatus(ActionCall call, string description)
        {
            _log[call].Add(description);
            LastLogEntry = description;
        }
        
        public IEnumerable<string> GetLog(ActionCall call)
        {
            return _log[call];
        }

        public string LastLogEntry { get; private set; }
    }
}