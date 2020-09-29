using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameWebApi
{
    public class NewItem
    {
        [Range(1, 99, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Level { get; set; }

        [EnumDataType(typeof(ItemType), ErrorMessage = "Invalid ItemType")]
        [Range(0, 2)] public ItemType Type { get; set; }


        [CorrectDateValidation]
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }
    }
}