﻿using Prism.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace PJK.WPF.PRISM.PM2020.Module.Mana.Wrappers
{
    public class NotifyDataErrorInfoBase : BindableBase, INotifyDataErrorInfo
    {
        private Dictionary<string, List<string>> _errorsByPropertyName
         = new Dictionary<string, List<string>>();

        public bool HasErrors => _errorsByPropertyName.Any();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            if (propertyName != null)
            {
                return _errorsByPropertyName.ContainsKey(propertyName)
                  ? _errorsByPropertyName[propertyName]
                  : null;
            }
            else
            {
                return null;
            }
        }

        public IEnumerable GetAllPropertiesWithErrors()
        {
            if (_errorsByPropertyName.Count > 0)
            {
                List<string> myErrorList = new List<string>();
                               
                foreach(KeyValuePair <string, List<string>> PropNameError in _errorsByPropertyName)
                {
                    foreach(string k in PropNameError.Value)
                    {
                        myErrorList.Add(PropNameError.Key + " -> " + k);
                    }

                }

                    return myErrorList;

                    //return _errorsByPropertyName.Keys.ToList();
            }
            else
            {
                return null;
            }
        }

        protected virtual void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            RaisePropertyChanged(nameof(HasErrors));
            if(_errorsByPropertyName.Count > 0)
            {
                ErrorMessages = GetAllPropertiesWithErrors();
            }
            //base.OnPropertyChanged(nameof(HasErrors));
        }

        protected void AddError(string propertyName, string error)
        {
            if (!_errorsByPropertyName.ContainsKey(propertyName))
            {
                _errorsByPropertyName[propertyName] = new List<string>();
            }
            if (!_errorsByPropertyName[propertyName].Contains(error))
            {
                _errorsByPropertyName[propertyName].Add(error);
                OnErrorsChanged(propertyName);
            }
        }

        protected void ClearErrors(string propertyName)
        {
            if (_errorsByPropertyName.ContainsKey(propertyName))
            {
                _errorsByPropertyName.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }
        }


        private IEnumerable _errorMessages;
        public IEnumerable ErrorMessages
        {
            get { return _errorMessages; }
            set { SetProperty(ref _errorMessages, value); }
        }
    }
}
