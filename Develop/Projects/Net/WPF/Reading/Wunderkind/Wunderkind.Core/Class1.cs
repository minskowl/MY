using Windows.Foundation.Collections;

namespace Wunderkind.Core
{


    public sealed class Settings
    {
        private IPropertySet _set;

        public static readonly Settings Default= new Settings();

        public Settings()
        {
           _set = Windows.Storage.ApplicationData.Current.LocalSettings.Values;

        }



        public string Voice
        {
            get
            {
                return ((string)(_set["Voice"]));
            }
            set
            {
                _set["Voice"] = value;
            }
        }

        
        

        public int VoiceVolume
        {
            get
            {
                return ((int)(_set["VoiceVolume"]));
            }
            set
            {
                _set["VoiceVolume"] = value;
            }
        }

        
        
        
        public double FontSize
        {
            get
            {
                return ((double)(_set["FontSize"]));
            }
            set
            {
                _set["FontSize"] = value;
            }
        }

        
        
        
        public int WordWidthFrom
        {
            get
            {
                return ((int)(_set["WordWidthFrom"]));
            }
            set
            {
                _set["WordWidthFrom"] = value;
            }
        }

        
        

        public int WordWidthTo
        {
            get
            {
                return ((int)(_set["WordWidthTo"]));
            }
            set
            {
                _set["WordWidthTo"] = value;
            }
        }

        
        
        
        public string FontFamily
        {
            get
            {
                return ((string)(_set["FontFamily"]));
            }
            set
            {
                _set["FontFamily"] = value;
            }
        }

        
        
     
        public int VoiceRate
        {
            get
            {
                return ((int)(_set["VoiceRate"]));
            }
            set
            {
                _set["VoiceRate"] = value;
            }
        }

        
        
      
        public int SyllablesMode
        {
            get
            {
                return ((int)(_set["SyllablesMode"]));
            }
            set
            {
                _set["SyllablesMode"] = value;
            }
        }

        
        
   
        public int WordSyllablesCountFrom
        {
            get
            {
                return ((int)(_set["WordSyllablesCountFrom"]));
            }
            set
            {
                _set["WordSyllablesCountFrom"] = value;
            }
        }

        
        
        
        public int WordSyllablesCountTo
        {
            get
            {
                return ((int)(_set["WordSyllablesCountTo"]));
            }
            set
            {
                _set["WordSyllablesCountTo"] = value;
            }
        }

        
        
      
        public bool WordRepeatable
        {
            get
            {
                return ((bool)(_set["WordRepeatable"]));
            }
            set
            {
                _set["WordRepeatable"] = value;
            }
        }

        
        
        public int CountingNumberFrom
        {
            get
            {
                return ((int)(_set["CountingNumberFrom"]));
            }
            set
            {
                _set["CountingNumberFrom"] = value;
            }
        }

        
        
     
        public int CountingNumberTo
        {
            get
            {
                return ((int)(_set["CountingNumberTo"]));
            }
            set
            {
                _set["CountingNumberTo"] = value;
            }
        }

        
        
     
        public bool CountingRepeatable
        {
            get
            {
                return ((bool)(_set["CountingRepeatable"]));
            }
            set
            {
                _set["CountingRepeatable"] = value;
            }
        }

        
        
      
        public int SummationSecondNumberFrom
        {
            get
            {
                return ((int)(_set["SummationSecondNumberFrom"]));
            }
            set
            {
                _set["SummationSecondNumberFrom"] = value;
            }
        }

        
        
      
        public int SummationSecondNumberTo
        {
            get
            {
                return ((int)(_set["SummationSecondNumberTo"]));
            }
            set
            {
                _set["SummationSecondNumberTo"] = value;
            }
        }

        
        
        
        public int SummationFirstNumberFrom
        {
            get
            {
                return ((int)(_set["SummationFirstNumberFrom"]));
            }
            set
            {
                _set["SummationFirstNumberFrom"] = value;
            }
        }

        
        
        
        public int SummationFirstNumberTo
        {
            get
            {
                return ((int)(_set["SummationFirstNumberTo"]));
            }
            set
            {
                _set["SummationFirstNumberTo"] = value;
            }
        }

        
        
        
        public int SummationMode
        {
            get
            {
                return ((int)(_set["SummationMode"]));
            }
            set
            {
                _set["SummationMode"] = value;
            }
        }

        
        
        public int CompareNumberFrom
        {
            get
            {
                return ((int)(_set["CompareNumberFrom"]));
            }
            set
            {
                _set["CompareNumberFrom"] = value;
            }
        }

        
        
        
        public int CompareNumberTo
        {
            get
            {
                return ((int)(_set["CompareNumberTo"]));
            }
            set
            {
                _set["CompareNumberTo"] = value;
            }
        }

        
        
        
        public bool WordSyllablesView
        {
            get
            {
                return ((bool)(_set["WordSyllablesView"]));
            }
            set
            {
                _set["WordSyllablesView"] = value;
            }
        }

        
        
        
        public int SyllablesTypes
        {
            get
            {
                return ((int)(_set["SyllablesTypes"]));
            }
            set
            {
                _set["SyllablesTypes"] = value;
            }
        }

        
        public int SummationDigitViewMode
        {
            get
            {
                return ((int)(_set["SummationDigitViewMode"]));
            }
            set
            {
                _set["SummationDigitViewMode"] = value;
            }
        }

        
        
        public global::Reading.Core.Settings.TableSummationModelSettings TableSummationModelSettings
        {
            get
            {
                return ((global::Reading.Core.Settings.TableSummationModelSettings)(_set["TableSummationModelSettings"]));
            }
            set
            {
                _set["TableSummationModelSettings"] = value;
            }
        }

        
        
        public global::Reading.Core.Settings.FindPairSettings FindPairSettings
        {
            get
            {
                return ((global::Reading.Core.Settings.FindPairSettings)(_set["FindPairSettings"]));
            }
            set
            {
                _set["FindPairSettings"] = value;
            }
        }

        
        
    
        public bool VoiceEnabled
        {
            get
            {
                return ((bool)(_set["VoiceEnabled"]));
            }
            set
            {
                _set["VoiceEnabled"] = value;
            }
        }
    }
}
