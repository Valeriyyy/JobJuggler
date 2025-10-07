namespace JobJuggler.Common;

public static class StringExtensions
{
    public static string ToSnakeCase<TEnum>(this TEnum value) where TEnum : Enum
    {
        return ToSnakeCase(value.ToString());
    }
    
    public static string ToSnakeCase(this string str)
    {
        if (string.IsNullOrEmpty(str))
            return str;

        // Roughly allocate double the size (worst case every char is uppercase and gets a '_')
        var buffer = new char[str.Length * 2];
        var span = buffer.AsSpan();
        var pos = 0;

        for (var i = 0; i < str.Length; i++)
        {
            var c = str[i];
            if (c == ' ')
                continue;

            if (pos > 0 && char.IsUpper(c))
            {
                span[pos++] = '_';
            }

            span[pos++] = char.ToLowerInvariant(c);
        }

        return new string(span.Slice(0, pos));
    }
}