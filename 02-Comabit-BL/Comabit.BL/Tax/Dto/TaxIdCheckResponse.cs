// <copyright file="TaxIdCheck.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.BL.Tax.Dto
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class TaxIdCheckResponse
    {
        private List<TaxIdCheckState> _checkStates;

        private List<TaxIdCheckState> CheckStates
        {
            get
            {
                if (_checkStates == null)
                {
                    _checkStates = new List<TaxIdCheckState>();

                    _checkStates.Add(new TaxIdCheckState("200", "Die angefragte USt-IdNr. ist gültig.", TaxIdCheckStateType.Valid));
                    _checkStates.Add(new TaxIdCheckState("201", "Die angefragte USt-IdNr. ist ungültig.", TaxIdCheckStateType.Invalid));
                    _checkStates.Add(new TaxIdCheckState("202", "Die angefragte USt-IdNr. ist ungültig. Sie ist nicht in der Unternehmerdatei des betreffenden EU-Mitgliedstaates registriert.", TaxIdCheckStateType.Invalid));
                    _checkStates.Add(new TaxIdCheckState("203", "Die angefragte USt-IdNr. ist ungültig. Sie ist erst ab dem ... gültig (siehe Feld 'Gueltig_ab').", TaxIdCheckStateType.Invalid));
                    _checkStates.Add(new TaxIdCheckState("204", "Die angefragte USt-IdNr. ist ungültig. Sie war im Zeitraum von ... bis ... gültig (siehe Feld 'Gueltig_ab' und 'Gueltig_bis'). ", TaxIdCheckStateType.Invalid));
                    _checkStates.Add(new TaxIdCheckState("205", "Ihre Anfrage kann derzeit durch den angefragten EU-Mitgliedstaat oder aus anderen Gründen nicht beantwortet werden.", TaxIdCheckStateType.ServiceUnavailable));
                    _checkStates.Add(new TaxIdCheckState("206", "Ihre deutsche USt-IdNr. ist ungültig. Eine Bestätigungsanfrage ist daher nicht möglich.", TaxIdCheckStateType.CheckImpossible));
                    _checkStates.Add(new TaxIdCheckState("207", "Sie sind nicht berechtigt, Bestätigungsanfragen zu stellen.", TaxIdCheckStateType.CheckImpossible));
                    _checkStates.Add(new TaxIdCheckState("208", "Für die von Ihnen angefragte USt-IdNr. läuft gerade eine Anfrage von einem anderen Nutzer. Eine Bearbeitung ist daher nicht möglich.", TaxIdCheckStateType.ServiceUnavailable));
                    _checkStates.Add(new TaxIdCheckState("209", "Die angefragte USt-IdNr. ist ungültig. Sie entspricht nicht dem Aufbau der für diesen EU-Mitgliedstaat gilt.", TaxIdCheckStateType.Invalid));
                    _checkStates.Add(new TaxIdCheckState("210", "Die angefragte USt-IdNr. ist ungültig. Sie entspricht nicht den Prüfziffernregeln die für diesen EU-Mitgliedstaat gelten.", TaxIdCheckStateType.Invalid));
                    _checkStates.Add(new TaxIdCheckState("211", "Die angefragte USt-IdNr. ist ungültig. Sie enthält unzulässige Zeichen (wie z.B. Leerzeichen oder Punkt oder Bindestrich usw.). ", TaxIdCheckStateType.Invalid));
                    _checkStates.Add(new TaxIdCheckState("212", "Die angefragte USt-IdNr. ist ungültig. Sie enthält ein unzulässiges Länderkennzeichen.", TaxIdCheckStateType.Invalid));
                    _checkStates.Add(new TaxIdCheckState("213", "Sie sind nicht zur Abfrage einer deutschen USt-IdNr. berechtigt.", TaxIdCheckStateType.CheckImpossible));
                    _checkStates.Add(new TaxIdCheckState("214", "Ihre deutsche USt-IdNr. ist fehlerhaft. Sie beginnt mit 'DE' gefolgt von 9 Ziffern.", TaxIdCheckStateType.RequestIdInvalid));
                    _checkStates.Add(new TaxIdCheckState("215", "Ihre Anfrage enthält nicht alle notwendigen Angaben für eine einfache Bestätigungsanfrage.", TaxIdCheckStateType.Invalid));
                    _checkStates.Add(new TaxIdCheckState("216", "Ihre Anfrage enthält nicht alle notwendigen Angaben für eine qualifizierte Bestätigungsanfrage", TaxIdCheckStateType.Invalid));
                    _checkStates.Add(new TaxIdCheckState("217", "Bei der Verarbeitung der Daten aus dem angefragten EU-Mitgliedstaat ist ein Fehler aufgetreten.", TaxIdCheckStateType.ServiceUnavailable));
                    _checkStates.Add(new TaxIdCheckState("218", "Eine qualifizierte Bestätigung ist zur Zeit nicht möglich. Einfache Bestätigungsanfrage: Die angefragte USt - IdNr.ist gültig.", TaxIdCheckStateType.Valid));
                    _checkStates.Add(new TaxIdCheckState("219", "Bei der Durchführung der qualifizierten Bestätigungsanfrage ist ein Fehler aufgetreten. Einfache Bestätigungsanfrage: Die angefragte USt - IdNr.ist gültig.", TaxIdCheckStateType.Valid));
                    _checkStates.Add(new TaxIdCheckState("220", "Bei der Anforderung der amtlichen Bestätigungsmitteilung ist ein Fehler aufgetreten. Sie werden kein Schreiben erhalten.", TaxIdCheckStateType.CheckImpossible));
                    _checkStates.Add(new TaxIdCheckState("221", "Die Anfragedaten enthalten nicht alle notwendigen Parameter oder einen ungültigen Datentyp.", TaxIdCheckStateType.Invalid));
                    _checkStates.Add(new TaxIdCheckState("223", "Die angefragte USt-IdNr. ist gültig. Die Druckfunktion steht nicht mehr zur Verfügung...", TaxIdCheckStateType.Valid));
                    _checkStates.Add(new TaxIdCheckState("999", "Eine Bearbeitung Ihrer Anfrage ist zurzeit nicht möglich. Bitte versuchen Sie es später noch einmal.", TaxIdCheckStateType.ServiceUnavailable));

                }

                return _checkStates;
            }
        }

        private Dictionary<string, string> ResponseData
        {
            get; set;
        }

        private string ErrorCode => GetItemData("ErrorCode");

        private string UstId_1 => GetItemData("UstId_1");

        private string UstId_2 => GetItemData("UstId_2");

        private string Firmenname => GetItemData("Firmenname");

        private string Ort => GetItemData("Ort");

        private string PLZ => GetItemData("PLZ");

        private string Strasse => GetItemData("Strasse");

        private string Erg_Name => GetItemData("Erg_Name");

        private string Erg_Ort => GetItemData("Erg_Ort");

        private string Erg_PLZ => GetItemData("Erg_PLZ");

        private string Erg_Str => GetItemData("Erg_Str");

        private string Gueltig_ab => GetItemData("Gueltig_ab");

        private string Gueltig_bis => GetItemData("Gueltig_bis");

        private TaxIdCheckState _state;

        /// <summary>
        /// gets response state object with message and type (valid/invalid/...)
        /// </summary>
        /// <returns></returns>
        public TaxIdCheckState State
        {
            get
            {
                if(_state == null)
                {
                    _state = CheckStates.Where(c => c.Code == ErrorCode).FirstOrDefault();

                    if (_state == null)
                    {
                        _state = new TaxIdCheckState("0", "Unbekannter Status", TaxIdCheckStateType.Unknown);
                    }
                }

                return _state;
            }
        }

        private List<TaxIdCheckFieldType> _invalidFields;

        /// <summary>
        /// gets list of invalid fields, if any submitted field or combination of fields is invalid
        /// </summary>
        /// <returns></returns>
        public List<TaxIdCheckFieldType> InvalidFields
        {
            get
            {
                if(_invalidFields == null)
                { 
                    _invalidFields = new List<TaxIdCheckFieldType>();

                    if (State.Type == TaxIdCheckStateType.Invalid)
                    {
                        _invalidFields.Add(TaxIdCheckFieldType.TaxId);
                    }

                    if (Erg_Name == "B") // = stimmt nicht überein
                    {
                        _invalidFields.Add(TaxIdCheckFieldType.CompanyName);
                    }

                    if (Erg_Ort == "B")
                    {
                        _invalidFields.Add(TaxIdCheckFieldType.City);
                    }

                    if (Erg_PLZ == "B")
                    {
                        _invalidFields.Add(TaxIdCheckFieldType.PostalCode);
                    }

                    if (Erg_Str == "B")
                    {
                        _invalidFields.Add(TaxIdCheckFieldType.Street);
                    }
                }

                return _invalidFields;
            }
        }

        public TaxIdCheckResponse(Dictionary<string, string> responseData)
        {
            ResponseData = responseData;
        }

        private string GetItemData(string key)
        {
            if (ResponseData.ContainsKey(key))
            {
                return ResponseData[key];
            }

            return "";
        }
    }
}