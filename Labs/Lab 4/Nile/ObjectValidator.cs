/*
 * Matthew McNatt
 * ITSE 1430
 * 4/17/2019
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nile
{
    /// <summary>A helper class to simplify the process of validating objects</summary>
    static public class ObjectValidator
    {
        /// <summary>Validates an object</summary>
        /// <param name="value">Any object that implements IValidateable object</param>
        static public void Validate( IValidatableObject value )
        {
            Validator.ValidateObject(value, new ValidationContext(value));
        }
    }
}
