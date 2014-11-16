using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    public class GameSettings
    {
        private readonly bool mandatoryCaptures = true;
        private readonly bool longestCaptureSequence = true;
        private readonly bool captureBackwards = true;

        public bool MandatoryCaptures { get { return mandatoryCaptures; } }
        public bool LongestCaptureSequence { get { return longestCaptureSequence; } }
        public bool CaptureBackwards { get { return captureBackwards; } }

        public GameSettings() { }

        public GameSettings(bool mandatoryCaptures,
                            bool longestCaptureSequence,
                            bool captureBackwards)
        {
            if (!mandatoryCaptures && longestCaptureSequence)
                throw new ArgumentException("Longest capture sequence cannot be required when captures are optional", "longestCaptureSequence");

            this.mandatoryCaptures = mandatoryCaptures;
            this.longestCaptureSequence = longestCaptureSequence;
            this.captureBackwards = captureBackwards;
        }
    }
}
