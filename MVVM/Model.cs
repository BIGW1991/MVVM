using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM
{
    public abstract class Model : ObservableObject, IDataErrorInfo
    {
        public string this[string columnName]
        {
            get { return OnValidate(columnName); }
        }

        public string Error
        {
            get
            {
                throw new NotSupportedException();
            }
        }
        /// <summary>
        /// Validálja a property-t, amely neve megegyezik a specifikációnak.
        /// </summary>
        /// <param name="propertyName">A property neve a validáláshoz</param>
        /// <returns>Validációs hibával tér vissza, vagy null értékkel, ha nincs hiba</returns>
        protected virtual string OnValidate(string propertyName)
        {
            ValidationContext context = new ValidationContext(this)
            {
                MemberName = propertyName
            };

            Collection<ValidationResult> results = new Collection<ValidationResult>();
            bool isValid = Validator.TryValidateObject(this, context, results, true);

            if (!isValid)
            {
                ValidationResult result = results.SingleOrDefault(p => p.MemberNames.Any(memberName => memberName == propertyName));

                return result == null ? null : result.ErrorMessage;
            }
            return null;
        }
    }
}
