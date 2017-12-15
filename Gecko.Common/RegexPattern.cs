using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class RegexPattern
{
    public const string EMail = @"^(\w)+(\.\w+)*@(\w)+((\.\w+)+)$";
    public const string Mobile = @"^1[34578]\d{9}$";
    public const string Tel = @"@\d{3}-\d{8}|\d{4}-\d{7}$";
    public const string UserName = @"^[a-zA-Z][\.\w]{3,17}$";
    public const string QQ = @"^[1-9][0-9]{4,9}$";
    public const string ZipCode = @"^[1-9]\d{5}$";
    public const string IdNo = @"^\d{15}|\d{18}$";
    public const string IP = @"^((?:(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d)))\.){3}(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d))))$";


    public const string Hanzi = @"[\u4e00-\u9fa5]+";
    public const string Tag = @"<(\S*?)[^>]*>";
    public const string FullTag = @"<(\S*?)[^>]*>.*?</\1>|<.*?/>";
    public const string Url = @"[a-zA-z]+://[^\s]*";

    public const string Float = @"^-?([1-9]\d*\.\d*|0\.\d*[1-9]\d*|0?\.0+|0)$";

    public const string Path = @"^[a-zA-Z][:][\\]([^?/*|<>:""\\]+[\\])*[^?/*|<>:""\\]*[^.]([.][^?/*|<>:""\\]+[^.])?$";
    public const string RelativePath = @"^[\\]([^?/*|<>:""\\]+[\\])*[^?/*|<>:""\\]*[^.]([.][^?/*|<>:""\\]+[^.])?$";

 
}
