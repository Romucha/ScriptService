using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptService.Models.Data
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class ScriptContentMaxSizeAttribute : ValidationAttribute
    {
        private double _maxSize;
        public ScriptContentMaxSizeAttribute(double kiloBytes)
        {
            _maxSize = kiloBytes;
        }

        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return false;
            }
            string input = value.ToString();
            var bytes = Encoding.UTF8.GetBytes(input);
            return bytes.Length / 1000.0 <= _maxSize;
        }
    }
}
