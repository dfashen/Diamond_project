﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integration;

namespace CZ
{
    class ConvertToDiamond
    {
        public static List<Diamond.Core.Models.LegalStatusEvent> Sub6(List<OutElements.Sub6> elementOuts)
        {
            /*list of record for whole gazette chapter*/
            List<Diamond.Core.Models.LegalStatusEvent> fullGazetteInfo = new List<Diamond.Core.Models.LegalStatusEvent>();
            string dateFromName = Methods.GetDateFromGazette(CZ_main.CurrentFileName);
            if (elementOuts != null)
            {
                int leCounter = 1;
                /*Create a new event to fill*/
                foreach (var record in elementOuts)
                {
                    Diamond.Core.Models.LegalStatusEvent legalEvent = new Diamond.Core.Models.LegalStatusEvent();
                    legalEvent.GazetteName = Path.GetFileName(CZ_main.CurrentFileName.Replace(".tetml", ".pdf"));
                    /*Setting subcode*/
                    legalEvent.SubCode = "6";
                    /*Setting Section Code*/
                    legalEvent.SectionCode = "MM1A";
                    /*Setting Country Code*/
                    legalEvent.CountryCode = "CZ";
                    /*Setting File Name*/
                    legalEvent.Id = leCounter++; // creating uniq identifier
                    Biblio biblioData = new Biblio();
                    biblioData.EuropeanPatents = new List<EuropeanPatent>
                    {
                        new EuropeanPatent { PubNumber = record.PubNumber }
                    };
                    legalEvent.LegalEvent = new LegalEvent { Number = record.PubNumber, Date = dateFromName.Replace("-", "/") };
                    legalEvent.Biblio = biblioData;
                    fullGazetteInfo.Add(legalEvent);
                }
            }
            return fullGazetteInfo;
        }
    }
}
