﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integration;

namespace Diamond_AP
{
    class ConvertToDiamond
    {
        /*Applications Filed*/
        public static List<Diamond.Core.Models.LegalStatusEvent> AppFiledConvertation(List<ProcessAppFiled.ElementOut> elementOuts)
        {
            /*list of record for whole gazette chapter*/
            List<Diamond.Core.Models.LegalStatusEvent> fullGazetteInfo = new List<Diamond.Core.Models.LegalStatusEvent>();
            if (elementOuts != null)
            {
                int leCounter = 1;
                /*Create a new event to fill*/
                foreach (var record in elementOuts)
                {
                    Diamond.Core.Models.LegalStatusEvent legalEvent = new Diamond.Core.Models.LegalStatusEvent();

                    legalEvent.GazetteName = Path.GetFileName(Diamond_AP_main.CurrentFileName.Replace(".tetml", ".pdf"));
                    /*Setting subcode*/
                    legalEvent.SubCode = "1";
                    /*Setting Section Code*/
                    legalEvent.SectionCode = "AF";
                    /*Setting Country Code*/
                    legalEvent.CountryCode = "AP";
                    /*Setting File Name*/
                    legalEvent.Id = leCounter++; // creating uniq identifier
                    Biblio biblioData = new Biblio();
                    /*Elements output*/
                    biblioData.Application.Number = record.I21;
                    biblioData.Application.Date = record.I22;
                    //biblioData.Application.Date = DateTime.Parse(record.I23); //К какому полю отнести это значение?
                    /*30*/
                    if (record.I31 != null && record.I31.Count() == record.I32.Count() && record.I31.Count() == record.I33.Count())
                    {
                        biblioData.Priorities = new List<Priority>();
                        for (int i = 0; i < record.I31.Count(); i++)
                        {
                            Priority priority = new Priority();
                            priority.Country = record.I33[i];
                            priority.Date = record.I32[i];
                            priority.Number = record.I31[i];
                            priority.Sequence = i;
                            biblioData.Priorities.Add(priority);
                        }
                    }
                    /*---------------------*/
                    /*51 classification*/
                    if (record.I51V != null && record.I51Y != null && record.I51V.Count() == record.I51Y.Count())
                    {
                        biblioData.Ipcs = new List<Ipc>();
                        for (int i = 0; i < record.I51V.Count(); i++)
                        {
                            Ipc ipc = new Ipc();
                            ipc.Class = record.I51V[i];
                            ipc.Date = record.I51Y[i];
                            biblioData.Ipcs.Add(ipc);
                        }
                    }
                    /*-----------------*/
                    /*54 Title*/
                    Title title = new Title()
                    {
                        Language = "EN",
                        Text = record.I54
                    };
                    biblioData.Titles.Add(title);
                    /*--------*/
                    /*71 name, address, country code*/
                    if (record.I71 != null)
                    {
                        biblioData.Applicants = new List<PartyMember>();
                        for (int i = 0; i < record.I71.Count(); i++)
                        {
                            PartyMember applicants = new PartyMember()
                            {
                                Name = record.I71[i]
                            };
                            biblioData.Applicants.Add(applicants);
                        }
                    }
                    /*--------------*/
                    /*72 name, country code*/
                    if (record.I72 != null)
                    {
                        biblioData.Inventors = new List<PartyMember>();
                        for (int i = 0; i < record.I72.Count(); i++)
                        {
                            PartyMember inventor = new PartyMember()
                            {
                                Name = record.I72[i]
                            };
                            biblioData.Inventors.Add(inventor);
                        }
                    }
                    /*---------------------*/
                    /*74 name, address, cc*/
                    biblioData.Agents = new List<PartyMember>();
                    PartyMember agent = new PartyMember()
                    {
                        Name = record.I74
                    };
                    biblioData.Agents.Add(agent);
                    /*--------------------*/
                    /*75*/
                    if (record.I75 != null)
                    {
                        for (int i = 0; i < record.I75.Count(); i++)
                        {
                            biblioData.InvOrApps = new List<PartyMember>();
                            PartyMember member = new PartyMember()
                            {
                                Name = record.I75[i]
                            };
                            biblioData.InvOrApps.Add(member);
                        }
                    }
                    /*--------------------*/
                    /*84*/
                    IntConvention intConvention = new IntConvention();
                    if (record.I84 != null/* || record.I86N !=null*/)
                    {
                        intConvention.DesignatedStates = record.I84.ToList();
                    }
                    /*--------------------*/
                    /*86*/
                    if (record.I86D != null && record.I86N != null)
                    {
                        intConvention.PctApplNumber = record.I86N;
                        intConvention.PctApplDate = record.I86D;
                    }
                    /*--------------------*/
                    if (intConvention != null)
                    {
                        biblioData.IntConvention = intConvention;
                    }
                    /*--------------------*/
                    /*96/97 number and date*/
                    biblioData.EuropeanPatents = new List<EuropeanPatent>();
                    EuropeanPatent euPatent = new EuropeanPatent()
                    {
                        AppNumber = record.I96N,
                        AppDate = record.I96D,
                    };
                    biblioData.EuropeanPatents.Add(euPatent);
                    /*-------------------**/
                    legalEvent.Biblio = biblioData;
                    fullGazetteInfo.Add(legalEvent);
                }
            }
            return fullGazetteInfo;
        }
        /*Applications Pending Grant*/
        public static List<Diamond.Core.Models.LegalStatusEvent> AppPendingGrantConvertation(List<ProcessAppPendingGrant.ElementOut> elementOuts)
        {
            /*list of record for whole gazette chapter*/
            List<Diamond.Core.Models.LegalStatusEvent> fullGazetteInfo = new List<Diamond.Core.Models.LegalStatusEvent>();
            if (elementOuts != null)
            {
                int leCounter = 1;
                /*Create a new event to fill*/
                foreach (var record in elementOuts)
                {
                    Diamond.Core.Models.LegalStatusEvent legalEvent = new Diamond.Core.Models.LegalStatusEvent();

                    legalEvent.GazetteName = Path.GetFileName(Diamond_AP_main.CurrentFileName.Replace(".tetml", ".pdf"));
                    /*Setting subcode*/
                    legalEvent.SubCode = "2";
                    /*Setting Section Code*/
                    legalEvent.SectionCode = "AZ";
                    /*Setting Country Code*/
                    legalEvent.CountryCode = "AP";
                    /*Setting File Name*/
                    legalEvent.Id = leCounter++; // creating uniq identifier
                    Biblio biblioData = new Biblio();
                    /*Elements output*/
                    biblioData.Application.Number = record.I21;
                    biblioData.Application.Date = record.I22;
                    //biblioData.Application.Date = DateTime.Parse(record.I23); //К какому полю отнести это значение?
                    /*51 classification*/
                    if (record.I51V != null && record.I51Y != null && record.I51V.Count() == record.I51Y.Count())
                    {
                        biblioData.Ipcs = new List<Ipc>();
                        for (int i = 0; i < record.I51V.Count(); i++)
                        {
                            Ipc ipc = new Ipc();
                            ipc.Class = record.I51V[i];
                            ipc.Date = record.I51Y[i];
                            biblioData.Ipcs.Add(ipc);
                        }
                    }
                    /*-----------------*/
                    /*54 Title*/
                    Title title = new Title()
                    {
                        Language = "EN",
                        Text = record.I54
                    };
                    biblioData.Titles.Add(title);
                    /*--------*/
                    /*74 name, address, cc*/
                    biblioData.Agents = new List<PartyMember>();
                    PartyMember agent = new PartyMember()
                    {
                        Name = record.I74
                    };
                    biblioData.Agents.Add(agent);
                    /*--------------------*/
                    /*75*/
                    if (record.I75 != null)
                    {
                        for (int i = 0; i < record.I75.Count(); i++)
                        {
                            biblioData.InvOrApps = new List<PartyMember>();
                            PartyMember member = new PartyMember()
                            {
                                Name = record.I75[i]
                            };
                            biblioData.InvOrApps.Add(member);
                        }
                    }
                    /*--------------------*/
                    /*84*/
                    IntConvention intConvention = new IntConvention();
                    if (record.I84 != null/* || record.I86N !=null*/)
                    {
                        intConvention.DesignatedStates = record.I84.ToList();
                    }
                    /*--------------------*/
                    if (intConvention != null)
                    {
                        biblioData.IntConvention = intConvention;
                    }
                    /*-------------------**/
                    legalEvent.Biblio = biblioData;
                    fullGazetteInfo.Add(legalEvent);
                }
            }
            return fullGazetteInfo;
        }
        /*Utility Model Applications Filed*/
        public static List<Diamond.Core.Models.LegalStatusEvent> UmAppFiledConvertation(List<ProcessUtilityModelAppFiled.ElementOut> elementOuts)
        {
            /*list of record for whole gazette chapter*/
            List<Diamond.Core.Models.LegalStatusEvent> fullGazetteInfo = new List<Diamond.Core.Models.LegalStatusEvent>();
            if (elementOuts != null)
            {
                int leCounter = 1;
                /*Create a new event to fill*/
                foreach (var record in elementOuts)
                {
                    Diamond.Core.Models.LegalStatusEvent legalEvent = new Diamond.Core.Models.LegalStatusEvent();

                    legalEvent.GazetteName = Path.GetFileName(Diamond_AP_main.CurrentFileName.Replace(".tetml", ".pdf"));
                    /*Setting subcode*/
                    legalEvent.SubCode = "4";
                    /*Setting Section Code*/
                    legalEvent.SectionCode = "AZ";
                    /*Setting Country Code*/
                    legalEvent.CountryCode = "AP";
                    /*Setting File Name*/
                    legalEvent.Id = leCounter++; // creating uniq identifier
                    Biblio biblioData = new Biblio();
                    /*Elements output*/
                    biblioData.Application.Number = record.I21;
                    biblioData.Application.Date = record.I22;
                    //biblioData.Application.Date = DateTime.Parse(record.I23); //К какому полю отнести это значение?
                    /*51 classification*/
                    if (record.I51V != null && record.I51Y != null && record.I51V.Count() == record.I51Y.Count())
                    {
                        biblioData.Ipcs = new List<Ipc>();
                        for (int i = 0; i < record.I51V.Count(); i++)
                        {
                            Ipc ipc = new Ipc();
                            ipc.Class = record.I51V[i];
                            ipc.Date = record.I51Y[i];
                            biblioData.Ipcs.Add(ipc);
                        }
                    }
                    /*-----------------*/
                    /*54 Title*/
                    Title title = new Title()
                    {
                        Language = "EN",
                        Text = record.I54
                    };
                    biblioData.Titles.Add(title);
                    /*--------*/
                    /*71 name, address, country code*/
                    biblioData.Applicants = new List<PartyMember>();
                    PartyMember applicants = new PartyMember()
                    {
                        Name = record.I71
                    };
                    biblioData.Applicants.Add(applicants);
                    /*--------------*/
                    /*72 name, country code*/
                    if (record.I72 != null)
                    {
                        biblioData.Inventors = new List<PartyMember>();
                        for (int i = 0; i < record.I72.Count(); i++)
                        {
                            PartyMember inventor = new PartyMember()
                            {
                                Name = record.I72[i]
                            };
                            biblioData.Inventors.Add(inventor);
                        }
                    }
                    /*---------------------*/
                    /*74 name, address, cc*/
                    biblioData.Agents = new List<PartyMember>();
                    PartyMember agent = new PartyMember()
                    {
                        Name = record.I74
                    };
                    biblioData.Agents.Add(agent);
                    /*--------------------*/
                    /*75*/
                    if (record.I75 != null)
                    {
                        for (int i = 0; i < record.I75.Count(); i++)
                        {
                            biblioData.InvOrApps = new List<PartyMember>();
                            PartyMember member = new PartyMember()
                            {
                                Name = record.I75[i]
                            };
                            biblioData.InvOrApps.Add(member);
                        }
                    }
                    /*--------------------*/
                    /*84*/
                    IntConvention intConvention = new IntConvention();
                    if (record.I84 != null/* || record.I86N !=null*/)
                    {
                        intConvention.DesignatedStates = record.I84.ToList();
                    }
                    /*--------------------*/
                    if (intConvention != null)
                    {
                        biblioData.IntConvention = intConvention;
                    }
                    /*--------------------*/
                    /*96/97 number and date*/
                    biblioData.EuropeanPatents = new List<EuropeanPatent>();
                    EuropeanPatent euPatent = new EuropeanPatent()
                    {
                        AppNumber = record.I96N,
                        AppDate = record.I96D
                    };
                    biblioData.EuropeanPatents.Add(euPatent);
                    /*-------------------**/
                    legalEvent.Biblio = biblioData;
                    fullGazetteInfo.Add(legalEvent);
                }
            }
            return fullGazetteInfo;
        }
        /*Patent Assigned*/
        public static List<Diamond.Core.Models.LegalStatusEvent> PatentAssignedConvertation(List<ProcessPatentsAssigned.ElementOut> elementOuts)
        {
            /*list of record for whole gazette chapter*/
            List<Diamond.Core.Models.LegalStatusEvent> fullGazetteInfo = new List<Diamond.Core.Models.LegalStatusEvent>();
            if (elementOuts != null)
            {
                int leCounter = 1;
                /*Create a new event to fill*/
                foreach (var record in elementOuts)
                {
                    Diamond.Core.Models.LegalStatusEvent legalEvent = new Diamond.Core.Models.LegalStatusEvent();

                    legalEvent.GazetteName = Path.GetFileName(Diamond_AP_main.CurrentFileName.Replace(".tetml", ".pdf"));
                    /*Setting subcode*/
                    legalEvent.SubCode = "14";
                    /*Setting Section Code*/
                    legalEvent.SectionCode = "BZ";
                    /*Setting Country Code*/
                    legalEvent.CountryCode = "AP";
                    /*Setting File Name*/
                    legalEvent.Id = leCounter++; // creating uniq id
                    Biblio biblioData = new Biblio();
                    /*Elements output*/
                    biblioData.Publication.Number = record.I11;
                    biblioData.Application.Number = record.I21;
                    //biblioData.Application.Date = DateTime.Parse(record.I23); //К какому полю отнести это значение?
                    /*51 classification*/
                    if (record.I51V != null && record.I51Y != null && record.I51V.Count() == record.I51Y.Count())
                    {
                        biblioData.Ipcs = new List<Ipc>();
                        for (int i = 0; i < record.I51V.Count(); i++)
                        {
                            Ipc ipc = new Ipc();
                            ipc.Class = record.I51V[i];
                            ipc.Date = record.I51Y[i];
                            biblioData.Ipcs.Add(ipc);
                        }
                    }
                    /*-----------------*/
                    /*54 Title*/
                    Title title = new Title()
                    {
                        Language = "EN",
                        Text = record.I54
                    };
                    biblioData.Titles.Add(title);
                    /*--------*/
                    /*71 name, address, country code*/
                    biblioData.Applicants = new List<PartyMember>();
                    PartyMember applicants = new PartyMember()
                    {
                        Name = record.I71
                    };
                    biblioData.Applicants.Add(applicants);
                    /*--------------*/
                    /*72 name, country code*/
                    if (record.I72 != null)
                    {
                        biblioData.Inventors = new List<PartyMember>();
                        for (int i = 0; i < record.I72.Count(); i++)
                        {
                            PartyMember inventor = new PartyMember()
                            {
                                Name = record.I72[i]
                            };
                            biblioData.Inventors.Add(inventor);
                        }
                    }
                    /*---------------------*/
                    /*74 name, address, cc*/
                    biblioData.Agents = new List<PartyMember>();
                    PartyMember agent = new PartyMember()
                    {
                        Name = record.I74
                    };
                    biblioData.Agents.Add(agent);
                    /*--------------------*/
                    /*84*/
                    IntConvention intConvention = new IntConvention();
                    if (record.I84 != null/* || record.I86N !=null*/)
                    {
                        intConvention.DesignatedStates = record.I84.ToList();
                    }
                    /*--------------------*/
                    if (intConvention != null)
                    {
                        biblioData.IntConvention = intConvention;
                    }
                    /*--------------------*/
                    legalEvent.Biblio = biblioData;
                    fullGazetteInfo.Add(legalEvent);
                }
            }
            return fullGazetteInfo;
        }
        /*Patent Application Assigned*/
        public static List<Diamond.Core.Models.LegalStatusEvent> PatentAppAssignedConvertation(List<ProcessPatentAppAssigned.ElementOut> elementOuts)
        {
            /*list of record for whole gazette chapter*/
            List<Diamond.Core.Models.LegalStatusEvent> fullGazetteInfo = new List<Diamond.Core.Models.LegalStatusEvent>();
            if (elementOuts != null)
            {
                int leCounter = 1;
                /*Create a new event to fill*/
                foreach (var record in elementOuts)
                {
                    Diamond.Core.Models.LegalStatusEvent legalEvent = new Diamond.Core.Models.LegalStatusEvent();

                    legalEvent.GazetteName = Path.GetFileName(Diamond_AP_main.CurrentFileName.Replace(".tetml", ".pdf"));
                    /*Setting subcode*/
                    legalEvent.SubCode = "6";
                    /*Setting Section Code*/
                    legalEvent.SectionCode = "AZ";
                    /*Setting Country Code*/
                    legalEvent.CountryCode = "AP";
                    /*Setting File Name*/
                    legalEvent.Id = leCounter++; // creating uniq identifier
                    Biblio biblioData = new Biblio();
                    /*Elements output*/
                    biblioData.Publication.Number = record.I11;
                    biblioData.Application.Number = record.I21;
                    //biblioData.Application.Date = DateTime.Parse(record.I23); //К какому полю отнести это значение?
                    /*51 classification*/
                    if (record.I51V != null && record.I51Y != null && record.I51V.Count() == record.I51Y.Count())
                    {
                        biblioData.Ipcs = new List<Ipc>();
                        for (int i = 0; i < record.I51V.Count(); i++)
                        {
                            Ipc ipc = new Ipc();
                            ipc.Class = record.I51V[i];
                            ipc.Date = record.I51Y[i];
                            biblioData.Ipcs.Add(ipc);
                        }
                    }
                    /*-----------------*/
                    /*54 Title*/
                    Title title = new Title()
                    {
                        Language = "EN",
                        Text = record.I54
                    };
                    biblioData.Titles.Add(title);
                    /*--------*/
                    /*71 name, address, country code*/
                    biblioData.Applicants = new List<PartyMember>();
                    PartyMember applicants = new PartyMember()
                    {
                        Name = record.I71
                    };
                    biblioData.Applicants.Add(applicants);
                    /*--------------*/
                    /*72 name, country code*/
                    if (record.I72 != null)
                    {
                        biblioData.Inventors = new List<PartyMember>();
                        for (int i = 0; i < record.I72.Count(); i++)
                        {
                            PartyMember inventor = new PartyMember()
                            {
                                Name = record.I72[i]
                            };
                            biblioData.Inventors.Add(inventor);
                        }
                    }
                    /*---------------------*/
                    /*74 name, address, cc*/
                    biblioData.Agents = new List<PartyMember>();
                    PartyMember agent = new PartyMember()
                    {
                        Name = record.I74
                    };
                    biblioData.Agents.Add(agent);
                    /*--------------------*/
                    /*84*/
                    IntConvention intConvention = new IntConvention();
                    if (record.I84 != null/* || record.I86N !=null*/)
                    {
                        intConvention.DesignatedStates = record.I84.ToList();
                    }
                    /*--------------------*/
                    if (intConvention != null)
                    {
                        biblioData.IntConvention = intConvention;
                    }
                    /*--------------------*/

                    legalEvent.Biblio = biblioData;
                    fullGazetteInfo.Add(legalEvent);
                }
            }
            return fullGazetteInfo;
        }
        /*Utility Model Application Pending Registration*/
        public static List<Diamond.Core.Models.LegalStatusEvent> UmAppPendingRegConvertation(List<ProcessUtilityModelAppPendingReg.ElementOut> elementOuts)
        {
            /*list of record for whole gazette chapter*/
            List<Diamond.Core.Models.LegalStatusEvent> fullGazetteInfo = new List<Diamond.Core.Models.LegalStatusEvent>();
            if (elementOuts != null)
            {
                int leCounter = 1;
                /*Create a new event to fill*/
                foreach (var record in elementOuts)
                {
                    Diamond.Core.Models.LegalStatusEvent legalEvent = new Diamond.Core.Models.LegalStatusEvent();

                    legalEvent.GazetteName = Path.GetFileName(Diamond_AP_main.CurrentFileName.Replace(".tetml", ".pdf"));
                    /*Setting subcode*/
                    legalEvent.SubCode = "13";
                    /*Setting Section Code*/
                    legalEvent.SectionCode = "AZ";
                    /*Setting Country Code*/
                    legalEvent.CountryCode = "AP";
                    /*Setting File Name*/
                    legalEvent.Id = leCounter++; // creating uniq identifier
                    Biblio biblioData = new Biblio();
                    /*Elements output*/
                    biblioData.Application.Number = record.I21;
                    biblioData.Application.Date = record.I22;
                    //biblioData.Application.Date = DateTime.Parse(record.I23); //К какому полю отнести это значение?
                    /*51 classification*/
                    if (record.I51V != null && record.I51Y != null && record.I51V.Count() == record.I51Y.Count())
                    {
                        biblioData.Ipcs = new List<Ipc>();
                        for (int i = 0; i < record.I51V.Count(); i++)
                        {
                            Ipc ipc = new Ipc();
                            ipc.Class = record.I51V[i];
                            ipc.Date = record.I51Y[i];
                            biblioData.Ipcs.Add(ipc);
                        }
                    }
                    /*-----------------*/
                    /*54 Title*/
                    Title title = new Title()
                    {
                        Language = "EN",
                        Text = record.I54
                    };
                    biblioData.Titles.Add(title);
                    /*--------*/
                    /*74 name, address, cc*/
                    biblioData.Agents = new List<PartyMember>();
                    PartyMember agent = new PartyMember()
                    {
                        Name = record.I74
                    };
                    biblioData.Agents.Add(agent);
                    /*--------------------*/
                    /*84*/
                    IntConvention intConvention = new IntConvention();
                    if (record.I84 != null/* || record.I86N !=null*/)
                    {
                        intConvention.DesignatedStates = record.I84.ToList();
                    }
                    /*--------------------*/
                    if (intConvention != null)
                    {
                        biblioData.IntConvention = intConvention;
                    }
                    /*--------------------*/

                    legalEvent.Biblio = biblioData;
                    fullGazetteInfo.Add(legalEvent);
                }
            }
            return fullGazetteInfo;
        }
    }
}
