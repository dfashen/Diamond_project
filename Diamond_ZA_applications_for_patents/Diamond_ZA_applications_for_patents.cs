﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Integration;
using System.Web;


namespace Diamond_ZA_applications_for_patents
{
    class Diamond_ZA_applications_for_patents
    {
        private static readonly string I54 = "54:";
        private static readonly string I71 = "71:";
        private static readonly string I72 = "72:";
        private static readonly string I31 = "31:";
        private static readonly string I32 = "32:";
        private static readonly string I33 = "33:";

        class ElementOut
        {
            public string I21 { get; set; }
            public string I22 { get; set; }
            public string I54 { get; set; }
            public string[] I71Name { get; set; }
            public string[] I71Adress { get; set; }
            public string[] I71CountryCode { get; set; }
            public string[] I72 { get; set; }
            public string[] I31 { get; set; }
            public string[] I32 { get; set; }
            public string[] I33 { get; set; }
        }
        static string CcIdentification(string ccStr)
        {
            string tmpStr = "";
            List<string> ccFullNames = new List<string> { "afghanistan", "aland islands", "albania", "algeria", "american samoa", "andorra", "angola", "anguilla", "antarctica", "antigua and barbuda", "argentina", "armenia", "aruba", "australia", "austria", "azerbaijan", "bahamas", "bahrain", "bangladesh", "barbados", "belarus", "belgium", "belize", "benin", "bermuda", "bhutan", "bolivia", "bosnia and herzegovina", "botswana", "bouvet island", "brazil", "british virgin islands", "british indian ocean territory", "brunei darussalam", "bulgaria", "burkina faso", "burundi", "cambodia", "cameroon", "canada", "cape verde", "cayman islands", "central african republic", "chad", "chile", "china", "hong kong, sar china", "macao, sar china", "christmas island", "cocos (keeling) islands", "colombia", "comoros", "congo (brazzaville)", "congo, (kinshasa)", "cook islands", "costa rica", "côte d'ivoire", "croatia", "cuba", "cyprus", "czech republic", "denmark", "djibouti", "dominica", "dominican republic", "ecuador", "egypt", "el salvador", "equatorial guinea", "eritrea", "estonia", "ethiopia", "falkland islands (malvinas)", "faroe islands", "fiji", "finland", "france", "french guiana", "french polynesia", "french southern territories", "gabon", "gambia", "georgia", "germany", "ghana", "gibraltar", "greece", "greenland", "grenada", "guadeloupe", "guam", "guatemala", "guernsey", "guinea", "guinea-bissau", "guyana", "haiti", "heard and mcdonald islands", "holy see (vatican city state)", "honduras", "hungary", "iceland", "india", "indonesia", "iran, islamic republic of", "iraq", "ireland", "isle of man", "israel", "italy", "jamaica", "japan", "jersey", "jordan", "kazakhstan", "kenya", "kiribati", "korea (north)", "korea (south)", "kuwait", "kyrgyzstan", "lao pdr", "latvia", "lebanon", "lesotho", "liberia", "libya", "liechtenstein", "lithuania", "luxembourg", "macedonia, republic of", "madagascar", "malawi", "malaysia", "maldives", "mali", "malta", "marshall islands", "martinique", "mauritania", "mauritius", "mayotte", "mexico", "micronesia, federated states of", "moldova", "monaco", "mongolia", "montenegro", "montserrat", "morocco", "mozambique", "myanmar", "namibia", "nauru", "nepal", "netherlands", "netherlands antilles", "new caledonia", "new zealand", "nicaragua", "niger", "nigeria", "niue", "norfolk island", "northern mariana islands", "norway", "oman", "pakistan", "palau", "palestinian territory", "panama", "papua new guinea", "paraguay", "peru", "philippines", "pitcairn", "poland", "portugal", "puerto rico", "qatar", "réunion", "romania", "russian federation", "rwanda", "saint-barthélemy", "saint helena", "saint kitts and nevis", "saint lucia", "saint-martin (french part)", "saint pierre and miquelon", "saint vincent and grenadines", "samoa", "san marino", "sao tome and principe", "saudi arabia", "senegal", "serbia", "seychelles", "sierra leone", "singapore", "slovakia", "slovenia", "solomon islands", "somalia", "south africa", "south georgia and the south sandwich islands", "south sudan", "spain", "sri lanka", "sudan", "suriname", "svalbard and jan mayen islands", "swaziland", "sweden", "switzerland", "syrian arab republic (syria)", "taiwan, republic of china", "tajikistan", "tanzania, united republic of", "thailand", "timor-leste", "togo", "tokelau", "tonga", "trinidad and tobago", "tunisia", "turkey", "turkmenistan", "turks and caicos islands", "tuvalu", "uganda", "ukraine", "united arab emirates", "united kingdom", "united states of america", "us minor outlying islands", "uruguay", "uzbekistan", "vanuatu", "venezuela (bolivarian republic)", "viet nam", "virgin islands, us", "wallis and futuna islands", "western sahara", "yemen", "zambia", "zimbabwe", "p. r. china", "republic of korea", "korea", "republic of san marino", "u s a", "u.s.a", "united states", "uae", "u.a.e", "congo", "switzerland", "chile", "prc", "p.r.c.", "england", "london", "united kingdom", "hong kong", "india", "cayman", "méxico", "r.o.c.", "u.s.a", "united sates of america", "united state of america,", "vietnam", "russia", "european union", "benelux" };
            List<string> ccShortNames = new List<string> { "AF", "AX", "AL", "DZ", "AS", "AD", "AO", "AI", "AQ", "AG", "AR", "AM", "AW", "AU", "AT", "AZ", "BS", "BH", "BD", "BB", "BY", "BE", "BZ", "BJ", "BM", "BT", "BO", "BA", "BW", "BV", "BR", "VG", "IO", "BN", "BG", "BF", "BI", "KH", "CM", "CA", "CV", "KY", "CF", "TD", "CL", "CN", "HK", "MO", "CX", "CC", "CO", "KM", "CG", "CD", "CK", "CR", "CI", "HR", "CU", "CY", "CZ", "DK", "DJ", "DM", "DO", "EC", "EG", "SV", "GQ", "ER", "EE", "ET", "FK", "FO", "FJ", "FI", "FR", "GF", "PF", "TF", "GA", "GM", "GE", "DE", "GH", "GI", "GR", "GL", "GD", "GP", "GU", "GT", "GG", "GN", "GW", "GY", "HT", "HM", "VA", "HN", "HU", "IS", "IN", "ID", "IR", "IQ", "IE", "IM", "IL", "IT", "JM", "JP", "JE", "JO", "KZ", "KE", "KI", "KP", "KR", "KW", "KG", "LA", "LV", "LB", "LS", "LR", "LY", "LI", "LT", "LU", "MK", "MG", "MW", "MY", "MV", "ML", "MT", "MH", "MQ", "MR", "MU", "YT", "MX", "FM", "MD", "MC", "MN", "ME", "MS", "MA", "MZ", "MM", "NA", "NR", "NP", "NL", "AN", "NC", "NZ", "NI", "NE", "NG", "NU", "NF", "MP", "NO", "OM", "PK", "PW", "PS", "PA", "PG", "PY", "PE", "PH", "PN", "PL", "PT", "PR", "QA", "RE", "RO", "RU", "RW", "BL", "SH", "KN", "LC", "MF", "PM", "VC", "WS", "SM", "ST", "SA", "SN", "RS", "SC", "SL", "SG", "SK", "SI", "SB", "SO", "ZA", "GS", "SS", "ES", "LK", "SD", "SR", "SJ", "SZ", "SE", "CH", "SY", "TW", "TJ", "TZ", "TH", "TL", "TG", "TK", "TO", "TT", "TN", "TR", "TM", "TC", "TV", "UG", "UA", "AE", "GB", "US", "UM", "UY", "UZ", "VU", "VE", "VN", "VI", "WF", "EH", "YE", "ZM", "ZW", "CN", "KR", "KR", "SM", "US", "US", "US", "AE", "AE", "CD", "CH", "CL", "CN", "CN", "GB", "GB", "GB", "HK", "IN", "KY", "MX", "TW", "US", "US", "US", "VN", "RU", "EM", "BX" };
            foreach (var country in ccFullNames)
            {
                if (ccStr.ToLower().Contains(country))
                {
                    tmpStr = ccShortNames.ElementAt(ccFullNames.IndexOf(country));
                    return tmpStr;
                }
                else { tmpStr = ccStr; }
            }
            return tmpStr;
        }
        /*Date*/
        static string DateSwap(string tmpDate)
        {
            string swapDate;
            string[] splitDate = tmpDate.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
            return swapDate = splitDate[2] + "-" + splitDate[1] + "-" + splitDate[0];
        }

        /*71 owner split*/
        static (string[] ownerName, string[] ownerAddress, string[] ownerCountry) OwnerSplit(string tmpStr)
        {
            tmpStr = tmpStr/*.Replace(" and ", ";")*/.Replace(I71, "").Replace("\n", " ").Trim();
            string[] ownerName = null;
            string[] ownerAddress = null;
            string[] ownerCountry = null;
            string[] splitedOwner = null;
            if (tmpStr.Contains(";"))
            {
                splitedOwner = tmpStr.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                if (splitedOwner.Count() > 0)
                {
                    foreach (var record in splitedOwner)
                    {
                        if (record.Contains(","))
                        {
                            string tmpSplName = null;
                            string tmpSplAddr = null;
                            if (Regex.IsMatch(record.ToLower(), @"(\,.*\sllc\,)|(\,.*\sgmbh\,)|(\,.*\sinc(\.|\,)*\,)|(\,.*\sltd(\.|\,)*\,)|(\,.*\ss\.a\.\,)|(\,.*\ss\.l\.\,)|(\,.*\ss\.a\.u\.\,)"))
                            {
                                if (record.Count(f => f == ',') >= 2)
                                {
                                    int firstIndex = record.IndexOf(",");
                                    int secondIndex = record.IndexOf(",", firstIndex + 1);
                                    tmpSplName = record.Remove(secondIndex).Trim();
                                    tmpSplAddr = record.Substring(secondIndex).Trim(',').Trim();
                                }
                            }
                            else
                            {
                                tmpSplName = record.Remove(record.IndexOf(",")).Trim();
                                tmpSplAddr = record.Substring(record.IndexOf(",")).Trim(',').Trim();
                            }
                            ownerName = (ownerName ?? Enumerable.Empty<string>()).Concat(new string[] { tmpSplName }).ToArray();
                            ownerAddress = (ownerAddress ?? Enumerable.Empty<string>()).Concat(new[] { tmpSplAddr }).ToArray();
                            ownerCountry = (ownerCountry ?? Enumerable.Empty<string>()).Concat(new[] { CcIdentification(record) }).ToArray();
                        }
                    }
                }
            }
            else if (tmpStr.Contains(","))
            {
                string tmpSplName = null;
                string tmpSplAddr = null;
                if (Regex.IsMatch(tmpStr.ToLower(), @"(\,.*\sllc\,)|(\,.*\sgmbh\,)|(\,.*\sinc(\.|\,)*\,)|(\,.*\sltd(\.|\,)*\,)|(\,.*\ss\.a\.\,)|(\,.*\ss\.l\.\,)|(\,.*\ss\.a\.u\.\,)"))
                {
                    if (tmpStr.Count(f => f == ',') >= 2)
                    {
                        int firstIndex = tmpStr.IndexOf(",");
                        int secondIndex = tmpStr.IndexOf(",", firstIndex + 1);
                        tmpSplName = tmpStr.Remove(secondIndex).Trim();
                        tmpSplAddr = tmpStr.Substring(secondIndex).Trim(',').Trim();
                    }
                }
                else
                {
                    tmpSplName = tmpStr.Remove(tmpStr.IndexOf(",")).Trim();
                    tmpSplAddr = tmpStr.Substring(tmpStr.IndexOf(",")).Trim(',').Trim();
                }
                ownerName = (ownerName ?? Enumerable.Empty<string>()).Concat(new string[] { tmpSplName }).ToArray();
                ownerAddress = (ownerAddress ?? Enumerable.Empty<string>()).Concat(new[] { tmpSplAddr }).ToArray();
                ownerCountry = (ownerCountry ?? Enumerable.Empty<string>()).Concat(new[] { CcIdentification(tmpStr) }).ToArray();
            }
            return (ownerName, ownerAddress, ownerCountry);
        }
        /*72 split*/
        static string[] StSplit(string tmpStr)
        {
            tmpStr = tmpStr.Replace(I72, "").Replace(",", "").Trim();
            string[] stSplitted = null;
            List<string> stSplittedTrimed = null;
            stSplittedTrimed = new List<string>();
            if (tmpStr.Contains(";"))
            {
                stSplitted = tmpStr.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in stSplitted)
                {
                    stSplittedTrimed.Add(item.Trim());
                }
                stSplitted = stSplittedTrimed.ToArray();
            }
            else
            {
                stSplitted = (stSplitted ?? Enumerable.Empty<string>()).Concat(new string[] { tmpStr }).ToArray();
            }
            return stSplitted;
        }
        /*Splitting record by INIDs numbers*/
        static string[] RecSplit(string recString)
        {
            string[] splittedRecord = null;
            List<string> splittedRecordTrimed = null;
            splittedRecordTrimed = new List<string>();
            string tempStrC = recString.Replace("\n", " ");
            if (recString != "")
            {
                if (recString.Contains("\n"))
                {
                    recString = recString.Replace("\n", " ");
                }
                if (recString.Contains("~"))
                {
                    splittedRecord = tempStrC.Split(new string[] { "~" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in splittedRecord)
                    {
                        splittedRecordTrimed.Add(HttpUtility.HtmlDecode(item.Replace("¿", " ").Trim()));
                    }
                }
            }
            return splittedRecord = splittedRecordTrimed.ToArray();
        }
        static List<ElementOut> ElementsOut = new List<ElementOut>();

        static void Main(string[] args)
        {
            /*Folder with tetml files to process*/
            var dir = new DirectoryInfo(@"D:\_DFA_main\_Patents\ZA\App\");
            /*list of tetml files*/
            var files = new List<string>();
            foreach (FileInfo file in dir.GetFiles("*.tetml", SearchOption.AllDirectories)) { files.Add(file.FullName); }
            foreach (var tetFile in files)
            {
                ElementsOut.Clear();
                string FileName = tetFile;
                XElement tet = XElement.Load(FileName);
                var root = Directory.GetParent(FileName);
                string folderPath = Path.Combine(root.FullName);
                Directory.CreateDirectory(folderPath);
                /*TXT file for output information*/
                string path = Path.Combine(folderPath, FileName.Substring(0, FileName.IndexOf(".")) + ".txt"); //Output Filename
                StreamWriter sf = new StreamWriter(path);
                /*TETML elements*/
                var elements = tet.Descendants().Where(d => d.Name.LocalName == "Text"/* && !d.Value.StartsWith("- APPLIED ON")*/)
                    .SkipWhile(e => !e.Value.Contains("APPLICATIONS FOR PATENTS"))
                    .ToList();
                List<string> sortedElements = null;
                ElementOut currentElement = null;
                sortedElements = new List<string>();
                string I22Date = null;
                for (int i = 0; i < elements.Count; ++i)
                {
                    var element = elements[i];
                    string value = element.Value;

                    if (Regex.IsMatch(value, @"\d{4}\/\d{5}\s*\~") /*&& !value.StartsWith("")*/)
                    {
                        int tmpInc = i;
                        string tmpRecordValue = "";
                        do
                        {
                            tmpRecordValue += elements[tmpInc].Value + " ";
                            ++tmpInc;
                        } while (tmpInc < elements.Count()
                        && !Regex.IsMatch(elements[tmpInc].Value, @"\d{4}\/\d{5}\s*\~")
                        && !elements[tmpInc].Value.StartsWith("- APPLIED ON"));
                        sortedElements.Add(tmpRecordValue.Trim());
                    }
                    if (Regex.IsMatch(value, @"\-\s*APPLIED\s*ON"))
                    {
                        sortedElements.Add(value.Trim());
                    }
                }
                if (sortedElements != null)
                {
                    foreach (var record in sortedElements)
                    {
                        if (Regex.IsMatch(record, @"\-\s*APPLIED\s*ON"))
                        {
                            I22Date = record.Replace("APPLIED ON", "").Replace("-", "").Replace("/", "-").Trim();
                        }
                        string[] recordSplitted = RecSplit(record);
                        if (recordSplitted != null)
                        {
                            foreach (var item in recordSplitted)
                            {
                                /*21*/
                                if (Regex.IsMatch(item, @"^\d{4}\/\d{5}"))
                                {
                                    currentElement = new ElementOut();
                                    ElementsOut.Add(currentElement);
                                    currentElement.I21 = item;
                                }
                                /*22*/
                                if (I22Date != null)
                                {
                                    currentElement.I22 = I22Date;
                                }
                                /*54*/
                                if (item.StartsWith(I54))
                                {
                                    currentElement.I54 = item.Replace(I54, "").Trim();
                                }
                                /*71*/
                                if (item.StartsWith(I71))
                                {
                                    var (ownerName, ownerAddress, ownerCountry) = OwnerSplit(item);
                                    currentElement.I71Name = ownerName;
                                    currentElement.I71Adress = ownerAddress;
                                    currentElement.I71CountryCode = ownerCountry;
                                }
                                /*72*/
                                if (item.StartsWith(I72))
                                {
                                    currentElement.I72 = StSplit(item.Trim());
                                }
                                /*31*/
                                if (item.StartsWith(I31))
                                {
                                    currentElement.I31 = (currentElement.I31 ?? Enumerable.Empty<string>()).Concat(new string[] { item.Replace(I31, "").Trim() }).ToArray();
                                }
                                /*32*/
                                if (item.StartsWith(I32))
                                {
                                    string tmpItem = item.Replace(I32, "").Trim();
                                    if (item.Contains(I33))
                                    {
                                        currentElement.I32 = (currentElement.I32 ?? Enumerable.Empty<string>()).Concat(new string[] { DateSwap(tmpItem.Remove(tmpItem.IndexOf(I33)).Replace(";", "").Trim()) }).ToArray();
                                        currentElement.I33 = (currentElement.I33 ?? Enumerable.Empty<string>()).Concat(new string[] { tmpItem.Substring(tmpItem.IndexOf(I33)).Replace(I33, "").Trim() }).ToArray();
                                    }
                                    else
                                    {
                                        currentElement.I32 = (currentElement.I32 ?? Enumerable.Empty<string>()).Concat(new string[] { DateSwap(item.Replace(I32, "").Trim()) }).ToArray();
                                    }
                                }
                                /*33*/
                                if (item.StartsWith(I33))
                                {
                                    currentElement.I33 = (currentElement.I33 ?? Enumerable.Empty<string>()).Concat(new string[] { item.Replace(I33, "").Trim() }).ToArray();
                                }
                            }
                        }
                    }
                }
                /*Output*/
                if (ElementsOut != null)
                {
                    int leCounter = 1;
                    /*list of record for whole gazette chapter*/
                    List<Diamond.Core.Models.LegalStatusEvent> fullGazetteInfo = new List<Diamond.Core.Models.LegalStatusEvent>();
                    /*Create a new event to fill*/
                    foreach (var record in ElementsOut)
                    {
                        Diamond.Core.Models.LegalStatusEvent legalEvent = new Diamond.Core.Models.LegalStatusEvent();

                        legalEvent.GazetteName = Path.GetFileName(tetFile.Replace(".tetml", ".pdf"));
                        /*Setting subcode*/
                        legalEvent.SubCode = "1";
                        /*Setting Section Code*/
                        legalEvent.SectionCode = "BA";
                        /*Setting Country Code*/
                        legalEvent.CountryCode = "ZA";
                        /*Setting File Name*/
                        legalEvent.Id = leCounter++; // creating uniq identifier
                        Biblio biblioData = new Biblio();
                        /*Elements output*/
                        biblioData.Application.Number = record.I21;
                        biblioData.Application.Date = record.I22;
                        /*30*/
                        if (record.I31 != null && record.I32 != null && record.I33 != null)
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
                        /*54 Title*/
                        Title title = new Title()
                        {
                            Language = "EN",
                            Text = record.I54
                        };
                        biblioData.Titles.Add(title);
                        /*--------------*/
                        /*71 name, address, country code*/
                        if (record.I71Adress != null && record.I71Name != null)
                        {
                            biblioData.Applicants = new List<PartyMember>();
                            for (int i = 0; i < record.I71Name.Count(); i++)
                            {
                                PartyMember applicants = new PartyMember()
                                {
                                    Name = record.I71Name[i],
                                    Address1 = record.I71Adress[i],
                                    Country = record.I71CountryCode[i]
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
                                    Name = record.I72[i],
                                };
                                biblioData.Inventors.Add(inventor);
                            }
                        }
                        /*--------------------*/

                        legalEvent.Biblio = biblioData;
                        fullGazetteInfo.Add(legalEvent);
                    }

                    //foreach (var rec in fullGazetteInfo)
                    //{
                    //    string tmpValue = JsonConvert.SerializeObject(rec);
                    //    string url = @"https://staging.diamond.lighthouseip.online/external-api/import/legal-event";
                    //    HttpClient httpClient = new HttpClient();
                    //    httpClient.BaseAddress = new Uri(url);
                    //    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //    var content = new StringContent(tmpValue.ToString(), Encoding.UTF8, "application/json");
                    //    var result = httpClient.PostAsync("", content).Result;
                    //    var answer = result.Content.ReadAsStringAsync().Result;
                    //}
                }
                //if (ElementsOut != null)
                //{
                //    foreach (var elemOut in ElementsOut)
                //    {
                //        if (elemOut.I21 != null)
                //        {
                //            sf.WriteLine("****");

                //            sf.WriteLine("21:\t" + elemOut.I21);
                //            sf.WriteLine("22:\t" + elemOut.I22);
                //            sf.WriteLine("54:\t" + elemOut.I54);
                //            /*71*/
                //            if (elemOut.I71Name != null && elemOut.I71Name.Count() == elemOut.I71Adress.Count() && elemOut.I71Name.Count() == elemOut.I71CountryCode.Count())
                //            {
                //                for (int i = 0; i < elemOut.I71Name.Count(); i++)
                //                {
                //                    sf.WriteLine("71N:\t" + elemOut.I71Name[i]);
                //                    sf.WriteLine("71A:\t" + elemOut.I71Adress[i]);
                //                    sf.WriteLine("71C:\t" + elemOut.I71CountryCode[i]);
                //                }
                //            }
                //            /*72*/
                //            if (elemOut.I72 != null)
                //            {
                //                foreach (var item in elemOut.I72)
                //                {
                //                    sf.WriteLine("72:\t" + item);
                //                }
                //            }
                //            /*31,32,33 Priority*/
                //            if (elemOut.I31 != null && elemOut.I31.Count() == elemOut.I32.Count() && elemOut.I31.Count() == elemOut.I33.Count())
                //            {
                //                for (int i = 0; i < elemOut.I31.Count(); i++)
                //                {
                //                    sf.WriteLine("31:\t" + elemOut.I31[i]);
                //                    sf.WriteLine("32:\t" + elemOut.I32[i]);
                //                    sf.WriteLine("33:\t" + elemOut.I33[i]);
                //                }
                //            }
                //        }
                //    }
                //}
                //sf.Flush();
                //sf.Close();
            }
        }
    }
}
