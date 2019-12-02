using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardCreatorDatabase.Logic
{
    public static class FileValidator
    {
        public static bool ValidateCardFileType(string pathToValdiate)
        {
            string result = Path.GetExtension(pathToValdiate);
            switch (result)
            {
                case ".json":
                    return true;
                case ".JSON":
                    return true;
                default:
                    break;
            }
            return false;

        }

        public static bool ValidateImageFileType(string pathToValdiate)
        {
            string result = Path.GetExtension(pathToValdiate);
            switch (result)
            {
                case ".png":
                    return true;
                case ".PNG":
                    return true;
                case ".jpeg":
                    return true;
                case ".JPG":
                    return true;
                case ".jpg":
                    return true;


                default:
                    break;
            }
            return false;

        }
    }
}
