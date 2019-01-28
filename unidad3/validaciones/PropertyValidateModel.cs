using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  WpfBiblioteca.Model
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class PropertyValidateModel : IDataErrorInfo
    {
        // Devuelve el nombre de la Prop Asociada con sus errores de las restricciones
        public string this[string columnName]
        {
            get
            {
                var validationResults = new List<ValidationResult>();

                if (Validator.TryValidateProperty(
                        GetType().GetProperty(columnName).GetValue(this)
                        , new ValidationContext(this) { MemberName = columnName }
                        , validationResults))
                    return null;

                return validationResults.First().ErrorMessage;
            }
        }

        // Concatenación de errores:

        public String errores(Object obj)
        {
            String mensError = "";

            ValidationContext validationContext = new ValidationContext(obj, null, null);
            List<ValidationResult> errores = new List<ValidationResult>();
            Validator.TryValidateObject(obj, validationContext, errores, true);

            if (errores.Count() > 0)
            {
                string mensageErrores = string.Empty;
                foreach (var error in errores)
                {
                    error.MemberNames.First();

                    mensError += error.ErrorMessage + " * ";
                    // mensError += error.ErrorMessage + Environment.NewLine;
                }
                return mensError;
            }
            else
            {
                return mensError;
            }
        }

        public string Error { get { return null; } }
    }
}
