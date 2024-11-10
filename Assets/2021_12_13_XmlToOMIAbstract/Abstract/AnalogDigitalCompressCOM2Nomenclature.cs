using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace OMIAbstraction
{

    public interface IAnalogDigitalCompressCOM2Nomenclature
    {
        void GetPortObserved(out string comIdName);
        void GetAnalogDigitalPatternIdName(out string idName);
    }
    [System.Serializable]
    public class AnalogDigitalCompressCOM2Nomenclature : IAnalogDigitalCompressCOM2Nomenclature
    {
        //Example: COM20
        public string m_serialPortComId;

        //Example: FootBoard V2
        public string m_maskPatternIdName;

        public void GetAnalogDigitalPatternIdName(out string idName)
        {
            idName = m_maskPatternIdName ;
        }

        public void GetPortObserved(out string comIdName)
        {
            comIdName= m_serialPortComId  ;
        }
    }
}